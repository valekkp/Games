using System;
using System.Drawing;

namespace AirForce
{
    public class FighterShip : FlyingObject
    {
        private readonly Size mSize = new Size(50, 30);
        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mSpeed = 2;
        private readonly int mHealthPoints = 3;

        public FighterShip(Size gameFieldSize)
        {
            Random random = new Random();
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Size = mSize;
            Position = new Point(gameFieldSize.Width + mSize.Width / 2, mSize.Height / 2 + random.Next(gameFieldSize.Height - mSize.Height / 2));
        }

        public override void MakeAction()
        {
            
        }
    }
}