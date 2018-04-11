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
using System.Windows.Forms;

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

            
            var xDifference = clickPosition.X - player.Position.X;
            var yDifference = clickPosition.Y - player.Position.Y;

            var ratio = Math.Min(xDifference, yDifference) / Math.Max(xDifference, yDifference);

            float lambda = (float)(distance / Math.Pow(distance, 2));
            var xOnCircle = (player.Position.X + lambda * clickPosition.X) / (1 + lambda);
            var yOnCircle = (player.Position.Y + lambda * clickPosition.Y) / (1 + lambda);
            var xSpeed = player.Position.X - xOnCircle;
            var ySpeed = player.Position.Y - yOnCircle;

            player.MovementVector.X += xSpeed;
            player.MovementVector.Y += ySpeed;
            

            var cellMass = player.Mass / 10;
            player.Mass -= cellMass;
            var cellPosition = new Point2D();
            cells.Add(new NeutralCell(new Point2D(player.Position.X - xSpeed,
                    player.Position.Y - ySpeed),
                cellMass,
                new Point2D(-xSpeed, -ySpeed)));
        }

        public static void StopPlayer()
        {
            player.MovementVector = new Point2D();
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
