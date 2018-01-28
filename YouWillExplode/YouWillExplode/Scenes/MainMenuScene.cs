namespace YouWillExplode
{
    using System;
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class MainMenuScene : Scene
    {
        private VerticalLayout buttonLayout;
        private Button playButton, settingsButton, quitButton;

        public MainMenuScene(YouWillExplode game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            this.playButton.Draw(gameTime);
            this.settingsButton.Draw(gameTime);
            this.quitButton.Draw(gameTime);
        }

        public override void Load()
        {
            var buttonResources = new Button.Resources()
            {
                PressedTexture = this.Game.Content.Load<Texture2D>("ButtonPressed"),
                ReleasedTexture = this.Game.Content.Load<Texture2D>("ButtonReleased"),
                Font = this.Game.Content.Load<SpriteFont>("ButtonFont")
            };

            var buttonSize = new Vector2(128, 32);
            this.playButton = new Button(
                buttonSize,
                "Play",
                () => this.Game.SceneManager.ActiveScene = new GameScene(this.Game),
                buttonResources,
                this.Game.InputManager)
            {
                Margin = 4
            };

            this.playButton.Initialize(this.Game.SpriteBatch);

            this.settingsButton = new Button(
                buttonSize,
                "Settings",
                () => this.Game.SceneManager.ActiveScene = new SettingsMenuScene(this.Game),
                buttonResources,
                this.Game.InputManager)
            {
                Margin = 4
            };

            this.settingsButton.Initialize(this.Game.SpriteBatch);

            this.quitButton = new Button(
                buttonSize,
                "Quit",
                () => this.Game.Exit(),
                buttonResources,
                this.Game.InputManager)
            {
                Margin = 4
            };

            this.quitButton.Initialize(this.Game.SpriteBatch);

            this.buttonLayout = new VerticalLayout(this.Game.ScreenManager)
            {
                Alignment = Alignment.Middle,
                MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center,
                Spacing = 20
            };

            this.buttonLayout.Items.Add(this.playButton);
            this.buttonLayout.Items.Add(this.settingsButton);
            this.buttonLayout.Items.Add(this.quitButton);

            this.buttonLayout.Initialize();
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            this.playButton.Update(gameTime);
            this.settingsButton.Update(gameTime);
            this.quitButton.Update(gameTime);

            double t = gameTime.TotalGameTime.TotalSeconds;

            this.buttonLayout.MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center + new Point(
                (int)(Math.Cos(t) * 10.0),
                (int)(Math.Sin(t) * 10.0));

            this.buttonLayout.Spacing = (int)((Math.Sin(t) + 1) * 10.0);
        }
    }
}