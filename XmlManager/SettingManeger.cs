using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                throw new IOException("File not found");

            Settings result = null;
            using (TextReader Stream = new StreamReader(path, Encoding.UTF8))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(Settings));
                result = Serializer.Deserialize(Stream) as Settings;
                Stream.Close();

                if (result == null)
                    throw new Exception("File not Deserialize");
            }
            return result;
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
