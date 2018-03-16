using System;
using System.Drawing;

namespace AirForce
{
    public class PlayerShip : FlyingObject
    {
        private readonly Size mSize = new Size(50, 30);
        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mSpeed = 2;
        private readonly int mHealthPoints = 3;

        public PlayerShip(Size gameFieldSize)
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
