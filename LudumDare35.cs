using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MrPhilGames
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LudumDare35 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bear;
        Texture2D man;

        Texture2D player;
        Vector2 playerPosition;
        bool transforming = true;
        double transformTimer;


        public LudumDare35()
        {
            graphics = new GraphicsDeviceManager(this);
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
            playerPosition = new Vector2(100, 100);

            player = man = Content.Load<Texture2D>("man");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update timers
            if (transforming)
            {
                if (transformTimer < gameTime.TotalGameTime.TotalSeconds)
                {
                    transforming = false;
                }
            }

            // TODO: Add your update logic here
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A))
            {
                playerPosition.X += -1;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                playerPosition.X += 1;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                playerPosition.Y += -1;
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                playerPosition.Y += 1;
            }
            if (keyboard.IsKeyDown(Keys.X) &&
                transforming == false )
            {
                transforming = true;
                transformTimer = gameTime.TotalGameTime.TotalSeconds + 1.0;

                if (player == man)
                {
                    player = bear;
                }
                else
                {
                    player = man;
                }
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
            spriteBatch.Draw(player, playerPosition, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
