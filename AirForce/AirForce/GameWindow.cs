using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirForce
{
    public partial class GameWindow : Form
    {
        private readonly GameController mGameController;
        private readonly Timer mDrawingTimer = new Timer();

        public GameWindow()
        {
            InitializeComponent();
            mGameController = new GameController(GameField.Size);

            mDrawingTimer.Interval = 1;
            mDrawingTimer.Tick += (s, e) => GameField.Refresh();
            mDrawingTimer.Start();
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            mGameController.Update(e.Graphics);
        }
    }
}
