namespace StickHero
{
    partial class GameForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PlayingBoardPictureBox = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PlayingBoardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayingBoardPictureBox
            // 
            this.PlayingBoardPictureBox.Location = new System.Drawing.Point(28, 33);
            this.PlayingBoardPictureBox.Name = "PlayingBoardPictureBox";
            this.PlayingBoardPictureBox.Size = new System.Drawing.Size(189, 175);
            this.PlayingBoardPictureBox.TabIndex = 0;
            this.PlayingBoardPictureBox.TabStop = false;
            this.PlayingBoardPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayingBoardPictureBox_Paint);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.PlayingBoardPictureBox);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Stick Hero";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.PlayingBoardPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PlayingBoardPictureBox;
        public System.Windows.Forms.Timer timer;
    }
}

