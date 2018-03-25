namespace AirForce
{
    partial class GameWindow
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
            this.playerHealthPointsLabel = new System.Windows.Forms.Label();
            this.KillsAmountLabel = new System.Windows.Forms.Label();
            this.GameField = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameField)).BeginInit();
            this.SuspendLayout();
            // 
            // playerHealthPointsLabel
            // 
            this.playerHealthPointsLabel.AutoSize = true;
            this.playerHealthPointsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playerHealthPointsLabel.Location = new System.Drawing.Point(12, 239);
            this.playerHealthPointsLabel.Name = "playerHealthPointsLabel";
            this.playerHealthPointsLabel.Size = new System.Drawing.Size(146, 25);
            this.playerHealthPointsLabel.TabIndex = 1;
            this.playerHealthPointsLabel.Text = "HealthPoints";
            // 
            // KillsAmountLabel
            // 
            this.KillsAmountLabel.AutoSize = true;
            this.KillsAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.KillsAmountLabel.Location = new System.Drawing.Point(164, 239);
            this.KillsAmountLabel.Name = "KillsAmountLabel";
            this.KillsAmountLabel.Size = new System.Drawing.Size(136, 25);
            this.KillsAmountLabel.TabIndex = 2;
            this.KillsAmountLabel.Text = "KillsAmount";
            // 
            // GameField
            // 
            this.GameField.Location = new System.Drawing.Point(0, 0);
            this.GameField.Name = "GameField";
            this.GameField.Size = new System.Drawing.Size(700, 500);
            this.GameField.TabIndex = 3;
            this.GameField.TabStop = false;
            this.GameField.Paint += new System.Windows.Forms.PaintEventHandler(this.GameField_Paint);
            // 
            // GameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.GameField);
            this.Controls.Add(this.KillsAmountLabel);
            this.Controls.Add(this.playerHealthPointsLabel);
            this.Name = "GameWindow";
            this.Text = "Air Force";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameWindow_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GameField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label playerHealthPointsLabel;
        private System.Windows.Forms.Label KillsAmountLabel;
        private System.Windows.Forms.PictureBox GameField;
    }
}

