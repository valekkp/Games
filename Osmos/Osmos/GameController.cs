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
        private static List<Cell> deadCells;
        private static GameState gameState;
        private static float sumOfMass;

        public enum GameState
        {
            Playing,
            Victory,
            Defeat
        }

        public static void Start()
        {
            cells = new List<Cell>();
            deadCells = new List<Cell>();
            player = new PlayerCell(new Point2D(GameWindow.GameFieldSize.Width/2, GameWindow.GameFieldSize.Height/2),
                5000, new Point2D(0, 0));
            gameState = GameState.Playing;
            cells.Add(player);
        }

        public static void PushCell()
        {
            var cellMass = player.Mass / 10;
            player.Mass -= cellMass;
            var cellRadius = (float)Math.Sqrt(cellMass / Math.PI);

            var cellMovementVector = SetNewCellMovementVector();

            var distanceMultiplier = (player.Radius + cellRadius) / Point2D.DistanceBetween(player.Position, cellMovementVector + player.Position);
            var position = player.Position + cellMovementVector * distanceMultiplier;

            cells.Add(
                CellFactory.Create(
                    position,
                    cellMass,
                    cellMovementVector));
            player.MovementVector -= cellMovementVector;
        }

        public static void StopPlayer()
        {
            player.MovementVector = new Point2D();
        }
        

        public static void Update(Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            if (gameState == GameState.Playing)
            {
                sumOfMass = 0;
                foreach (var cell in cells)
                {
                    
                    if (!(cell is PlayerCell))
                    {
                        cell.OuterColor = cell.Mass < player.Mass ? Cell.ColorWhenSmaller : Cell.ColorWhenBigger;
                    }

                    cell.Draw(graphics);
                    cell.Move();
                    foreach (var targetCell in cells)
                    {
                        if(!cell.Equals(targetCell))
                            IntersectionController.IntersectCells(cell, targetCell);
                    }

                    sumOfMass += cell.Mass;

                    if (player.Mass <= 0)
                        gameState = GameState.Defeat;

                    if (cell.Mass <= 0)
                        deadCells.Add(cell);
                }
                graphics.DrawString("Global mass: " + sumOfMass, new Font(FontFamily.GenericSansSerif, 10), Brushes.Crimson, 10, 10);
                cells = cells.Except(deadCells).ToList();
                deadCells.Clear();
            }

            if (gameState == GameState.Defeat)
            {
                //MessageBox.Show("You lost!");
                gameState = GameState.Playing;
                Start();
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
