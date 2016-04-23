using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace AirForce
{
    public class Bullets : FlyingObject
    {
        public const double speed = 10;
        public bool used;

        public Bullets(PointD location)
        {
            brush = Brushes.DarkRed;
            width = 4;
            height = 4;
            used = false;
            
            this.Location = location;
        }

        public static Bullets CreateBullet(PointD location)
        {
            return new Bullets(location);
        }
    }
}
