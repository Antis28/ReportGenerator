using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GuardsConsole
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var holidays = HolidaysReader.ReadHolidays();
            SettingsHolidays settings = new SettingsHolidays { Holidays = holidays };

            JsonManager.WriteProductTest(settings);
        }
    }

    public static class HolidaysReader
    {
        public static List<DateTime> ReadHolidays()
        {
            return Read(@"Holidays.txt");
        }

        private static List<DateTime> Read(string fileName)
        {
            var holidays = new List<DateTime>();
            var path = Environment.CurrentDirectory + @"\" + fileName;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    DateTime result;
                    DateTime.TryParse(line, out result);
                    if (result != DateTime.MinValue)
                    {
                        holidays.Add(result);
                        // Console.WriteLine(line);
                    }
                    else
                    {
                        Console.WriteLine("Error: " + line);
                    }

                }
            }

            return holidays;
        }
    }

    public class SettingsGuards
    {
        public string guard1 = "guard1";
        public string guard2 = "guard2";
        public string guard3 = "guard3";

        /// <summary>
        /// Начальник 
        /// </summary>
        public string director = "director";

        /// <summary>
        /// Заместитель начальника
        /// </summary>
        public string deputy_chief = "deputy_chief";

        /// <summary>
        /// Начальник управления(филиала)
        /// Branch manager
        /// </summary>
        public string branchManager = "Начальник управления(филиала)";

        /// <summary>
        /// Начальник отдела
        /// Department head
        /// </summary>
        public string departmentHead = "Начальник отдела";
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class SettingsHolidays
    {
        [JsonProperty] public List<DateTime> Holidays { get; set; }
    }
}
