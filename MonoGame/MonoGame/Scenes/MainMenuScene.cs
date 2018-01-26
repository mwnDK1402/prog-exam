namespace MonoGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class MainMenuScene : Scene
    {
        private Button playButton;

        public MainMenuScene()
        {
        }

        public override void Draw(GameTime gameTime)
        {
            this.playButton.Draw(gameTime);
        }

        public override void Load()
        {
            var buttonSize = new Point(128, 32);
            this.playButton = new Button(
                new Rectangle(
                    new Point(
                        (this.ScreenManager.ScreenWidth - buttonSize.X) / 2,
                        (this.ScreenManager.ScreenHeight - buttonSize.Y) / 2),
                    buttonSize),
                4,
                this.OnPlayPressed,
                this.Content.Load<Texture2D>("PlayButtonPressed"),
                this.Content.Load<Texture2D>("PlayButtonReleased"),
                this.InputManager);

            this.playButton.Initialize(this.SpriteBatch);
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.playButton.Update(gameTime);
        }

        private void OnPlayPressed()
        {
            this.SceneManager.ActiveScene = new GameScene();
        }
    }
}