using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GraphicGuards.ConsoleMenager;


namespace GraphicGuards
{
    class Program
    {
        static void Main(string[] args)
        {
            String guard11 = "Сторож 1";
            String guard22 = "Сторож 2";
            String guard33 = "Сторож 3";

            DateTime now = DateTime.Now;
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime last = firstDay.AddMonths(1).AddDays(-1);

            Case3(guard11, guard22, guard33, firstDay, last);
            


            firstDay = new DateTime(firstDay.Year, firstDay.AddMonths(1).Month, 1);
            last = firstDay.AddMonths(1).AddDays(-1);
            Case3(guard11, guard22, guard33, firstDay, last);

            firstDay = new DateTime(firstDay.Year, firstDay.Month, 1).AddMonths(1);
            last = firstDay.AddMonths(1).AddDays(-1);
            Case3(guard11, guard22, guard33, firstDay, last);
        }

        

        private static void Case3(string guard11, string guard22, string guard33, DateTime firstDay, DateTime last)
        {
            PrintHeader(guard11, firstDay, last);
            PrintGuardName(guard11);
            PrintGuard(firstDay);


            PrintGuardName(guard22);
            PrintGuard(firstDay.AddDays(1));


            PrintGuardName(guard33);
            PrintGuard(firstDay.AddDays(2));

            Wait();
        }

        
    }
}
