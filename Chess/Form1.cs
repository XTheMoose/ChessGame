using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
        Bitmap DragPiece = new Bitmap(GridPixSize / 8, GridPixSize / 8);

        // Initialize piece string data
        const string startPosition = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
        const string Types = "rnbqkpe";

        // Initialize Board Array Variables (type, colour, location)
        enum PlayerType { r, n, b, q, k, p, e }
        
        enum ColourType { Black, White, Neutral }

        struct PieceType
        {
            public PlayerType Player;
            public ColourType Colour;
            public int Row;
            public int Col;
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

            DrawSideBar();

            MessageBox.Show("test");

            DrawGrid();
            ReadFEN(startPosition);
        }
        private void GameTick_Tick(object sender, EventArgs e)
        {
            DrawGrid();
            
            MouseX.Text = c.ToString();
            MouseY.Text = r.ToString();
        }
        private void ResetGrid()
        {

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
            SolidBrush highlight = new SolidBrush(Color.FromArgb(160, 255, 0, 0));

            int X = (mouse.X / SquareSize) * SquareSize;
            int Y = (mouse.Y / SquareSize) * SquareSize;
            if (mouse.X >= 0 && mouse.Y >= 0) { g.FillRectangle(highlight, X, Y, SquareSize, SquareSize); }

            //Draw Piece Overlay
            g.DrawImage(PieceImg, 0, 0);

            // Draw drag piece on mouse if mouse is down
            if (MouseDown)
            {
                g.DrawImage(DragPiece, mouse.X - SquareSize * 3 / 10, mouse.Y - SquareSize / 2, SquareSize * 3 / 5, SquareSize);
            }
            pbxGrid.Image = GridImg;

            g.Dispose();
        }
        
        
        private void DrawSideBar()
        {

            /*
            Bitmap SideImg = new Bitmap(pbxSide.Width, pbxSide.Height);
            Graphics g = Graphics.FromImage(SideImg);

            int IconSize = 0;
            int IconMaxHeight = (pbxSide.Height / Icons.Length) * 6 / 10;
            int IconMaxWidth = pbxSide.Width * 6 / 10;

            if (IconMaxHeight >= IconMaxWidth)
            {
                IconSize = IconMaxWidth;
            }
            else if (IconMaxWidth >= IconMaxHeight)
            {
                IconSize = IconMaxHeight;
            }

            int LocationX = (pbxSide.Width - IconSize) / 2;
            int LocationY = LocationX * 2;

            for (int i = 0; i < Icons.Length; i++)
            {
                g.DrawImage(Icons[i], LocationX, LocationY, IconSize, IconSize);
                LocationY += IconSize + 2 * LocationX;
            }

            pbxSide.Image = SideImg;
            */
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
            SplitFEN = FEN.Split('/', ' ');
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
            else
            {
            }
            return sprite;
        }



        // DRAG CODE //
        private void StartDrag(object sender, MouseEventArgs e)
        {
            MouseDown = true;

            if (board[r, c].Player == PlayerType.e) { MouseDown = false; }
            else
            {
                DragPiece = FindSprite(r, c);

                board[r, c].Row = r;
                board[r, c].Col = c;

                mousePiece = board[r, c];
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
                if (0 > r || r > 7 || 0 > c || c > 7)
                {
                    board[mousePiece.Row, mousePiece.Col] = mousePiece;
                }
                else { board[r, c] = mousePiece; }
                mousePiece = empty;

                ReadFEN(BoardtoFEN());
            }
        }



        private void pbxGrid_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void pbxGrid_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void pbxSide_Click(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This will Initialize a New Game");
        }
    }
}
