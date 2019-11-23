using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static GraphicGuards.ConsoleMenager;

namespace GraphicGuards
{
    class Guard
    {
        private readonly Dictionary<DateTime, int> duties = new Dictionary<DateTime, int>(15);

        public Guard(DateTime firstDuty, bool continueDuty = false)
        {
            int hourContinueDuty = 8;
            int dayOff = 0;

            DateTime nextMonths = firstDuty.AddMonths(1);
            DateTime firstDay = new DateTime(firstDuty.Year, firstDuty.Month, 1);
            DateTime lastDay = new DateTime(nextMonths.Year, nextMonths.Month, 1).AddDays(-1);

            if (continueDuty || firstDuty.Day > 2)
            {
                duties.Add(firstDay, hourContinueDuty);
                duties.Add(firstDay.AddDays(1), dayOff);
            }
            else if (firstDuty.Day == 2)
            {
                duties.Add(firstDay, dayOff);
            }

            for (DateTime currentDay = firstDuty; currentDay <= lastDay; currentDay = currentDay.AddDays(1))
            {
                duties.Add(currentDay, HourDuty(currentDay));

                currentDay = currentDay.AddDays(1);
                if (currentDay > lastDay)
                    break;

                duties.Add(currentDay, hourContinueDuty);

                currentDay = currentDay.AddDays(1);
                if (currentDay > lastDay)
                    break;

                duties.Add(currentDay, dayOff);
            }
        }

        public void Print()
        {
            PrintGuardDuty(duties);
        }
        public Dictionary<DateTime, int> GetDuty()
        {
            return duties;
        }
        // Дежурство с прошлого месяца
        private void CheckContinueDuty(DateTime firstDuty)
        {
            int hourContinueDuty = 8;
            int dayOff = 0;
            int dateForContinueDuty = 3;
            if (firstDuty.Day == dateForContinueDuty)
            {
                DateTime firstDay = new DateTime(firstDuty.Year, firstDuty.Month, 1);
                duties.Add(firstDay, hourContinueDuty);
                duties.Add(firstDay.AddDays(1), dayOff);
            }
        }
        int HourDuty(DateTime date)
        {
            int weekday = 7;
            int beforeWeekend = 8;
            int weekend = 16;

            DayOfWeek day = date.DayOfWeek;
            switch (day)
            {
                case DayOfWeek.Sunday:
                    return weekend;
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                    return weekday;
                case DayOfWeek.Friday:
                case DayOfWeek.Saturday:
                    return beforeWeekend;
            }
            return 0;
        }
    }
}
