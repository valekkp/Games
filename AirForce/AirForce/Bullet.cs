using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public class Bullet : FlyingObject
    {
        public static readonly Size mSize = new Size(10, 10);

        private readonly Brush mBrush = Brushes.Firebrick;
        private readonly int mSpeed = 5;
        private readonly int mHealthPoints = 1;

        public Bullet(Point2D position)
        {
            Speed = mSpeed;
            HealthPoints = mHealthPoints;
            Size = mSize;
            Brush = mBrush;
            Position = position;
        }
    }
}
