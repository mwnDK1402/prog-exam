namespace Utility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using DatabaseContract;
    using Utility;

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
            List<Profile> profiles = new List<Profile>();
            string[] files = PathUtility.GetFilesRelative("Profiles", "*.profile");

            if (files.Length == 0)
            {
                return profiles;
            }

            foreach (var file in files)
            {
                using (XmlReader reader = XmlReader.Create(file, this.readerSettings))
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

            List<Profile> newProfiles = new List<Profile>(profiles.Count);

            foreach (var grouping in profilesByNameGroups)
            {
                if (grouping.Count() <= 1)
                {
                    newProfiles.Add(grouping.Single());
                    continue;
                }

                string name = grouping.Key;
                foreach (var profile in grouping)
                {
                    name = name.GetIncremented();
                    var newProfile = profile;
                    newProfile.Name = name;
                    newProfiles.Add(newProfile);
                }
            }

            StringBuilder pathBuilder = new StringBuilder();

            foreach (var profile in newProfiles)
            {
                pathBuilder.Append(PathUtility.GetProcessDirectory());
                pathBuilder.Append("Profiles\\");
                pathBuilder.Append(profile.Name);
                pathBuilder.Append(".profile");

                using (StreamWriter sw = new StreamWriter(pathBuilder.ToString()))
                {
                    XmlWriter tw = XmlWriter.Create(sw, this.writerSettings);
                    this.serializer.Serialize(tw, profile);
                }

                pathBuilder.Clear();
            }
        }
    }
}