using System;
using System.Drawing;

namespace AirForce
{
    public abstract class FlyingObject
    {
        public Point Position;

        protected Brush Brush { get; set; }

        public Size Size { get; protected set; }

        protected int Speed { get; set; }

        protected int HealthPoints { get; set; }

        protected Random Random = new Random();

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Brush, Position.X - Size.Width / 2, Position.Y - Size.Height / 2, Size.Width,
                Size.Height);
        }

        public virtual void Move()
        {
            Position.X -= Speed;
        }

        public virtual void MakeAction()
        {
        }
    }
}