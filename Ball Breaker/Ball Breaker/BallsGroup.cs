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

        public void GenerateBalls()
        {
            for (int i = 0; i < Game.NumberOfBalls; i++)
            {
                for (int j = 0; j < Game.NumberOfBalls; j++)
                {
                    balls[i, j] = new Ball(i, j);
                    balls[i, j].SetRandomColor();
                    //Console.WriteLine("Ball {0},{1} was created using {2} color", i, j, balls[i, j].GetColor());
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

        bool[,] equalBalls = new bool[Game.NumberOfBalls, Game.NumberOfBalls];
        bool[,] used = new bool[Game.NumberOfBalls, Game.NumberOfBalls];
        private Ball.Colors equalColor = Ball.Colors.Blue;
        private int equalNumber = 0;
        private void CountEqual(int x, int y)
        {
            if (!used[y, x] && balls[y, x].GetColor() == equalColor)
            {
                used[y, x] = true;
                if (x > 0)
                    CountEqual(x - 1, y);
                if (x < Game.NumberOfBalls - 1)
                    CountEqual(x + 1, y);
                if (y > 0)
                    CountEqual(x, y - 1);
                if (y < Game.NumberOfBalls - 1)
                    CountEqual(x, y + 1);
                equalBalls[y, x] = true;
                equalNumber++;
                Console.WriteLine("Number was increased, curret is " + equalNumber);
            }
        }

        public bool[,] EqualBalls(int x, int y)
        {
            equalNumber = 0;
            equalBalls = new bool[Game.NumberOfBalls, Game.NumberOfBalls];
            used = new bool[Game.NumberOfBalls, Game.NumberOfBalls];
            if (x < 12 && y < 12)
            {
                equalColor = balls[y, x].GetColor();
                CountEqual(x, y);
            }
            Console.WriteLine("There are {0} equal balls", equalNumber);
            return (equalNumber > 1) ? equalBalls : new bool[Game.NumberOfBalls, Game.NumberOfBalls];
        }
    }
}
