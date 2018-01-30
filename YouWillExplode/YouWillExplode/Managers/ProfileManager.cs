namespace YouWillExplode
{
    using System.Collections.Generic;
    using DatabaseContract;

    internal sealed class ProfileManager
    {
        private IProfileDatabase database;

        public ProfileManager(IProfileDatabase database, PreferencesManager preferencesManager)
        {
            this.database = database;
            this.Profiles = new List<Profile>(this.database.Load());
            if (preferencesManager.Preferences.FirstUse)
            {
                this.Profiles.Add(Profile.DefaultLeft);
                this.Profiles.Add(Profile.DefaultRight);
                this.Save();
            }
        }

        public Profile ActiveLeftProfile { get; set; } = Profile.DefaultLeft;

        public Profile ActiveRightProfile { get; set; } = Profile.DefaultRight;

        public List<Profile> Profiles { get; private set; }

        public void Save() =>
            this.database.Save(this.Profiles);
    }
}