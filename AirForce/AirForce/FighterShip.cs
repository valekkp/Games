using System;
using System.Drawing;

namespace AirForce
{
    public class FighterShip : FlyingObject
    {
        public static Size Size = new Size(50, 50);

        private static readonly PlayerShip player = PlayerShip.GetInstance();

        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mSpeed = 2;
        private readonly int mHealthPoints = 3;
        private int mCooldown = 0;

        public FighterShip(Point2D position)
        {
            Type = FlyingObjectType.Fighter;
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            base.Size = Size;
            Position = position;
        }

        public bool ReadyToShoot()
        {
            return Position.Y < player.Position.Y + player.Size.Height / 2
                   && Position.Y > player.Position.Y - player.Size.Height / 2
                   && mCooldown == 0;
        }

        public void Shoot()
        {
            if (mCooldown == 0)
            {
                GameController.GetInstance()
                    .EnemyBullets.Add(new Bullet(
                        new Point2D(
                            Position.X - Size.Width / 2- Bullet.Size.Width / 2, Position.Y), 
                        FlyingObjectType.EnemyBullet));
                mCooldown = 100;
            }
        }

        public void SubtractCooldown()
        {
            if(mCooldown > 0)
                mCooldown -= 1;
        }

        public void Dodge()
        {
            
        }
    }
}