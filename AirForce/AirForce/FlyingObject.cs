using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public abstract class FlyingObject
    {
        public PointD Location;
        public Brush brush;
        public float width;
        public float height;

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(brush, (float)Location.X, (float)Location.Y, width, height);
        }
    }
}