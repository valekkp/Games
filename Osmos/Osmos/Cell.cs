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

                Radius = (float) Math.Sqrt(mMass / Math.PI);
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

        public Brush Color = Brushes.Chartreuse;

        protected IMovable mover;

        public virtual void Move()
        {
            mover.Move();
        }

        public Cell(Point2D position, float mass, Point2D movementVector)
        {
            mover = new MovingBehavior(this);
            Mass = mass;
            Position = position;
            MovementVector = movementVector;
        }

        public virtual void Draw(Graphics graphics)
        {
            graphics.FillEllipse(Color, (Position.X - Radius) % GameWindow.GameFieldSize.Width, (Position.Y - Radius) % GameWindow.GameFieldSize.Height, Radius * 2, Radius * 2);
            graphics.DrawEllipse(ColorWhenSmaller, (Position.X - Radius) % GameWindow.GameFieldSize.Width, (Position.Y - Radius) % GameWindow.GameFieldSize.Width, Radius * 2, Radius * 2);
            graphics.FillEllipse(Brushes.Red, Position.X - 1, Position.Y - 1, 2f, 2f);
        }
    }
}
