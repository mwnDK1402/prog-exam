namespace YouWillExplode
{
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class SettingsMenuScene : Scene
    {
        private Button.Resources buttonResources;
        private VerticalLayout removeableButtonLayout;

        public SettingsMenuScene(YouWillExplode game) : base(game)
        {
        }

        protected override void OnLoad()
        {
            this.buttonResources = new Button.Resources()
            {
                PressedTexture = this.Game.Content.Load<Texture2D>("ButtonPressed"),
                ReleasedTexture = this.Game.Content.Load<Texture2D>("ButtonReleased"),
                Font = this.Game.Content.Load<SpriteFont>("ButtonFont")
            };

            var backButton = new Button(
                new Rectangle(8, this.Game.ScreenManager.ScreenHeight - 40, 92, 32),
                "Back",
                () => this.Game.SceneManager.ActiveScene = new MainMenuScene(this.Game),
                this.buttonResources);

            this.Manage(backButton);

            var createButtonButton = new Button(
                new Rectangle(),
                "+",
                this.AddNewButton,
                this.buttonResources);

            this.Manage(createButtonButton);

            // Create layout
            this.removeableButtonLayout = new VerticalLayout(this.Game.ScreenManager)
            {
                Spacing = 4
            };

            this.Manage(this.removeableButtonLayout);

            // Populate layout with buttons
            this.removeableButtonLayout.Items.Add(createButtonButton);

            for (int i = 0; i < 5; ++i)
            {
                this.AddNewButton();
            }

            // Set layout position
            this.removeableButtonLayout.MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center;
        }

        private void AddNewButton()
        {
            RemoveableButton button = this.GetNewButton();
            this.removeableButtonLayout.Items.Add(button);
            this.Manage(button);
        }

        private RemoveableButton GetNewButton()
        {
            var newButton =
                new RemoveableButton(
                    new Button(
                        new Vector2(92, 32),
                        "Test",
                        () => { },
                        this.buttonResources),
                    this.buttonResources)
                {
                    Spacing = 4
                };

            return newButton;
        }
    }
}