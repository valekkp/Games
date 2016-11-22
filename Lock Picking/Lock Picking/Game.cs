using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace Lock_Picking
{
    class Game
    {
        private Random random = new Random();
        private const int PenWidth = 2;
        private const int ScrewdriverPenWidth = 2;
        private static Pen lockPen = new Pen(Color.Black, PenWidth);
        private static Pen lockpickPen = new Pen(Color.Gray, PenWidth);
        private static Pen scredriverPen = new Pen(Color.Gray, ScrewdriverPenWidth);

        public static Point CenterPosition = new Point(200, 200);

        public const int LockSize = 200;

        public int lockpicksUsed = 0;
        public int locksOpened = 0;

        private Lock currentLock;
        private Lockpick lockpick;
        private Screwdriver screwdriver;

        public void DrawElements(Graphics graphics)
        {
            graphics.DrawEllipse(lockPen, CenterPosition.X - LockSize/2, CenterPosition.Y - LockSize/2, LockSize, LockSize);

            graphics.DrawLine(lockpickPen, Lockpick.StartingLocation, lockpick.EndingLocation);

            graphics.DrawLine(scredriverPen, Screwdriver.StartingLocation, screwdriver.EndingLocation);
        }

        public void Start()
        {
            Cursor.Hide();
            //Cursor.Position = GameForm.CursorLocation;
            Console.WriteLine("Starting location X: " + Cursor.Position.X);
            Console.WriteLine("Starting location Y: " + Cursor.Position.Y);
            currentLock = new Lock();
            lockpick = new Lockpick();
            screwdriver = new Screwdriver();
        }

        public void MoveLockpick(int x, int y)
        {
            lockpick.Move(x, y);
        }

        public void BreakLockpick()
        {
            lockpick.Durability--;
            lockpickPen.Color = lockpickPen.Color == Color.Gray ? Color.Red : Color.Gray;
            if (lockpick.Durability == 0)
            {
                lockpicksUsed++;
                lockpick = new Lockpick();
                lockpickPen.Color = Color.Gray;
            }
        }

        public void MoveScrewdriver(bool isTurning)
        {
            if (screwdriver.lockOpened)
            {
                locksOpened++;
                Start();
            }
            if (isTurning)
            {
                if (lockpick.angle <= currentLock.MinOpeningAngle && lockpick.angle >= currentLock.MaxOpeningAngle)
                    screwdriver.Turn();
                else
                {
                    screwdriver.Return();
                    BreakLockpick();
                }
            }
            else
            {
                lockpickPen.Color = Color.Gray;
                screwdriver.Return();
            }
        }
    }
}
