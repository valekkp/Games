﻿using System;
using System.Drawing;

namespace AirForce
{
    public class Bullet : FlyingObject
    {
        private readonly Size mSize = new Size(5, 5);
        private readonly Brush mBrush = Brushes.Firebrick;
        private readonly int mSpeed = 5;
        private readonly int mHealthPoints = 1;

        public Bullet(Size gameFieldSize)
        {
            Random random = new Random();
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Position = new Point(gameFieldSize.Width + mSize.Width / 2, mSize.Height / 2 + random.Next(gameFieldSize.Height - mSize.Height / 2));
        }

        public override void MakeAction()
        {

        }
    }
}
