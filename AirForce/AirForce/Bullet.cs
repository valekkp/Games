using System.Drawing;

namespace AirForce
{
    public class Bullet : FlyingObject
    {
        public new static readonly Size Size = new Size(10, 10);
        public const int Speed = 5;

        private readonly Brush mEnemyBrush = Brushes.Firebrick;
        private readonly Brush mPlayerBrush = Brushes.DarkBlue;
        
        private readonly int mHealthPoints = 1;

        public Bullet(FlyingObject source)
        {
            Mover = new MovingBehavior(this);
            HorizontalSpeed = source is PlayerShip ? Speed : -Speed;
            Type = source is PlayerShip ? FlyingObjectType.PlayerBullet : FlyingObjectType.EnemyBullet;
            HealthPoints = mHealthPoints;
            base.Size = Size;
            Brush = source is PlayerShip ? mPlayerBrush : mEnemyBrush;
            Position = source is PlayerShip
                ? new Point2D(source.Position.X + source.Size.Width / 2 + Size.Width / 2, source.Position.Y)
                : new Point2D(source.Position.X - source.Size.Width / 2 - Size.Width / 2, source.Position.Y);
        }
    }
}
