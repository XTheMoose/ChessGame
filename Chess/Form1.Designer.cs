namespace GridExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbxGrid = new System.Windows.Forms.PictureBox();
            this.pbxSide = new System.Windows.Forms.PictureBox();
            this.GameTick = new System.Windows.Forms.Timer(this.components);
            this.MouseX = new System.Windows.Forms.Label();
            this.MouseY = new System.Windows.Forms.Label();
            this.pbxInfo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxGrid
            // 
            this.pbxGrid.BackColor = System.Drawing.Color.Transparent;
            this.pbxGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxGrid.Location = new System.Drawing.Point(93, 12);
            this.pbxGrid.Name = "pbxGrid";
            this.pbxGrid.Size = new System.Drawing.Size(656, 656);
            this.pbxGrid.TabIndex = 0;
            this.pbxGrid.TabStop = false;
            this.pbxGrid.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxGrid_MouseClick);
            this.pbxGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartDrag);
            this.pbxGrid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxGrid_MouseMove);
            this.pbxGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EndDrag);
            // 
            // pbxSide
            // 
            this.pbxSide.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.pbxSide.Location = new System.Drawing.Point(0, 0);
            this.pbxSide.Name = "pbxSide";
            this.pbxSide.Size = new System.Drawing.Size(76, 720);
            this.pbxSide.TabIndex = 1;
            this.pbxSide.TabStop = false;
            this.pbxSide.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxSide_Click);
            // 
            // GameTick
            // 
            this.GameTick.Tick += new System.EventHandler(this.GameTick_Tick);
            // 
            // MouseX
            // 
            this.MouseX.AutoSize = true;
            this.MouseX.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MouseX.Location = new System.Drawing.Point(1073, 48);
            this.MouseX.Name = "MouseX";
            this.MouseX.Size = new System.Drawing.Size(54, 25);
            this.MouseX.TabIndex = 2;
            this.MouseX.Text = "Test";
            // 
            // MouseY
            // 
            this.MouseY.AutoSize = true;
            this.MouseY.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MouseY.Location = new System.Drawing.Point(1004, 48);
            this.MouseY.Name = "MouseY";
            this.MouseY.Size = new System.Drawing.Size(54, 25);
            this.MouseY.TabIndex = 3;
            this.MouseY.Text = "Test";
            // 
            // pbxInfo
            // 
            this.pbxInfo.BackColor = System.Drawing.Color.Transparent;
            this.pbxInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxInfo.Location = new System.Drawing.Point(768, 12);
            this.pbxInfo.Name = "pbxInfo";
            this.pbxInfo.Size = new System.Drawing.Size(484, 656);
            this.pbxInfo.TabIndex = 4;
            this.pbxInfo.TabStop = false;
            this.pbxInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbxInfo_MouseMove);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.MouseY);
            this.Controls.Add(this.MouseX);
            this.Controls.Add(this.pbxInfo);
            this.Controls.Add(this.pbxSide);
            this.Controls.Add(this.pbxGrid);
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxGrid;
        private System.Windows.Forms.PictureBox pbxSide;
        private System.Windows.Forms.Timer GameTick;
        private System.Windows.Forms.Label MouseX;
        private System.Windows.Forms.Label MouseY;
        private System.Windows.Forms.PictureBox pbxInfo;
    }
}

