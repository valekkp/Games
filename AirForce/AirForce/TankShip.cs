using System;
using System.Drawing;
using System.Security.Cryptography;

namespace AirForce
{
    public class TankShip : FlyingObject
    {
        private readonly Size mSize = new Size(80, 60);
        private readonly Brush mBrush = Brushes.Brown;
        private readonly int mSpeed = 1;
        private readonly int mHealthPoints = 5;

        public TankShip(Size gameFieldSize)
        {
            Random random = new Random();
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Size = mSize;
            Position = new Point(gameFieldSize.Width + mSize.Width/2, mSize.Height / 2 + random.Next(gameFieldSize.Height - mSize.Height/2));
        }

        public override void MakeAction()
        {
            
        }
    }
}
