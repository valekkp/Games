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
    public partial class Form1 : Form
    {
        public const int BoardLength = 500;
        public const int LockOffset = 50;
        Game game = new Game();
        private bool screwdriverIsMoving = false;
        private int score = 0;
        private int lockpicksUsed = 0;

        public Form1()
        {
            InitializeComponent();
            SetParameters();
            game.Start();
            PlayingBoard.Refresh();
            LockpicksUsedLabel.Text = "Lockpicks used: 0";
            LocksOpenedLabel.Text = "Locks opened: 0";
        }

        private void SetParameters()
        {
            PlayingBoard.Location = new Point(0,0);
            PlayingBoard.Size = new Size(BoardLength, BoardLength);
            timer1.Start();
        }

        private void PlayingBoard_Paint(object sender, PaintEventArgs e)
        {
            game.DrawElements(e.Graphics);
        }

        private void PlayingBoard_MouseMove(object sender, MouseEventArgs e)
        {
            game.MoveLockpick(e.Location);
            PlayingBoard.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                screwdriverIsMoving = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                screwdriverIsMoving = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (screwdriverIsMoving)
                game.MoveScrewdriver();
            else
            {
                game.ReturnScrewdriver();
            }

            if (game.CheckLockpickDurability())
            {
                game.NewLockpick();
                lockpicksUsed++;
                LockpicksUsedLabel.Text = "Lockpicks used: " + lockpicksUsed;
            }

            if (game.CheckLockState())
            {
                game.NewScrewdriverAndLock();
                score++;
                LocksOpenedLabel.Text = "Locks opened: " + score;
            }

            PlayingBoard.Refresh();
        }
    }
}
