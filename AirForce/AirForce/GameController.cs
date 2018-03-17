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
        public List<FlyingObject> FlyingObjects { get; private set; }
        public List<FlyingObject> DeadObjects { get; private set; }

        public Size GameFieldSize { get; }

        public PlayerShip Player;

        private readonly Timer mMoveTimer = new Timer();
        private readonly Timer mSpawnTimer = new Timer();

        private static GameController instance;

        private GameController()
        {
            mMoveTimer.Interval = 1;
            mMoveTimer.Tick += (s,e) => MoveObject();

            mSpawnTimer.Interval = 1000;
            mSpawnTimer.Tick += (s, e) => SpawnObject();

            GameFieldSize = GameWindow.GameFieldSize;

            StartGame();
        }

        public static GameController GetInstance()
        {
            return instance ?? new GameController();
        }

        private void StartGame()
        {
            FlyingObjects = new List<FlyingObject>();
            DeadObjects = new List<FlyingObject>();
            
            Player = PlayerShip.GetInstance();
            FlyingObjects.Add(Player);

            mMoveTimer.Start();
            mSpawnTimer.Start();
        }

        private void MoveObject()
        {
            foreach (var flyingObject in FlyingObjects)
            {
                flyingObject.Move();
                if (flyingObject.Position.X <= -flyingObject.Size.Width)
                    DeadObjects.Add(flyingObject);
            }

            DeadObjects.Clear();
        }

        Random random = new Random();
        private void SpawnObject()
        {
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
