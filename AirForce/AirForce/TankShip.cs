using System;
using System.Drawing;
using System.Security.Cryptography;

namespace AirForce
{
    public class TankShip : FlyingObject
    {
        private readonly Size mSize = new Size(80, 80);
        private readonly Brush mBrush = Brushes.Brown;
        private readonly int mSpeed = 1;
        private readonly int mHealthPoints = 5;

        public TankShip(Size gameFieldSize)
        {
            Rectangle rectangle = new Rectangle();
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Size = mSize;
            Position = new Point(gameFieldSize.Width + mSize.Width/2, mSize.Height / 2 + Random.Next(0, gameFieldSize.Height - mSize.Height/2));
        }
    }
}
