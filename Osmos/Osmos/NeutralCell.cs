using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmos
{
    class NeutralCell : Cell
    {
        public NeutralCell(Point2D position, float mass, Point2D movementVector)
            : base(position, mass, movementVector)
        {

        }
    }
}
