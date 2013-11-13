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

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(position.ToString());
            sb.AppendLine(velocity.ToString());
            sb.AppendLine(acceleration.ToString());
            spriteBatch.DrawString(Util.Font, sb, position, Color.Black);
        }

        public void Jump()
        {
            if (OnGround)
            {
                IsJumping = true;
            }
        }

        private void DoPhysics(float elapsed)
        {
            // Keep old velocity for Vertlet integration
            oldVelocity = velocity;
            
            // Add acceleration to velocity
            velocity += (acceleration + gravity) * elapsed;

            velocity = Vector2.Clamp(velocity, new Vector2(-200, -1000), new Vector2(200, 1000));

            if (IsJumping && OnGround)
            {
                velocity.Y -= 600;
                OnGround = false;
            }
            // Add integrated velocity to position
            position += (velocity + oldVelocity) * 0.5f * elapsed;
            
            if (acceleration.X == 0 && OnGround)
            {
                velocity.X *= 1 - elapsed / 0.3f;
            }


            if (Math.Abs(velocity.X) < 0.5f)
            {
                velocity.X = 0;
            }
            if (Math.Abs(velocity.Y) < 0.2f)
            {
                velocity.Y = 0;
            }

            position.X = MathHelper.Clamp(position.X, 0, 800 - Width);
            position.Y = MathHelper.Clamp(position.Y, 0, 480 - Height);

            if (position.Y == 480 - Height)
            {
                OnGround = true;
                IsJumping = false;
                velocity.Y = 0;
            }

            //// If entity is out of bounds, reverse velocity
            //if (position.X <= 0 || position.X >= 800 - Width)
            //{
            //    velocity.X *= -1;
            //}
            //if (position.Y <= 0 || position.Y >= 480 - Height)
            //{
            //    velocity.Y *= -1;
            //}
        }
    }
}
