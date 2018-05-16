using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Osmos
{
    public static class IntersectionController
    {
        public static void IntersectCells(Cell source, Cell target)
        {
            Cell largerCell = source.Mass >= target.Mass ? source : target;
            Cell smallerCell = source.Mass <= target.Mass ? source : target;

            if (DoCellsIntersect(source, target))
            {
                var massAmount = CountMassAmount(largerCell, smallerCell);

                if (smallerCell.Mass <= massAmount)
                {
                    largerCell.Mass += smallerCell.Mass;
                    smallerCell.Mass = 0;
                }
                else
                {
                    largerCell.Mass += massAmount;
                    smallerCell.Mass -= massAmount;
                }
            }
        }

        private static bool DoCellsIntersect(Cell source, Cell target)
        {
            return Point2D.DistanceBetween(source.Position, target.Position) < source.Radius + target.Radius;
        }

        private static float CountMassAmount(Cell source, Cell target)
        {
            var r1 = Math.Max(source.Radius, target.Radius);
            var r2 = Math.Min(source.Radius, target.Radius);
            var distance = Point2D.DistanceBetween(source.Position, target.Position);
            //var mass1 = Math.PI * Math.Pow(r1 + (2 * r1 * distance - distance * distance) / (2 * r1 - 2 * distance + 2 * r2) -
            //            distance, 2) - Math.PI * r1 * r1;

            //var x = (2 * r1 * distance - distance * distance) / (2 * r1 - 2 * distance + 2 * r2);

            //var mass2 = Math.PI * r2 * r2 - Math.PI * Math.Pow(r2 - (2 * r1 * distance - distance * distance) / (2 * r1 - 2 * distance + 2 * r2), 2);

            var y = (r1 * r1 - r2 * r2 - distance * distance + 2 * distance * r2) / (2 * distance);
            //var x = distance - r1 - r2 + y;
            //var mass1 = Math.PI * (Math.Pow(r1 + x, 2) - Math.Pow(r1, 2));

            if (y > r2)
                return target.Mass;

            var mass2 = Math.PI * (Math.Pow(r2, 2) - Math.Pow(r2 - y, 2));

            return (float)mass2;
        }
    }
}
