﻿using System;
using System.Drawing;
using System.Security.Cryptography;

namespace AirForce
{
    public class TankShip : FlyingObject
    {
        public static readonly Size Size = new Size(80, 80);

        private readonly Brush mBrush = Brushes.Brown;
        private readonly int mSpeed = 1;
        private readonly int mHealthPoints = 5;

        public TankShip(Point2D position)
        {
            Mover = new MovingBehavior(this);
            Type = FlyingObjectType.Tank;
            HorizontalSpeed = -mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            base.Speed = mSpeed;
            Position = position;
        }
    }
}
