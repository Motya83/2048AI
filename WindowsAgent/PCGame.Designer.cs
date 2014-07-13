namespace WindowsAgent
{
    partial class PCGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PCGame));
            this.txtBoard = new System.Windows.Forms.TextBox();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnAIStart = new System.Windows.Forms.Button();
            this.lblGameStatus = new System.Windows.Forms.Label();
            this.ddlDepth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlAlgorithm = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbKeepGoing = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBoard
            // 
            this.txtBoard.Enabled = false;
            this.txtBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoard.Location = new System.Drawing.Point(527, 276);
            this.txtBoard.Multiline = true;
            this.txtBoard.Name = "txtBoard";
            this.txtBoard.Size = new System.Drawing.Size(232, 218);
            this.txtBoard.TabIndex = 0;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(16, 37);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(75, 23);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // btnAIStart
            // 
            this.btnAIStart.Location = new System.Drawing.Point(142, 37);
            this.btnAIStart.Name = "btnAIStart";
            this.btnAIStart.Size = new System.Drawing.Size(75, 23);
            this.btnAIStart.TabIndex = 2;
            this.btnAIStart.Text = "Start AI";
            this.btnAIStart.UseVisualStyleBackColor = true;
            this.btnAIStart.Click += new System.EventHandler(this.btnAIStart_Click);
            // 
            // lblGameStatus
            // 
            this.lblGameStatus.AutoSize = true;
            this.lblGameStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameStatus.Location = new System.Drawing.Point(97, 75);
            this.lblGameStatus.Name = "lblGameStatus";
            this.lblGameStatus.Size = new System.Drawing.Size(0, 13);
            this.lblGameStatus.TabIndex = 3;
            // 
            // ddlDepth
            // 
            this.ddlDepth.FormattingEnabled = true;
            this.ddlDepth.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.ddlDepth.Location = new System.Drawing.Point(76, 63);
            this.ddlDepth.Name = "ddlDepth";
            this.ddlDepth.Size = new System.Drawing.Size(121, 21);
            this.ddlDepth.TabIndex = 4;
            this.ddlDepth.SelectedIndexChanged += new System.EventHandler(this.ddlDepth_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Depth:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlAlgorithm);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ddlDepth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(527, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 107);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algorithm Settings";
            // 
            // ddlAlgorithm
            // 
            this.ddlAlgorithm.FormattingEnabled = true;
            this.ddlAlgorithm.Items.AddRange(new object[] {
            "Minimax",
            "Alpha-Beta ",
            "Expectimax"});
            this.ddlAlgorithm.Location = new System.Drawing.Point(76, 30);
            this.ddlAlgorithm.Name = "ddlAlgorithm";
            this.ddlAlgorithm.Size = new System.Drawing.Size(121, 21);
            this.ddlAlgorithm.TabIndex = 8;
            this.ddlAlgorithm.SelectedIndexChanged += new System.EventHandler(this.ddlAlgorithm_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Algorithm:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbKeepGoing);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnNewGame);
            this.groupBox2.Controls.Add(this.btnAIStart);
            this.groupBox2.Controls.Add(this.lblGameStatus);
            this.groupBox2.Location = new System.Drawing.Point(527, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 145);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Game Options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Game Status:";
            // 
            // cbKeepGoing
            // 
            this.cbKeepGoing.AutoSize = true;
            this.cbKeepGoing.Location = new System.Drawing.Point(16, 105);
            this.cbKeepGoing.Name = "cbKeepGoing";
            this.cbKeepGoing.Size = new System.Drawing.Size(201, 17);
            this.cbKeepGoing.TabIndex = 5;
            this.cbKeepGoing.Text = "Keep playing after reaching 2048 tile.";
            this.cbKeepGoing.UseVisualStyleBackColor = true;
            // 
            // PCGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsAgent.ImageResources.Board;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(769, 506);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PCGame";
            this.Text = "PCGame";
            this.Activated += new System.EventHandler(this.PCGame_Activated);
            this.Load += new System.EventHandler(this.PCGame_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoard;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnAIStart;
        private System.Windows.Forms.Label lblGameStatus;
        private System.Windows.Forms.ComboBox ddlDepth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox ddlAlgorithm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbKeepGoing;
    }
}