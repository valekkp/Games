namespace Lock_Picking
{
    partial class Form1
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
            this.PlayingBoard = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.LocksOpenedLabel = new System.Windows.Forms.Label();
            this.LockpicksUsedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PlayingBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // PlayingBoard
            // 
            this.PlayingBoard.Location = new System.Drawing.Point(0, 0);
            this.PlayingBoard.Name = "PlayingBoard";
            this.PlayingBoard.Size = new System.Drawing.Size(400, 400);
            this.PlayingBoard.TabIndex = 0;
            this.PlayingBoard.TabStop = false;
            this.PlayingBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayingBoard_Paint);
            this.PlayingBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PlayingBoard_MouseMove);
            // 
            // timer1
            // 
            this.timer1.Interval = 4;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LocksOpenedLabel
            // 
            this.LocksOpenedLabel.AutoSize = true;
            this.LocksOpenedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LocksOpenedLabel.Location = new System.Drawing.Point(420, 84);
            this.LocksOpenedLabel.Name = "LocksOpenedLabel";
            this.LocksOpenedLabel.Size = new System.Drawing.Size(163, 25);
            this.LocksOpenedLabel.TabIndex = 1;
            this.LocksOpenedLabel.Text = "Locks Opened";
            // 
            // LockpicksUsedLabel
            // 
            this.LockpicksUsedLabel.AutoSize = true;
            this.LockpicksUsedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LockpicksUsedLabel.Location = new System.Drawing.Point(420, 187);
            this.LockpicksUsedLabel.Name = "LockpicksUsedLabel";
            this.LockpicksUsedLabel.Size = new System.Drawing.Size(178, 25);
            this.LockpicksUsedLabel.TabIndex = 1;
            this.LockpicksUsedLabel.Text = "Lockpicks Used";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 410);
            this.Controls.Add(this.LockpicksUsedLabel);
            this.Controls.Add(this.LocksOpenedLabel);
            this.Controls.Add(this.PlayingBoard);
            this.Name = "Form1";
            this.Text = "Lockpicking";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.PlayingBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PlayingBoard;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label LocksOpenedLabel;
        private System.Windows.Forms.Label LockpicksUsedLabel;
    }
}

