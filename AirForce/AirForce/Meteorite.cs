using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    class Meteorite : FlyingObject
    {
        //public static readonly Size Size = new Size(100, 100);
        public const int Speed = 2;

        private readonly Brush mBrush = Brushes.PaleVioletRed;
        private readonly int mHealthPoints = 10;

        private Random random = new Random();

        public Meteorite(Point2D position)
        {
            Mover = new MovingBehavior(this);
            Type = FlyingObjectType.Meteorite;
            HorizontalSpeed = -Speed;
            VerticalSpeed = Speed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            int diameter = random.Next(70, 111);
            Size = new Size(diameter, diameter);
            Position = position;
        }

        public override void Move()
        {
            Mover.Move();
        }
    }
}
