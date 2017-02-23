namespace Langton_s_Ant
{
    partial class GamingForm
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
            this.GamingBoard = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.GamingBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // GamingBoard
            // 
            this.GamingBoard.Location = new System.Drawing.Point(0, 0);
            this.GamingBoard.Name = "GamingBoard";
            this.GamingBoard.Size = new System.Drawing.Size(272, 249);
            this.GamingBoard.TabIndex = 0;
            this.GamingBoard.TabStop = false;
            this.GamingBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.GamingBoard_Paint);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GamingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.GamingBoard);
            this.DoubleBuffered = true;
            this.Name = "GamingForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.GamingBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GamingBoard;
        private System.Windows.Forms.Timer timer1;
    }
}

