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


            //Case4(guard11, guard22, guard33, firstDay, last);


            int guardNameLength = guard11.Length + 1;

            Header(now, guardNameLength);
            Case5(now, 3);
            
            Header(now.AddMonths(1), guardNameLength);
            Case5(now.AddMonths(1), 3);

            Header(now.AddMonths(2), guardNameLength);
            Case5(now.AddMonths(2), 1);

            Header(now.AddMonths(3), guardNameLength);
            Case5(now.AddMonths(3), 2);

            Header(now.AddMonths(4), guardNameLength);
            Case5(now.AddMonths(4), 1);

            Wait();


            /*
            firstDay = new DateTime(firstDay.Year, firstDay.AddMonths(1).Month, 1);
            last = firstDay.AddMonths(1).AddDays(-1);
            Case3(guard11, guard22, guard33, firstDay, last);

            firstDay = new DateTime(firstDay.Year, firstDay.Month, 1).AddMonths(1);
            last = firstDay.AddMonths(1).AddDays(-1);
            Case3(guard11, guard22, guard33, firstDay, last);
            */
        }
        private static void Header(DateTime now, int guardNameLength)
        {
            DateTime firstDay = new DateTime(now.Year, now.Month, 1);
            DateTime last = firstDay.AddMonths(1).AddDays(-1);
            PrintHeader(guardNameLength, firstDay, last);
        }



        private static void Case5(DateTime now, int guardNumber)
        {
            GuardManager manager = new GuardManager(now);
            manager.SetGurdContinueDuty(guardNumber);
            manager.PlanGraphic();
            manager.PrintGraphic();
        }


        private static void Case4(string guard11, string guard22, string guard33, DateTime firstDay, DateTime last)
        {
            Dictionary<DateTime, int> duty;
            Guard guard = null;

            int guardNameLength = guard11.Length + 1;
            
            PrintHeader(guardNameLength, firstDay, last);
            PrintGuardName(guard11);
            guard = new Guard(firstDay);
            guard.Print();


            PrintGuardName(guard22);
            guard = new Guard(firstDay.AddDays(1));
            duty = guard.GetDuty();
            PrintGuardDuty(duty);


            PrintGuardName(guard33);
            guard = new Guard(firstDay.AddDays(2));
            duty = guard.GetDuty();
            PrintGuardDuty(duty);

            Wait();
        }

    }
}
