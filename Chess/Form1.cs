using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace GridExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Initialize Grid Variables
        const int GridPixSize = 656;
        const int SquareNum = 8;
        const int SquareSize = GridPixSize / SquareNum;

        // Initalize Image Variables
        Bitmap GridImg = new Bitmap(GridPixSize, GridPixSize);
        Bitmap PieceImg = new Bitmap(GridPixSize, GridPixSize);

        //Create SideBar Icons
        Bitmap[] Icons = new Bitmap[7] {
                Properties.Resources.NewGame,
                Properties.Resources.ImportGame,
                Properties.Resources.Archive,
                Properties.Resources.SetGame,
                Properties.Resources.Collapse,
                Properties.Resources.Light,
                Properties.Resources.Settings };

        //Create Black Pieces
        Bitmap[] BlackSprite = new Bitmap[6] {
                Properties.Resources.bRook,
                Properties.Resources.bKnight,
                Properties.Resources.bBishop,
                Properties.Resources.bQueen,
                Properties.Resources.bKing,
                Properties.Resources.bPawn };

        //Create White Pieces
        Bitmap[] WhiteSprite = new Bitmap[6] {
                Properties.Resources.wRook,
                Properties.Resources.wKnight,
                Properties.Resources.wBishop,
                Properties.Resources.wQueen,
                Properties.Resources.wKing,
                Properties.Resources.wPawn };

        Color Light = Color.Tan;
        Color Dark = Color.Black;

        // Initialize Mouse Variables
        Point mouse;
        int c;
        int r;
        new bool MouseDown = false;

        bool Threats = false;
        string NextToPlay = "w";
        Bitmap DragPiece = new Bitmap(GridPixSize / 8, GridPixSize / 8);

        // Initialize piece string data
        const string startPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        const string Types = "rnbqkpe";

        // Initialize Board Array Variables (type, colour, location)
        enum PlayerType { r, n, b, q, k, p, e }
        
        enum ColourType { Black, White, Neutral }

        enum LegalMove { Illegal, Legal, Capture, Self}
        bool Danger = false;

        enum Castling { Legal, Illegal}

        struct PieceType
        {
            public PlayerType Player;
            public ColourType Colour;
            public int Row;
            public int Col;
            public LegalMove Legal;
            public Castling Castling;
        }

        // Initialize default board variavles and presets
        PieceType[,] board;
        PieceType empty = new PieceType { Player = PlayerType.e, Colour = ColourType.Neutral };
        PieceType mousePiece;

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new PieceType[SquareNum, SquareNum];
            mousePiece = empty;

            GameTick.Interval = 10;
            GameTick.Start();

            DrawGrid();
            ReadFEN(startPosition);
        }
        private void GameTick_Tick(object sender, EventArgs e)
        {
            DrawGrid();
        }

        private void pbxGrid_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = e.Location;
            if (mouse.X >= 0) { c = mouse.X / SquareSize; }
            else { c = -1; }

            if (mouse.Y >= 0) { r = mouse.Y / SquareSize; }
            else { r = -1; }
        }

        private void DrawGrid()                                     // Draws board, piece, and drag overlays
        {
            Graphics g = Graphics.FromImage(GridImg);
            g.Clear(pbxGrid.BackColor);

            // Draw Checker Board
            SolidBrush b = new SolidBrush(Color.Black);

            for (int r = 0; r < 8; r++)
                for (int c = 0; c < 8; c++)
                {
                    if (r % 2 != c % 2)
                    {
                        b.Color = Dark;
                    }
                    else
                    {
                        b.Color = Light;
                    }
                    g.FillRectangle(b, c * SquareSize, r * SquareSize, SquareSize, SquareSize);
                }

            //Highlights Squares covered by mouse
            SolidBrush highlight = new SolidBrush(Color.FromArgb(100, 255, 160, 0));

            int X = (mouse.X / SquareSize) * SquareSize;
            int Y = (mouse.Y / SquareSize) * SquareSize;
            if (mouse.X >= 0 && mouse.Y >= 0) { g.FillRectangle(highlight, X, Y, SquareSize, SquareSize); }

            
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (board[r, c].Legal == LegalMove.Self || board[r, c].Legal == LegalMove.Capture || board[r, c].Legal == LegalMove.Legal)
                    {
                        if (board[r, c].Legal == LegalMove.Legal)
                        {
                            highlight.Color = Color.FromArgb(160, 200, 200, 255);
                        }
                        else if (board[r, c].Legal == LegalMove.Capture)
                        {
                            highlight.Color = Color.FromArgb(160, 255, 0, 0);
                        }
                        else if (board[r, c].Legal == LegalMove.Self)
                        {
                            highlight.Color = Color.FromArgb(160, 0, 0, 255);
                        }

                        else { highlight.Color = Color.Transparent; }
                        g.FillRectangle(highlight, c * SquareSize, r * SquareSize, SquareSize, SquareSize);
                    }
                }
            }
            
            //Draw Piece Overlay
            g.DrawImage(PieceImg, 0, 0);

            // Draw drag piece on mouse if mouse is down
            if (MouseDown)
            {
                g.DrawImage(DragPiece, mouse.X - SquareSize * 3 / 10, mouse.Y - SquareSize / 2, SquareSize * 3 / 5, SquareSize);
                if (mousePiece.Player == PlayerType.r) { StraightLegal(false); }
                else if (mousePiece.Player == PlayerType.b) { DiagonalLegal(false); }
                else if (mousePiece.Player == PlayerType.q) { DiagonalLegal(false); StraightLegal(false); }
                else if (mousePiece.Player == PlayerType.k) 
                {
                    DiagonalLegal(true); StraightLegal(true); CheckGrid();

                }
                else if (mousePiece.Player == PlayerType.n) { KnightJump(); }
                else if (mousePiece.Player == PlayerType.p) { Pawn(); }
                
            }
            pbxGrid.Image = GridImg;

            g.Dispose();
        }



        // FEN STRING CODE //
        private string[] FENtoPosition(string FEN)                  // Used to split only the position portion of the FEN
        {
            string[] SplitFEN;
            SplitFEN = FEN.Split('/', ' ');

            string[] boardlayout = new string[8];
            for (int i = 0; i < 8; i++)
            {
                boardlayout[i] = SplitFEN[i];
            }
            return boardlayout;
        }
        private string[] FENtoRules(string FEN)                     // Used to split only the rules portion of the FEN
        {
            string[] SplitFEN;
            SplitFEN = FEN.Split('/',' ');
            string[] boardrules = new string[5];
            for (int i = 8; i < SplitFEN.Length; i++)
            {
                boardrules[i - 8] = SplitFEN[i];
            }
            return boardrules;

        }
        private void ReadFEN(string FEN)                            // Convert FEN string to enumerated board positions
        {
            Graphics g = Graphics.FromImage(PieceImg);
            g.Clear(Color.Transparent);

            for (int row = 0; row < 8; row++)
            {
                string nextrow = FENtoPosition(FEN)[row];                               // Converts FEN string to array of positions
                int col = 0;
                for (int index = 0; index < nextrow.Length; index++)
                {
                    if (nextrow[index] >= '0' && nextrow[index] <= '9')                     // Check if character if a number. If it is, assign blank square
                    {
                        int i;
                        for (i = 0; i < int.Parse(nextrow[index].ToString()); i++)
                        {
                            board[row, col + i] = empty;
                        }
                        col += i;
                    }
                    else
                    {
                        if (Char.IsUpper(nextrow[index]))                                 // Check if character is black or white (depends on capitalization)
                        {
                            board[row, col].Colour = ColourType.White;
                        }
                        else if (Char.IsUpper(nextrow[index]) == false)
                        {
                            board[row, col].Colour = ColourType.Black;
                        }

                        char piece = Char.ToLower(nextrow[index]);                        

                        for (int n = 0; n < Types.Length; n++)                          // Assign the position a playertype equal to the enumerated value
                        {
                            if (piece == Types[n])
                            {
                                board[row, col].Player = (PlayerType)n;
                                break;
                            }
                        }

                        Bitmap sprite = FindSprite(row, col);                           // Asks for sprite resource
                        g.DrawImage(sprite, (col * SquareSize) + (SquareSize * 2 / 10), row * SquareSize, SquareSize * 6 / 10, SquareSize);

                        col++;
                    }
                }
            }
        }

        private int ColourToPlay(string FEN)
        {
            if (FEN == "w")
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        private string BoardtoFEN()                                         // Read board arrays to create FEN position
        {
            string FEN = "";
            for (int row = 0; row < SquareNum;  row++)
            {
                int count = 0;
                for (int col = 0; col < SquareNum; col++) 
                {
                    int index = (int)board[row, col].Player;
                    char type = Types[index];
                    if (board[row, col].Colour == ColourType.White)
                    {
                        type = Char.ToUpper(type);
                    }
                    
                    if (board[row, col].Player == PlayerType.e)
                    {
                        count++;
                    }
                    else
                    {
                        if (count > 0)
                        {
                            FEN += count.ToString();
                            count = 0;
                        }
                        FEN += type.ToString();
                    }
                }
                if (count > 0)
                {
                    FEN += count.ToString();
                    count = 0;
                }
                if (row == SquareNum - 1) { }
                else { FEN += "/"; }
            }
            FEN += RulestoFEN();
        return FEN;
        }
        private string RulestoFEN()
        {
            string FEN = " ";
            FEN += NextToPlay;
            return FEN;
        }

        private Bitmap FindSprite(int row, int col)                                     // Determines Sprite IMG from board location
        {
            Graphics g = Graphics.FromImage(PieceImg);
            Bitmap sprite = new Bitmap(GridPixSize / 8, GridPixSize / 8);

            if (board[row, col].Colour == ColourType.White)                             // Determines the desired sprite using the colour and n value
            {
                sprite = WhiteSprite[(int)board[row, col].Player];
            }
            else if (board[row, col].Colour == ColourType.Black)
            {
                sprite = BlackSprite[(int)board[row, col].Player];
            }
            else { }
            return sprite;
        }



        // DRAG CODE //
        private void StartDrag(object sender, MouseEventArgs e)
        {
            MouseDown = true;

            if (board[r, c].Colour == (ColourType)ColourToPlay(FENtoRules(BoardtoFEN())[0]))
            {
                if (Danger)
                {
                    if (board[r, c].Player == PlayerType.k)
                    {
                        LoadDrag();
                    }
                }
                else
                {
                    LoadDrag();
                }
            }
            else
            {
                MouseDown = false;
            }

            void LoadDrag()
            {
                DragPiece = FindSprite(r, c);

                board[r, c].Row = r;
                board[r, c].Col = c;

                mousePiece = board[r, c];
                mousePiece.Colour = board[r, c].Colour;
                board[r, c] = empty;
                ReadFEN(BoardtoFEN());
            }
        }
        private void EndDrag(object sender, MouseEventArgs e)
        {
            MouseDown = false;
            
            if (mousePiece.Player == PlayerType.e) { }
            else
            {
                if (0 > r || r > 7 || 0 > c || c > 7 || board[r, c].Legal == LegalMove.Illegal || board[r, c].Legal == LegalMove.Self)
                {
                    board[mousePiece.Row, mousePiece.Col] = mousePiece;
                }
                else 
                { 
                    board[r, c] = mousePiece;
                    if (FENtoRules(BoardtoFEN())[0] == "w")
                    {
                        NextToPlay = "b";
                    }
                    else
                    {
                        NextToPlay = "w";
                    }
                }
                mousePiece = empty;

                ReadFEN(BoardtoFEN());
            }
            ClearLegal();
        }


        // LEGAL MOVES //
        private void ClearLegal()
        {
            for (int c = 0; c < 8; c++)
            {
                for (int r = 0; r < 8; r++)
                {
                    board[r, c].Legal = LegalMove.Illegal;
                }
            }
        }   
        private void StraightLegal(bool king)
        {
            int selfC = mousePiece.Col;
            int selfR = mousePiece.Row;

            board[selfR, selfC].Legal = LegalMove.Self;

            StraightCalc(selfR, selfC, true, king);
        }

        private void DiagonalLegal(bool king)
        {
            int selfC = mousePiece.Col;
            int selfR = mousePiece.Row;

            board[selfR, selfC].Legal = LegalMove.Self;

            DiagonalCalc(selfR, selfC, true, king);
        }

        private void KnightJump()
        {
            int selfC = mousePiece.Col;
            int selfR = mousePiece.Row;

            board[selfR, selfC].Legal = LegalMove.Self;

            KnightCalc(selfR, selfC, true);
        }

        private void Pawn()
        {
            int selfC = mousePiece.Col;
            int selfR = mousePiece.Row;

            int Colour = 1;
            board[selfR, selfC].Legal = LegalMove.Self;

            if (mousePiece.Colour == ColourType.Black) { Colour = 1; }
            else if (mousePiece.Colour == ColourType.White) { Colour = -1; }

            // Allow Forward Movement if Empty Square
            if (selfR + Colour < 8 && selfR + Colour >= 0)
            {
                if (board[selfR + Colour, selfC].Colour == ColourType.Neutral)
                {
                    LegalCalculations(selfR + Colour, selfC);
                }

            }

            // Allow Pawn moves 2 if on start squares
            if (selfR + Colour * 2 < 8 && selfR + Colour * 2 >= 0)
            {
                if (selfR == 1 || selfR == 6)
                {
                    if (board[selfR + 2 * Colour, selfC].Colour == ColourType.Neutral)
                    {
                        LegalCalculations(selfR + Colour * 2, selfC);
                    }
                }
            }

            // Allow Diagonal Attack if Enemy Piece Present
            for (int d = -1; d < 2; d += 2)
            {
                if (selfR + Colour < 8 && selfR + Colour >=0 && 
                    selfC + d < 8 && selfC + d >= 0)
                {
                    if (board[selfR + Colour, selfC + d].Colour != mousePiece.Colour 
                        && board[selfR + Colour, selfC + d].Colour != ColourType.Neutral)
                    {
                        LegalCalculations(selfR + Colour, selfC + d);
                    }
                }
            }
        }

        //CHECKS
        private void CheckGrid()
        {
            int selfC = mousePiece.Col;
            int selfR = mousePiece.Row;

            for (int r = -1; r <= 1; r++)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (selfC + c > 7 || selfC + c < 0 || selfR + r > 7 || selfR + r < 0)
                    {
                        continue;
                    }
                    StraightCalc(selfR + r, selfC + c, false, false);
                    DiagonalCalc(selfR + r, selfC + c, false, false);
                    KnightCalc(selfR + r, selfC + c, false);
                    if (Threats)
                    {
                        if (r == 0 && c == 0)
                        {
                            Danger = true;
                        }
                        else { board[selfR + r, selfC + c].Legal = LegalMove.Illegal; }
                        Threats = false;
                    }
                }
            }
        }
        private bool CheckCalculations(int r, int c, string pieces)
        {
            int search = 0;
            for (int i = 0; i < pieces.Length; i++)
            {
                for (int n = 0; n < Types.Length; n++)
                {
                    if (pieces[i] == Types[n])
                    { 
                        search = n;
                        break;
                    }
                }
                //Piece in bounds
                if (r < 0 || r > 7 || c < 0 || r > 7)
                { return false; }
                //Is square empty
                else if (board[r, c].Player == PlayerType.e)
                {
                    return false;
                }
                //Square Occupied
                else
                {
                    //Is square occupied by opposite colour and is the searched piece
                    if (board[r, c].Colour != mousePiece.Colour && board[r, c].Colour != ColourType.Neutral && board[r, c].Player == (PlayerType)search)
                    {
                        Threats = true;
                        return true;
                    }
                    //Is square occupied by friendly piece blocking check
                    else if (board[r, c].Colour == mousePiece.Colour && board[r, c].Player != PlayerType.k)
                    { return true; }
                    //Is square occupied by enemy piece blocking check
                    else if (board[r, c].Colour != mousePiece.Colour && board[r, c].Player != (PlayerType)search && board[r, c].Colour != ColourType.Neutral && board[r,c].Player != PlayerType.q)
                    { return true; }
                    
                }
            }
            return false;
        }

        //CALCULATIONS

        private void StraightCalc(int selfR, int selfC, bool Legal, bool king)
        {
            bool exit;

            // Up Down
            for (int n = -1; n <= 1; n += 2)
            {
                int start = 0;
                if (n > 0) { start = 8; }
                else if (n < 0) { start = -1; }
                for (int r = selfR + n; r != start; r += n)
                {
                    if (Legal)
                    {
                        exit = LegalCalculations(r, selfC);
                        if (exit || king) { break; }
                    }
                    else if (Legal != true)
                    {
                        exit = CheckCalculations(r, selfC, "rq");
                        if (Threats) { return; }
                        if (exit) { break; }
                    }
                }
            }
            // Right Left
            for (int n = -1; n <= 1; n += 2)
            {
                int start = 0;
                if (n > 0) { start = 8; }
                else if (n < 0) { start = -1; }
                for (int c = selfC + n; c != start; c += n)
                {
                    if (Legal)
                    {
                        exit = LegalCalculations(selfR, c);
                        if (exit || king) { break; }
                    }
                    else if (Legal != true)
                    {
                        exit = CheckCalculations(selfR, c, "rq");
                        if (Threats) { return; }
                        if (exit) { break; }
                    }
                }
            }
        }
        private void DiagonalCalc(int selfR, int selfC, bool Legal, bool king)
        {
            bool exit;

            int[,] limit = { { 7 - selfC, selfC, 7 - selfC, selfC }, { 7 - selfR, selfR, selfR, 7 - selfR } };
            int[,] direction = { { 1, -1, -1, 1 }, { 1, -1, 1, -1 } };

            for (int n = 0; n < 4; n++)
            {
                for (int i = 1; i <= limit[0, n] && i <= limit[1, n]; i++)
                {   
                    if (Legal)
                    {
                        exit = LegalCalculations(i * direction[0, n] + selfR, i * direction[1, n] + selfC);
                        if (exit || king) { break; }
                    }
                    else if (Legal != true)
                    {
                        exit = CheckCalculations(i * direction[0, n] + selfR, i * direction[1, n] + selfC, "bq");
                        if (Threats) { return; }
                        if (exit) { break; }
                    }
                }

            }
        }

        private void KnightCalc(int selfR, int selfC, bool Legal)
        {
            bool exit;

            if (selfR + 1 < 8 && selfC + 2 < 8) { CheckLegal(selfR + 1, selfC + 2); }
            if (selfR + 2 < 8 && selfC + 1 < 8) { CheckLegal(selfR + 2, selfC + 1); }
            if (selfR + 2 < 8 && selfC - 1 >= 0) { CheckLegal(selfR + 2, selfC - 1); }
            if (selfR + 1 < 8 && selfC - 2 >= 0) { CheckLegal(selfR + 1, selfC - 2); }
            if (selfR - 1 >= 0 && selfC - 2 >= 0) { CheckLegal(selfR - 1, selfC - 2); }
            if (selfR - 2 >= 0 && selfC - 1 >= 0) { CheckLegal(selfR - 2, selfC - 1); }
            if (selfR - 2 >= 0 && selfC + 1 < 8) { CheckLegal(selfR - 2, selfC + 1); }
            if (selfR - 1 >= 0 && selfC + 2 < 8) { CheckLegal(selfR - 1, selfC + 2); }

            void CheckLegal(int r, int c)
            {
                if (Legal)
                {
                    LegalCalculations(r, c);
                }
                else if (Legal != true)
                {
                    CheckCalculations(r, c, "n");
                    if (Threats) { return; }
                }
            }
        }

        private bool LegalCalculations(int r, int c)
        {
            if (r < 0 || r > 7 || c < 0 || r > 7)
            { return false; }
            else if (board[r, c].Player == PlayerType.e)
            {
                board[r, c].Legal = LegalMove.Legal;
                return false;
            }
            else
            {
                if (board[r, c].Colour != mousePiece.Colour && board[r, c].Colour != ColourType.Neutral)
                {
                    board[r, c].Legal = LegalMove.Capture;
                }
                else if (board[r, c].Colour == mousePiece.Colour)
                {
                    board[r, c].Legal = LegalMove.Illegal;
                }
                return true;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will be used to import game PGNs or FENs");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will be used to view an archive of previous games");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will open an interactive FEN position editor");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will change the theme from light to dark");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will open theme settings");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will open a turtorial page for the program");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will collapse the sidebar and the info box");
        }
    }
}
