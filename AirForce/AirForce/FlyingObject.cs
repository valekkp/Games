using System.Drawing;

namespace AirForce
{
    public abstract class FlyingObject
    {
        protected Brush Brush { get; set; }

        protected Point Position { get; set; }

        protected Size Size { get; set; }

        protected int Speed { get; set; }

        protected int HealthPoints { get; set; }

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Brush, Position.X - Size.Width / 2, Position.Y - Size.Height / 2, Size.Width, Size.Height);
        }

        public void Move()
        {
            Position = new Point(Position.X - Speed, Position.Y);
        }

        public abstract void MakeAction();
    }
}