namespace YouWillExplode
{
    using System.Collections.Generic;
    using DatabaseContract;
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class SettingsMenuScene : Scene
    {
        private Button.Resources buttonResources;
        private List<Profile> profiles;
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
                () => this.AddNewButton("New Profile"),
                this.buttonResources);

            this.Manage(createButtonButton);

            this.profiles = new List<Profile>(this.Game.ProfileManager.GetProfiles());

            // Create layout
            this.removeableButtonLayout = new VerticalLayout(this.Game.ScreenManager)
            {
                Spacing = 4
            };

            this.Manage(this.removeableButtonLayout);

            // Populate layout with buttons
            this.removeableButtonLayout.Items.Add(createButtonButton);

            foreach (Profile profile in this.profiles)
            {
                this.AddNewButton(profile.Name);
            }

            // Set layout position
            this.removeableButtonLayout.MiddlePosition = this.Game.ScreenManager.Viewport.Bounds.Center;
        }

        private void AddNewButton(string text)
        {
            RemoveableButton button = this.GetNewButton(text);
            this.removeableButtonLayout.Items.Add(button);
            this.Manage(button);
        }

        private RemoveableButton GetNewButton(string text)
        {
            var newButton =
                new RemoveableButton(
                    new Button(
                        new Vector2(92, 32),
                        text,
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