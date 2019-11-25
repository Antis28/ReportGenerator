using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicGenerator
{
    class GuardManager
    {
        private Guard guard1;
        private Guard guard2;
        private Guard guard3;

        private DateTime firstDay;
        private DateTime last;

        private int continueDuty;

        public GuardManager(DateTime date)
        {
            firstDay = new DateTime(date.Year, date.Month, 1);
            last = firstDay.AddMonths(1).AddDays(-1);
        }
        public void SetGurdContinueDuty(int number)
        {
            if (1 > number || 3 < number)
            {
                throw new ArgumentException("Номер сторожа должен быть от 1 до 3, а ваш номер - " + number);
            }
            continueDuty = number;
        }

        public void PlanGraphic()
        {
            if (continueDuty == 3)
            {
                guard1 = new Guard(firstDay);
                guard2 = new Guard(firstDay.AddDays(1));
                guard3 = new Guard(firstDay.AddDays(2));
            }
            if (continueDuty == 2)
            {
                guard1 = new Guard(firstDay.AddDays(1));
                guard2 = new Guard(firstDay.AddDays(2));
                guard3 = new Guard(firstDay);
            }
            if (continueDuty == 1)
            {
                guard1 = new Guard(firstDay.AddDays(2));
                guard2 = new Guard(firstDay);
                guard3 = new Guard(firstDay.AddDays(1));
            }

        }
        public Guard GetGuard1()
        {
            return guard1;
        }
        public Guard GetGuard2()
        {
            return guard2;
        }
        public Guard GetGuard3()
        {
            return guard3;
        }
    }
}
