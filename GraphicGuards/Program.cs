using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

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
            DateTime last = new DateTime(now.Year, now.Month + 1, 1).AddDays(-1);

            //Case1(guard11, guard22, guard33, firstDay, last);
            //Case2(guard11, guard22, guard33, firstDay);



            Case3(guard11, guard22, guard33, firstDay, last);
        }

        private static void Case3(string guard11, string guard22, string guard33, DateTime firstDay, DateTime last)
        {
            WriteLine(new string('-', 100));
            Write(new string('-', guard11.Length + 1));
            for (DateTime currentDay = firstDay; currentDay < last.AddDays(1); currentDay = currentDay.AddDays(1))
            {
                Write(currentDay.ToString("dd") + " ");
            }
            WriteLine();
            Write(new string('-', guard11.Length + 1));
            for (DateTime currentDay = firstDay; currentDay < last.AddDays(1); currentDay = currentDay.AddDays(1))
            {
                Write(currentDay.ToString("ddd") + " ");
            }
            WriteLine();
            WriteLine(new string('-', 100));
            Write(guard11 + "-");
            Write(new string(' ', 1));
            PrintGuard(firstDay);


            Write(guard22 + "-");
            Write(new string(' ', 4));
            PrintGuard(firstDay.AddDays(1));

            Write(guard33 + "-");
            Write(new string(' ', 1));
            PrintGuard(firstDay.AddDays(2));

            ReadKey();
        }

        private static void PrintGuard(DateTime firstDay)
        {
            Guard guard = new Guard(firstDay);
            Dictionary<DateTime, int> duty = guard.GetDuty();
            foreach (var item in duty)
            {
                ConsoleColor(item.Value);

                Write(item.Value);
                if (item.Value > 9)
                    Write(new string(' ', 1));
                else
                    Write(new string(' ', 2));
            }
            ResetColor();
        }

        

        private static void Case2(string guard11, string guard22, string guard33, DateTime firstDay)
        {
            Guard guard1 = new Guard(firstDay);
            Dictionary<DateTime, int> duty1 = guard1.GetDuty();

            foreach (var item in duty1)
            {
                WriteLine(guard11 + " дежурит " + item.Value + " часов " + item.Key.ToString("d ddd MMMM"));
            }
            ReadLine();
            WriteLine(new string('-', 50));

            Guard guard2 = new Guard(firstDay.AddDays(1));
            Dictionary<DateTime, int> duty2 = guard2.GetDuty();

            foreach (var item in duty2)
            {
                WriteLine(guard22 + " дежурит " + item.Value + " часов " + item.Key.ToString("d ddd MMMM"));
            }
            ReadLine();
            WriteLine(new string('-', 50));

            Guard guard3 = new Guard(firstDay.AddDays(2));
            Dictionary<DateTime, int> duty3 = guard3.GetDuty();

            foreach (var item in duty3)
            {
                WriteLine(guard33 + " дежурит " + item.Value + " часов " + item.Key.ToString("d ddd MMMM"));
            }
            ReadLine();
        }

        private static void Case1(string guard11, string guard22, string guard33, DateTime firstDay, DateTime last)
        {
            // Удалить, уже есть в классе Guard
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
            
            for (DateTime currentDay = firstDay; currentDay < last;)
            {
                if (currentDay.Day > last.Day)
                {
                    break;
                }
                int duty = HourDuty(currentDay);
                WriteLine(guard11 + " дежурит " + duty + " часов " + currentDay.ToString("d ddd MMMM"));
                currentDay = currentDay.AddDays(1);

                if (currentDay.Day > last.Day)
                {
                    break;
                }
                duty = HourDuty(currentDay);
                WriteLine(guard22 + " дежурит " + duty + " часов " + currentDay.ToString("d ddd MMMM"));
                currentDay = currentDay.AddDays(1);

                if (currentDay.Day > last.Day)
                {
                    break;
                }
                duty = HourDuty(currentDay);
                WriteLine(guard33 + " дежурит " + duty + " часов " + currentDay.ToString("d ddd MMMM"));
                currentDay = currentDay.AddDays(1);

                WriteLine(new string('-', 50));
            }
        }

        private static void ConsoleColor(int item)
        {
            if (item == 7)
            {
                Console.ForegroundColor = System.ConsoleColor.Green; // устанавливаем цвет
            }
            if (item == 8)
            {
                Console.ForegroundColor = System.ConsoleColor.Yellow; // устанавливаем цвет
            }
            if (item == 0)
            {
                Console.ForegroundColor = System.ConsoleColor.DarkGray; // устанавливаем цвет
            }
            if (item == 16)
            {
                Console.ForegroundColor = System.ConsoleColor.Red; // устанавливаем цвет
            }
        }
        private static void ResetColor()
        {
            Console.ForegroundColor = System.ConsoleColor.Gray; // устанавливаем цвет
        }
    }
    class Guard
    {
        private readonly Dictionary<DateTime, int> duties = new Dictionary<DateTime, int>(15);

        public Guard(DateTime firstDuty)
        {
            DateTime last = new DateTime(firstDuty.Year, firstDuty.Month + 1, 1);
            CheckContinueDuty(firstDuty);
            int hourContinueDuty = 8;
            int dayOff = 0;

            for (DateTime currentDay = firstDuty; currentDay < last; currentDay = currentDay.AddDays(1))
            {
                duties.Add(currentDay, HourDuty(currentDay));

                currentDay = currentDay.AddDays(1);
                if (currentDay >= last)
                    break;

                duties.Add(currentDay, hourContinueDuty);

                currentDay = currentDay.AddDays(1);
                if (currentDay >= last)
                    break;

                duties.Add(currentDay, dayOff);
            }
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

        public Dictionary<DateTime, int> GetDuty()
        {
            return duties;
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
