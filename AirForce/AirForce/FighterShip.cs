using System;
using System.Drawing;

namespace AirForce
{
    public class FighterShip : FlyingObject
    {
        private readonly Size mSize = new Size(50, 50);
        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mSpeed = 2;
        private readonly int mHealthPoints = 3;

        public FighterShip(Size gameFieldSize)
        {
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Size = mSize;
            Position = new Point(gameFieldSize.Width + Size.Width / 2, Size.Height / 2 + Random.Next(gameFieldSize.Height - Size.Height / 2));
        }
    }
}