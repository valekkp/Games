using System.Drawing;

namespace AirForce
{
    public class Bullet : FlyingObject
    {
        public readonly double Speed;
        public bool Used;
        public readonly bool IsEnemyBullet;

        private Bullet(PointD location, bool isEnemyBullet)
        {
            if (isEnemyBullet)
            {
                Brush = Brushes.MidnightBlue;
                Speed = -10;
            }
            else
            {
                Brush = Brushes.DarkRed;
                Speed = 10;
            }
            IsEnemyBullet = isEnemyBullet;
            Width = 4;
            Height = 4;
            Used = false;
            Location = location;
        }

        public static Bullet CreateBullet(PointD location, bool isEnemyBullet)
        {
            return new Bullet(location, isEnemyBullet);
        }
    }
}
