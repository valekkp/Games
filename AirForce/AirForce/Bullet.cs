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

        //public Bullet(Point2D position)
        //{
        //    HorizontalSpeed = Speed;
        //    HealthPoints = mHealthPoints;
        //    base.Size = Size;
        //    Position = position;
        //}

        public Bullet(FlyingObject source)
        {
            mover = new MovingHorizontallyBehavior(this);
            HorizontalSpeed = source is PlayerShip ? Speed : -Speed;
            Type = source is PlayerShip ? FlyingObjectType.PlayerBullet : FlyingObjectType.EnemyBullet;
            HealthPoints = mHealthPoints;
            base.Size = Size;
            base.Speed = Speed;
            Brush = source is PlayerShip ? mPlayerBrush : mEnemyBrush;
            Position = source is PlayerShip
                ? new Point2D(source.Position.X + source.Size.Width / 2 + Size.Width / 2, source.Position.Y)
                : new Point2D(source.Position.X - source.Size.Width / 2 - Size.Width / 2, source.Position.Y);
        }
    }
}
