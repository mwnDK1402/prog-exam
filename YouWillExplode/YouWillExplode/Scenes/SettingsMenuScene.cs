namespace YouWillExplode
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class SettingsMenuScene : Scene
    {
        private Button backButton;

        public SettingsMenuScene(YouWillExplode game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            this.backButton.Draw(gameTime);
        }

        public override void Load()
        {
            this.backButton = new Button(
                new Rectangle(8, this.Game.ScreenManager.ScreenHeight - 40, 92, 32),
                this.OnBackPressed,
                this.Game.Content.Load<Texture2D>("BackButtonPressed"),
                this.Game.Content.Load<Texture2D>("BackButtonReleased"),
                this.Game.InputManager);

            this.backButton.Initialize(this.Game.SpriteBatch);
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.backButton.Update(gameTime);
        }

        private void OnBackPressed()
        {
            this.Game.SceneManager.ActiveScene = new MainMenuScene(this.Game);
        }
    }
}