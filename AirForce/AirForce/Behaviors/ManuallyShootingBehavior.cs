using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirForce.Behaviors
{
    class ManuallyShootingBehavior : IShootable
    {
        public int Cooldown { get; set; }

        private readonly FlyingObject source;

        public ManuallyShootingBehavior(FlyingObject source)
        {
            this.source = source;
        }

        public bool ReadyToShoot()
        {
            if (Keyboard.IsKeyUp(Key.Space) && Cooldown > 5)
                Cooldown = 5;
            return Keyboard.IsKeyDown(Key.Space) && Cooldown == 0;
        }

        public void Shoot()
        {
            if (ReadyToShoot())
            {
                GameController.GetInstance()
                    .Bullets.Add(new Bullet(source));
                Cooldown = 25;
            }
            else if (Cooldown > 0)
                Cooldown -= 1;
        }
    }
}
