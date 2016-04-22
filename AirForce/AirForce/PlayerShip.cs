using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AirForce
{
    public class PlayerShip : FlyingObject
    {
        public double Speed;
        public int HealthPoints;

        public PlayerShip(PointD location, double speed, int healthPoints)
        {
            width = 80;
            height = 50;
            this.Location = location;
            Speed = speed;
            HealthPoints = healthPoints;
            brush = Brushes.CadetBlue;
        }
    }
}
