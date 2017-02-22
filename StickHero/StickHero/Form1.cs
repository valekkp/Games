using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StickHero
{
    public partial class GameForm : Form
    {
        public const int ClientWidth = 600;
        public const int ClientHeight = 600;
        
        public const int OffsetBeforeStick = 10;

        private Brush scoreBrush = Brushes.Crimson;

        //private string startMessage = "Press and hold SPACE to grow stick, release to drop it";
        
        private Game game = new Game();

        public GameForm()
        {
            InitializeComponent();
            SetElementsParameters();
            game.Start();
            
            timer.Interval = 1;
            game.Restart();
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

        private void PlayingBoardPictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawElements(e.Graphics);
        }

        private Brush platformBrush = Brushes.Black;
        private Brush doubleScoreBrush = Brushes.Red;

        private void DrawElements(Graphics graphics)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;

            Point point1 = new Point(0, 0);
            Point point2 = new Point(PlayingBoardPictureBox.Width, PlayingBoardPictureBox.Height);
            LinearGradientBrush gradiendBackground = new LinearGradientBrush(point1, point2, Color.White, Color.DodgerBlue);

            graphics.FillRectangle(gradiendBackground, 0, 0, PlayingBoardPictureBox.Width, PlayingBoardPictureBox.Height);

            graphics.DrawString(game.GetScore().ToString(), new Font(FontFamily.GenericMonospace, 40), scoreBrush, PlayingBoardPictureBox.Width/2 - 20, 50);

            game.GetStick().Draw(graphics);
            game.GetHero().Draw(graphics);
            game.GetHeroPlatform().Draw(graphics, platformBrush);
            game.GetAnotherPlatform().Draw(graphics, platformBrush);
            game.GetDoubleScorePlatform().Draw(graphics, doubleScoreBrush);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (game.GetState())
            {
                case Game.GameState.Waiting:
                    GrowStick();
                    break;
                case Game.GameState.StickFalling:
                    DropStick();
                    break;
                case Game.GameState.HeroMoving:
                    MoveHero();
                    break;
                case Game.GameState.BoardMoving:
                    game.MoveBoard();
                    break;
                case Game.GameState.HeroFalling:
                    DropHero();
                    break;
            }
            PlayingBoardPictureBox.Refresh();
        }

        private void GrowStick()
        {
            if (isStickGrowing && game.GetState() == Game.GameState.Waiting)
                game.GetStick().Grow();
            if (game.GetStick().Length == Stick.MaxLength)
                game.SetState(Game.GameState.StickFalling);
        }

        private void DropStick()
        {
            game.GetStick().Drop();
            if (game.GetStick().GetAngle() == 0)
            {
                if (game.GetStick().IsOnPlatform(game.GetDoubleScorePlatform()))
                    game.SetScore(game.GetScore() + 1);
                if (game.GetStick().IsOnPlatform(game.GetAnotherPlatform()))
                    game.SetScore(game.GetScore() + 1);
                game.SetState(Game.GameState.HeroMoving);
            }
        }

        private void MoveHero()
        {
            game.MoveHero();

            Platform platform = game.GetAnotherPlatform();
            int expectedPosition = platform.Position.X + platform.Width - Stick.Width - Hero.Width - OffsetBeforeStick;
            if (game.GetHero().Position.X >= expectedPosition)
            {
                game.SetState(Game.GameState.BoardMoving);
            }
        }

        private void DropHero()
        {
            game.GetHero().Drop();
            if (game.GetHero().Position.Y >= ClientHeight)
                game.Restart();
        }

        private bool isStickGrowing;
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                isStickGrowing = true;
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if(game.GetState() == Game.GameState.Waiting)
                    game.SetState(Game.GameState.StickFalling);
                isStickGrowing = false;
            }
        }
    }
}
