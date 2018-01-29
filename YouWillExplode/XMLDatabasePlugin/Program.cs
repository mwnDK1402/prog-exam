namespace XMLDatabasePlugin
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DatabaseContract;

    internal class Program
    {
        private static void Main(string[] args)
        {
            ProfileDatabase database = new ProfileDatabase();

            Profile one = new Profile()
            {
                Name = "Wagner"
            };

            Profile two = new Profile()
            {
                Name = "Simon"
            };

            IList<Profile> profiles = new List<Profile>()
            {
                one,
                one,
                one,
                two
            };

            StringBuilder profilesBuilder = new StringBuilder();
            foreach (var profile in profiles)
            {
                profilesBuilder.AppendLine(profile.Name);
            }

            Console.Write(profilesBuilder.ToString());

            profilesBuilder.Clear();

            database.Save(profiles);

            profiles = database.Load();

            foreach (var profile in profiles)
            {
                profilesBuilder.AppendLine(profile.Name);
            }

            Console.Write(profilesBuilder.ToString());

            profilesBuilder.Clear();

            Console.ReadKey();
        }
    }
}