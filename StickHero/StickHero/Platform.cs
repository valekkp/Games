using System.Drawing;
using System.Drawing.Drawing2D;


namespace StickHero
{
    class Platform
    {
        public int Width;
        public int Height;
        public Point Position;

        public Platform(int width, int height, int xCoord, int yCoord)
        {
            Width = width;
            Height = height;
            Position.X = xCoord;
            Position.Y = yCoord;
        }

        public void Draw(Graphics graphics, Brush brush)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            Brush platformBrush = brush;

            graphics.FillRectangle(platformBrush,
                                    Position.X, Position.Y,
                                    Width, Height);
        }

    }
}
