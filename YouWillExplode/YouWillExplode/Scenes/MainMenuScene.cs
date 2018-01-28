namespace YouWillExplode
{
    using System;
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class MainMenuScene : Scene
    {
        private VerticalLayout buttonLayout;

        public MainMenuScene(YouWillExplode game) : base(game)
        {
        }

        protected override void OnLoad()
        {
            var buttonResources = new Button.Resources()
            {
                PressedTexture = this.Game.Content.Load<Texture2D>("ButtonPressed"),
                ReleasedTexture = this.Game.Content.Load<Texture2D>("ButtonReleased"),
                Font = this.Game.Content.Load<SpriteFont>("ButtonFont")
            };

            int margin = 4;

            var buttonSize = new Vector2(128, 32);

            var playButton = new Button(
                buttonSize,
                "Play",
                () => this.Game.SceneManager.ActiveScene = new GameScene(this.Game),
                buttonResources)
            {
                Margin = margin
            };

            var settingsButton = new Button(
                buttonSize,
                "Settings",
                () => this.Game.SceneManager.ActiveScene = new SettingsMenuScene(this.Game),
                buttonResources)
            {
                Margin = margin
            };

            var quitButton = new Button(
                buttonSize,
                "Quit",
                () => this.Game.Exit(),
                buttonResources)
            {
                Margin = margin
            };

            this.buttonLayout = new VerticalLayout(this.Game.ScreenManager)
            {
                Alignment = Alignment.Middle,
                MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center,
                Spacing = 20
            };

            this.buttonLayout.Items.Add(playButton);
            this.buttonLayout.Items.Add(settingsButton);
            this.buttonLayout.Items.Add(quitButton);

            this.Manage(playButton);
            this.Manage(settingsButton);
            this.Manage(quitButton);

            this.Manage(this.buttonLayout);
        }

        protected override void OnUpdated(GameTime gameTime)
        {
            double t = gameTime.TotalGameTime.TotalSeconds;

            this.buttonLayout.MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center + new Point(
                (int)(Math.Cos(t) * 10.0),
                (int)(Math.Sin(t) * 10.0));

            this.buttonLayout.Spacing = (int)((Math.Sin(t) + 1) * 10.0);
        }
    }
}