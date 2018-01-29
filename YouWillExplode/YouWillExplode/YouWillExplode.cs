namespace YouWillExplode
{
    using System;
    using System.Linq;
    using DatabaseContract;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using SimplePlugin;

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    internal sealed class YouWillExplode : Game
    {
        private GraphicsDeviceManager graphics;
        private IProfileDatabase profileDatabase;

        public YouWillExplode()
        {
            this.graphics = new GraphicsDeviceManager(this);
            this.Content.RootDirectory = "Content";
        }

        public InputManager InputManager { get; private set; }

        public ProfileManager ProfileManager { get; private set; }

        public PreferencesManager PreferencesManager { get; private set; }

        public SceneManager SceneManager { get; private set; }

        public ScreenManager ScreenManager { get; private set; }

        public SpriteBatch SpriteBatch { get; private set; }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.SpriteBatch.Begin();

            this.SceneManager.Draw(gameTime);

            this.SpriteBatch.End();

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
            this.InputManager = new InputManager();
            this.ScreenManager = new ScreenManager(this.GraphicsDevice);
            this.IsMouseVisible = true;

            try
            {
                this.profileDatabase =
                    GenericPluginLoader<IProfileDatabase>
                        .LoadPlugins(global::Utility.PathUtility.GetProcessDirectory() + "Plugins")
                        .Single();
            }
            catch (InvalidOperationException e)
            {
                throw new Exception("Plugin missing: IProfileDatabase", e);
            }

            this.PreferencesManager = new PreferencesManager();
            this.ProfileManager = new ProfileManager(this.profileDatabase, this.PreferencesManager);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            this.SceneManager = new SceneManager()
            {
                // Dependent on Content, spriteBatch, inputManager, and screenManager being initialized
                // This ambiguity is a downside of implicit dependencies through access to public properties
                ActiveScene = new MainMenuScene(this)
            };
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            this.ProfileManager.Save();
            this.PreferencesManager.Save();
            base.OnExiting(sender, args);
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
            // Global input
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //// TODO: Add your update logic here
            this.InputManager.Update(gameTime);
            this.SceneManager.Update(gameTime);
            this.ScreenManager.Update(gameTime);

            base.Update(gameTime);
        }
    }
}