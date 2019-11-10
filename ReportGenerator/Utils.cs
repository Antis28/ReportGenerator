using System;
using System.Collections.Generic;
using System.Text;

namespace ReportGenerator
{
    static class Utils
    {
        /// <summary>
        /// 'Возвращает слова в падеже, зависимом от заданного числа

        /// number - Число от которого зависит выбранное слово
        /// nominativ - Именительный падеж слова. Например "день"
        /// genetiv - Родительный падеж слова. Например "дня"
        /// plural - Множественное число слова. Например "дней"
        /// </summary>
        public static String GetDeclension(int number, string nominativ, string genetiv, string plural)
        {
            var titles = new[] { nominativ, genetiv, plural };
            var cases = new[] { 2, 0, 1, 1, 1, 2 };
            int index = -1;

            if (number % 100 > 4 && number % 100 < 20)
            {
                index = 2;

            }
            else if (number % 10 < 5)
            {
                index = cases[(number % 10)];
            }
            else
            {
                index = cases[5];
            }
            return titles[index];


            //return titles[number % 100 > 4 && number % 100 < 20 ? 2 : cases[(number % 10 < 5) ? number % 10 : 5]];
        }
        public static String GetDeclensionFiles(int number)
        {
            return GetDeclension(number, "файл", "файла", "файлов");
        }
    }
}
