using System.Drawing;

namespace AirForce
{
    public class PlayerShip : FlyingObject
    {
        public double Speed;
        public int HealthPoints;

        public PlayerShip(PointD location, double speed, int healthPoints)
        {
            Width = 80;
            Height = 50;
            Location = location;
            Speed = speed;
            HealthPoints = healthPoints;
            Brush = Brushes.CadetBlue;
        }
    }
}
