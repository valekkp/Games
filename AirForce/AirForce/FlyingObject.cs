using System.Drawing;

namespace AirForce
{
    public abstract class FlyingObject
    {
        public PointD Location;
        protected Brush Brush;
        public float Width;
        public float Height;

        protected PointD LocationCenter
        {
            get
            {
                return new PointD(Location.X + Width/2, Location.Y + Height/2);
            }
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brush, (float)Location.X, (float)Location.Y, Width, Height);
        }
    }
}