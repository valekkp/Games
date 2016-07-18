using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock_Picking
{
    class Game
    {
        private const int PenWidth = 1;
        private static Pen lockPen = new Pen(Color.Black, PenWidth);

        private Lockpick lockpick = new Lockpick();

        public void DrawElements(Graphics graphics)
        {
            graphics.DrawEllipse(lockPen, 50, 50, 200, 200);

        }
    }
}
