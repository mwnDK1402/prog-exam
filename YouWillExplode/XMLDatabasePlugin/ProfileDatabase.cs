namespace Utility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using DatabaseContract;

    public sealed class ProfileDatabase : IProfileDatabase
    {
        private XmlReaderSettings readerSettings;
        private XmlSerializer serializer;
        private XmlWriterSettings writerSettings;

        public ProfileDatabase()
        {
            this.serializer = new XmlSerializer(typeof(Profile));

            this.readerSettings = new XmlReaderSettings();

            this.writerSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
        }

        public ICollection<Profile> Load()
        {
            var profiles = new List<Profile>();
            string[] files = PathUtility.GetFilesRelative("Profiles", "*.profile");

            if (files.Length == 0)
            {
                return profiles;
            }

            foreach (string file in files)
            {
                using (var reader = XmlReader.Create(file, this.readerSettings))
                {
                    profiles.Add((Profile)this.serializer.Deserialize(reader));
                }
            }

            return profiles;
        }

        public void Save(ICollection<Profile> profiles)
        {
            // Handle duplicate names
            IEnumerable<IGrouping<string, Profile>> profilesByNameGroups = profiles.GroupBy(p => p.Name);

            var newProfiles = new List<Profile>(profiles.Count);

            foreach (IGrouping<string, Profile> grouping in profilesByNameGroups)
            {
                if (grouping.Count() <= 1)
                {
                    newProfiles.Add(grouping.Single());
                    continue;
                }

                string name = grouping.Key;
                var enumerator = grouping.GetEnumerator();

                // We skip the first profile if the name ends with a number
                enumerator.MoveNext();
                if (enumerator.Current.Name.EndsWithNumber())
                {
                    Profile newProfile = enumerator.Current;
                    newProfile.Name = name;
                    newProfiles.Add(newProfile);
                }
                else
                {
                    enumerator.Reset();
                }

                while (enumerator.MoveNext())
                {
                    name = name.GetIncremented();
                    Profile newProfile = enumerator.Current;
                    newProfile.Name = name;
                    newProfiles.Add(newProfile);
                }
            }

            var pathBuilder = new StringBuilder();

            foreach (Profile profile in newProfiles)
            {
                pathBuilder.Append(PathUtility.GetProcessDirectory());
                pathBuilder.Append("Profiles\\");
                Directory.CreateDirectory(pathBuilder.ToString());
                pathBuilder.Append(profile.Name);
                pathBuilder.Append(".profile");

                using (var sw = new StreamWriter(pathBuilder.ToString()))
                {
                    var tw = XmlWriter.Create(sw, this.writerSettings);
                    this.serializer.Serialize(tw, profile);
                }

                pathBuilder.Clear();
            }
        }
    }
}