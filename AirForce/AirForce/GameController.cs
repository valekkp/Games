using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Timer = System.Windows.Forms.Timer;

namespace AirForce
{
    static class GameController
    {
        public static Size GameFieldSize { get; }
        public static readonly IEnumerable<FlyingObject> FlyingObjects;

        private static List<FlyingObject> flyingObjects;
        private static List<Bullet> bulletsToBeAdded;

        private static readonly PlayerShip player;

        private static readonly Timer mUpdateTimer = new Timer();
        private static readonly Timer mSpawnTimer = new Timer();

        private static readonly Random mRandom = new Random();

        static GameController()
        {
            mSpawnTimer.Interval = 1000;
            mSpawnTimer.Tick += (s, e) => SpawnObject();

            flyingObjects = new List<FlyingObject>();
            bulletsToBeAdded = new List<Bullet>();
            FlyingObjects = flyingObjects;

            GameFieldSize = GameWindow.GameFieldSize;

            StartGame();
            player = new PlayerShip();
            flyingObjects.Add(player);
        }

        private static void StartGame()
        {
            

            mUpdateTimer.Start();
            mSpawnTimer.Start();
        }

        public static void UpdateObjects()
        {
            foreach (var flyingObject in flyingObjects)
            {
                flyingObject.Move();
                flyingObject.Shoot();
            }

            flyingObjects.AddRange(bulletsToBeAdded);
            bulletsToBeAdded.Clear();
            CheckIntersections();

            ClearDeadObjects();
        }

        private static void ClearDeadObjects()
        {
            flyingObjects
                .RemoveAll(flyingObject => 
                    flyingObject == null
                    || flyingObject.Position.X < -flyingObject.Size.Width / 2
                    || flyingObject.Position.X > GameFieldSize.Width + flyingObject.Size.Width / 2
                    || flyingObject.HealthPoints == 0);
        }

        private static void CheckIntersections()
        {
            foreach (var source in flyingObjects)
            {
                foreach (var target in flyingObjects)
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

        private static void SpawnObject()
        {
            int randomNumber = mRandom.Next(4, 17);
            FlyingObject objectToBeAdded = null;
            switch (randomNumber / 4)
            {
                case (int)FlyingObjectType.Fighter:
                    objectToBeAdded = new FighterShip(new Point2D(GameFieldSize.Width + FighterShip.Size.Width / 2, FighterShip.Size.Height / 2 + mRandom.Next(GameFieldSize.Height - FighterShip.Size.Height / 2)), player);
                    break;
                case (int)FlyingObjectType.Tank:
                    objectToBeAdded = new TankShip(new Point2D(GameFieldSize.Width + TankShip.Size.Width / 2, TankShip.Size.Height / 2 + mRandom.Next(0, GameFieldSize.Height - TankShip.Size.Height / 2)));
                    break;
                case (int)FlyingObjectType.Bird:
                    objectToBeAdded = new Bird(new Point2D(GameFieldSize.Width + Bird.Size.Width / 2, Bird.Size.Height / 2 + mRandom.Next(0, GameFieldSize.Height - Bird.Size.Height / 2)));
                    break;
                case (int)FlyingObjectType.Meteorite:
                    objectToBeAdded = new Meteorite(new Point2D(mRandom.Next(100, GameFieldSize.Width), -100));
                    break;
                
            }
            
            flyingObjects.Add(objectToBeAdded);
        }

        public static void DrawObjects(Graphics graphics)
        {
            foreach (var flyingObject in flyingObjects)
            {
                flyingObject.Draw(graphics);
            }
        }

        public static void AddBullet(Bullet bulletToAdd, FlyingObject source)
        {
            bulletsToBeAdded.Add(bulletToAdd);
        }

    }
}
