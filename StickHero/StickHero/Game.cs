using System;
using System.Drawing;

namespace StickHero
{
    class Game
    {
        public const int minWidth = Hero.Width + Stick.Width + GameForm.OffsetBeforeStick;
        public const int maxWidth = 180;

        public const int playingHeight = 100;

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

        private GameState State;

        int randomWidth = new Random().Next(minWidth, maxWidth);

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
            heroPlatform = new Platform(100, playingHeight, 0, GameForm.ClientHeight - playingHeight);
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
            State = GameState.Waiting;
            SetScore(0);
            stick = new Stick();
            stick.SetAngle(-90);
            CreateNewPlatform();
            hero.XCoord = heroPlatform.Position.X + heroPlatform.Width - Hero.Width - Stick.Width - GameForm.OffsetBeforeStick;
            hero.YCoord = GameForm.ClientHeight - heroPlatform.Height - Hero.Height;
            stick.StartingPoint = new Point(hero.XCoord + Hero.Width + GameForm.OffsetBeforeStick, hero.YCoord + Hero.Height);
            stick.EndingPoint = new Point(hero.XCoord + Hero.Width + GameForm.OffsetBeforeStick, hero.YCoord + Hero.Height);
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
            return State;
        }
        public void SetState(GameState state)
        {
            State = state;
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
            stick.StartingPoint = new Point(hero.XCoord + Hero.Width + GameForm.OffsetBeforeStick, hero.YCoord + Hero.Height);
            stick.EndingPoint = new Point(hero.XCoord + Hero.Width + GameForm.OffsetBeforeStick, hero.YCoord + Hero.Height);
            stick.SetAngle(-90);
        }

        public void MoveHero()
        {
            hero.Move();
            // Movement on a stick
            if (hero.XCoord > GetStick().StartingPoint.X - Hero.Width && hero.XCoord < GetStick().EndingPoint.X)
                hero.YCoord = GetHeroPlatform().Position.Y - Stick.Width / 2 - Hero.Height;
            else
                hero.YCoord = GetHeroPlatform().Position.Y - Hero.Height;

            if (!GetStick().IsOnPlatform(GetAnotherPlatform()) &&
                hero.XCoord >= GetHeroPlatform().Position.X + GetHeroPlatform().Width
                && hero.XCoord < GetAnotherPlatform().Position.X)
            {
                hero.XCoord = GetHeroPlatform().Position.X + GetHeroPlatform().Width;
                SetState(Game.GameState.HeroFalling);
            }

            if (GetStick().IsOnPlatform(GetAnotherPlatform()))
            {
                if (hero.XCoord >= GetAnotherPlatform().Position.X)
                    GetStick().Delete();

                CheckHeroEndPoint();
            }
        }

        private void CheckHeroEndPoint()
        {
            if (hero.XCoord >= GetAnotherPlatform().Position.X + GetAnotherPlatform().Width - Hero.Width - Stick.Width - GameForm.OffsetBeforeStick)
            {
                hero.XCoord = GetAnotherPlatform().Position.X + GetAnotherPlatform().Width - Hero.Width - Stick.Width - GameForm.OffsetBeforeStick;
                SetState(Game.GameState.BoardMoving);
            }
        }

        public void MoveBoard()
        {
            hero.XCoord -= GameSpeed;
            heroPlatform.Position.X -= GameSpeed;
            anotherPlatform.Position.X -= GameSpeed;
            doubleScorePlatform.Position.X -= GameSpeed;
            if (hero.XCoord <= 50)
            {
                while (hero.XCoord < 50)
                {
                    hero.XCoord++;
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
