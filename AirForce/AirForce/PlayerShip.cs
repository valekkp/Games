using System;
using System.Drawing;
using System.Windows.Input;

namespace AirForce
{
    public class PlayerShip : FlyingObject
    {
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }

        private readonly int mSpeed = 5;
        private readonly Size mSize = new Size(50, 50);
        private readonly Brush mBrush = Brushes.DarkOliveGreen;
        private readonly int mHealthPoints = 3;

        private static PlayerShip instance;

        private PlayerShip()
        {
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Size = mSize;
            Speed = mSpeed;
            Position = new Point(100, GameWindow.GameFieldSize.Height/2);
        }

        public static PlayerShip GetInstance()
        {
            return instance ?? (instance = new PlayerShip());
        }

        public override void Move()
        {
            SetSpeed();

            Position.X += HorizontalSpeed;
            Position.Y += VerticalSpeed;
        }

        //ToDo: Find better solution and clean
        private void SetSpeed()
        {
            //Combination

            if ((Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left))
                && (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right)))
            {
                HorizontalSpeed = 0;
                return;
            }

            if ((Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up))
                && (Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down)))
            {
                VerticalSpeed = 0;
                return;
            }

            //Pressed

            if (Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left))
            {
                HorizontalSpeed = -Speed;
            }

            if (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right))
            {
                HorizontalSpeed = Speed;
            }

            if (Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up))
            {
                VerticalSpeed = -Speed;
            }

            if (Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down))
            {
                VerticalSpeed = Speed;
            }

            //Not pressed

            if (Keyboard.IsKeyUp(Key.A) && Keyboard.IsKeyUp(Key.Left)
                && Keyboard.IsKeyUp(Key.D) && Keyboard.IsKeyUp(Key.Right))
            {
                HorizontalSpeed = 0;
            }


            if (Keyboard.IsKeyUp(Key.W) && Keyboard.IsKeyUp(Key.Up)
                && Keyboard.IsKeyUp(Key.S) && Keyboard.IsKeyUp(Key.Down))
            {
                VerticalSpeed = 0;
            }
        }
    }
}
