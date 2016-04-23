using System;
using System.Drawing;
using System.Windows.Forms;
using AirForce;

public class Ship : FlyingObject
{
    public ShipType Type;
    public double Speed;
    public int HealthPoints;

    public Ship(PointD location, ShipType type, double speed, int healthPoints, Brush brush, int width, int height)
    {
        this.brush = brush;
        this.Location = location;
        this.width = width;
        this.height = height;
        Type = type;
        Speed = speed;
        HealthPoints = healthPoints;
    }

    public static Ship CreateRandomShip(int limitX, int limitY)
    {
        Random random = new Random();
        int speedMultiplier;
        int healthPoints;
        int width = 50;
        int height = 30;
        Brush brush;
        ShipType shipType = (ShipType)random.Next(0, 3);
        switch (shipType)
        {
            case ShipType.Small:          //todo: need refactoring
                healthPoints = 1;
                speedMultiplier = 5;
                //width *= 1;
                //height *= 1;
                brush = Brushes.Tomato;
                break;
            case ShipType.Middle:
                healthPoints = 3;
                speedMultiplier = 3;
                width *= 2;
                height *= 2;
                brush = Brushes.Crimson;
                break;
            case ShipType.Big:
                healthPoints = 5;
                speedMultiplier = 1;
                width *= 3;
                height *= 3;
                brush = Brushes.DarkBlue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        double speed = speedMultiplier + random.NextDouble();
        PointD location = new PointD(limitX, random.NextDouble() * (limitY - height));

        return new Ship(location, shipType, speed, healthPoints, brush, width, height);
    }
}