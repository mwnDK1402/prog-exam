namespace YouWillExplode
{
    using System;
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class MainMenuScene : Scene
    {
        private VerticalLayout buttonLayout;
        private Button playButton, settingsButton;

        public MainMenuScene(YouWillExplode game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            this.playButton.Draw(gameTime);
            this.settingsButton.Draw(gameTime);
        }

        public override void Load()
        {
            var buttonSize = new Point(128, 32);
            this.playButton = new Button(
                new Rectangle(
                    new Point(0, 0),
                    buttonSize),
                4,
                this.OnPlayPressed,
                this.Game.Content.Load<Texture2D>("PlayButtonPressed"),
                this.Game.Content.Load<Texture2D>("PlayButtonReleased"),
                this.Game.InputManager);

            this.playButton.Initialize(this.Game.SpriteBatch);

            this.settingsButton = new Button(
                new Rectangle(
                    new Point(0, 0),
                    buttonSize),
                4,
                this.OnSettingsPressed,
                this.Game.Content.Load<Texture2D>("SettingsButtonPressed"),
                this.Game.Content.Load<Texture2D>("SettingsButtonReleased"),
                this.Game.InputManager);

            this.settingsButton.Initialize(this.Game.SpriteBatch);

            this.buttonLayout = new VerticalLayout(this.Game.ScreenManager)
            {
                Alignment = LayoutAlignment.Middle,
                MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center,
                Spacing = 20
            };

            this.buttonLayout.Items.Add(this.playButton);
            this.buttonLayout.Items.Add(this.settingsButton);

            this.buttonLayout.Initialize();
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.playButton.Update(gameTime);
            this.settingsButton.Update(gameTime);

            double t = gameTime.TotalGameTime.TotalSeconds;

            this.buttonLayout.MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center + new Point(
                (int)(Math.Cos(t) * 10.0),
                (int)(Math.Sin(t) * 10.0));

            this.buttonLayout.Spacing = (int)((Math.Sin(t) + 1) * 10.0);
        }

        private void OnPlayPressed()
        {
            this.Game.SceneManager.ActiveScene = new GameScene(this.Game);
        }

        private void OnSettingsPressed()
        {
            this.buttonLayout.Items.Remove(this.settingsButton);
            ////throw new NotImplementedException();
        }
    }
}