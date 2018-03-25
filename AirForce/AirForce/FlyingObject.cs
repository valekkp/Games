using System;
using System.Drawing;

namespace AirForce
{
    public abstract class FlyingObject
    {
        public Point2D Position;

        protected Brush Brush { get; set; }

        public Size Size { get; protected set; } 

        public int HorizontalSpeed { get;  set; }
        public int VerticalSpeed { get; set; }

        public int HealthPoints { get; set; }

        public FlyingObjectType Type { get; protected set; }

        protected IMovable Mover;

        protected IShootable Shooter;

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Brush, Position.X - Size.Width / 2, Position.Y - Size.Height / 2, Size.Width,
                Size.Height);
        }

        public virtual void Move()
        {
            Mover?.Move();
        }

        public virtual Bullet Shoot()
        {
            return Shooter?.Shoot();
        }

        public virtual void MakeAction()
        {
        }
    }
}