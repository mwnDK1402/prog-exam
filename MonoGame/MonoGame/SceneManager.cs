using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Utility;

namespace MonoGame
{
    internal sealed class SceneManager
    {
        private Scene activeScene;
        private ContentManager contentManager;
        private InputManager inputManager;
        private ScreenManager screenManager;
        private SpriteBatch spriteBatch;

        public SceneManager(ContentManager content, SpriteBatch spriteBatch, InputManager inputManager, ScreenManager screenManager)
        {
            this.contentManager = content;
            this.inputManager = inputManager;
            this.screenManager = screenManager;
            this.spriteBatch = spriteBatch;
        }

        public Scene ActiveScene
        {
            set
            {
                if (this.activeScene != null)
                {
                    this.activeScene.Unload();
                }

                this.activeScene = value;

                if (this.activeScene != null)
                {
                    this.activeScene.Initialize(this.contentManager, this.inputManager, this, this.screenManager, this.spriteBatch);
                    this.activeScene.Load();
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            this.activeScene.Draw(gameTime);
        }

        public void Update(GameTime gameTime)
        {
            this.activeScene.Update(gameTime);
        }
    }
}