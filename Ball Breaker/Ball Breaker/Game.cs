using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ball_Breaker
{
    class Game
    {
        public const int NumberOfBalls = 12;

        private BallsGroup balls = new BallsGroup();

        void Start()
        {
            
        }

        public BallsGroup GetBallsGroup()
        {
            return balls;
        }

    }
}
