namespace WindowsAgent
{
    partial class BoardEstimator
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
            this.pbScreenGrab = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtBoardEstimate = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenGrab)).BeginInit();
            this.SuspendLayout();
            // 
            // pbScreenGrab
            // 
            this.pbScreenGrab.Location = new System.Drawing.Point(12, 12);
            this.pbScreenGrab.Name = "pbScreenGrab";
            this.pbScreenGrab.Size = new System.Drawing.Size(500, 500);
            this.pbScreenGrab.TabIndex = 0;
            this.pbScreenGrab.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(364, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "Estimate Board";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtBoardEstimate
            // 
            this.txtBoardEstimate.Location = new System.Drawing.Point(12, 529);
            this.txtBoardEstimate.Multiline = true;
            this.txtBoardEstimate.Name = "txtBoardEstimate";
            this.txtBoardEstimate.Size = new System.Drawing.Size(255, 167);
            this.txtBoardEstimate.TabIndex = 2;
            // 
            // BoardEstimator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 718);
            this.Controls.Add(this.txtBoardEstimate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbScreenGrab);
            this.Name = "BoardEstimator";
            this.Text = "BoardEstimator";
            ((System.ComponentModel.ISupportInitialize)(this.pbScreenGrab)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbScreenGrab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBoardEstimate;
    }
}

