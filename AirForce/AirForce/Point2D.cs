using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    public struct Point2D
    {
        public int X;
        public int Y;

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int DistanceTo(Point2D target)
        {
            return (int) Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2));
        }
    }
}
