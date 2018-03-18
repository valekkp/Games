using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public class Bullet : FlyingObject
    {
        public static readonly Size Size = new Size(10, 10);
        public static readonly int Speed = 5;

        private readonly Brush mEnemyBrush = Brushes.Firebrick;
        private readonly Brush mPlayerBrush = Brushes.DarkBlue;
        
        private readonly int mHealthPoints = 1;

        public Bullet(Point2D position)
        {
            base.Speed = Speed;
            HealthPoints = mHealthPoints;
            base.Size = Size;
            Position = position;
        }

        public Bullet(Point2D position, FlyingObjectType bulletType)
        {
            Type = bulletType;
            base.Speed = bulletType == FlyingObjectType.PlayerBullet ? -Speed : Speed;
            HealthPoints = mHealthPoints;
            base.Size = Size;
            Brush = bulletType == FlyingObjectType.PlayerBullet ? mPlayerBrush : mEnemyBrush;
            Position = position;
        }
    }
}
