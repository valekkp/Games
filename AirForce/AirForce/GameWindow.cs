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
     
    }
}
