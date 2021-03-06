﻿using System;
using System.Drawing;
using System.Windows.Input;
using AirForce.Behaviors;

namespace AirForce
{
    public class PlayerShip : FlyingObject
    {
        public static readonly int Speed = 4;
        public new static Size Size = new Size(50, 50);

        private readonly Brush mBrush = Brushes.DarkOliveGreen;
        private readonly int mHealthPoints = 1000;

        public PlayerShip()
        {
            Mover = new ManuallyMovingBehavior(this);
            Shooter = new ManuallyShootingBehavior(this);
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            HorizontalSpeed = 0;
            VerticalSpeed = 0;
            Position = new Point2D(100, GameWindow.GameFieldSize.Height / 2);
        }

        public override void Move()
        {
            Mover.Move();
        }
    }
}
