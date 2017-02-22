using System.Drawing;

namespace StickHero
{
    class Hero
    {
        public const int Width = 50;
        public const int Height = 50;

        private Image heroImage = Properties.Resources.Hero;

        public Point Position;

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(heroImage, Position.X, Position.Y, Width, Height);
        }

        public void Move()
        {
            Position.X += Game.GameSpeed;
        }

        public void Drop()
        {
            Position.Y += Game.GameSpeed;
        }
    }
}
