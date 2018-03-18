using System;
using System.Drawing;
using System.Windows.Input;

namespace AirForce
{
    public class PlayerShip : FlyingObject
    {
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public int Cooldown { get; private set; }

        public Point2D CurrentPosition
        {
            get { return Position; }
        }

        private readonly int mSpeed = 4;
        private readonly Size mSize = new Size(50, 50);
        private readonly Brush mBrush = Brushes.DarkOliveGreen;
        private readonly int mHealthPoints = 1000;

        private static PlayerShip instance;

        private PlayerShip()
        {
            HealthPoints = mHealthPoints;
            Brush = mBrush;
            Size = mSize;
            Speed = mSpeed;
            Position = new Point2D(100, GameWindow.GameFieldSize.Height / 2);
            Cooldown = 0;
        }

        public static PlayerShip GetInstance()
        {
            return instance ?? (instance = new PlayerShip());
        }

        public override void Move()
        {
            SetSpeed();

            Position.X += HorizontalSpeed;
            if (Position.X < Size.Width / 2) Position.X = Size.Width / 2;
            if (Position.X > GameWindow.GameFieldSize.Width - Size.Width / 2)
                Position.X = GameWindow.GameFieldSize.Width - Size.Width / 2;

            Position.Y += VerticalSpeed;
            if (Position.Y < Size.Height / 2) Position.Y = Size.Height / 2;
            if (Position.Y > GameWindow.GameFieldSize.Height - Size.Height / 2)
                Position.Y = GameWindow.GameFieldSize.Height - Size.Height / 2;
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

        public bool ReadyToShoot()
        {
            return Cooldown == 0;
        }

        public void Shoot()
        {
            GameController.GetInstance()
                    .PlayerBullets.Add(new Bullet(
                        new Point2D(Position.X + Size.Width / 2 + Bullet.Size.Width/2, Position.Y), 
                        FlyingObjectType.PlayerBullet));
            Cooldown = 25;
        }

        public void SetFasterCooldown()
        {
            if(Cooldown > 5)
                Cooldown = 5;
        }

        public void SubtractCooldown()
        {
            if(Cooldown > 0)
                Cooldown -= 1;
        }
    }
}
