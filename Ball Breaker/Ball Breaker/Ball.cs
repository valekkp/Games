using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ball_Breaker
{
    class Ball
    {
        public enum Colors
        {
            Green,
            Blue,
            Red,
            Yellow,
            Violet
        }

        private Dictionary<Colors, Brush> brushColors = new Dictionary<Colors, Brush>()
        {
            {Colors.Green,  Brushes.Green},
            {Colors.Blue,   Brushes.DarkBlue},
            {Colors.Red,    Brushes.DarkRed},
            {Colors.Yellow, Brushes.DarkGoldenrod},
            {Colors.Violet, Brushes.Violet},
        };

        private Colors color;
        private Point position;

        public Ball(int positionY, int positionX)
        {
            position.X = positionX;
            position.Y = positionY;
        }

        public Colors GetColor()
        {
            return color;
        }

        static Random random = new Random();
        public void SetRandomColor()
        {
            color = (Colors) random.Next(0, 5);
        }

        public void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.FillEllipse(brushColors[color], new Rectangle(Form1.Offset + (Form1.BallSize + 3)*position.X, Form1.Offset + (Form1.BallSize + 3)*position.Y, Form1.BallSize, Form1.BallSize));
        }
    }
}
