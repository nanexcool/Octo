using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Octo.Engine;

namespace Octo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState state;
        KeyboardState oldState;

        bool paused = false;

        Room room;
        Entity player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Util.Initialize(this);

            room = new Room(24, 14);

            player = new Entity()
            {
                Texture = Content.Load<Texture2D>("Octocat")
            };

            player.Width = player.Texture.Width;
            player.Height = player.Texture.Height;

            room.Entities.Add(player);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            oldState = state;
            state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.Escape))
                this.Exit();

            if (state.IsKeyDown(Keys.P) && oldState.IsKeyUp(Keys.P))
            {
                paused = !paused;
            }

            if (!paused)
            {
                Vector2 d = Vector2.Zero;
                if (state.IsKeyDown(Keys.A))
                {
                    d.X = -1;
                }
                if (state.IsKeyDown(Keys.E))
                {
                    d.X = 1;
                }
                if (state.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
                {
                    player.Jump();
                }

                player.Move(d);

                room.Update(elapsed);
            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            room.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
