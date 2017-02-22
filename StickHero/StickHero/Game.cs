using System;
using System.Drawing;

namespace StickHero
{
    class Game
    {
        private const int MinWidth = Hero.Width + Stick.Width + GameForm.OffsetBeforeStick;
        private const int MaxWidth = 180;

        public const int PlayingHeight = 100;

        private const int DoubleScorePlatformWidth = 15;
        private const int DoubleScorePlatformHeight = 5;

        public enum GameState
        {
            Waiting,
            StickFalling,
            HeroMoving,
            BoardMoving,
            HeroFalling
        }

        public const int GameSpeed = 10;

        private GameState state;

        private int randomWidth = new Random().Next(MinWidth, MaxWidth);

        private Platform heroPlatform;
        private Platform anotherPlatform;
        private Platform doubleScorePlatform;

        private Hero hero;

        public Platform GetHeroPlatform()
        {
            return heroPlatform;
        }
        public Platform GetAnotherPlatform()
        {
            return anotherPlatform;
        }
        public Platform GetDoubleScorePlatform()
        {
            return doubleScorePlatform;
        }

        private Stick stick;
        public Stick GetStick()
        {
            return stick;
        }

        public void Start()
        {
            Random random = new Random();
            heroPlatform = new Platform(100, PlayingHeight, 0, GameForm.ClientHeight - PlayingHeight);
            anotherPlatform = new Platform(randomWidth, heroPlatform.Height,
                                           random.Next(heroPlatform.Position.X + heroPlatform.Width + 10, 
                                                       GameForm.ClientWidth - randomWidth), 
                                           GameForm.ClientHeight - heroPlatform.Height);
            doubleScorePlatform = new Platform(DoubleScorePlatformWidth, DoubleScorePlatformHeight,
                                    anotherPlatform.Position.X + anotherPlatform.Width / 2 - DoubleScorePlatformWidth / 2, anotherPlatform.Position.Y);
            stick = new Stick();
            hero = new Hero();
        }

        public void Restart()
        {
            state = GameState.Waiting;
            SetScore(0);
            stick = new Stick();
            stick.SetAngle(-90);
            CreateNewPlatform();
            hero.Position.X = heroPlatform.Position.X + heroPlatform.Width - Hero.Width - Stick.Width - GameForm.OffsetBeforeStick;
            hero.Position.Y = GameForm.ClientHeight - heroPlatform.Height - Hero.Height;
            stick.StartingPoint = new Point(hero.Position.X + Hero.Width + GameForm.OffsetBeforeStick, hero.Position.Y + Hero.Height);
            stick.EndingPoint = new Point(hero.Position.X + Hero.Width + GameForm.OffsetBeforeStick, hero.Position.Y + Hero.Height);
        }

        private int score;
        public int GetScore()
        {
            return score;
        }
        public void SetScore(int points)
        {
            score = points;
        }

        public GameState GetState()
        {
            return state;
        }
        public void SetState(GameState newState)
        {
            state = newState;
        }

        public Hero GetHero()
        {
            return hero;
        }

        private void CreateNewPlatform()
        {
            Random random = new Random();
            if (GetScore() > 0)
                heroPlatform = new Platform(anotherPlatform.Width, anotherPlatform.Height,
                                            anotherPlatform.Position.X, anotherPlatform.Position.Y);

            anotherPlatform = new Platform(randomWidth, heroPlatform.Height,
                                    random.Next(heroPlatform.Position.X + heroPlatform.Width + 10, GameForm.ClientWidth - randomWidth), GameForm.ClientHeight - heroPlatform.Height);
            doubleScorePlatform = new Platform(DoubleScorePlatformWidth, DoubleScorePlatformHeight,
                                    anotherPlatform.Position.X + anotherPlatform.Width / 2 - DoubleScorePlatformWidth / 2, anotherPlatform.Position.Y);
            stick.StartingPoint = new Point(hero.Position.X + Hero.Width + GameForm.OffsetBeforeStick, hero.Position.Y + Hero.Height);
            stick.EndingPoint = new Point(hero.Position.X + Hero.Width + GameForm.OffsetBeforeStick, hero.Position.Y + Hero.Height);
            stick.SetAngle(-90);
        }

        public void MoveHero()
        {
            hero.Move();
            // Movement on a stick
            if (hero.Position.X > GetStick().StartingPoint.X - Hero.Width && hero.Position.X < GetStick().EndingPoint.X)
                hero.Position.Y = GetHeroPlatform().Position.Y - Stick.Width / 2 - Hero.Height;
            else
                hero.Position.Y = GetHeroPlatform().Position.Y - Hero.Height;

            if (!GetStick().IsOnPlatform(GetAnotherPlatform()) &&
                hero.Position.X >= GetHeroPlatform().Position.X + GetHeroPlatform().Width
                && hero.Position.X < GetAnotherPlatform().Position.X)
            {
                hero.Position.X = GetHeroPlatform().Position.X + GetHeroPlatform().Width;
                SetState(GameState.HeroFalling);
            }

            if (GetStick().IsOnPlatform(GetAnotherPlatform()))
            {
                if (hero.Position.X >= GetAnotherPlatform().Position.X)
                    GetStick().Delete();

                CheckHeroEndPoint();
            }
        }

        private void CheckHeroEndPoint()
        {
            if (hero.Position.X >= GetAnotherPlatform().Position.X + GetAnotherPlatform().Width - Hero.Width - Stick.Width - GameForm.OffsetBeforeStick)
            {
                hero.Position.X = GetAnotherPlatform().Position.X + GetAnotherPlatform().Width - Hero.Width - Stick.Width - GameForm.OffsetBeforeStick;
                SetState(GameState.BoardMoving);
            }
        }

        public void MoveBoard()
        {
            hero.Position.X -= GameSpeed;
            heroPlatform.Position.X -= GameSpeed;
            anotherPlatform.Position.X -= GameSpeed;
            doubleScorePlatform.Position.X -= GameSpeed;
            if (hero.Position.X <= 50)
            {
                while (hero.Position.X < 50)
                {
                    hero.Position.X++;
                    heroPlatform.Position.X++;
                    anotherPlatform.Position.X++;
                    doubleScorePlatform.Position.X++;
                }
                CreateNewPlatform();
                SetState(GameState.Waiting);
            }
        }

    }
}
