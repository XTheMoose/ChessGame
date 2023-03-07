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
            this.GameTick = new System.Windows.Forms.Timer(this.components);
            this.MouseX = new System.Windows.Forms.Label();
            this.MouseY = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pbxInfo = new System.Windows.Forms.PictureBox();
            this.pbxGrid = new System.Windows.Forms.PictureBox();
            this.panelSideBar = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbxInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGrid)).BeginInit();
            this.panelSideBar.SuspendLayout();
            this.SuspendLayout();
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
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.BackgroundImage = global::GridExample.Properties.Resources.Collapse;
            this.button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Location = new System.Drawing.Point(10, 639);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(56, 30);
            this.button8.TabIndex = 12;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.BackgroundImage = global::GridExample.Properties.Resources.NewGame;
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(10, 577);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(56, 56);
            this.button7.TabIndex = 11;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.BackgroundImage = global::GridExample.Properties.Resources.Settings;
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(10, 515);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(56, 56);
            this.button6.TabIndex = 10;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = global::GridExample.Properties.Resources.Light;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(10, 453);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(56, 56);
            this.button5.TabIndex = 9;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = global::GridExample.Properties.Resources.SetGame;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(10, 198);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 56);
            this.button4.TabIndex = 8;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = global::GridExample.Properties.Resources.Archive;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(10, 136);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(56, 56);
            this.button3.TabIndex = 7;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::GridExample.Properties.Resources.ImportGame;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(10, 74);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(56, 56);
            this.button2.TabIndex = 6;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::GridExample.Properties.Resources.NewGame;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(10, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 56);
            this.button1.TabIndex = 5;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // panelSideBar
            // 
            this.panelSideBar.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelSideBar.Controls.Add(this.button1);
            this.panelSideBar.Controls.Add(this.button8);
            this.panelSideBar.Controls.Add(this.button7);
            this.panelSideBar.Controls.Add(this.button6);
            this.panelSideBar.Controls.Add(this.button5);
            this.panelSideBar.Controls.Add(this.button4);
            this.panelSideBar.Controls.Add(this.button3);
            this.panelSideBar.Controls.Add(this.button2);
            this.panelSideBar.Location = new System.Drawing.Point(0, 0);
            this.panelSideBar.Name = "panelSideBar";
            this.panelSideBar.Size = new System.Drawing.Size(76, 683);
            this.panelSideBar.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.MouseY);
            this.Controls.Add(this.MouseX);
            this.Controls.Add(this.pbxInfo);
            this.Controls.Add(this.pbxGrid);
            this.Controls.Add(this.panelSideBar);
            this.MaximumSize = new System.Drawing.Size(1280, 720);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chess";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxGrid)).EndInit();
            this.panelSideBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbxGrid;
        private System.Windows.Forms.Timer GameTick;
        private System.Windows.Forms.Label MouseX;
        private System.Windows.Forms.Label MouseY;
        private System.Windows.Forms.PictureBox pbxInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panelSideBar;
    }
}

