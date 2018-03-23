using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce
{
    class ShootingBehavior : IShootable
    {
        public int Cooldown { get; set; }

        private readonly FlyingObject source;
        private readonly FlyingObject target;

        public ShootingBehavior(FlyingObject source, FlyingObject target)
        {
            this.source = source;
            this.target = target;
        }

        public void Shoot()
        {
            if (ReadyToShoot())
            {
                GameController.AddBullet(new Bullet(source), source);
                Cooldown = 100;
            }
            else if(Cooldown > 0)
                Cooldown--;
        }

        public bool ReadyToShoot()
        {
            return source.Position.Y < target.Position.Y + PlayerShip.Size.Height / 2
                   && source.Position.Y > target.Position.Y - PlayerShip.Size.Height / 2
                   && Cooldown == 0;
        }
    }
}
