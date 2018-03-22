using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirForce
{
    class MovingBehavior : IMovable
    {
        private readonly FlyingObject flyingObject;

        public MovingBehavior(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void Move()
        {
            flyingObject.Position.X += flyingObject.HorizontalSpeed;
            flyingObject.Position.Y += flyingObject.VerticalSpeed;
        }
    }
}
