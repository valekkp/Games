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
        public static Size GameFieldSize = new Size(700, 400);

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

        public static Point2D GameFieldCursorPosition { get; private set; }

        private void GameField_MouseDown(object sender, MouseEventArgs e)
        {
            GameFieldCursorPosition = new Point2D(e.X, e.Y);
            if (e.Button == MouseButtons.Right)
            {
                GameController.StopPlayer();
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                GameController.PushCell();
                return;
            }
        }

        private void GameField_Paint(object sender, PaintEventArgs e)
        {
            GameController.Update(e.Graphics);
            //e.Graphics.DrawRectangle(Pens.Black, 0, 0, GameFieldSize.Width + 6, GameFieldSize.Height + 2);
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                if(gameTimer.Enabled) gameTimer.Stop();
                GameField.Refresh();
            }

            if (e.KeyCode == Keys.F)
            {
                if(!gameTimer.Enabled)
                    gameTimer.Start();
            }
        }
    }
}
