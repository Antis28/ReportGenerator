using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class InputFilter
    {
        public static bool CheckDigit(char ch)
        {
            if (!Char.IsDigit(ch) && ch != 8)
                return true;
            return false;
        }
    }
}
