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

        public static void PushCell()
        {
            var cellMass = player.Mass / 10;
            player.Mass -= cellMass;

            var movementVector = SetNewCellMovementVector();
            cells.Add(
                CellFactory.Create(
                    player.Position - player.MovementVector,
                    cellMass,
                    movementVector));
            player.MovementVector -= movementVector;
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

        private static Point2D SetNewCellMovementVector()
        {
            Point2D clickPosition = GameWindow.GameFieldCursorPosition;
            var distance = Point2D.DistanceBetween(player.Position, clickPosition);

            float lambda = (float)(distance / Math.Pow(distance, 2));
            var xOnCircle = (player.Position.X + lambda * clickPosition.X) / (1 + lambda);
            var yOnCircle = (player.Position.Y + lambda * clickPosition.Y) / (1 + lambda);
            var xSpeed = player.Position.X - xOnCircle;
            var ySpeed = player.Position.Y - yOnCircle;

            return new Point2D(-xSpeed, -ySpeed);
        }
    }
}
