using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ball_Breaker
{
    public partial class Form1 : Form
    {
        public const int LineLength = 360;
        public const int Offset = 25;

        private Game game = new Game();

        public Form1()
        {
            InitializeComponent();
            SetParameters();
            GameBoard.Refresh();
        }

        private void SetParameters()
        {
            const int width = 600;
            const int height = 600;
            GameBoard.Size = new Size(width, height);
            //GameBoard.Width = width;
            //GameBoard.Height = height;
        }

        private void GameBoard_Paint(object sender, PaintEventArgs e)
        {
            //DrawLines(e.Graphics);
            DrawBalls(e.Graphics);
        }

        //private void DrawLines(Graphics graphics)
        //{
        //    Pen linePen = Pens.Black;
        //    for (int i = 0; i < 13; i++)
        //    {
        //        graphics.DrawLine(linePen, 0, Offset * i + 3 * i, LineLength, Offset * i + 3 * i);
        //        graphics.DrawLine(linePen, Offset * i + 3 * i, 0, Offset * i + 3 * i, LineLength);
        //    }
        //}

        private void DrawBalls(Graphics graphics)
        {
            foreach (var ball in game.GetBallsGroup().GetBalls())
            {
                ball.Draw(graphics);
            }
            GameBoard.Refresh();
        }
    }
}
