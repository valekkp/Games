using System.Drawing;

namespace AirForce
{
    public class MiddleShip : Ship
    {
        public MiddleShip(PointD location, ShipType type, double speed, int healthPoints, Brush brush, int width, int height)
        {
            Brush = brush;
            Location = location;
            Width = width;
            Height = height;
            Type = type;
            Speed = speed;
            HealthPoints = healthPoints;
        }
    }
}