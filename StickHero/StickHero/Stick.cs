using System.Drawing;
using System.Drawing.Drawing2D;
using static System.Math;

namespace StickHero
{
    class Stick
    {
        public const int MaxLength = GameForm.ClientWidth - Game.PlayingHeight;

        public const int Width = 10;

        public int Length;

        public Point StartingPoint;
        public Point EndingPoint;

        public Stick()
        {
            angle = -90;
        }

        public void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            Pen stickPen = new Pen(Color.DarkBlue, Width);
            graphics.DrawLine(stickPen, StartingPoint, EndingPoint);
        }

        private int angle;

        public int GetAngle()
        {
            return angle;
        }

        public void SetAngle(int value)
        {
            angle = value;
        }

        private int growingSpeed = 10;
        public void Grow()
        {
            EndingPoint.Y -= growingSpeed;

            Length = StartingPoint.Y - EndingPoint.Y;
            if (Length >= MaxLength)
            {
                Length = MaxLength;
            }
        }

        private int fallingSpeed = 2;
        public void Drop()
        {
            angle += fallingSpeed;
            if (angle > 0)
            {
                angle = 0;
            }
            EndingPoint.X = (int)(StartingPoint.X + Length * Cos(angle*PI/180));
            EndingPoint.Y = (int)(StartingPoint.Y + Length * Sin(angle*PI/180));
        }

        public bool IsOnPlatform(Platform platform)
        {
            return EndingPoint.X >= platform.Position.X &&
                   EndingPoint.X <= platform.Position.X + platform.Width;
        }

        public void Delete()
        {
            EndingPoint.X = StartingPoint.X;
            EndingPoint.Y = StartingPoint.Y;
        }
    }
}
