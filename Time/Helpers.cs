using System;
using System.Collections.Generic;
using System.Text;

namespace Time
{
    static class Helpers
    {
        public static int BoolToInt(bool b)
        {
            return b ? 1 : 0;
        }
        
        public static string Suffix(this int num)
        {
            if (num.ToString().EndsWith("11")) return num + "th";
            if (num.ToString().EndsWith("12")) return num + "th";
            if (num.ToString().EndsWith("13")) return num + "th";
            if (num.ToString().EndsWith("1")) return num + "st";
            if (num.ToString().EndsWith("2")) return num + "nd";
            if (num.ToString().EndsWith("3")) return num + "rd";
            return num + "th";
        }
    }
}
