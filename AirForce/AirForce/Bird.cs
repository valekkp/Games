using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirForce.Behaviors;

namespace AirForce
{
    class Bird : FlyingObject
    {
        public static Size Size = new Size(20, 20);
        public const int Speed = 1;

        private readonly Brush mBrush = Brushes.OrangeRed;
        private readonly int mHealthPoints = 1;

        public Bird(Point2D position)
        {
            Mover = new MovingRandomlyBehavior(this);
            Type = FlyingObjectType.Bird;
            HorizontalSpeed = -Speed;
            VerticalSpeed = Speed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            Position = position;
        }

        public override void Move()
        {
            Mover.Move();
        }
    }
}
