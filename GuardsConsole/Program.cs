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
            var holidays = HolidaysReader.Read();
            SettingsHolidays settings = new SettingsHolidays { Holidays = holidays };
            
            JsonManager.WriteProductTest(settings);
        }
    }

    public static class HolidaysReader
    {
        public static List<DateTime> Read()
        {
            var holidays = new List<DateTime>();
            var path = Environment.CurrentDirectory + @"\Holidays.txt";
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var result = DateTime.MaxValue;
                    DateTime.TryParse(line, out result);
                    if (result != DateTime.MaxValue)
                    {
                        holidays.Add(result);
                    }
                    
                    Console.WriteLine(line);
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
        [JsonProperty]
        public List<DateTime> Holidays { get; set; }
    }
}
