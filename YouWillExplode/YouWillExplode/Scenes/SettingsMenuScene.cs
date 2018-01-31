﻿namespace YouWillExplode
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

            var profiles = this.Game.ProfileManager.Profiles;

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

        /// <summary>
        /// Toggle editing a profile.
        /// If the profile is already being edited, and this method is called,
        /// the profile editor will be closed.
        /// </summary>
        /// <param name="newButton">The button that was pressed.</param>
        private void EditProfile(RemoveableButton newButton)
        {
            throw new NotImplementedException();
        }

        private void RemoveProfile(Profile profile)
        {
        }

        private class ProfileButton : Entity
        {
            private Profile profile;

            public ProfileButton(Profile profile, ProfileEditor editor, SettingsMenuScene scene, Button.Resources resources)
            {
                this.Button =
                    new RemoveableButton(
                        new Button(
                            new Vector2(92, 32),
                            profile.Name,
                            () => editor.EditProfile(this.profile),
                            resources),
                        () => scene.Destroy(this),
                        resources)
                    {
                        Spacing = 4
                    };

                scene.Manage(this.Button);

                this.profile = profile;
            }

            public RemoveableButton Button { get; private set; }
        }
    }
}