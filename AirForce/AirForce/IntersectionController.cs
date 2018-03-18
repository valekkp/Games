using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace AirForce
{
    public static class IntersectionController
    {
        //private static int flyingObjectsTypesCount = Enum.GetNames(typeof(FlyingObjectType)).Length;
        public static bool[,] IntersectionTable =
        {
            #region Player
            {
               /*with Player*/ false,
               /*with Fighter*/ true,
               /*with Tank*/ true,
               /*with Meteorite*/ true,
               /*with Bird*/ true,
               /*with PlayerBullet*/ false,
               /*with EnemyBullet*/ true,
            },
            #endregion Player

            #region Fighter
            {
            /*with Player*/ true,
            /*with Fighter*/ false,
            /*with Tank*/ false,
            /*with Meteorite*/ true,
            /*with Bird*/ false,
            /*with PlayerBullet*/ true,
            /*with EnemyBullet*/ false,
            },
            #endregion Fighter

            #region Tank
            {
                /*with Player*/ true,
                /*with Fighter*/ false,
                /*with Tank*/ false,
                /*with Meteorite*/ true,
                /*with Bird*/ false,
                /*with PlayerBullet*/ true,
                /*with EnemyBullet*/ false,
            },
            #endregion Player

            #region Meteorite
            {
                /*with Player*/ true,
                /*with Fighter*/ true,
                /*with Tank*/ true,
                /*with Meteorite*/ false,
                /*with Bird*/ true,
                /*with PlayerBullet*/ true,
                /*with EnemyBullet*/ false,
            },
            #endregion Meteorite

            #region Bird
            {
                /*with Player*/ true,
                /*with Fighter*/ false,
                /*with Tank*/ false,
                /*with Meteorite*/ true,
                /*with Bird*/ false,
                /*with PlayerBullet*/ false,
                /*with EnemyBullet*/ false,
            },
            #endregion Bird

            #region PlayerBullet
            {
                /*with Player*/ false,
                /*with Fighter*/ true,
                /*with Tank*/ true,
                /*with Meteorite*/ true,
                /*with Bird*/ false,
                /*with PlayerBullet*/ false,
                /*with EnemyBullet*/ false,
            },
            #endregion PlayerBullet

            #region EnemyBullet
            {
                /*with Player*/ true,
                /*with Fighter*/ false,
                /*with Tank*/ false,
                /*with Meteorite*/ false,
                /*with Bird*/ false,
                /*with PlayerBullet*/ false,
                /*with EnemyBullet*/ false,
            },
            #endregion EnemyBullet

        };

        public static bool DoCirclesIntersect(FlyingObject source, FlyingObject target)
        {
            return IntersectionTable[(int)source.Type, (int)target.Type] 
                   && source.HealthPoints > 0 
                   && target.HealthPoints > 0 
                   && source.Position.DistanceTo(target.Position) < (source.Size.Width / 2 + target.Size.Width / 2);
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
    }
}
