using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace AirForce
{
    public class FighterShip : FlyingObject
    {
        public static Size Size = new Size(50, 50);

        private static readonly PlayerShip player = PlayerShip.GetInstance();

        private readonly Brush mBrush = Brushes.DarkGoldenrod;
        private readonly int mSpeed = 2;
        private readonly int mHealthPoints = 3;
        private int mVerticalSpeed = 0;
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

        public override void Move()
        {
            Position.X -= mSpeed;
            Position.Y += mVerticalSpeed;
            if (Position.Y - Size.Height / 2 < 0)
                Position.Y = Size.Height / 2;
            if (Position.Y + Size.Height / 2 > GameWindow.GameFieldSize.Height)
                Position.Y = GameWindow.GameFieldSize.Height - Size.Height / 2;
        }

        private bool ReadyToShoot()
        {
            return Position.Y < player.Position.Y + player.Size.Height / 2
                   && Position.Y > player.Position.Y - player.Size.Height / 2
                   && mCooldown == 0;
        }

        public void Shoot()
        {
            if (ReadyToShoot())
            {
                GameController.GetInstance()
                    .EnemyBullets.Add(new Bullet(
                        new Point2D(
                            Position.X - Size.Width / 2- Bullet.Size.Width / 2, Position.Y), 
                        FlyingObjectType.EnemyBullet));
                mCooldown = 100;
            }
            else if(mCooldown > 0)
                mCooldown -= 1;
        }

        public void Dodge()
        {
            var bullets = GameController.GetInstance().FlyingObjects
            .Where(x => x is Bullet && (x as Bullet).Type == FlyingObjectType.PlayerBullet)
                .Where(bullet => bullet.Position.X - bullet.Size.Width / 2 < Position.X + Size.Width / 2)
                .OrderByDescending(x => x.Position.X);
            var bulletToDodge = bullets.FirstOrDefault(bullet =>
                bullet.Position.Y + Bullet.Size.Height / 2 > Position.Y - Size.Height / 2
                && bullet.Position.Y - Bullet.Size.Height / 2 < Position.Y + Size.Height / 2);
            if (bulletToDodge != null)
            {
                if (Position.Y > bulletToDodge.Position.Y) mVerticalSpeed = mSpeed;
                else mVerticalSpeed = -mSpeed;
            }
            else
                mVerticalSpeed = 0;
        }
    }
}