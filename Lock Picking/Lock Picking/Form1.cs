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
        private int cooldown = 0;
        private int score = 0;
        private int lockpicksUsed = 0;
        private bool screwdriverPartialMoving = false;

        public Form1()
        {
            InitializeComponent();
            game.Start();
            PlayingBoard.Refresh();
            LockpicksUsedLabel.Text = "Lockpicks used: 0";
            LocksOpenedLabel.Text = "Locks opened: 0";
        }

        private void SetParameters()
        {
            PlayingBoard.Location = new Point(0,0);
            PlayingBoard.Size = new Size(BoardLength, BoardLength);
        }

        public void PlayingBoard_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen lockPen = new Pen(Color.Brown);
            graphics.DrawEllipse(lockPen, LockOffset, LockOffset, BoardLength / 2, BoardLength / 2);
            Pen linePen = new Pen(Color.Brown);
            graphics.DrawLine(linePen, LockOffset, LockOffset + BoardLength / 4, BoardLength / 2 + LockOffset, LockOffset + BoardLength / 4);
            game.GetLockpick().Draw(e.Graphics);
            game.GetScrewdriver().Draw(e.Graphics);
            timer1.Start();
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
                game.GetScrewdriver().Move(game.GetLockpick(), game.GetLock());
            else
            {
                game.GetScrewdriver().Return();
                game.GetLockpick().CurrentColor = Color.Green;
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
