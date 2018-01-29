using System.Collections.Generic;
using DatabaseContract;
using Microsoft.Xna.Framework.Input;

namespace YouWillExplode
{
    internal sealed class ProfileManager
    {
        private IProfileDatabase database;

        private List<Profile> profiles;

        public ProfileManager(IProfileDatabase database, PreferencesManager preferencesManager)
        {
            this.database = database;
            this.profiles = new List<Profile>(this.database.Load());
            if (preferencesManager.Preferences.FirstUse)
            {
                var p1 = new Profile()
                {
                    Name = "Player1",
                    Up = (int)Keys.W,
                    Down = (int)Keys.S,
                    Left = (int)Keys.A,
                    Right = (int)Keys.D,
                    Shoot = (int)Keys.LeftShift
                };

                var p2 = new Profile()
                {
                    Name = "Player1",
                    Up = (int)Keys.Up,
                    Down = (int)Keys.Down,
                    Left = (int)Keys.Left,
                    Right = (int)Keys.Right,
                    Shoot = (int)Keys.RightShift
                };

                this.profiles.Add(p1);
                this.profiles.Add(p2);

                this.Save();
            }
        }

        public ICollection<Profile> GetProfiles() =>
            this.profiles.ToArray();

        public void Save() =>
            this.database.Save(this.profiles);
    }
}