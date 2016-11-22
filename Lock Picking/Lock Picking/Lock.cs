using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Lock_Picking
{
    class Lock
    {
        public double MinOpeningAngle;
        public double MaxOpeningAngle;

        public double MinAngle;
        public double MaxAngle;

        private Random random = new Random();

        public Lock()
        {
            while(MinAngle - MaxAngle < 10*PI/180)
            {
                MinAngle = random.NextDouble() * -PI;
                MaxAngle = MinOpeningAngle + random.NextDouble() * (-PI - MinOpeningAngle);
            }
            MinOpeningAngle = MinAngle - (MinAngle - MaxAngle) * 0.4;
            MaxOpeningAngle = MaxAngle + (MinAngle - MaxAngle) * 0.4;
        }

    }
}
