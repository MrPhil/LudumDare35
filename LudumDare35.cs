using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MrPhilGames
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LudumDare35 : Game
    {
        GraphicsDeviceManager graphics;
        const int Width = 800;
        const int Height = 600;

        GroundTile[,] groundTiles = new GroundTile[Width, Height];

        SpriteBatch spriteBatch;
        Texture2D bear;
        public Texture2D man;
        Texture2D grass;

        public Player player;
        bool transforming = true;
        double transformTimer;

        Random random = new Random((int)(1972 * DateTime.Now.Ticks));

        public LudumDare35()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Width;
            graphics.PreferredBackBufferHeight = Height;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            player.Position = new Vector2(100, 100);
            Global.Pixel = new Texture2D(graphics, 1, 1);
            Global.Pixel.SetData<Color>(new Color[] { Color.White });// fill the texture with white

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
            bear = Content.Load<Texture2D>("bear");
            player.Texture = man = Content.Load<Texture2D>("man");
            grass = Content.Load<Texture2D>("grass");

            GroundTile groundTile;
            for (int x = 0; x < Width; x += 32)
            {
                for (int y = 0; y < Height; y += 32)
                {
                    groundTile = groundTiles[x, y] = new GroundTile(x, y);
                    groundTile.Texture = grass;
                    groundTile.Lit = random.Next(5) < 4;
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            // Update timers
            if (transforming)
            {
                if (transformTimer < gameTime.TotalGameTime.TotalSeconds)
                {
                    transforming = false;
                }
            }

            // TODO: Add your update logic here

            // Did we step into the light (and vica-versa?)
            if (transforming == false)
            {
                // TODO: MAybe Optimize around the player's position?
                GroundTile groundTile;
                for (int x = 0; x < Width; x += 32)
                {
                    for (int y = 0; y < Height; y += 32)
                    {
                        groundTile = groundTiles[x, y];

                        if (groundTile.PositionAsRect.Intersects(player.PositionAsRect) &&
                            ((player.IsMan && !groundTile.Lit) ||
                            (!player.IsMan && groundTile.Lit)))
                        {
                            StartPlayerTransformation(gameTime);
                        }
                    }
                }
            }



            KeyboardState keyboard = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                player.Position.X += -1;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                player.Position.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                player.Position.Y += -1;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                player.Position.Y += 1;
            }
            if (keyboard.IsKeyDown(Keys.X) &&
                transforming == false)
            {
                StartPlayerTransformation(gameTime);
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            // Grass first
            GroundTile groundTile;
            for (int x = 0; x < Width; x += 32)
            {
                for (int y = 0; y < Height; y += 32)
                {
                    groundTile = groundTiles[x, y];
                    spriteBatch.Draw(groundTile.Texture, groundTile.Position,
                        groundTile.Lit ? Color.White : Color.Gray);
                }
            }

            spriteBatch.Draw(player.Texture, player.Position, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void StartPlayerTransformation(GameTime gameTime)
        {
            transforming = true;
            transformTimer = gameTime.TotalGameTime.TotalSeconds + 1.0;

            if (player.Texture == man)
            {
                player.Texture = bear;
            }
            else
            {
                player.Texture = man;
            }
        }
    }
}
