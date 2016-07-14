using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace StickHero
{
    public partial class GameForm : Form
    {
        private enum GameState
        {
            Waiting,
            StickFalling,
            HeroMoving,
            BoardMoving,
            HeroFalling
        }

        const int ClientWidth = 600;
        const int ClientHeight = 600;

        const int HeroWidth = 30;
        const int HeroHeight = 50;
        const int StickWidth = 10;

        const int OffsetBeforeStick = 10;

        const int DoubleScorePlatformWidth = 15;
        const int DoubleScorePlatformHeight = 5;

        const int GameSpeed = 3;
        
        private Pen stickPen = new Pen(Color.DarkBlue, StickWidth);
        private Brush heroBrush = Brushes.Brown;
        private Brush platformBrush = Brushes.Black;
        private Brush doubleScoreBrush = Brushes.Red;
        private Brush scoreBrush = Brushes.Crimson;

        private Platform heroPlatform;
        private Platform anotherPlatform;
        private Platform doubleScorePlatform;
        private Random random = new Random();

        private int heroXCoord;
        private int heroYCoord;
        private Point heroPositionOnPlatform;
        private Point heroStartingPositionOnPlatform;

        private int stickLength = 0;
        private int stickXOffset = 0;

        private int score;

        private Point startingStickPoint;
        private Point endStickPoint;

        private bool isPressed = false;
        private bool isStickOnPlatform;
        private bool isStickOnDoubleScorePlatform;
        
        private GameState gameState;

        public GameForm()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            heroPlatform = new Platform(100, 100, 0, ClientHeight - 100);
            SetElementsParameters();
            timer.Interval = 1;
            Restart();
            timer.Start();
            PlayingBoardPictureBox.Refresh();
        }

        private void SetElementsParameters()
        {
            PlayingBoardPictureBox.Location = new Point(0, 0);
            PlayingBoardPictureBox.Size = new Size(ClientWidth, ClientHeight);
            ClientSize = new Size(ClientWidth, ClientHeight);
            DesktopLocation = new Point(0, 0);
        }

        private void DrawElements(Graphics graphics)
        {
            graphics.FillRectangle(platformBrush, 
                                    heroPlatform.Position.X, anotherPlatform.Position.Y, 
                                    heroPlatform.Width, heroPlatform.Height);

            graphics.FillRectangle(platformBrush,
                                    anotherPlatform.Position.X, anotherPlatform.Position.Y,
                                    anotherPlatform.Width, heroPlatform.Height);

            graphics.FillRectangle(heroBrush,
                                heroXCoord, heroYCoord,
                                HeroWidth, HeroHeight);

            graphics.FillRectangle(doubleScoreBrush,
                                    doubleScorePlatform.Position.X, doubleScorePlatform.Position.Y,
                                    DoubleScorePlatformWidth, DoubleScorePlatformHeight);

            graphics.DrawLine(stickPen, startingStickPoint, endStickPoint);

            graphics.DrawString(score.ToString(), new Font(FontFamily.GenericMonospace, 40), scoreBrush, PlayingBoardPictureBox.Width/2 - 20, 50);
        }

        private void PlayingBoardPictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawElements(e.Graphics);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Waiting:
                    if(isPressed) GrowStick();
                    break;
                case GameState.StickFalling:
                    DropStick();
                    break;
                case GameState.HeroMoving:
                    MoveHero();
                    break;
                case GameState.BoardMoving:
                    MoveBoard();
                    break;
                case GameState.HeroFalling:
                    DropHero();
                    break;
            }
            PlayingBoardPictureBox.Refresh();
        }

        private void GrowStick()
        {
            endStickPoint.Y -= GameSpeed;
            stickLength = startingStickPoint.Y - endStickPoint.Y;
            if (stickLength >= ClientWidth - startingStickPoint.X)
            {
                stickLength = ClientWidth - startingStickPoint.X;
                isPressed = false;
                gameState = GameState.StickFalling;
            }
        }

        private void DropStick() //todo: Рефакторинг, если возможно
        {
            stickXOffset += GameSpeed;
            if (stickXOffset >= stickLength)
            {
                stickXOffset = stickLength;
                gameState = GameState.HeroMoving;
            }
            endStickPoint.X = startingStickPoint.X + stickXOffset;
            endStickPoint.Y = startingStickPoint.Y - (int)Sqrt(Pow(stickLength, 2) - Pow(stickXOffset, 2));
            if (endStickPoint.X >= anotherPlatform.Position.X && 
                endStickPoint.X <= anotherPlatform.Position.X + anotherPlatform.Width)
                isStickOnPlatform = true;
            else
            {
                isStickOnPlatform = false;
            }
            if (endStickPoint.X >= doubleScorePlatform.Position.X &&
                endStickPoint.X <= doubleScorePlatform.Position.X + doubleScorePlatform.Width)
                isStickOnDoubleScorePlatform = true;
            else
            {
                isStickOnDoubleScorePlatform = false;
            }
        }

        private void MoveHero() //todo: Рефакторинг кода, если возможно
        {
            heroXCoord += GameSpeed;
            if (!isStickOnPlatform &&
                heroXCoord >= heroPlatform.Position.X + heroPlatform.Width)
            {
                heroXCoord = heroPlatform.Position.X + heroPlatform.Width;
                gameState = GameState.HeroFalling;
            }
            if (isStickOnPlatform)
            {
                // Путь по палке
                if (heroXCoord > startingStickPoint.X - HeroWidth && heroXCoord < anotherPlatform.Position.X)
                    heroYCoord = heroPlatform.Position.Y - StickWidth/2 - HeroHeight;
                else
                    heroYCoord = heroPlatform.Position.Y - HeroHeight;

                if (heroXCoord >= anotherPlatform.Position.X)
                    DeleteStick();

                if (heroXCoord >=
                    anotherPlatform.Position.X + anotherPlatform.Width - HeroWidth - StickWidth - OffsetBeforeStick)
                {
                    while (heroXCoord >
                           anotherPlatform.Position.X + anotherPlatform.Width - HeroWidth - StickWidth -
                           OffsetBeforeStick)
                        heroXCoord--;
                    gameState = GameState.BoardMoving;
                    score++;
                    if (isStickOnDoubleScorePlatform)
                        score++;
                    isStickOnPlatform = false;
                    isStickOnDoubleScorePlatform = false;
                }
            }
        }

        private void DeleteStick()
        {
            endStickPoint.X = startingStickPoint.X;
            endStickPoint.Y = startingStickPoint.Y;
        }

        private void MoveBoard()
        {
            heroXCoord -= GameSpeed;
            heroPlatform.Position.X -= GameSpeed;
            anotherPlatform.Position.X -= GameSpeed;
            doubleScorePlatform.Position.X -= GameSpeed;
            if (heroXCoord <= 50)
            {
                while (heroXCoord < 50)
                {
                    heroXCoord++;
                    heroPlatform.Position.X++;
                    anotherPlatform.Position.X++;
                    doubleScorePlatform.Position.X++;
                }
                CreateNewPlatform(true);
                gameState = GameState.Waiting;
            }
        }

        private void DropHero()
        {
            stickXOffset -= GameSpeed * 3;
            if (stickXOffset <= OffsetBeforeStick)
            {
                stickXOffset = OffsetBeforeStick;
            }
            endStickPoint.X = startingStickPoint.X + stickXOffset;
            endStickPoint.Y = startingStickPoint.Y + (int)Sqrt(Pow(stickLength, 2) - Pow(stickXOffset, 2));

            heroYCoord += GameSpeed;
            if (heroYCoord >= ClientHeight)
                Restart();
        }

        private void Restart()
        {
            gameState = GameState.Waiting;
            score = 0;
            stickXOffset = 0;
            CreateNewPlatform(false);
            heroXCoord = heroPlatform.Position.X + heroPlatform.Width - HeroWidth - StickWidth - OffsetBeforeStick;
            heroYCoord = ClientHeight - heroPlatform.Height - HeroHeight;
            startingStickPoint = new Point(heroXCoord + HeroWidth + OffsetBeforeStick, heroYCoord + HeroHeight);
            endStickPoint = new Point(heroXCoord + HeroWidth + OffsetBeforeStick, heroYCoord + HeroHeight);
            PlayingBoardPictureBox.Refresh();
        }

        private void CreateNewPlatform(bool isHeroPlatformChanged)
        {
            if(isHeroPlatformChanged)
                heroPlatform = new Platform(anotherPlatform.Width, anotherPlatform.Height,
                                            anotherPlatform.Position.X, anotherPlatform.Position.Y);
            int randomWidth = random.Next(50, 151);
            anotherPlatform = new Platform(randomWidth, heroPlatform.Height,
                                    random.Next(heroPlatform.Position.X + heroPlatform.Width + 10, ClientWidth - randomWidth), ClientHeight - heroPlatform.Height);
            doubleScorePlatform = new Platform(DoubleScorePlatformWidth, DoubleScorePlatformHeight, 
                                    anotherPlatform.Position.X + anotherPlatform.Width / 2 - DoubleScorePlatformWidth / 2, anotherPlatform.Position.Y);
            startingStickPoint = new Point(heroXCoord + HeroWidth + OffsetBeforeStick, heroYCoord + HeroHeight);
            endStickPoint = new Point(heroXCoord + HeroWidth + OffsetBeforeStick, heroYCoord + HeroHeight);
            stickXOffset = 0;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && gameState == GameState.Waiting)
            {
                if (!isPressed)
                    isPressed = true;
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (gameState == GameState.Waiting)
            {
                gameState = GameState.StickFalling;
                isPressed = false;
            }
        }
    }
}
