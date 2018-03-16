using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;

namespace AirForce
{
    class GameController
    {
        private readonly Timer mMoveTimer = new Timer();
        private readonly Timer mSpawnTimer = new Timer();

        public List<FlyingObject> FlyingObjects { get; private set; }

        public Size GameFieldSize { get; }

        public GameController(Size gameFieldSize)
        {
            mMoveTimer.Interval = 1;
            mMoveTimer.Tick += (s,e) => MoveObject();
            mMoveTimer.Start();

            mSpawnTimer.Interval = 1000;
            mSpawnTimer.Tick += (s, e) => SpawnObject();
            mSpawnTimer.Start();

            FlyingObjects = new List<FlyingObject>();

            GameFieldSize = gameFieldSize;
        }

        private void MoveObject()
        {
            foreach (var flyingObject in FlyingObjects)
            {
                flyingObject.Move();
            }
        }

        private void SpawnObject()
        {
            Random random = new Random();
            
            switch ((ShipType)random.Next(2))
            {
                case ShipType.Fighter:
                    FlyingObjects.Add(new FighterShip(GameFieldSize));
                    break;
                case ShipType.Tank:
                    FlyingObjects.Add(new TankShip(GameFieldSize));
                    break;
                default:
                    break;
            }
        }

        public void Update(Graphics graphics)
        {
            foreach (var flyingObject in FlyingObjects)
            {
                flyingObject.Draw(graphics);
            }
        }

    }
}
