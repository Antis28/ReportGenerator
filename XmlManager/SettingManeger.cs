using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlManager
{
    public class SettingManeger
    {
        static string path = Environment.CurrentDirectory + @"\Settings.xml";

        public static Settings Load()
        {
            if (!File.Exists(path))
                throw new IOException("Settings - file not found!");

            Settings result = null;
            using (TextReader Stream = new StreamReader(path, Encoding.UTF8))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(Settings));
                result = Serializer.Deserialize(Stream) as Settings;
                Stream.Close();

                if (result == null)
                    throw new Exception("File not Deserialize.");
            }
            return result;
        }
        public static Settings LoadOrCreateNew()
        {
            if (File.Exists(path))
                return Load();
            Settings settings = new Settings()
            {
                Guard1 = "Не назначено",
                Guard2 = "Не назначено",
                Guard3 = "Не назначено",
                Supervisor = "Не назначено",
                Director = "Не назначено",
                Deputy_chief = "Не назначено",
                Superior = "Не назначено",
            };
            Save(settings);
            return settings;
        }
        public static void Save(Settings setting)
        {
            using (TextWriter stream = new StreamWriter(path, false, Encoding.UTF8))
            {
                //Now save
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));

                xmlSerializer.Serialize(stream, setting);
                stream.Close();
            }
        }
    }
}
