using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlManager;

namespace Utils
{
    public static class Helpers
    {
       

        public static string GetSupervisorFull(int index, Settings settings)
        {
            String position = String.Empty;
            //TODO: получать строки из ресурсов (XML || JSON?)
            switch (index)
            {
                case 0:
                    position = "Начальник управления" + new String(' ', 70) + settings.Director;
                    break;
                case 1:
                    position = "И.о. начальника управления" + new String(' ', 70) + settings.Deputy_chief;
                    break;
                default:
                    break;
            }
            return position;
        }
        public static string GetSupervisorShort(int index, Settings settings)
        {
            String position = String.Empty;
            //TODO: получать строки из ресурсов (XML || JSON?)
            switch (index)
            {
                case 0:
                    position = settings.Director;
                    break;
                case 1:
                    position = settings.Deputy_chief;
                    break;
                default:
                    break;
            }
            return position;
        }
    }
}
