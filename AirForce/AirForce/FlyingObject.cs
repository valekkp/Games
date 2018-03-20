using System;
using System.Drawing;

namespace AirForce
{
    public abstract class FlyingObject
    {
        public Point2D Position;

        protected Brush Brush { get; set; }

        public Size Size { get; protected set; } 

        public int Speed { get; set; }
        public int HorizontalSpeed { get;  set; }
        public int VerticalSpeed { get; set; }

        public int HealthPoints { get; set; }

        protected Random Random = new Random();

        public FlyingObjectType Type { get; protected set; }

        protected IMovable mover;

        protected IShootable shooter;

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Brush, Position.X - Size.Width / 2, Position.Y - Size.Height / 2, Size.Width,
                Size.Height);
        }

        public virtual void Move()
        {
            mover?.Move();
        }

        public virtual void Shoot()
        {
            shooter?.Shoot();
        }

        public virtual void MakeAction()
        {
        }
    }
}