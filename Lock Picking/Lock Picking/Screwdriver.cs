using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace Lock_Picking
{
    class Screwdriver
    {
        public static int Length = Game.LockSize / 2 + 50;

        public static PointF StartingLocation;
        public PointF EndingLocation;
        public bool lockOpened = false;

        public double angle = 90*PI/180;

        public Screwdriver()
        {
            StartingLocation.X = Game.CenterPosition.X;
            StartingLocation.Y = Game.CenterPosition.Y;
            EndingLocation.X = Game.CenterPosition.X;
            EndingLocation.Y = Game.CenterPosition.Y + Length;
        }

        public void Turn()
        {
            if (angle < PI - PI/180)
            {
                angle += PI/180;
            }
            else
            {
                lockOpened = true;
            }
            EndingLocation.X = StartingLocation.X + (float)Cos(angle) * Length;
            EndingLocation.Y = StartingLocation.Y + (float)Sin(angle) * Length;
        }

        public void Return()
        {
            if(angle > 90*PI/180)
                angle -= PI/180;
            EndingLocation.X = StartingLocation.X + (float)Cos(angle) * Length;
            EndingLocation.Y = StartingLocation.Y + (float)Sin(angle) * Length;
        }
    }
}
