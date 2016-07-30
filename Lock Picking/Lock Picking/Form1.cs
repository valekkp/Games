using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lock_Picking
{
    public partial class GameForm : Form
    {
        Game game = new Game();
        private bool isSpacePressed = false;

        public GameForm()
        {
            InitializeComponent();
            SetElementParameters();
            game.Start();
            //GamePictureBox.Refresh();
        }

        private void SetElementParameters()
        {
            GamePictureBox.Location = new Point(0, 0);
            GamePictureBox.Size = new Size(2 * Game.CenterPosition.X, 2 * Game.CenterPosition.Y);
            LocksOpenedLabel.Location = new Point(50, GamePictureBox.ClientSize.Height);
            LockpicksBroken.Location = new Point(LocksOpenedLabel.Size.Width + 50, GamePictureBox.ClientSize.Height);
            ClientSize = new Size(2 * Game.CenterPosition.X, 2 * Game.CenterPosition.Y + LockpicksBroken.Height + LocksOpenedLabel.Height + 20);
            LockpicksBroken.Location = new Point(LocksOpenedLabel.Location.X, LocksOpenedLabel.Location.Y + LocksOpenedLabel.Height);
            timer.Interval = 10;
            timer.Start();
        }

        private void GamePictureBox_Paint(object sender, PaintEventArgs e)
        {
            game.DrawElements(e.Graphics);
        }

        private void GamePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            game.MoveLockpick(e.Location.X, e.Location.Y);
            GamePictureBox.Refresh();
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isSpacePressed = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.MoveScrewdriver(isSpacePressed);
            GamePictureBox.Refresh();
            LockpicksBroken.Text = "Lockpicks broken: " + game.lockpicksUsed.ToString();
            LocksOpenedLabel.Text = "Locks opened: " + game.locksOpened.ToString();
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                isSpacePressed = false;
        }
    }
}
