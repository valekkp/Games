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
        public List<Bullet> EnemyBullets { get; private set; }

        public Size GameFieldSize { get; }

        public PlayerShip Player;

        private readonly Timer mMoveTimer = new Timer();
        private readonly Timer mSpawnTimer = new Timer();

        private static GameController instance;

        private readonly Random mRandom = new Random();

        private GameController()
        {
            mMoveTimer.Interval = 1;
            mMoveTimer.Tick += (s,e) => MoveObjects();

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
            EnemyBullets = new List<Bullet>();
            
            Player = PlayerShip.GetInstance();
            FlyingObjects.Add(Player);

            mMoveTimer.Start();
            mSpawnTimer.Start();
        }

        private void MoveObjects()
        {
            foreach (var flyingObject in FlyingObjects)
            {
                flyingObject.Move();
                if(flyingObject is FighterShip) ((FighterShip)flyingObject).AddCooldown();
                if (flyingObject is FighterShip && (flyingObject as FighterShip).ReadyToShoot(Player))
                {
                    EnemyBullets.Add(new Bullet(flyingObject.Position));
                    (flyingObject as FighterShip).ResetCooldown();
                }

                if (flyingObject.Position.X <= -flyingObject.Size.Width)
                    DeadObjects.Add(flyingObject);
            }

                FlyingObjects = FlyingObjects.Concat(EnemyBullets).ToList();
                EnemyBullets.Clear();
                CheckIntersections();
                FlyingObjects = FlyingObjects.Except(DeadObjects).ToList();
                DeadObjects.Clear();
        }

        private void CheckIntersections()
        {
            foreach (var ship in FlyingObjects)
            {
                if (ship is PlayerShip) continue;
                if (DoesIntersect(ship, Player))
                {
                    DeadObjects.Add(ship);
                }
            }
        }

        private bool DoesIntersect(FlyingObject ship, PlayerShip player)
        {
            return ship.Position.DistanceTo(player.Position) < (ship.Size.Width/2 + player.Size.Width/2);
        }


        private void SpawnObject()
        {
            switch ((ShipType)mRandom.Next(2))
            {
                case ShipType.Fighter:
                    FlyingObjects.Add(new FighterShip(new Point2D(GameFieldSize.Width + FighterShip.Size.Width / 2, FighterShip.Size.Height / 2 + mRandom.Next(GameFieldSize.Height - FighterShip.Size.Height / 2))));
                    break;
                case ShipType.Tank:
                    FlyingObjects.Add(new TankShip(new Point2D(GameFieldSize.Width + TankShip.Size.Width / 2, TankShip.Size.Height / 2 + mRandom.Next(0, GameFieldSize.Height - TankShip.Size.Height / 2))));
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
