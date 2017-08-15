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
        enum Colors
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

        public string GetColor()
        {
            return color.ToString();
        }

        
        public void SetRandomColor()
        {
            Random random = new Random();
            color = (Colors) random.Next(0, 5);
        }

        public void Draw(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.FillEllipse(brushColors[color], new Rectangle(Form1.Offset*position.X + 3*position.X, Form1.Offset*position.Y + 3 * position.Y, Form1.Offset, Form1.Offset));
        }
    }
}
