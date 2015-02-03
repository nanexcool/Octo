using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Octo.Engine
{
    class Entity
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position 
        {
            get { return position; }
            set { position = value; }
        }
        public Color Color { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle DrawRect
        {
            get { return new Rectangle((int)position.X, (int)position.Y, Width, Height); }
        }

        public bool OnGround = false;
        public bool IsJumping = false;

        protected Vector2 position;
        protected Vector2 oldVelocity;
        protected Vector2 velocity;
        protected Vector2 acceleration;

        protected Vector2 gravity = new Vector2(0, 1000);
        
        protected float friction = 0.9f;

        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X + 25, (int)Position.Y + 10, Width - 50, Height - 20);
            }
        }

        public Entity()
        {
            Texture = Util.Texture;
            Position = Vector2.Zero;
            Color = Color.White;

            Width = 32;
            Height = 64;
        }

        public virtual void Update(float elapsed)
        {
            DoPhysics(elapsed);
        }

        

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DrawRect, Color);

            spriteBatch.Draw(Util.Texture, CollisionBox, Color.Red * 0.5f);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DrawRect.ToString());
            sb.AppendLine(CollisionBox.ToString());
            sb.AppendLine(velocity.ToString());
            sb.AppendLine(acceleration.ToString());
            sb.AppendLine("OnGround:" + OnGround.ToString() + " Jumping:" + IsJumping.ToString());
            spriteBatch.DrawString(Util.Font, sb, position, Color.Black);
        }

        public void Jump()
        {
            if (OnGround)
            {
                IsJumping = true;
            }
        }

        public void Move(Vector2 d)
        {
            acceleration = d * 400;
        }

        private void DoPhysics(float elapsed)
        {
            // Keep old velocity for Vertlet integration
            oldVelocity = velocity;
            
            // Add acceleration to velocity
            velocity += (acceleration + gravity) * elapsed;
            //velocity = (0.5f * (acceleration + gravity) * elapsed * elapsed) + (velocity * elapsed);

            velocity = Vector2.Clamp(velocity, new Vector2(-300, -1000), new Vector2(300, 1000));

            if (IsJumping && OnGround)
            {
                velocity.Y -= 600;
                OnGround = false;
            }

            if (OnGround)
            {
                velocity.Y = 0;
            }

            if (CollisionBox.Right >= 800)
            {
                //velocity.X = 0;
            }

            // Add integrated velocity to position
            position += (velocity + oldVelocity) * 0.5f * elapsed;
            // Ground friction
            if (acceleration.X == 0 && OnGround)
            {
                velocity.X *= 1 - elapsed / 0.1f;
            }

            if (Math.Abs(velocity.X) < 0.2f)
            {
                velocity.X = 0;
            }
            if (Math.Abs(velocity.Y) < 0.2f)
            {
                velocity.Y = 0;
            }
            
            if (CollisionBox.Bottom >= 450)
            {
                OnGround = true;
                IsJumping = false;
                velocity.Y = 0;
            }
        }
    }
}
