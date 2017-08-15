using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ball_Breaker
{
    class BallsGroup
    {
        private Ball[,] balls = new Ball[Game.NumberOfBalls,Game.NumberOfBalls];

        public BallsGroup()
        {
            GenerateBalls();
        }

        private void GenerateBalls()
        {
            for (int i = 0; i < Game.NumberOfBalls; i++)
            {
                for (int j = 0; j < Game.NumberOfBalls; j++)
                {
                    balls[i, j] = new Ball(i, j);
                    balls[i, j].SetRandomColor();
                    Console.WriteLine("Ball {0},{1} was created using {2} color", i, j, balls[i, j].GetColor());
                }
            }
        }

        public void DrawBalls(Graphics graphics)
        {
            foreach (var ball in balls)
            {
                ball.Draw(graphics);
            }
        }

        public Ball[,] GetBalls()
        {
            return balls;
        }
    }
}
