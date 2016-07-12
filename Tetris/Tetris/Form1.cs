using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        enum CellState
        {
            Empty,
            Filled
        }

        public const int CellSize = 30;
        public const int CellsXMax = 10;
        public const int CellsYMax = 20;

        private CellState[,] board = new CellState[CellsYMax, CellsXMax];

        private Random random = new Random();

        private Figure figure;
        private Figure nextFigure;
        private Figure checkedFigure;

        private int score;

        private Direction movingDirection;

        private bool keyIsPressed;

        private bool canMoveLeft;
        private bool canMoveRight;

        public Form1()
        {
            InitializeComponent();
            timer2.Interval = 100;
            timer2.Start();
            SetSizes();
            Restart();
            BoardPictureBox.Refresh();
        }

        private void Restart()
        {
            timer1.Stop();
            timer1.Interval = 1000;
            score = 0;
            ScoreLabel.Text = "Score: " + score;
            for (int i = 0; i < CellsYMax; i++)
            {
                for (int j = 0; j < CellsXMax; j++)
                    board[i,j] = CellState.Empty;
            }
            figure = CreateNewFigure(new Point(CellsXMax / 2, 1), (FigureType)random.Next(0,8));
            nextFigure = CreateNewFigure(new Point(1, 1), (FigureType)random.Next(0, 8));
            movingDirection = Direction.None;
            timer1.Start();
        }

        private void SetSizes()
        {
            const int startingX = 10;
            const int startingY = 10;
            const int indent = 20; //Отступ
            BoardPictureBox.Location = new Point(startingX, startingY);
            BoardPictureBox.Size = new Size(CellSize * CellsXMax + 1, CellSize * CellsYMax + 1);
            NextFigurePictureBox.Location = new Point(BoardPictureBox.Width + startingX + indent, startingY);
            NextFigurePictureBox.Size = new Size(CellSize * 4, CellSize * 4);
            ScoreLabel.Location = new Point(BoardPictureBox.Width + startingX + indent, startingY + NextFigurePictureBox.Height + indent);
            int clientWidth = BoardPictureBox.Width + NextFigurePictureBox.Width + 2 * indent;
            int clientHeight = BoardPictureBox.Height + indent;
            ClientSize = new Size(clientWidth, clientHeight);
        }

        private void DrawLines(Graphics graphics)
        {
            Pen linePen = new Pen(Color.Black);
            linePen.Width = 1;
            for (int i = 0; i <= CellsYMax; i++)
            {
                 graphics.DrawLine(linePen, 0, i * CellSize, CellsXMax * CellSize, i * CellSize);    
                 graphics.DrawLine(linePen, i * CellSize, 0, i * CellSize, CellsYMax * CellSize);    
            }
        }

        private void DrawFigure(Graphics graphics)
        {
            Brush figureBrush = Brushes.Blue;
            foreach (Point position in figure.GetAbsoluteCoordinates())
            {
                int xcoord = position.X * CellSize;
                int ycoord = position.Y * CellSize;
                graphics.FillRectangle(figureBrush, xcoord + 1, ycoord + 1, CellSize - 1, CellSize - 1);
            }
            for (int i = 0; i < CellsYMax; i++)
            {
                for (int j = 0; j < CellsXMax; j++)
                {
                    if(board[i,j] == CellState.Filled)
                        graphics.FillRectangle(figureBrush, j * CellSize + 1, i * CellSize + 1, CellSize - 1, CellSize - 1);
                }
            }
        }

        private void BoardPictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawLines(e.Graphics);
            DrawFigure(e.Graphics);
        }

        private Figure CreateNewFigure(Point centerPosition, FigureType figureType)
        {
             return new Figure(figureType, centerPosition);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckFallingDown())
                figure.FallDown();
            else
            {
                foreach (Point position in figure.GetAbsoluteCoordinates())
                {
                    board[position.Y, position.X] = CellState.Filled;
                }
                CheckFullLines();
                ScoreLabel.Text = "Score: " + score;
                figure = CreateNewFigure(new Point(CellsXMax / 2, 1), nextFigure.type);
                nextFigure = CreateNewFigure(new Point(1, 1), (FigureType)random.Next(0,8));
                NextFigurePictureBox.Refresh();
                if (CheckLostGame())
                {
                    timer1.Stop();
                    MessageBox.Show("You lost! Try Again!");
                    Restart();
                }
            }
            BoardPictureBox.Refresh();
        }

        private bool CheckFallingDown()
        {
            foreach (Point position in figure.GetAbsoluteCoordinates())
            {
                if (position.Y + 1 >= CellsYMax || board[position.Y + 1, position.X] == CellState.Filled)
                    return false;
            }
            return true;
        }

        private bool CheckLostGame()
        {
            return figure.GetAbsoluteCoordinates().Any(position => board[position.Y, position.X] == CellState.Filled);
        }

        private void CheckFullLines()
        {
            int linesDeleted = 0;
            for (int i = 0; i < CellsYMax; i++)
            {
                int count = 0;
                for (int j = 0; j < CellsXMax; j++)
                {
                    if (board[i, j] == CellState.Filled)
                        count++;
                    else
                        break;
                }
                if (count == CellsXMax)
                {
                    DeleteLine(i);
                    linesDeleted++;
                }
            }
            switch (linesDeleted)
            {
                case 1:
                    score += 100;
                    break;
                case 2:
                    score += 300;
                    break;
                case 3:
                    score += 700;
                    break;
                case 4:
                    score += 1500;
                    break;
            }
        }

        private void DeleteLine(int index)
        {
            for (int i = index; i > 0; i--)
            {
                for (int j = 0; j < CellsXMax; j++)
                {
                     board[i, j] = board[i - 1, j];
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) // todo: fix keys pressing, make them work properly
        {
            if (e.KeyCode == Keys.Left)
                movingDirection = Direction.Left;
            if (e.KeyCode == Keys.Right)
                movingDirection = Direction.Right;
            if (e.KeyCode == Keys.Down)
                movingDirection = Direction.Down;
            if (e.KeyCode == Keys.Up)
            {
                if (CheckRotation())
                {
                    figure.Rotate();
                }
            }
            if(e.KeyCode == Keys.Space)
                while(CheckFallingDown())
                    figure.FallDown();
                    
            keyIsPressed = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Down)
            //    timer1.Interval = 1000;
            movingDirection = Direction.None;
            keyIsPressed = false;
        }

        private void timer2_Tick(object sender, EventArgs e) //todo: Исправить дублирование
        {
            canMoveLeft = true;
            canMoveRight = true;
            if (movingDirection != Direction.None && keyIsPressed)
            {
                if (movingDirection == Direction.Left)
                {
                    foreach (Point position in figure.GetAbsoluteCoordinates())
                    {
                        if ((position.X - 1 < 0) || (board[position.Y, position.X - 1] == CellState.Filled))
                        {
                            canMoveLeft = false;
                                break;
                        }
                    }
                    if(canMoveLeft)
                        figure.Move(movingDirection);
                }

                if (movingDirection == Direction.Right)
                {
                    foreach (Point position in figure.GetAbsoluteCoordinates())
                    {
                        if ((position.X + 1 > CellsXMax - 1) || (board[position.Y, position.X + 1] == CellState.Filled))
                        {
                            canMoveRight = false;
                                break;
                        }
                    }
                    if (canMoveRight)
                        figure.Move(movingDirection);
                }

                if (movingDirection == Direction.Down && CheckFallingDown())
                    figure.FallDown();
            }
            BoardPictureBox.Refresh();
        }

        private void NextFigurePictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawNextFigure(e.Graphics);
        }

        private void DrawNextFigure(Graphics graphics)
        {
            Brush figureBrush = Brushes.Blue;
            foreach (Point position in nextFigure.GetAbsoluteCoordinates())
            {
                graphics.FillRectangle(figureBrush, position.X * CellSize + 1, position.Y * CellSize + 1, CellSize - 1, CellSize - 1);
            }
        }

        private bool CheckRotation()
        {
            if (figure.type != FigureType.O && figure.type != FigureType.Dot)
            {
                checkedFigure = new Figure(figure.type, figure.Position);
                checkedFigure.SetOffsetsToFigure(figure);
                checkedFigure.Rotate();
                foreach (Point check in checkedFigure.GetAbsoluteCoordinates())
                {
                    if (check.Y < 0 ||
                        check.Y >= CellsYMax ||
                        check.X < 0 ||
                        check.X >= CellsXMax ||
                        board[check.Y, check.X] == CellState.Filled)
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }
    }
}
