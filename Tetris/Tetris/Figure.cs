using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class Figure
    {
        //private int coordinateX;
        //private int coordinateY;

        private List<int> coordinatesX = new List<int>();
        private List<int> coordinatesY = new List<int>();

        private static int CellsXMax = 10;
        private static int CellsYMax = 20;

        private static int XCenter = CellsXMax / 2;
        private FigureType figureType;

        public Figure(FigureType type)
        {
            //Console.WriteLine("Test");
            switch (type)
            {
                case FigureType.dot:
                    Console.WriteLine("dot: ");
                    coordinatesX.Add(CellsXMax / 2);
                    coordinatesY.Add(0);
                    break;
                case FigureType.O:
                    Console.WriteLine("O: ");
                    coordinatesX.AddRange(new [] { XCenter - 1, XCenter, XCenter - 1, XCenter });
                    coordinatesY.AddRange(new [] { 0, 0, 1, 1 });
                    break;
                case FigureType.Z:
                    Console.WriteLine("Z: ");
                    coordinatesX.AddRange(new [] { XCenter - 1, XCenter, XCenter, XCenter + 1 });
                    coordinatesY.AddRange(new [] { 0, 0, 1, 1 });
                    break;
                case FigureType.S:
                    Console.WriteLine("S: ");
                    coordinatesX.AddRange(new[] { XCenter, XCenter + 1, XCenter - 1, XCenter });
                    coordinatesY.AddRange(new[] { 0, 0, 1, 1 });
                    break;
                case FigureType.T:
                    Console.WriteLine("T: ");
                    coordinatesX.AddRange(new[] { XCenter - 1, XCenter, XCenter + 1, XCenter });
                    coordinatesY.AddRange(new[] { 0, 0, 0, 1 });
                    break;
                case FigureType.L:
                    Console.WriteLine("L: ");
                    coordinatesX.AddRange(new[] { XCenter, XCenter, XCenter, XCenter + 1 });
                    coordinatesY.AddRange(new[] { 0, 1, 2, 2 });
                    break;
                case FigureType.J:
                    Console.WriteLine("J: ");
                    coordinatesX.AddRange(new[] { XCenter, XCenter, XCenter - 1, XCenter });
                    coordinatesY.AddRange(new[] { 0, 1, 2, 2 });
                    break;
                case FigureType.I:
                    Console.WriteLine("I: ");
                    coordinatesX.AddRange(new[] { XCenter, XCenter, XCenter, XCenter });
                    coordinatesY.AddRange(new[] { 0, 1, 2, 3 });
                    break;
            }
            Console.WriteLine("X: " + coordinatesX.Count);
            Console.WriteLine("Y: " + coordinatesY.Count);
        }

        public void FallingDown()
        {
            for(int i = 0; i < GetPartsCount(); i++)
            {
                coordinatesY[i]++;
            }
        }

        public void Move(string direction)
        {
            int variableX = 0;
            int variableY = 0;

            if (direction == "Left")
            {
                variableX = -1;
                variableY = 0;
            }
            if (direction == "Right")
            {
                variableX = 1;
                variableY = 0;
            }
            if (direction == "Down")
            {
                variableX = 0;
                variableY = 1;
            }
            if(!(variableX == 0 && variableY == 0))
                for (int i = 0; i < GetPartsCount(); i++)
                    {
                        coordinatesX[i] += variableX;
                        coordinatesY[i] += variableY;
                    }
        }

        public void DeleteCoordinates()
        {
            coordinatesX.Clear();
            coordinatesY.Clear();
        }

        public Point GetPoint(int index)
        {
            return new Point(coordinatesX[index], coordinatesY[index]);
        }

        public int GetPartsCount()
        {
            return coordinatesX.Count;
        }

        public bool CheckMoving()
        {
            for (int i = 0; i < GetPartsCount(); i++)
            {
                if (!(GetPoint(i).X - 1 >= 0 &&
                      GetPoint(i).X + 1 <= CellsXMax &&
                      GetPoint(i).Y - 1 >= 0 &&
                      GetPoint(i).Y + 1 <= CellsYMax))
                    return false;
            }
            return true;
        }
    }
}
