using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace Ball_Breaker
{
    public partial class Form1 : Form
    {
        public const int LineLength = 360;
        public const int Offset = 10;
        public const int BallSize = 25;

        private Game game = new Game();

        public Form1()
        {
            InitializeComponent();
            SetParameters();
            GameBoard.Refresh();
            game.Start();
        }

        private void SetParameters()
        {
            const int width = 600;
            const int height = 600;
            GameBoard.Size = new Size(width, height);
            //GameBoard.Width = width;
            //GameBoard.Height = height;
        }

        private void GameBoard_Paint(object sender, PaintEventArgs e)
        {
            //DrawLines(e.Graphics);
            DrawBalls(e.Graphics);
            if(ballX > -1 && ballX < Game.NumberOfBalls && ballY > -1 && ballY < Game.NumberOfBalls)
                Outline(e.Graphics);
        }

        //private void DrawLines(Graphics graphics)
        //{
        //    Pen linePen = Pens.Black;
        //    for (int i = 0; i < 13; i++)
        //    {
        //        graphics.DrawLine(linePen, 0, BallSize * i + 3 * i, LineLength, BallSize * i + 3 * i);
        //        graphics.DrawLine(linePen, BallSize * i + 3 * i, 0, BallSize * i + 3 * i, LineLength);
        //    }
        //}

        private void DrawBalls(Graphics graphics)
        {
            foreach (var ball in game.GetBallsGroup().GetBalls())
            {
                ball.Draw(graphics);
            }
            GameBoard.Refresh();
        }

        bool[,] equalBalls = new bool[Game.NumberOfBalls, Game.NumberOfBalls];

        private int ballX;
        private int ballY;
        private void GameBoard_MouseMove(object sender, MouseEventArgs e)
        {
            int k = 0;
            ballX = (e.Location.X - Offset) / (BallSize + 3);
            ballY = (e.Location.Y - Offset) / (BallSize + 3);
            equalBalls = game.GetBallsGroup().EqualBalls(ballX, ballY);
            Console.WriteLine("Cursor in on {0},{1} ball", ballY, ballX);
        }

        private Pen linePen = Pens.Black;
        private void Outline(Graphics graphics)
        {
            for (int y = 0; y < Game.NumberOfBalls; y++)
            {
                for (int x = 0; x < Game.NumberOfBalls; x++)
                {
                    if (equalBalls[y, x])
                    {
                        if (y - 1 >= 0 && !equalBalls[y - 1, x] || y == 0)
                            graphics.DrawLine(linePen, Offset + (BallSize + 3) * x, Offset + (BallSize + 3) * y,
                                Offset + (BallSize + 3) * (x + 1) - 3, Offset + (BallSize + 3) * y);
                        if (y + 1 < Game.NumberOfBalls && !equalBalls[y + 1, x] || y == Game.NumberOfBalls - 1)
                            graphics.DrawLine(linePen, Offset + (BallSize + 3) * x,
                                Offset + (BallSize + 3) * (y + 1) - 3, Offset + (BallSize + 3) * (x + 1) - 3,
                                Offset + (BallSize + 3) * (y + 1) - 3);
                        if (x - 1 >= 0 && !equalBalls[y, x - 1] || x == 0)
                            graphics.DrawLine(linePen, Offset + (BallSize + 3) * x, Offset + (BallSize + 3) * y,
                                Offset + (BallSize + 3) * x, Offset + (BallSize + 3) * (y + 1) - 3);
                        if (x + 1 < Game.NumberOfBalls && !equalBalls[y, x + 1] || x == Game.NumberOfBalls - 1)
                            graphics.DrawLine(linePen, Offset + (BallSize + 3) * (x + 1) - 3,
                                Offset + (BallSize + 3) * y, Offset + (BallSize + 3) * (x + 1) - 3,
                                Offset + (BallSize + 3) * (y + 1) - 3);
                    }
                }
            }
            
        }
    }
}
