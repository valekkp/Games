using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
    }
}
