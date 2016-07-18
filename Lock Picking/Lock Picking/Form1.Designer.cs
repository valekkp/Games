namespace Lock_Picking
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
            this.GamePictureBox = new System.Windows.Forms.PictureBox();
            this.LocksOpenedLabel = new System.Windows.Forms.Label();
            this.LockpicksBroken = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GamePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GamePictureBox
            // 
            this.GamePictureBox.Location = new System.Drawing.Point(8, 8);
            this.GamePictureBox.Name = "GamePictureBox";
            this.GamePictureBox.Size = new System.Drawing.Size(265, 244);
            this.GamePictureBox.TabIndex = 0;
            this.GamePictureBox.TabStop = false;
            this.GamePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePictureBox_Paint);
            // 
            // LocksOpenedLabel
            // 
            this.LocksOpenedLabel.AutoSize = true;
            this.LocksOpenedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LocksOpenedLabel.Location = new System.Drawing.Point(12, 269);
            this.LocksOpenedLabel.Name = "LocksOpenedLabel";
            this.LocksOpenedLabel.Size = new System.Drawing.Size(66, 24);
            this.LocksOpenedLabel.TabIndex = 1;
            this.LocksOpenedLabel.Text = "label1";
            // 
            // LockpicksBroken
            // 
            this.LockpicksBroken.AutoSize = true;
            this.LockpicksBroken.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LockpicksBroken.Location = new System.Drawing.Point(136, 269);
            this.LockpicksBroken.Name = "LockpicksBroken";
            this.LockpicksBroken.Size = new System.Drawing.Size(66, 24);
            this.LockpicksBroken.TabIndex = 2;
            this.LockpicksBroken.Text = "label2";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 319);
            this.Controls.Add(this.LockpicksBroken);
            this.Controls.Add(this.LocksOpenedLabel);
            this.Controls.Add(this.GamePictureBox);
            this.Name = "GameForm";
            this.Text = "Lock Picking";
            ((System.ComponentModel.ISupportInitialize)(this.GamePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GamePictureBox;
        private System.Windows.Forms.Label LocksOpenedLabel;
        private System.Windows.Forms.Label LockpicksBroken;
    }
}

