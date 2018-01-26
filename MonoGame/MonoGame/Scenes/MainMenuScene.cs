namespace MonoGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class MainMenuScene : Scene
    {
        private Button playButton;

        public MainMenuScene(MonoGame game) : base(game)
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
                        (this.Game.ScreenManager.ScreenWidth - buttonSize.X) / 2,
                        (this.Game.ScreenManager.ScreenHeight - buttonSize.Y) / 2),
                    buttonSize),
                4,
                this.OnPlayPressed,
                this.Game.Content.Load<Texture2D>("PlayButtonPressed"),
                this.Game.Content.Load<Texture2D>("PlayButtonReleased"),
                this.Game.InputManager);

            this.playButton.Initialize(this.Game.SpriteBatch);
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
            this.Game.SceneManager.ActiveScene = new GameScene(this.Game);
        }
    }
}