using System.IO;
using System;
using Newtonsoft.Json;

namespace GuardsConsole
{
    public class JsonManager
    {
        public static void WriteTest<T>(JsonSerializer serializer, T product)
        {
            var path = Environment.CurrentDirectory + @"\json.txt";

            using (var sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw)) { serializer.Serialize(writer, product); }
        }

        public static void WriteProductTest<T>(T product, string fileName = @"json")
        {
            var path = Environment.CurrentDirectory + @"\" + fileName + ".txt";

            var output = JsonConvert.SerializeObject(product, Formatting.Indented, new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            });

            using (var sw = new StreamWriter(path)) { sw.Write(output); }
        }
    }
}
