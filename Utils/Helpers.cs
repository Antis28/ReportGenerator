using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class Helpers
    {
        public static string GetSupervisorFull(int index)
        {
            String position = String.Empty;
            //TODO: получать строки из ресурсов (XML || JSON?)
            switch (index)
            {
                case 0:
                    position = "Начальник управления" + new String(' ', 70) + "М.В. Городкова";
                    break;
                case 1:
                    position = "И.о. начальника управления" + new String(' ', 70) + "Т.Н. Хомич";
                    break;
                default:
                    break;
            }
            return position;
        }
        public static string GetSupervisorShort(int index)
        {
            String position = String.Empty;
            //TODO: получать строки из ресурсов (XML || JSON?)
            switch (index)
            {
                case 0:
                    position = "М.В. Городкова";
                    break;
                case 1:
                    position = "Т.Н. Хомич";
                    break;
                default:
                    break;
            }
            return position;
        }
    }
}
