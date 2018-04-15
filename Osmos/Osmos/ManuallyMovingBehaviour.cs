using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Osmos
{
    class ManuallyMovingBehaviour : IMovable
    {
        private readonly Cell source;
        public ManuallyMovingBehaviour(Cell source)
        {
            this.source = source;
        }

        public void Move()
        {
            Point2D clickPosition = GameWindow.GameFieldCursorPosition;
            var distance = Point2D.DistanceBetween(source.Position, clickPosition);

            var xDifference = clickPosition.X - source.Position.X;
            var yDifference = clickPosition.Y - source.Position.Y;

            var ratio = Math.Min(xDifference, yDifference) / Math.Max(xDifference, yDifference);

            float lambda = (float)(distance / Math.Pow(distance, 2));
            var xOnCircle = (source.Position.X + lambda * clickPosition.X) / (1 + lambda);
            var yOnCircle = (source.Position.Y + lambda * clickPosition.Y) / (1 + lambda);
            var xSpeed = source.Position.X - xOnCircle;
            var ySpeed = source.Position.Y - yOnCircle;

            source.MovementVector.X += xSpeed;
            source.MovementVector.Y += ySpeed;
        }
    }
}
