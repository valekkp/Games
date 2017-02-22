using System.Drawing;
using static System.Math;

namespace StickHero
{
    class Hero
    {
        public const int Width = 50;
        public const int Height = 50;

        private Image heroImage = Properties.Resources.Hero;

        public int XCoord;
        public int YCoord;

        public void Draw(Graphics graphics)
        {
            graphics.DrawImage(heroImage, XCoord, YCoord, Width, Height);
        }

        public void Move()
        {
            XCoord += Game.GameSpeed;
        }

        public void Drop()
        {
            YCoord += Game.GameSpeed;
        }
    }
}
