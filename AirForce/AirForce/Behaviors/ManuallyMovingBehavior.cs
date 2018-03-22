using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirForce.Behaviors
{
    class ManuallyMovingBehavior : IMovable
    {
        private readonly FlyingObject source;

        public ManuallyMovingBehavior(FlyingObject source)
        {
            this.source = source;
        }

        public void Move()
        {
            SetSpeed();

            source.Position.X += source.HorizontalSpeed;
            if (source.Position.X < PlayerShip.Size.Width / 2)
                source.Position.X = PlayerShip.Size.Width / 2;
            if (source.Position.X > GameWindow.GameFieldSize.Width - PlayerShip.Size.Width / 2)
                source.Position.X = GameWindow.GameFieldSize.Width - PlayerShip.Size.Width / 2;

            source.Position.Y += source.VerticalSpeed;
            if (source.Position.Y < PlayerShip.Size.Height / 2)
                source.Position.Y = PlayerShip.Size.Height / 2;
            if (source.Position.Y > GameWindow.GameFieldSize.Height - PlayerShip.Size.Height / 2)
                source.Position.Y = GameWindow.GameFieldSize.Height - PlayerShip.Size.Height / 2;
        }

        private void SetSpeed()
        {
            if ((Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left))
                && (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right)))
            {
                source.HorizontalSpeed = 0;
                return;
            }

            if ((Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up))
                && (Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down)))
            {
                source.VerticalSpeed = 0;
                return;
            }

            //Pressed

            if (Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left))
            {
                source.HorizontalSpeed = -PlayerShip.Speed;
            }

            if (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right))
            {
                source.HorizontalSpeed = PlayerShip.Speed;
            }

            if (Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up))
            {
                source.VerticalSpeed = -PlayerShip.Speed;
            }

            if (Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down))
            {
                source.VerticalSpeed = PlayerShip.Speed;
            }

            //Not pressed

            if (Keyboard.IsKeyUp(Key.A) && Keyboard.IsKeyUp(Key.Left)
                                        && Keyboard.IsKeyUp(Key.D) && Keyboard.IsKeyUp(Key.Right))
            {
                source.HorizontalSpeed = 0;
            }


            if (Keyboard.IsKeyUp(Key.W) && Keyboard.IsKeyUp(Key.Up)
                                        && Keyboard.IsKeyUp(Key.S) && Keyboard.IsKeyUp(Key.Down))
            {
                source.VerticalSpeed = 0;
            }
        }
    }
}
