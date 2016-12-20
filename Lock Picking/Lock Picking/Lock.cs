using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock_Picking
{
    class Lock
    {
        public enum Type
        {
            Easy,
            Normal,
            Hard
        }

        Random random = new Random();
        public int InnerSectorMin;
        public int InnerSectorMax;
        public int OuterSectorMin;
        public int OuterSectorMax;
        public Type Difficulty = Type.Easy;

        public Lock()
        {
            InnerSectorMin = random.Next(20, 155);
            InnerSectorMax = random.Next(InnerSectorMin, 160);
            OuterSectorMin = InnerSectorMin - 20;
            OuterSectorMax = InnerSectorMax + 20;
            if(InnerSectorMax - InnerSectorMin < 25)
                Difficulty = Type.Normal;
            if (InnerSectorMax - InnerSectorMin < 10)
                Difficulty = Type.Hard;
        }

        public void Draw(Graphics graphics)
        {
            Pen lockPen = new Pen(Color.Brown);
            graphics.DrawEllipse(lockPen, Form1.LockOffset, Form1.LockOffset, Form1.BoardLength / 2, Form1.BoardLength / 2);
            Pen linePen = new Pen(Color.Brown);
            graphics.DrawLine(linePen, Form1.LockOffset, Form1.LockOffset + Form1.BoardLength / 4, Form1.BoardLength / 2 + Form1.LockOffset, Form1.LockOffset + Form1.BoardLength / 4);
        }
    }
}
