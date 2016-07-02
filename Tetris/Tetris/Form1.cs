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

        private const int CellSize = 30;
        private const int CellsXMax = 10;
        private const int CellsYMax = 20;

        private CellState[,] board = new CellState[CellsYMax, CellsXMax];

        private Random random = new Random();

        private Figure figure;

        public int Score;

        public string Direction;

        private bool keyIsPressed;

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
            Score = 0;
            ScoreLabel.Text = "Score: " + Score;
            for (int i = 0; i < CellsYMax; i++)
            {
                for (int j = 0; j < CellsXMax; j++)
                    board[i,j] = CellState.Empty;
            }
            figure = CreateNewFigure();
            timer1.Start();
        }

        private void SetSizes()
        {
            int startingX = 10;
            int startingY = 10;
            int indent = 20; //Отступ
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
            for (int i = 0; i < figure.GetPartsCount(); i++)
            {
                int Xcoord = figure.GetPoint(i).X * CellSize;
                int Ycoord = figure.GetPoint(i).Y * CellSize;
                graphics.FillRectangle(figureBrush, Xcoord + 1, Ycoord + 1, CellSize - 1, CellSize - 1);
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

        private Figure CreateNewFigure()
        {
            return new Figure((FigureType)random.Next(0, 8));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckFallingDown())
                figure.FallingDown();

            else
            {
                for (int i = 0; i < figure.GetPartsCount(); i++)
                {
                    board[figure.GetPoint(i).Y, figure.GetPoint(i).X] = CellState.Filled;
                }
                CheckFullLines();
                ScoreLabel.Text = "Score: " + Score;
                figure = CreateNewFigure();
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
            for (int i = 0; i < figure.GetPartsCount(); i++)
            {
                if (figure.GetPoint(i).Y + 1 >= CellsYMax || board[figure.GetPoint(i).Y + 1, figure.GetPoint(i).X] == CellState.Filled)
                    return false;
            }
            return true;
        }

        private bool CheckLostGame()
        {
            for (int i = 0; i < figure.GetPartsCount(); i++)
                if (board[figure.GetPoint(i).Y, figure.GetPoint(i).X] == CellState.Filled)
                    return true;
            return false;
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
                    else break;
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
                    Score += 100;
                    break;
                case 2:
                    Score += 300;
                    break;
                case 3:
                    Score += 700;
                    break;
                case 4:
                    Score += 1500;
                    break;
            }
        }

        private void DeleteLine(int index)
        {
            for (int i = 0; i < CellsXMax; i++)
            {
                board[index, i] = CellState.Empty;
            }
            for (int i = index + 1; i < CellsYMax; i++)
            {
                for (int j = 0; j < CellsXMax; j++)
                {
                    board[i - 1, j] = CellState.Filled;
                    board[i, j] = CellState.Empty;
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                Direction = "Left";
            if (e.KeyCode == Keys.Right)
                Direction = "Right";
            if (e.KeyCode == Keys.Down)
                Direction = "Down";
            keyIsPressed = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Direction = "None";
            keyIsPressed = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (Direction != "None" && figure.CheckMoving() && keyIsPressed)
                figure.Move(Direction);
            BoardPictureBox.Refresh();
        }
    }
}
