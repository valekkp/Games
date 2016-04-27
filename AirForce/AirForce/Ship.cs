using System;
using System.Drawing;

namespace AirForce
{
    public class Ship : FlyingObject
    {
        protected ShipType Type;
        public double Speed;
        public int HealthPoints;
        public int ShootDelay;
        //public Ship(PointD location, ShipType type, double speed, int healthPoints, Brush brush, int width, int height)
        //{
        //    this.brush = brush;
        //    this.Location = location;
        //    this.width = width;
        //    this.height = height;
        //    Type = type;
        //    Speed = speed;
        //    HealthPoints = healthPoints;
        //}

        public static Ship CreateRandomShip(int limitX, int limitY)
        {
            Random random = new Random();
            int speedMultiplier;
            int healthPoints;
            int width = 50;
            int height = 30;
            double speed;
            PointD location;
            Brush brush;
            ShipType shipType = (ShipType) random.Next(0, 2);
            switch (shipType)
            {
                //case ShipType.Small:          //todo: need refactoring
                //    healthPoints = 1;
                //    speedMultiplier = 5;
                //    brush = Brushes.Tomato;
                //    double speed = speedMultiplier + random.NextDouble();
                //    PointD location = new PointD(limitX, random.NextDouble() * (limitY - height));
                //    break;
                case ShipType.Middle:
                    healthPoints = 3;
                    speedMultiplier = 3;
                    width *= 2;
                    height *= 2;
                    brush = Brushes.Crimson;
                    speed = speedMultiplier + random.NextDouble();
                    location = new PointD(limitX, random.NextDouble()*(limitY - height));
                    return new MiddleShip(location, shipType, speed, healthPoints, brush, width, height);
                case ShipType.Big:
                    healthPoints = 5;
                    speedMultiplier = 1;
                    width *= 3;
                    height *= 3;
                    brush = Brushes.DarkBlue;
                    speed = speedMultiplier + random.NextDouble();
                    location = new PointD(limitX, random.NextDouble()*(limitY - height));
                    return new BigShip(location, shipType, speed, healthPoints, brush, width, height);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public Bullet Shoot(PlayerShip player)
        {
            if (Type == ShipType.Middle)
            {
                if (LocationCenter.Y >= player.Location.Y && LocationCenter.Y <= player.Location.Y + player.Height)
                {
                    return Bullet.CreateBullet(LocationCenter, true);
                }
            }
            return null;
        }
    }
}
