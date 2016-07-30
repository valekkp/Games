using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace Lock_Picking
{
    class Lockpick
    {
        public static int Length = Game.LockSize/2 + 50;

        public static PointF StartingLocation;

        private PointF cursorLastPosition;

        public int Durability;
        public float angle;

        public PointF EndingLocation;

        public Lockpick()
        {
            Durability = 100;
            angle = (float) (-90*PI/180);
            StartingLocation.X = Game.CenterPosition.X;
            StartingLocation.Y = Game.CenterPosition.Y;
            EndingLocation.X = StartingLocation.X + (float)Cos(angle) * Length;
            EndingLocation.Y = StartingLocation.Y + (float)Sin(angle) * Length;
            cursorLastPosition.X = StartingLocation.X;
            cursorLastPosition.Y = StartingLocation.Y;
            //Cursor.Position = new Point((int)cursorLastPosition.X + Form.ActiveForm.Location.X, (int)cursorLastPosition.Y + Form.ActiveForm.Location.X);
        }

        public void Move(int x, int y)
        {
            if (x > cursorLastPosition.X && angle < 0)
            {
                angle += (float)PI/180;
            }
            if (x < cursorLastPosition.X && angle > -PI + PI/180)
            {
                angle -= (float)PI/180;
            }
            EndingLocation.X = StartingLocation.X + (float)Cos(angle) * Length;
            EndingLocation.Y = StartingLocation.Y + (float)Sin(angle) * Length;
            cursorLastPosition.X = x;
            cursorLastPosition.Y = y;
        }
    }
}
