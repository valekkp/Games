using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AirForce
{
    public partial class GameWindow : Form
    {
        public static Size GameFieldSize = new Size(700, 500);

        private readonly GameController mGameController;
        private readonly Timer mDrawingTimer = new Timer();

        private readonly PlayerShip mPlayer = PlayerShip.GetInstance();

        public GameWindow()
        {
            InitializeComponent();
            mGameController = GameController.GetInstance();

            mDrawingTimer.Interval = 1;
            mDrawingTimer.Tick += (s, e) => GameField.Refresh();
            mDrawingTimer.Start();
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            mGameController.Update(e.Graphics);
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            var pressedKey = e.KeyCode;

            switch (pressedKey)
            {
                case Keys.Left:
                case Keys.A:
                    if(mPlayer.HorizontalSpeed > -5)
                        mPlayer.HorizontalSpeed -= 5;
                    break;
                case Keys.Up:
                case Keys.W:
                    if (mPlayer.VerticalSpeed > -5)
                        mPlayer.VerticalSpeed -= 5;
                    break;
                case Keys.Right:
                case Keys.D:
                    if (mPlayer.HorizontalSpeed < 5)
                        mPlayer.HorizontalSpeed += 5;
                    break;
                case Keys.Down:
                case Keys.S:
                    if (mPlayer.VerticalSpeed < 5)
                        mPlayer.VerticalSpeed += 5;
                    break;
            }
        }

        private void GameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            var pressedKey = e.KeyCode;
            switch (pressedKey)
            {
                case Keys.Left:
                case Keys.A:
                    if (mPlayer.HorizontalSpeed < 5)
                        mPlayer.HorizontalSpeed += 5;
                    break;
                case Keys.Up:
                case Keys.W:
                    if (mPlayer.VerticalSpeed < 5)
                        mPlayer.VerticalSpeed += 5;
                    break;
                case Keys.Right:
                case Keys.D:
                    if (mPlayer.HorizontalSpeed > -5)
                        mPlayer.HorizontalSpeed -= 5;
                    break;
                case Keys.Down:
                case Keys.S:
                    if (mPlayer.VerticalSpeed > -5)
                        mPlayer.VerticalSpeed -= 5;
                    break;
            }
        }
    }
}
