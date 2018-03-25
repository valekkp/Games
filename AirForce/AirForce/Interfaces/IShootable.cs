using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    public interface IShootable
    {
        int Cooldown { get; set; }

        bool ReadyToShoot();

        Bullet Shoot();
    }
}
