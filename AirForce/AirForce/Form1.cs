using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public enum ShipType
{
    //Small,
    Middle,
    Big
}

namespace AirForce
{
    public partial class Form1 : Form
    {
        private List<Ship> ships = new List<Ship>();
        private List<Bullet> bullets = new List<Bullet>(); 
        private PlayerShip playerShip;
        private bool checkShooting;
        private int shootDelay;
        private bool checkPressedUp;
        private bool checkPressedDown;
        private int killsAmount;

        public Form1()
        {
            const int width = 800;
            const int height = 400;
            InitializeComponent();
            ClientSize = new Size(width, height + 100);
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(width, height);
            playerShip = new PlayerShip(new PointD(50, 300), 0, 3);
            playerHealthPointsLabel.Location = new Point(0, pictureBox1.Size.Height);
            playerHealthPointsLabel.Text = "HP: " + playerShip.HealthPoints;
            KillsAmountLabel.Location = new Point(0, pictureBox1.Size.Height + playerHealthPointsLabel.Height + 5);
            KillsAmountLabel.Text = "Kills: " + killsAmount;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Pen linePen = Pens.Black;
            e.Graphics.DrawLine(linePen, 0, pictureBox1.Size.Height - 1, pictureBox1.Width, pictureBox1.Size.Height - 1);
            foreach (Ship ship in ships)
            {
                ship.Draw(e.Graphics);
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(e.Graphics);
            }
            playerShip.Draw(e.Graphics);
        }

        private void Moving()
        {
            foreach (Ship ship in ships)
            {
                ship.Location.X -= (int)ship.Speed;
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Location.X += bullet.Speed;
            }
            ships.RemoveAll(ship => ship.Location.X <= -ship.Width);
            bullets.RemoveAll(bullet => bullet.Location.X >= pictureBox1.Width);
            pictureBox1.Refresh();
        }

        private void PlayerMove()
        {
            if (checkPressedUp == false && checkPressedDown == false)
                playerShip.Speed = 0;
            if (playerShip.Location.Y + (float) playerShip.Speed < pictureBox1.Height - playerShip.Height &&
                playerShip.Location.Y + (float) playerShip.Speed >= 0)
            {
                playerShip.Location.Y += (float) playerShip.Speed;
            }
            pictureBox1.Refresh();
        }

        private void PlayerShoot()
        {
            PointD currentPlayerLocation = new PointD(playerShip.Location.X + playerShip.Width, playerShip.Location.Y + playerShip.Height/2);
            bullets.Add(Bullet.CreateBullet(currentPlayerLocation, false));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Moving();
            PlayerMove();
            CheckCollision();
            if(playerShip.HealthPoints == 0)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show("You lost! =(", "You lost!");
                Restart();
            }

            if (checkShooting && shootDelay == 0)
                PlayerShoot();

            shootDelay++;

            if (shootDelay > 10)
                shootDelay = 0;

            foreach (Ship ship in ships)
            {
                if(ship.Shoot(playerShip) != null && ship.ShootDelay == 0)
                    bullets.Add(ship.Shoot(playerShip));
                ship.ShootDelay++;
                if (ship.ShootDelay > 40)
                    ship.ShootDelay = 0;
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ships.Add(Ship.CreateRandomShip(pictureBox1.Size.Width, pictureBox1.Size.Height));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                playerShip.Speed = -10;
                checkPressedUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                playerShip.Speed = 10;
                checkPressedDown = true;
            }
            if (e.KeyCode == Keys.Space)
                checkShooting = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                checkShooting = false;
            if (e.KeyCode == Keys.Up)
            {
                checkPressedUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                checkPressedDown = false;
            }
        }

        private void CheckCollision()
        {
            RectangleF playerRectangle = new RectangleF((float) playerShip.Location.X, (float) playerShip.Location.Y, playerShip.Width, playerShip.Height);
            foreach (Ship ship in ships)
            {
                RectangleF shipRectangle = new RectangleF((float) ship.Location.X, (float) ship.Location.Y, ship.Width, ship.Height);
                if (playerRectangle.IntersectsWith(shipRectangle) && ship.HealthPoints > 0)
                {
                    playerShip.HealthPoints--;
                    playerHealthPointsLabel.Text = "HP: " + playerShip.HealthPoints;
                    ship.HealthPoints = 0;
                }
                foreach (Bullet bullet in bullets.Where(bullet => !bullet.IsEnemyBullet))
                {
                    RectangleF bulletRectangle = new RectangleF((float) bullet.Location.X, (float) bullet.Location.Y, bullet.Width, bullet.Height);
                    if (shipRectangle.IntersectsWith(bulletRectangle) && ship.HealthPoints > 0)
                    {
                        ship.HealthPoints--;
                        if (ship.HealthPoints == 0)
                        {
                            killsAmount++;
                            KillsAmountLabel.Text = "Kills: " + killsAmount;
                        }
                        bullet.Used = true;
                    }
                }
                //bullets.RemoveAll(bulletInShip => bulletInShip.used);
            }
            ships.RemoveAll(ship => ship.HealthPoints == 0);

            foreach (Bullet bullet in bullets.Where(bullet => bullet.IsEnemyBullet))
            {
                RectangleF bulletRectangle = new RectangleF((float)bullet.Location.X, (float)bullet.Location.Y, bullet.Width, bullet.Height);
                if (playerRectangle.IntersectsWith(bulletRectangle))
                {
                    playerShip.HealthPoints--;
                    playerHealthPointsLabel.Text = "HP: " + playerShip.HealthPoints;
                    bullet.Used = true;
                }
            }
            bullets.RemoveAll(bulletInShip => bulletInShip.Used);
        }

        private void Restart()
        {
            ships.Clear();
            bullets.Clear();
            timer1.Start();
            timer2.Start();
            playerShip = new PlayerShip(new PointD(50, 300), 0, 3);
            killsAmount = 0;
            KillsAmountLabel.Text = "Kills: " + killsAmount;
            playerHealthPointsLabel.Text = "HP: " + playerShip.HealthPoints;
        }
    }
}
