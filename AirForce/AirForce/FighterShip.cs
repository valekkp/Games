using System;
using System.Drawing;

namespace AirForce
{
    public class FighterShip : FlyingObject
    {
        public static Size Size = new Size(50, 50);

        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mSpeed = 2;
        private readonly int mHealthPoints = 3;
        private int mCooldown = 0;


        public FighterShip(Point2D position)
        {
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            Position = position;
        }

        public void ResetCooldown()
        {
            mCooldown = 100;
        }

        public void AddCooldown()
        {
            if(mCooldown > 0)
                mCooldown -= 1;
        }

        public bool ReadyToShoot(PlayerShip target)
        {
            return Position.Y < target.Position.Y + target.Size.Height / 2
                   && Position.Y > target.Position.Y - target.Size.Height / 2
                   && mCooldown == 0;
        }
    }
}