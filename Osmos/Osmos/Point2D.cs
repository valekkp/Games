using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osmos
{
    public struct Point2D
    {
        public float X;
        public float Y;

        public Point2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static float DistanceBetween(Point2D source, Point2D target)
        {
            return (float) Math.Sqrt(Math.Pow(source.X - target.X, 2) + Math.Pow(source.Y - target.Y, 2));
        }

        public static Point2D operator +(Point2D source, Point2D target)
        {
            return new Point2D(source.X + target.X, source.Y + target.Y);
        }

        public static Point2D operator -(Point2D source, Point2D target)
        {
            return new Point2D(source.X - target.X, source.Y - target.Y);
        }

        public static Point2D operator *(Point2D source, float multiplier)
        {
            return new Point2D(source.X * multiplier, source.Y * multiplier);
        }
    }
}
