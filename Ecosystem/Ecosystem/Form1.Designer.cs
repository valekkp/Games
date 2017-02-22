namespace Ecosystem
{
    partial class PlayingBoardForm
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
            this.PlayingBoardPictureBox = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PlayingBoardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayingBoardPictureBox
            // 
            this.PlayingBoardPictureBox.Location = new System.Drawing.Point(0, 0);
            this.PlayingBoardPictureBox.Name = "PlayingBoardPictureBox";
            this.PlayingBoardPictureBox.Size = new System.Drawing.Size(272, 249);
            this.PlayingBoardPictureBox.TabIndex = 0;
            this.PlayingBoardPictureBox.TabStop = false;
            this.PlayingBoardPictureBox.Click += new System.EventHandler(this.PlayingBoardPictureBox_Click);
            this.PlayingBoardPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayingBoardPictureBox_Paint);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PlayingBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.PlayingBoardPictureBox);
            this.Name = "PlayingBoardForm";
            this.Text = "Ecosystem";
            ((System.ComponentModel.ISupportInitialize)(this.PlayingBoardPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PlayingBoardPictureBox;
        private System.Windows.Forms.Timer timer1;
    }
}

