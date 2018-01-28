namespace YouWillExplode.Utility
{
    using Microsoft.Xna.Framework;

    internal sealed class SceneManager
    {
        private Scene activeScene;

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
                    this.activeScene.Load();
                }
            }
        }

        public void Draw(GameTime gameTime) =>
            this.activeScene.Draw(gameTime);

        public void Update(GameTime gameTime) =>
            this.activeScene.Update(gameTime);
    }
}