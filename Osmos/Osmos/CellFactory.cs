using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmos
{
    public static class CellFactory
    {
        public static Cell Create(Point2D position, float mass, Point2D movementVector)
        {
            return new NeutralCell(position, mass, movementVector);
        }
    }
}
