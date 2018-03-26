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
        private readonly FlyingObject source;
        private readonly int fieldOfView = 100;

        private IEnumerable<FlyingObject> objectsToDodge;

        public MovingAndDodgingBehavior(FlyingObject source, IEnumerable<FlyingObject> objectsToDodge)
        {
            this.source = source;
            this.objectsToDodge = objectsToDodge;
        }

        public void Move()
        {
            Dodge();
            int horizontalSpeed = source.HorizontalSpeed;
            int verticalSpeed = source.VerticalSpeed;
            Size size = source.Size;

            source.Position.X += horizontalSpeed;
            source.Position.Y += verticalSpeed;

            if (source.Position.Y - size.Height / 2 < 0)
                source.Position.Y = size.Height / 2;
            if (source.Position.Y + size.Height / 2 > GameWindow.GameFieldSize.Height)
                source.Position.Y = GameWindow.GameFieldSize.Height - size.Height / 2;
        }

        private void Dodge()
        {
            int verticalSpeed = FighterShip.Speed;
            Point2D sourcePosition = source.Position;

            FlyingObject bulletToDodge = BulletToDodge();
            if (bulletToDodge != null)
            {
                if (sourcePosition.Y > bulletToDodge.Position.Y)
                    source.VerticalSpeed = verticalSpeed;
                else
                    source.VerticalSpeed = -verticalSpeed;
            }
            else
                source.VerticalSpeed = 0;
        }

        private FlyingObject BulletToDodge()
        {
            Size size = source.Size;
            Point2D sourcePosition = source.Position;
            return objectsToDodge
                .OfType<Bullet>()
                .Where(bullet => bullet.HorizontalSpeed > 0
                                 && bullet.Position.X - Bullet.Size.Width / 2 < sourcePosition.X + size.Width / 2)
                .OrderByDescending(x => x.Position.X)
                .FirstOrDefault(bullet =>
                    bullet.Position.Y + Bullet.Size.Height / 2 > sourcePosition.Y - size.Height / 2
                    && bullet.Position.Y - Bullet.Size.Height / 2 < sourcePosition.Y + size.Height / 2
                    && IntersectionController.DistanceBetween(bullet, source) < fieldOfView);
        }
    }
}
