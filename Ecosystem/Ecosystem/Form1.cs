using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Ecosystem
{
    public partial class PlayingBoardForm : Form
    {
        public const int playingBoardWidth = 300;
        public const int playingBoardHeight = 300;

        private const int ballsAmount = 1;

        private PointF gravity = new PointF(0, 0.1f);

        private Ball[] balls = new Ball[ballsAmount];

        public PlayingBoardForm()
        {
            Random random = new Random();
            InitializeComponent();

            Size = new Size(playingBoardWidth + 50, playingBoardHeight + 50);

            PlayingBoardPictureBox.Width = playingBoardWidth;
            PlayingBoardPictureBox.Height = playingBoardHeight;


            timer1.Interval = 10;
            timer1.Start();

            for (int i = 0; i < ballsAmount; i++)
            {
                balls[i] = new Ball(random.Next(10, 200), random.Next(10, 200), random.Next(1, 51));
                //balls[i].ApplyForce(new PointF(1,1));
            }
        }

        private void PlayingBoardPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            foreach (var ball in balls)
            {
                ball.Draw(e.Graphics);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var ball in balls)
            {
                //ball.ApplyForce(gravity);
                ball.Update();
            }
            ActiveForm.Refresh();
        }

        private float forceMultiplier = 0.2f;
        private void PlayingBoardPictureBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs) e;

            if (me.Button == MouseButtons.Left)
            {
                foreach (var ball in balls)
                {
                    PointF force = new PointF(ball.GetPosition().X - me.Location.X,
                        ball.GetPosition().Y - me.Location.Y);
                    ball.ApplyForce(NormalizeForce(force));
                }
            }

            if (me.Button == MouseButtons.Right)
            {
                foreach (var ball in balls)
                {
                    PointF force = new PointF(me.Location.X - ball.GetPosition().X,
                                              me.Location.Y - ball.GetPosition().Y);
                    ball.ApplyForce(NormalizeForce(force));
                }
            }
        }

        private PointF NormalizeForce(PointF force)
        {
            return new PointF(force.X/Math.Abs(force.X), force.Y / Math.Abs(force.Y));
        }
    }
}
