using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace AirForce
{
    public static class IntersectionController
    {
        //private static int flyingObjectsTypesCount = Enum.GetNames(typeof(FlyingObjectType)).Length;

        public readonly static bool[,] IntersectionTable =
        {
            #region Player
            {
                /*with Player*/ false,
                /*with Fighter*/ true,
                /*with Tank*/ true,
                /*with Bird*/ true,
                /*with Meteorite*/ true,
                /*with EnemyBullet*/ true,
                /*with PlayerBullet*/ false,
            },
            #endregion Player

            #region Fighter
            {
                /*with Player*/ true,
                /*with Fighter*/ false,
                /*with Tank*/ false,
                /*with Bird*/ false,
                /*with Meteorite*/ true,
                /*with EnemyBullet*/ false,
                /*with PlayerBullet*/ true,
            },
            #endregion Fighter

            #region Tank
            {
                /*with Player*/ true,
                /*with Fighter*/ false,
                /*with Tank*/ false,
                /*with Bird*/ false,
                /*with Meteorite*/ true,
                /*with EnemyBullet*/ false,
                /*with PlayerBullet*/ true,
            },
            #endregion Player

            #region Bird
            {
                /*with Player*/ true,
                /*with Fighter*/ false,
                /*with Tank*/ false,
                /*with Bird*/ false,
                /*with Meteorite*/ true,
                /*with EnemyBullet*/ false,
                /*with PlayerBullet*/ false,
            },
            #endregion Bird

            #region Meteorite
            {
                /*with Player*/ true,
                /*with Fighter*/ true,
                /*with Tank*/ true,
                /*with Bird*/ true,
                /*with Meteorite*/ false,
                /*with EnemyBullet*/ true,
                /*with PlayerBullet*/ true,
            },
            #endregion Meteorite

            #region EnemyBullet
            {
                /*with Player*/ true,
                /*with Fighter*/ false,
                /*with Tank*/ false,
                /*with Bird*/ false,
                /*with Meteorite*/ true,
                /*with EnemyBullet*/ false,
                /*with PlayerBullet*/ false,
            },
            #endregion EnemyBullet

            #region PlayerBullet
            {
                /*with Player*/ false,
                /*with Fighter*/ true,
                /*with Tank*/ true,
                /*with Bird*/ false,
                /*with Meteorite*/ true,
                /*with EnemyBullet*/ false,
                /*with PlayerBullet*/ false,
            },
            #endregion PlayerBullet
        };

        public static bool DoCirclesIntersect(FlyingObject source, FlyingObject target)
        {
            return IntersectionTable[(int)source.Type, (int)target.Type] 
                   && source.HealthPoints > 0 
                   && target.HealthPoints > 0 
                   && source.Position.DistanceTo(target.Position) < (source.Size.Width / 2 + target.Size.Width / 2);
        }

        public static bool DoesTouchGround(FlyingObject source)
        {
            return !(source is Bird) && source.Position.Y + source.Size.Height/2 >= GameController.AirFieldSize.Height;
        }

        public static void ActionOnIntersection(FlyingObject source, FlyingObject target)
        {
            if (source is Bullet || target is Bullet)
            {
                source.HealthPoints -= 1;
                target.HealthPoints -= 1;
                return;
            }

            if (target is PlayerShip)
            {
                source.HealthPoints = 0;
                target.HealthPoints -= 1;
                return;
            }

            if (source is PlayerShip)
            {
                source.HealthPoints -= 1;
                target.HealthPoints = 0;
                return;
            }

            source.HealthPoints = 0;
            target.HealthPoints = 0;
        }

        public static int DistanceBetween(FlyingObject source, FlyingObject target)
        {
            return source.Position.DistanceTo(target.Position) - source.Size.Width - target.Size.Width;
        }
    }
}
