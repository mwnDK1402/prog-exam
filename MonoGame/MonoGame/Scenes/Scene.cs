namespace MonoGame
{
    using global::MonoGame.Utility;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    internal abstract class Scene
    {
        protected Scene()
        {
        }

        protected ContentManager Content { get; private set; }

        protected InputManager InputManager { get; private set; }

        protected SceneManager SceneManager { get; private set; }

        protected ScreenManager ScreenManager { get; private set; }

        protected SpriteBatch SpriteBatch { get; private set; }

        public abstract void Draw(GameTime gameTime);

        public void Initialize(ContentManager content, InputManager inputManager, SceneManager sceneManager, ScreenManager screenManager, SpriteBatch spriteBatch)
        {
            this.Content = content;
            this.InputManager = inputManager;
            this.SceneManager = sceneManager;
            this.ScreenManager = screenManager;
            this.SpriteBatch = spriteBatch;
        }

        public abstract void Load();

        public abstract void Unload();

        public abstract void Update(GameTime gameTime);
    }
}