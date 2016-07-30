using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock_Picking
{
    class Lock
    {
        public double MinAngle;
        public double MaxAngle;

        public Lock(double minAngle, double maxAngle)
        {
            this.MinAngle = minAngle;
            this.MaxAngle = maxAngle;
        }

    }
}
