using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace AirForce
{
    public class FighterShip : FlyingObject
    {
        public new static Size Size = new Size(50, 50);
        public static readonly int Speed = 2;

        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mHealthPoints = 3;

        public FighterShip(Point2D position, FlyingObject target, IEnumerable<FlyingObject> objectsToDodge)
        {
            Shooter = new ShootingBehavior(this, target);
            Mover = new MovingAndDodgingBehavior(this, objectsToDodge);
            Type = FlyingObjectType.Fighter;
            HorizontalSpeed = -Speed;
            VerticalSpeed = 0;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            Position = position;
        }

        public override void Move()
        {
            Mover.Move();
        }

        public override Bullet Shoot()
        {
            return Shooter.Shoot();
        }
    }
}