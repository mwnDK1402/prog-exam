namespace YouWillExplode
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using global::Utility;

    internal sealed class PreferencesManager
    {
        private static readonly string PreferencesPath = PathUtility.GetProcessDirectory() + "Preferences.xml";
        private XmlSerializer serializer;
        private XmlWriterSettings settings;

        public PreferencesManager()
        {
            this.serializer = new XmlSerializer(typeof(Preferences));

            this.settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };

            if (!File.Exists(PreferencesPath))
            {
                this.Preferences = new Preferences()
                {
                    FirstUse = true
                };

                this.Save();
            }
            else
            {
                using (var reader = XmlReader.Create(PreferencesPath))
                {
                    var preferences = (Preferences)this.serializer.Deserialize(reader);
                    preferences.FirstUse = false;
                    this.Preferences = preferences;
                }
            }
        }

        public Preferences Preferences { get; private set; }

        public void Save()
        {
            using (var writer = XmlWriter.Create(PreferencesPath, this.settings))
            {
                this.serializer.Serialize(writer, this.Preferences);
            }
        }
    }
}