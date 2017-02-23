using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Langton_s_Ant
{
    public partial class GamingForm : Form
    {
        private const int CellSize = 10;

        Color[,] brushes = new Color[600 / CellSize, 800 / CellSize];

        Point ant = new Point(300 / CellSize, 400 / CellSize);

        enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        enum Color
        {
            White,
            Red,
            Green,
            Blue
        }

        private Direction direction = Direction.Up;

        private Dictionary<Brush, string> rules = new Dictionary<Brush, string>()
        {
            { Brushes.White, "left" },
            { Brushes.Red, "right"}
        };

        public GamingForm()
        {
            InitializeComponent();
            GamingBoard.Size = new Size(800, 600);
            GamingBoard.Location = new Point(0, 0);
            Size = new Size(850, 650);
            Start();
        }

        public void Start()
        {
            timer1.Interval = 1;
            timer1.Start();
            for (int i = 0; i < 600 / CellSize; i++)
            {
                for (int j = 0; j < 800 / CellSize; j++)
                {
                    brushes[i,j] = Color.White;
                }
            }
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            switch (brushes[ant.Y, ant.X])
            {
                case Color.White:
                    direction = (Direction) Math.Abs(((int) direction + 1) % 4);
                    break;
                case Color.Red:
                    direction = (Direction) (((int)direction - 1 > -1 ? (int)direction - 1 : 3) % 4);
                    break;
                case Color.Green:
                    direction = (Direction)Math.Abs(((int)direction + 1) % 4);
                    break;
                case Color.Blue:
                    direction = (Direction)(((int)direction - 1 > -1 ? (int)direction - 1 : 3) % 4);
                    break;
            }

            brushes[ant.Y, ant.X] = (Color) ((int) (brushes[ant.Y, ant.X] + 1) % 4);

            GamingBoard.Refresh();

            GamingBoard.Update();
            switch (direction)
            {
                case Direction.Up:
                    ant.Y--;
                    break;
                case Direction.Right:
                    ant.X++;
                    break;
                case Direction.Down:
                    ant.Y++;
                    break;
                case Direction.Left:
                    ant.X--;
                    break;
            }
        }

        private void GamingBoard_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 600 / CellSize; i++)
            {
                for (int j = 0; j < 800 / CellSize; j++)
                {
                    Brush brush = Brushes.White;
                    switch (brushes[i, j])
                    {
                        case Color.White:
                            brush = Brushes.White;
                            break;
                        case Color.Red:
                            brush = Brushes.Red;
                            break;
                        case Color.Green:
                            brush = Brushes.Green;
                            break;
                        case Color.Blue:
                            brush = Brushes.Blue;
                            break;
                    }

                    e.Graphics.FillRectangle(brush, i * CellSize, j * CellSize, CellSize, CellSize);
                }
            }
        }
    }
}
