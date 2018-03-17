using System;
using System.Drawing;

namespace AirForce
{
    public class PlayerShip : FlyingObject
    {
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }

        private readonly Size mSize = new Size(50, 50);
        private readonly Brush mBrush = Brushes.DarkOliveGreen;
        private readonly int mHealthPoints = 3;

        private static PlayerShip instance;

        private PlayerShip()
        {
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Size = mSize;
            Position = new Point(100, GameWindow.GameFieldSize.Height/2);
        }

        public static PlayerShip GetInstance()
        {
            return instance ?? (instance = new PlayerShip());
        }

        public override void Move()
        {
            Position.X += HorizontalSpeed;
            Position.Y += VerticalSpeed;
        }
    }
}
