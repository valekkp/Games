using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Osmos
{
    public static class GameController
    {
        private static PlayerCell player;
        private static List<Cell> cells;

        public static void Start()
        {
            cells = new List<Cell>();
            player = new PlayerCell(new Point2D(GameWindow.GameFieldSize.Width/2, GameWindow.GameFieldSize.Height/2),
                5000, new Point2D(0, 0));
            cells.Add(player);
        }

        public static void MovePlayer(Point2D clickPosition)
        {
            var distance = Point2D.DistanceBetween(player.Position, clickPosition);
            var motionRadius = 1;

            if (distance > motionRadius)
            {
                float lambda = (float)((distance - motionRadius) / Math.Pow(distance, 2));
                var xSpeed = (player.Position.X + lambda * clickPosition.X) / (1 + lambda);
                var ySpeed = (player.Position.Y + lambda * clickPosition.Y) / (1 + lambda);
                var maxValue = Math.Abs(Math.Max(xSpeed, ySpeed));
                xSpeed /= maxValue;
                ySpeed /= maxValue;
                player.MovementVector.X += clickPosition.X > player.Position.X ? -xSpeed : xSpeed;
                player.MovementVector.Y += clickPosition.Y > player.Position.Y ? -ySpeed : ySpeed;
            }

            //player.Mass -= 100;
            //var cellPosition = new Point2D();
            //cells.Add(new NeutralCell(new Point2D(player.Position.X - (player.MovementVector.X + player.MovementVector.X > 0 ? 10 : - 10),
            //        player.Position.Y -(player.MovementVector.Y + player.MovementVector.Y > 0 ? 10 : -10)),
            //    1000,
            //    new Point2D(-player.MovementVector.X, -player.MovementVector.Y)));
        }

        public static void Update(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            foreach (var cell in cells)
            {
                cell.Draw(graphics);
                cell.Move();
            }
        }
    }
}
