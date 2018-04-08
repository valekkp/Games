using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osmos
{
    public partial class GameWindow : Form
    {
        public static Size GameFieldSize = new Size(700, 500);

        private Timer gameTimer = new Timer();

        public GameWindow()
        {
            InitializeComponent();
            GameField.Size = GameFieldSize;
            gameTimer.Interval = 1;
            gameTimer.Tick += (s,e) =>
            {
                GameField.Refresh();
            };
            gameTimer.Start();
            GameController.Start();
        }

        private void GameField_MouseDown(object sender, MouseEventArgs e)
        {
            GameController.MovePlayer(new Point2D(e.X, e.Y));
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            GameController.Update(e.Graphics);
        }
    }
}
