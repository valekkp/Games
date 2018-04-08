using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmos
{
    public abstract class Cell
    {
        public static readonly Pen ColorWhenSmaller = Pens.DeepSkyBlue;
        public static readonly Pen ColorWhenBigger = Pens.DarkRed;

        public float Radius { get; private set; }

        private float mMass;
        public float Mass
        {
            get { return mMass; }
            set
            {
                var oldValue = mMass;
                mMass = value;
                var difference = oldValue - mMass;
                var discriminant = Math.Pow(2 * Math.PI * Radius, 2) - 4 * difference * Math.PI;
                var x1 = (float)((2 * Math.PI * Radius + Math.Sqrt(discriminant)) / (2 * Math.PI));
                var x2 = (float)((2 * Math.PI * Radius - Math.Sqrt(discriminant)) / (2 * Math.PI));
                Radius -= x1 > 0 ? x1 : x2;
            }
        }

        private Point2D mPosition;
        public Point2D Position {
            get { return mPosition; }
            set
            {
                mPosition = value;
                if (mPosition.X - Radius < 0) mPosition.X += GameWindow.GameFieldSize.Width;
                if (mPosition.Y - Radius < 0) mPosition.Y += GameWindow.GameFieldSize.Height;
                if (mPosition.X + Radius > GameWindow.GameFieldSize.Width) mPosition.X %= GameWindow.GameFieldSize.Width;
                if (mPosition.Y + Radius > GameWindow.GameFieldSize.Height) mPosition.Y %= GameWindow.GameFieldSize.Height;
            }
        }
        public Point2D MovementVector;
        public Point2D ImpulseVector;

        public Brush Color = Brushes.Chartreuse;

        protected IMovable mover;

        public virtual void Move()
        {
            mover.Move();
        }

        public Cell(Point2D position, float mass, Point2D movementVector)
        {
            mover = new MovingBehavior(this);
            Position = position;
            Mass = mass;
            MovementVector = movementVector;
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Color, Position.X % GameWindow.GameFieldSize.Width, Position.Y % GameWindow.GameFieldSize.Height, Radius, Radius);
            graphics.DrawEllipse(ColorWhenSmaller, Position.X % GameWindow.GameFieldSize.Width, Position.Y % GameWindow.GameFieldSize.Width, Radius, Radius);
        }
    }
}
