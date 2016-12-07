using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using static System.Math;

namespace Lock_Picking
{
    class Screwdriver
    {
        public double Angle;
        private Point startingLocation = new Point(Form1.LockOffset + Form1.BoardLength / 4, Form1.LockOffset + Form1.BoardLength / 4);
        private PointF endingLocation;
        private const int Length = Form1.BoardLength / 4;
        public bool cantMove;
        public Screwdriver()
        {
            Angle = 90;
            endingLocation = new Point(Form1.LockOffset + Form1.BoardLength / 4, Form1.LockOffset + Form1.BoardLength / 2);
        }

        public void Draw(Graphics graphics)
        {
            Pen screwdriverPen = new Pen(Color.Brown, 3);
            graphics.DrawLine(screwdriverPen, startingLocation, endingLocation);
        }

        public void Move(Lockpick lockpick, Lock currentLock)
        {
            bool cantMove = false;
            if (lockpick.Angle > currentLock.OuterSectorMin && lockpick.Angle < currentLock.OuterSectorMax)
            {
                if (lockpick.Angle > currentLock.InnerSectorMin && lockpick.Angle < currentLock.InnerSectorMax)
                {
                    if (Angle < 180)
                        Angle++;
                    else
                        cantMove = true;
                }
                else
                {
                    if (Angle <
                        (180 - Max(lockpick.Angle - currentLock.InnerSectorMax, currentLock.InnerSectorMin - lockpick.Angle)))
                    {
                        Angle++;
                    }
                    else
                        cantMove = true;
                }
            }
            else
            {
                cantMove = true;
            }
            if (cantMove)
            {
                lockpick.Durability--;
                lockpick.CurrentColor = Color.DarkRed;
            }
            else
            {
                lockpick.CurrentColor = Color.Green;
            }
            endingLocation = new PointF((float)(startingLocation.X + Length * Cos(Angle*PI/180)), (float)(startingLocation.Y + Length * Sin(Angle*PI/180)));
        } 

        public void Return()
        {
            if (Angle > 90)
            {
                Angle--;
                endingLocation = new Point((int)(startingLocation.X + Length * Cos(Angle*PI/180)), (int)(startingLocation.Y + Length * Sin(Angle*PI/180)));
            }
        }
    }
}
