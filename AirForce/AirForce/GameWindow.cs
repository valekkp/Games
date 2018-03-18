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

        private readonly GameController mGameController;
        private readonly Timer mUpdateTimer = new Timer();
        private readonly PlayerShip mPlayer = PlayerShip.GetInstance();

        public GameWindow()
        {
            InitializeComponent();
            mGameController = GameController.GetInstance();
            mUpdateTimer.Interval = 1;
            mUpdateTimer.Tick += (s, e) =>
            {
                mGameController.UpdateObjects();
                GameField.Refresh();
            };
            mUpdateTimer.Start();
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            mGameController.Update(e.Graphics);
        }
     
    }
}
