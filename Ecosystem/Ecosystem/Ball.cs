using System.Drawing;

namespace Ecosystem
{
    class Ball
    {
        private const float VelocityLimit = 5;

        private PointF position;
        private float mass;
        private PointF velocity;
        private PointF acceleration;

        public Ball(float x, float y, float m)
        {
            position.X = x;
            position.Y = y;
            velocity = new PointF(0, 0);
            acceleration = new PointF(0, 0);
            mass = m;
        }

        public PointF GetPosition()
        {
            return position;
        }

        public void ApplyForce(PointF force)
        {
            acceleration.X += force.X;
            acceleration.Y += force.Y;
        }

        public void Update()
        {
            velocity.X += acceleration.X;
            velocity.Y += acceleration.Y;

            if (velocity.X > VelocityLimit)
                velocity.X = VelocityLimit;

            if (velocity.Y > VelocityLimit)
                velocity.Y = VelocityLimit;

            position.X += velocity.X;
            position.Y += velocity.Y;

            if (position.X + mass/2 >= PlayingBoardForm.playingBoardWidth ||
                position.X - mass / 2 <= 0)
                velocity.X = -velocity.X;

            if (position.Y + mass/2 >= PlayingBoardForm.playingBoardHeight ||
                position.Y - mass / 2 <= 0)
                velocity.Y = -velocity.Y;

            acceleration.X = 0;
            acceleration.Y = 0;
        }

        private Brush ballBrush = Brushes.DarkBlue;
        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(ballBrush, position.X - mass / 2, position.Y - mass / 2, mass, mass);
        }
    }
}
