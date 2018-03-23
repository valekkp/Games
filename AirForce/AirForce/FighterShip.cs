using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace AirForce
{
    public class FighterShip : FlyingObject
    {
        public static Size Size = new Size(50, 50);
        public static readonly int Speed = 2;

        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mHealthPoints = 3;
        private int mCooldown = 0;

        public FighterShip(Point2D position, FlyingObject target)
        {
            Shooter = new ShootingBehavior(this, target);
            Mover = new MovingAndDodgingBehavior(this);
            Type = FlyingObjectType.Fighter;
            HorizontalSpeed = -Speed;
            VerticalSpeed = 0;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            base.Speed = Speed;
            Position = position;
        }

        public override void Move()
        {
            Mover.Move();
        }

        public override void Shoot()
        {
            Shooter.Shoot();
        }
    }
}