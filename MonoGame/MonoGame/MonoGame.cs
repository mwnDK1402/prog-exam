namespace MonoGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MonoGame : Game
    {
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private MovingText text;

        public MonoGame()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            //// TODO: Add your drawing code here
            this.spriteBatch.Begin();

            this.text.Draw();

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //// TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.text = new MovingText(
                this.Content.Load<SpriteFont>("MainFont"),
                this.graphics.GraphicsDevice,
                this.spriteBatch,
                new Vector2(0f, 0f),
                Vector2.One * 100f,
                "Hello, World!",
                Color.Red);

            //// TODO: use this.Content to load your game content here
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
            {
                this.Exit();
            }

            //// TODO: Add your update logic here
            this.text.Update(gameTime);

            base.Update(gameTime);
        }

        private class MovingText
        {
            private Color color;
            private SpriteFont font;
            private GraphicsDevice graphics;
            private Vector2 position, size, velocity;
            private SpriteBatch spriteBatch;

            public MovingText(
                SpriteFont font,
                GraphicsDevice graphics,
                SpriteBatch spriteBatch,
                Vector2 position,
                Vector2 velocity,
                string content)
            {
                this.font = font;
                this.graphics = graphics;
                this.spriteBatch = spriteBatch;
                this.position = position;
                this.velocity = velocity;
                this.Content = content;
                this.Color = Color.Black;
            }

            public MovingText(
                SpriteFont font,
                GraphicsDevice graphics,
                SpriteBatch spriteBatch,
                Vector2 position,
                Vector2 velocity,
                string content,
                Color color)
                : this(font, graphics, spriteBatch, position, velocity, content)
            {
                this.Color = color;
            }

            public Color Color
            {
                get
                {
                    return this.color;
                }

                set
                {
                    this.color = value;
                    this.UpdateSize();
                }
            }

            public string Content { get; set; }

            public void Draw()
            {
                this.spriteBatch.DrawString(this.font, this.Content, this.position, this.Color);
            }

            public void Update(GameTime gameTime)
            {
                var screenSize = new Vector2(
                    this.graphics.Viewport.Width,
                    this.graphics.Viewport.Height);

                var space = screenSize - (this.position + this.size);

                var appliedVel = this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (this.position.X < 0f)
                {
                    appliedVel.X = -this.position.X;
                    this.velocity.X = -this.velocity.X;
                }
                else if (space.X < 0)
                {
                    appliedVel.X = space.X;
                    this.velocity.X = -this.velocity.X;
                }

                if (this.position.Y < 0f)
                {
                    appliedVel.Y = -this.position.Y;
                    this.velocity.Y = -this.velocity.Y;
                }
                else if (space.Y < 0)
                {
                    appliedVel.Y = space.Y;
                    this.velocity.Y = -this.velocity.Y;
                }

                this.position += appliedVel;
            }

            private void UpdateSize()
            {
                this.size = this.font.MeasureString(this.Content);
            }
        }
    }
}