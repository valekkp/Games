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
    public class GameController
    {
        public static readonly Size GameFieldSize = GameWindow.GameFieldSize;
        public static readonly Rectangle Ground = new Rectangle(0, GameFieldSize.Height - 100, GameFieldSize.Width, 100);
        public static readonly Size AirFieldSize = new Size(GameFieldSize.Width, GameFieldSize.Height - Ground.Height);
        public IEnumerable<FlyingObject> FlyingObjects;
        public GameState CurrentGameState;
        private int score;

        private List<FlyingObject> flyingObjects;
        private List<Bullet> bulletsToBeAdded;

        private PlayerShip player;

        private readonly Random mRandom = new Random();
        private int spawnerCooldown;
        private readonly int spawnerCooldownMax = 100;

        public GameController()
        {
            StartGame();
        }

        public void StartGame()
        {
            CurrentGameState = GameState.Playing;
            flyingObjects = new List<FlyingObject>();
            bulletsToBeAdded = new List<Bullet>();
            player = new PlayerShip();
            flyingObjects.Add(player);
            FlyingObjects = flyingObjects;
            score = 0;
        }

        public void UpdateGameState()
        {
            UpdateObjects();
            SpawnObjects();
        }

        public void UpdateObjects()
        {
            if (CurrentGameState == GameState.Playing)
            {
                foreach (var flyingObject in flyingObjects)
                {
                    flyingObject.Move();

                    var bullet = flyingObject.Shoot();
                    if(bullet != null)
                        bulletsToBeAdded.Add(bullet);
                }

                flyingObjects.AddRange(bulletsToBeAdded);
                bulletsToBeAdded.Clear();
                CheckIntersections();

                ClearDeadObjects();
            }
        }

        private void ClearDeadObjects()
        {
            flyingObjects
                .RemoveAll(flyingObject => 
                    flyingObject == null
                    || flyingObject.Position.X < -flyingObject.Size.Width / 2
                    || flyingObject.Position.X > AirFieldSize.Width + flyingObject.Size.Width / 2
                    || flyingObject.HealthPoints == 0);

            if(FlyingObjects.Any() 
               && !(FlyingObjects.First() is PlayerShip))
            {
                CurrentGameState = GameState.Defeat;
            }
        }

        private void CheckIntersections()
        {
            foreach (var source in flyingObjects)
            {
                if (IntersectionController.DoesTouchGround(source))
                    source.HealthPoints = 0;
                foreach (var target in flyingObjects)
                {
                    if (source.Equals(target)) continue;
                    if (IntersectionController.DoCirclesIntersect(source, target))
                    {
                        //DeadObjects.Add(ship);
                        IntersectionController.DamageOnIntersection(source, target);
                    }
                }
            }
            
        }

        private void SpawnObjects()
        {
            if (spawnerCooldown == 0)
            {
                spawnerCooldown = spawnerCooldownMax;
                int randomNumber = mRandom.Next(4, 17);
                FlyingObject objectToBeAdded = null;
                switch ((FlyingObjectType)(randomNumber / 4))
                {
                    case FlyingObjectType.Fighter:
                        objectToBeAdded =
                            new FighterShip(
                                new Point2D(AirFieldSize.Width + FighterShip.Size.Width / 2,
                                    FighterShip.Size.Height / 2 +
                                    mRandom.Next(AirFieldSize.Height - FighterShip.Size.Height / 2)),
                                player,
                                FlyingObjects);
                        break;
                    case FlyingObjectType.Tank:
                        objectToBeAdded = new TankShip(new Point2D(AirFieldSize.Width + TankShip.Size.Width / 2,
                            TankShip.Size.Height / 2 +
                            mRandom.Next(0, AirFieldSize.Height - TankShip.Size.Height / 2)));
                        break;
                    case FlyingObjectType.Bird:
                        objectToBeAdded = new Bird(new Point2D(AirFieldSize.Width + Bird.Size.Width / 2,
                            Bird.Size.Height / 2 + mRandom.Next(2 * AirFieldSize.Height / 3,
                                AirFieldSize.Height - Bird.Size.Height / 2)));
                        break;
                    case FlyingObjectType.Meteorite:
                        objectToBeAdded = new Meteorite(new Point2D(mRandom.Next(100, AirFieldSize.Width), -100));
                        break;

                }

                flyingObjects.Add(objectToBeAdded);
            }
            else if (spawnerCooldown > 0)
                spawnerCooldown--;

        }

        public void DrawObjects(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.DarkGreen, Ground);

            foreach (var flyingObject in flyingObjects)
            {
                flyingObject.Draw(graphics);
            }
        }
    }
}
