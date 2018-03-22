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

        private readonly FlyingObject flyingObject;

        public ShootingBehavior(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void Shoot()
        {
            if (ReadyToShoot())
            {
                GameController.GetInstance().Bullets.Add(new Bullet(flyingObject));
                Cooldown = 100;
            }
            else if(Cooldown > 0)
                Cooldown--;
        }

        public bool ReadyToShoot()
        {
            return flyingObject.Position.Y < PlayerShip.GetInstance().Position.Y + PlayerShip.Size.Height / 2
                   && flyingObject.Position.Y > PlayerShip.GetInstance().Position.Y - PlayerShip.Size.Height / 2
                   && Cooldown == 0;
        }
    }
}
