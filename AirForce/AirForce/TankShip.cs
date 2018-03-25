using System;
using System.Drawing;
using System.Security.Cryptography;

namespace AirForce
{
    public class TankShip : FlyingObject
    {
        public new static readonly Size Size = new Size(80, 80);
        public static readonly int Speed = 1;

        private readonly Brush mBrush = Brushes.Brown;
        private readonly int mHealthPoints = 5;

        public TankShip(Point2D position)
        {
            Mover = new MovingBehavior(this);
            Type = FlyingObjectType.Tank;
            HorizontalSpeed = -Speed;
            VerticalSpeed = 0;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            Position = position;
        }
    }
}
