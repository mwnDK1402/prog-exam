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
        private Dictionary<RemoveableButton, Profile> profileByButton;
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
                () => this.AddProfile(Profile.New),
                this.buttonResources);

            this.Manage(createButtonButton);

            var profiles = this.Game.ProfileManager.Profiles;

            // Create layout
            this.removeableButtonLayout = new VerticalLayout(this.Game.ScreenManager)
            {
                Spacing = 4
            };

            this.Manage(this.removeableButtonLayout);

            // Populate layout with buttons
            this.removeableButtonLayout.Items.Add(createButtonButton);

            this.profileByButton = new Dictionary<RemoveableButton, Profile>();
            foreach (Profile profile in profiles)
            {
                this.AddProfile(profile);
            }

            // Set layout position
            this.removeableButtonLayout.LeftPosition = new Point(24, this.Game.ScreenManager.Viewport.Bounds.Center.Y);
        }

        protected override void OnUnload()
        {
        }

        private void AddProfile(Profile profile)
        {
            RemoveableButton button = this.GetNewButton(profile.Name);
            this.removeableButtonLayout.Items.Add(button);
            this.Manage(button);

            this.profileByButton.Add(button, profile);
        }

        private RemoveableButton GetNewButton(string text)
        {
            var newButton =
                new RemoveableButton(
                    new Button(
                        new Vector2(92, 32),
                        text,
                        () => { /* Edit profile */ },
                        this.buttonResources),
                    this.buttonResources)
                {
                    Spacing = 4
                };

            return newButton;
        }
    }
}