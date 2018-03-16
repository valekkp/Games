using System.Drawing;

namespace AirForce
{
    public abstract class FlyingObject
    {
        private readonly float mWidth;
        private readonly float mHeight;

        protected Brush Brush;

        public PointD Location
        {
            get; private set;
        }

        public float Width
        {
            get { return mWidth; }
        }

        public float Height
        {
            get { return mHeight; }
        }

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