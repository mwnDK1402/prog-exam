namespace YouWillExplode
{
    using DatabaseContract;
    using Microsoft.Xna.Framework;

    internal sealed class ProfileButton : Entity
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