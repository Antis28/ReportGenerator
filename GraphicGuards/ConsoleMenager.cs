using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace GraphicGuards
{
    class ConsoleMenager
    {
        public static void PrintHeader(int guardNameLength, DateTime firstDay, DateTime last)
        {
            WriteLine(new string('-', 100));
            Write(new string(' ', 50));
            WriteLine(firstDay.ToString("MMMM"));
            WriteLine(new string('-', 100));
            Write(new string('-', guardNameLength));
            for (DateTime currentDay = firstDay; currentDay <= last; currentDay = currentDay.AddDays(1))
            {
                Write(currentDay.ToString("dd") + " ");
            }
            WriteLine();
            Write(new string('-', guardNameLength));
            for (DateTime currentDay = firstDay; currentDay <= last; currentDay = currentDay.AddDays(1))
            {
                Write(currentDay.ToString("ddd") + " ");
            }
            WriteLine();
        }
        public static void PrintGuardName(string guard)
        {
            WriteLine(new string('-', 100));
            Write(guard + "-");
            Write(new string(' ', 1));
        }
        public static void PrintGuardDuty(Dictionary<DateTime, int> duty)
        {
            foreach (var item in duty)
            {
                ConsoleSetColor(item.Value);

                Write(item.Value);
                if (item.Value > 9)
                    Write(new string(' ', 1));
                else
                    Write(new string(' ', 2));
            }
            ResetColor();
            WriteLine();
        }
        public static void PrintSeparator()
        {
            WriteLine(new string('-', 100));
        }
        public static void Wait()
        {
            ReadLine();
        }


        private static void ConsoleSetColor(int item)
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
