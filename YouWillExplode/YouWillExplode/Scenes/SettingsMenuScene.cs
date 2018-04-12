namespace YouWillExplode
{
    using System;
    using System.Collections.Generic;
    using DatabaseContract;
    using Layout;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal sealed class SettingsMenuScene : Scene
    {
        private Button.Resources buttonResources;
        private ProfileEditor editor;
        private List<ProfileButton> profiles;
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

            List<Profile> profiles = this.Game.ProfileManager.Profiles;

            // Create layout
            this.removeableButtonLayout = new VerticalLayout(this.Game.ScreenManager)
            {
                Spacing = 4
            };

            this.Manage(this.removeableButtonLayout);

            // Populate layout with buttons
            this.removeableButtonLayout.Items.Add(createButtonButton);

            this.profiles = new List<ProfileButton>();
            foreach (Profile profile in profiles)
            {
                this.AddProfile(profile);
            }

            // Set layout position
            this.removeableButtonLayout.LeftPosition = new Point(24, this.Game.ScreenManager.Viewport.Bounds.Center.Y);

            // Profile editor
            this.editor = new ProfileEditor();
            this.Manage(this.editor);
        }

        protected override void OnUnload()
        {
        }

        private void AddProfile(Profile profile)
        {
            var button = new ProfileButton(profile, this.editor, this, this.buttonResources);
            this.profiles.Add(button);
            this.removeableButtonLayout.Items.Add(button.Button);
            this.Manage(button);
        }

        private void RemoveProfile(Profile profile)
        {
        }
    }
}