using System;
using System.Collections;
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
        public Point Position;

        public FigureType type;

        private int rotateNumber;

        private Point[] offsets;

        public Figure(FigureType figureType, Point centerPosition)
        {
            Position = centerPosition;
            type = figureType;
            switch (type)
            {
                case FigureType.Dot:
                    offsets = new[] {new Point(0, 0)};
                    break;
                case FigureType.O:
                    offsets = new[] { new Point(-1, 0),
                                           new Point(0, 0),
                                           new Point(-1, 1),
                                           new Point(0, 1) };
                    break;
                case FigureType.Z:
                    offsets = new[] { new Point(-1, -1),
                                           new Point(0, -1),
                                           new Point(0, 0),
                                           new Point(1, 0) };
                    break;
                case FigureType.S:
                    offsets = new[] { new Point(1, -1),
                                           new Point(0, -1),
                                           new Point(0, 0),
                                           new Point(-1, 0) };
                    break;
                case FigureType.T:
                    offsets = new[] { new Point(-1, 0),
                                           new Point(0, 0),
                                           new Point(1, 0),
                                           new Point(0, 1) };
                    break;
                case FigureType.L:
                    offsets = new[] { new Point(0, -1),
                                           new Point(0, 0),
                                           new Point(0, 1),
                                           new Point(1, 1) };
                    break;
                case FigureType.J:
                    offsets = new[] { new Point(0, -1),
                                           new Point(0, 0),
                                           new Point(0, 1),
                                           new Point(-1, 1) };
                    break;
                case FigureType.I:
                    offsets = new[] { new Point(-1, 0),
                                           new Point(0, 0),
                                           new Point(1, 0),
                                           new Point(2, 0) };
                    break;
            }
        }

        public void FallDown()
        {
            Position.Y++;
        }

        public void Move(Direction direction)
        {
            int variableX = 0;
            int variableY = 0;

            if (direction == Direction.Left)
            {
                variableX = -1;
                variableY = 0;
            }
            if (direction == Direction.Right)
            {
                variableX = 1;
                variableY = 0;
            }
            if (direction == Direction.Down)
            {
                variableX = 0;
                variableY = 1;
            }
            if (!(variableX == 0 && variableY == 0))
            {
                Position.X += variableX;
                Position.Y += variableY;
            }
        }

        public Point[] GetAbsoluteCoordinates()
        {
            return offsets
                .Select(o => new Point(Position.X + o.X, Position.Y + o.Y))
                .ToArray();
        }

        public void SetOffsetsToFigure(Figure selectedFigure)
        {
            for (int i = 0; i < offsets.Length; i++)
            {
                this.offsets[i].X = selectedFigure.offsets[i].X;
                this.offsets[i].Y = selectedFigure.offsets[i].Y;
            }
        }

        public void Rotate()
        {
            if (type != FigureType.O && type != FigureType.Dot)
            {
                int temp;
                if(type == FigureType.J || type == FigureType.L || type == FigureType.T)
                    for (int i = 0; i < offsets.Length; i++)
                    {
                        temp = offsets[i].X;
                        offsets[i].X = -offsets[i].Y;
                        offsets[i].Y = temp;
                    }
                if (type == FigureType.S || type == FigureType.Z)
                {
                    rotateNumber++;
                    if (rotateNumber % 2 == 1)
                    {
                        for (int i = 0; i < offsets.Length; i++)
                        {
                            temp = offsets[i].X;
                            offsets[i].X = -offsets[i].Y;
                            offsets[i].Y = temp;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < offsets.Length; i++)
                        {
                            temp = offsets[i].X;
                            offsets[i].X = offsets[i].Y;
                            offsets[i].Y = -temp;
                        }
                    }
                }
                if (type == FigureType.I)
                {
                    for (int i = 0; i < offsets.Length; i++)
                    {
                        temp = offsets[i].X;
                        offsets[i].X = offsets[i].Y;
                        offsets[i].Y = temp;
                    }
                }
            }
        }

        public Point[] GetOffsets()
        {
            return offsets;
        }
    }
}
