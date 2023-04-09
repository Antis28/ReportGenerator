using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GuardsConsole
{
    internal class Program
    {
        public static void Main(string[] args) { }
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
        public string supervisor = "supervisor";
        /// <summary>
        /// Начальник отдела
        /// Department head
        /// </summary>
        public string superior = "superior";
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class SettingsHolidays
    {
        [JsonProperty]
        public List<DateTime> holidays;
    }
}
