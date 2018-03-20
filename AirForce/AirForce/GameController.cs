using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Timer = System.Windows.Forms.Timer;

namespace AirForce
{
    class GameController
    {
        public List<FlyingObject> FlyingObjects { get; private set; }
        public List<Bullet> Bullets { get; private set; }


        public Size GameFieldSize { get; }

        private readonly PlayerShip player = PlayerShip.GetInstance();

        private readonly Timer mUpdateTimer = new Timer();
        private readonly Timer mSpawnTimer = new Timer();

        private static GameController instance;

        private readonly Random mRandom = new Random();

        private GameController()
        {
            
            mSpawnTimer.Interval = 1000;
            mSpawnTimer.Tick += (s, e) => SpawnObject();

            GameFieldSize = GameWindow.GameFieldSize;

            StartGame();
        }

        public static GameController GetInstance()
        {
            return instance ?? (instance = new GameController());
        }

        private void StartGame()
        {
            FlyingObjects = new List<FlyingObject>();
            Bullets = new List<Bullet>();
            //DeadObjects = new List<FlyingObject>();
            FlyingObjects.Add(player);

            mUpdateTimer.Start();
            mSpawnTimer.Start();
        }

        public void UpdateObjects()
        {
            foreach (var flyingObject in FlyingObjects)
            {
                flyingObject.Move();
                flyingObject.Shoot();
            }

            FlyingObjects.AddRange(Bullets);
            Bullets.Clear();
            CheckIntersections();

            ClearDeadObjects();
        }

        private void ClearDeadObjects()
        {
            FlyingObjects.RemoveAll(flyingObject => flyingObject.Position.X <=
                                                    -flyingObject.Size.Width / 2
                                                    || flyingObject.Position.X >
                                                    GameFieldSize.Width + flyingObject.Size.Width / 2
                                                    || flyingObject.HealthPoints == 0);
        }

        private void CheckIntersections()
        {
            foreach (var source in FlyingObjects)
            {
                foreach (var target in FlyingObjects)
                {
                    if (source.Equals(target)) continue;
                    if (IntersectionController.DoCirclesIntersect(source, target))
                    {
                        //DeadObjects.Add(ship);
                        IntersectionController.ActionOnIntersection(source, target);
                    }
                }
            }
            
        }

        private void SpawnObject()
        {
            switch ((FlyingObjectType)mRandom.Next((int)FlyingObjectType.Fighter, (int) FlyingObjectType.Meteorite))
            {
                case FlyingObjectType.Fighter:
                    FlyingObjects.Add(new FighterShip(new Point2D(GameFieldSize.Width + FighterShip.Size.Width / 2, FighterShip.Size.Height / 2 + mRandom.Next(GameFieldSize.Height - FighterShip.Size.Height / 2))));
                    break;
                case FlyingObjectType.Tank:
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
