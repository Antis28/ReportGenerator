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
            DateTime last = firstDay.AddMonths(1).AddDays(-1);

            Case3(guard11, guard22, guard33, firstDay, last);
            WriteLine(new string('-', 100));


            firstDay = new DateTime(firstDay.Year, firstDay.AddMonths(1).Month, 1);
            last = firstDay.AddMonths(1).AddDays(-1);
            Case3(guard11, guard22, guard33, firstDay, last);

            firstDay = new DateTime(firstDay.Year, firstDay.Month, 1).AddMonths(1);
            last = firstDay.AddMonths(1).AddDays(-1);
            Case3(guard11, guard22, guard33, firstDay, last);
        }

        private static void PrintHeader(string guard11, DateTime firstDay, DateTime last)
        {
            WriteLine(new string('-', 100));
            Write(new string(' ', 50));
            WriteLine(firstDay.ToString("MMMM"));
            WriteLine(new string('-', 100));
            Write(new string('-', guard11.Length + 1));
            for (DateTime currentDay = firstDay; currentDay <= last; currentDay = currentDay.AddDays(1))
            {
                Write(currentDay.ToString("dd") + " ");
            }
            WriteLine();
            Write(new string('-', guard11.Length + 1));
            for (DateTime currentDay = firstDay; currentDay <= last; currentDay = currentDay.AddDays(1))
            {
                Write(currentDay.ToString("ddd") + " ");
            }
            WriteLine();
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

            ReadLine();
        }

        private static void PrintGuardName(string guard)
        {
            WriteLine(new string('-', 100));
            Write(guard + "-");
            Write(new string(' ', 1));
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
            WriteLine();
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
}
