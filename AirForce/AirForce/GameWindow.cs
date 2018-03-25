using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace AirForce
{
    public partial class GameWindow : Form
    {
        public static Size GameFieldSize = new Size(700, 500);

        private readonly Timer mTimer = new Timer();
        private readonly GameController gameController;

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(GameController gameController)
        {
            this.gameController = gameController;
            InitializeComponent();
            mTimer.Interval = 1;
            mTimer.Tick += (s, e) =>
            {
                gameController.UpdateGameState();
                GameField.Refresh();
            };
            gameController.StartGame();
            mTimer.Start();
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            switch (gameController.CurrentGameState)
            {
                case GameState.Playing:
                    e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                    gameController.DrawObjects(e.Graphics);
                    break;
                case GameState.Defeat:
                    e.Graphics.DrawString("Defeat! Press Enter to restart.", 
                        new Font(FontFamily.GenericSansSerif, 20, FontStyle.Regular), 
                        Brushes.Black, 
                        new Point(GameFieldSize.Width/2 - 100, GameFieldSize.Height/2 - 100));
                    break;
            }
        }

        private void GameWindow_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (gameController.CurrentGameState == GameState.Defeat
                && e.KeyCode == Keys.Enter)
            {
                gameController.StartGame();
            }
        }
    }
}
