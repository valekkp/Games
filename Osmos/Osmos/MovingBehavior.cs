using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmos
{
    class MovingBehavior : IMovable
    {
        private readonly Cell source;

        public MovingBehavior(Cell source)
        {
            this.source = source;
        }

        public void Move()
        {
            source.Position += source.MovementVector;
        }
    }
}
