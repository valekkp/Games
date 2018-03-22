using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    class MovingAndDodgingBehavior : IMovable
    {
        private readonly FlyingObject flyingObject;

        public MovingAndDodgingBehavior(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void Move()
        {
            Dodge();
            int horizontalSpeed = flyingObject.HorizontalSpeed;
            int verticalSpeed = flyingObject.VerticalSpeed;
            Size size = flyingObject.Size;

            flyingObject.Position.X += horizontalSpeed;
            flyingObject.Position.Y += verticalSpeed;
            if (flyingObject.Position.Y - size.Height / 2 < 0)
                flyingObject.Position.Y = size.Height / 2;
            if (flyingObject.Position.Y + size.Height / 2 > GameWindow.GameFieldSize.Height)
                flyingObject.Position.Y = GameWindow.GameFieldSize.Height - size.Height / 2;
        }

        private void Dodge()
        {
            int horizontalSpeed = flyingObject.Speed;
            Point2D position = flyingObject.Position;
            Size size = flyingObject.Size;

            var bullets = GameController.GetInstance().FlyingObjects
                .Where(x => x is Bullet && (x as Bullet).HorizontalSpeed > 0)
                .Where(bullet => bullet.Position.X - bullet.Size.Width / 2 < position.X + size.Width / 2)
                .OrderByDescending(x => x.Position.X);
            var bulletToDodge = bullets.FirstOrDefault(bullet =>
                bullet.Position.Y + Bullet.Size.Height / 2 > position.Y - size.Height / 2
                && bullet.Position.Y - Bullet.Size.Height / 2 < position.Y + size.Height / 2);
            if (bulletToDodge != null)
            {
                if (position.Y > bulletToDodge.Position.Y)
                    flyingObject.VerticalSpeed = horizontalSpeed;
                else
                    flyingObject.VerticalSpeed = -horizontalSpeed;
            }
            else
                flyingObject.VerticalSpeed = 0;
        }
    }
}
