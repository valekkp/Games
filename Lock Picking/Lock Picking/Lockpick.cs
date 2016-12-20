using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace Lock_Picking
{
    class Lockpick
    {
        public int Durability;
        private const int Length = Form1.BoardLength/2;
        private Point startingLocation = new Point(Form1.LockOffset + Form1.BoardLength / 4, Form1.LockOffset + Form1.BoardLength / 4);
        private PointF endingLocation;

        public float Angle;
        public Color CurrentColor = Color.Green;

        public Lockpick()
        {
            endingLocation = new Point(Form1.LockOffset + Form1.BoardLength / 4, Form1.LockOffset);
            Durability = 100;
        }

        public void Move(Point mouseLocation)
        {
            float sin;
            float cos;
            if (mouseLocation.Y < Form1.LockOffset + Form1.BoardLength/4)
            {
                cos = (float)((mouseLocation.X - startingLocation.X)/
                      Sqrt(Pow((startingLocation.X - mouseLocation.X), 2) +
                           Pow((startingLocation.Y - mouseLocation.Y), 2)));
                sin = (float)((mouseLocation.Y - startingLocation.Y)/
                             Sqrt(Pow((startingLocation.X - mouseLocation.X), 2) +
                                  Pow((startingLocation.Y - mouseLocation.Y), 2)));
                float lockpickX = startingLocation.X + Form1.BoardLength/4*cos;
                float lockpickY = startingLocation.Y + Form1.BoardLength/4*sin;
                endingLocation = new PointF(lockpickX, lockpickY);
                Angle = (float)(Acos(cos)*180/PI);
            }
        }

        public void Draw(Graphics graphics)
        {
            Pen lockpickPen = new Pen(CurrentColor, 2);
            graphics.DrawLine(lockpickPen, startingLocation, endingLocation);
        }
    }
}
