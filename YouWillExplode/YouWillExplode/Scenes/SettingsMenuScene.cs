namespace YouWillExplode
{
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class SettingsMenuScene : Scene
    {
        public SettingsMenuScene(YouWillExplode game) : base(game)
        {
        }

        protected override void OnLoad()
        {
            var resources = new Button.Resources()
            {
                PressedTexture = this.Game.Content.Load<Texture2D>("ButtonPressed"),
                ReleasedTexture = this.Game.Content.Load<Texture2D>("ButtonReleased"),
                Font = this.Game.Content.Load<SpriteFont>("ButtonFont")
            };

            var backButton = new Button(
                new Rectangle(8, this.Game.ScreenManager.ScreenHeight - 40, 92, 32),
                "Back",
                () => this.Game.SceneManager.ActiveScene = new MainMenuScene(this.Game),
                resources);

            this.Manage(backButton);

            // Create layout
            var removeableButtonLayout = new VerticalLayout(this.Game.ScreenManager);
            this.Manage(removeableButtonLayout);

            // Populate layout with buttons
            for (int i = 0; i < 5; ++i)
            {
                RemoveableButton button = this.GetNewButton(resources);
                removeableButtonLayout.Items.Add(button);
                this.Manage(button);
            }

            // Set layout position
            removeableButtonLayout.MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center;
        }

        private RemoveableButton GetNewButton(Button.Resources resources)
        {
            var newButton =
                new RemoveableButton(
                    new Button(
                        new Vector2(92, 32),
                        "Test",
                        () => { },
                        resources),
                    resources)
                {
                    Spacing = 4
                };

            return newButton;
        }
    }
}