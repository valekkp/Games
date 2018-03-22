using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce.Behaviors
{
    class MovingRandomlyBehavior : IMovable
    {
        private readonly FlyingObject flyingObject;
        private readonly Random random = new Random();

        public MovingRandomlyBehavior(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void Move()
        {
            var multiplier = random.Next(3) - 1;
            flyingObject.Position.X += flyingObject.HorizontalSpeed;
            flyingObject.Position.Y += flyingObject.VerticalSpeed * multiplier;
        }
    }
}
