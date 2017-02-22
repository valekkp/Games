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
        
        private Brush doubleScoreBrush = Brushes.Red;
        private Brush scoreBrush = Brushes.Crimson;

        private Random random = new Random();

        public bool isPressed = false;
        private bool isStickOnPlatform;
        private bool isStickOnDoubleScorePlatform;
        private bool isStarted = false;

        private string startMessage = "Press and hold SPACE to grow stick, release to drop it";
        
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

        private void DrawElements(Graphics graphics)
        {
            LinearGradientBrush gradiendBackground = new LinearGradientBrush(new Point(0, 0), new Point(PlayingBoardPictureBox.Width, PlayingBoardPictureBox.Height), Color.White, Color.DodgerBlue);
            graphics.FillRectangle(gradiendBackground, 0, 0, PlayingBoardPictureBox.Width, PlayingBoardPictureBox.Height);

                graphics.DrawString(game.GetScore().ToString(), new Font(FontFamily.GenericMonospace, 40), scoreBrush, PlayingBoardPictureBox.Width/2 - 20, 50);
        }

        private void PlayingBoardPictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawElements(e.Graphics);
            game.GetStick().Draw(e.Graphics);
            game.GetHero().Draw(e.Graphics);
            game.GetHeroPlatform().Draw(e.Graphics, Brushes.Black);
            game.GetAnotherPlatform().Draw(e.Graphics, Brushes.Black);
            game.GetDoubleScorePlatform().Draw(e.Graphics, Brushes.Red);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (game.GetState())
            {
                case Game.GameState.Waiting:

                    Console.WriteLine("WAITING");
                    if (growStick && game.GetState() == Game.GameState.Waiting)
                        game.GetStick().Grow();
                    if(game.GetStick().length == Stick.MaxLength)
                        game.SetState(Game.GameState.StickFalling);
                    break;

                case Game.GameState.StickFalling:

                    Console.WriteLine("STICKFALLING");
                    game.GetStick().Drop();
                    if (game.GetStick().GetAngle() == 0)
                    {
                        if(game.GetStick().IsOnPlatform(game.GetDoubleScorePlatform()))
                            game.SetScore(game.GetScore() + 1);
                        if(game.GetStick().IsOnPlatform(game.GetAnotherPlatform()))
                            game.SetScore(game.GetScore() + 1);
                        game.SetState(Game.GameState.HeroMoving);
                    }

                    break;

                case Game.GameState.HeroMoving:

                    Console.WriteLine("MOVINGHERO: " + game.GetHero().XCoord);
                    game.MoveHero();
                        
                    if (game.GetHero().XCoord >= game.GetAnotherPlatform().Position.X + game.GetAnotherPlatform().Width - Stick.Width - Hero.Width - GameForm.OffsetBeforeStick)
                    {
                        game.SetState(Game.GameState.BoardMoving);
                    }
                    break;

                case Game.GameState.BoardMoving:

                    Console.WriteLine("BOARDMOVING");
                    game.MoveBoard();
                    break;

                case Game.GameState.HeroFalling:

                    Console.WriteLine("HEROFALLING");
                    game.GetHero().Drop();
                    if (game.GetHero().YCoord >= GameForm.ClientHeight)
                        game.Restart();
                    break;
            }
            PlayingBoardPictureBox.Refresh();
        }


        private bool growStick = false;
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                growStick = true;
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if(game.GetState() == Game.GameState.Waiting)
                    game.SetState(Game.GameState.StickFalling);
                growStick = false;
            }
        }
    }
}
