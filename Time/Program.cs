using System;

namespace Time
{
    class Program
    {
        static void Main(string[] args)
        {
            Time t = new Time(1, 2, 3, 4, 5, 6);
            Time.Write(t);
            Console.ReadKey();
        }
    }

    class Time
    {
        private int Day, Month, Year, Hour, Minute, Second;

        private static readonly int[,] DaysOfMonths =
        {
            {0, 31, 28, 31, 30, 31, 30, 31, 30, 31, 30, 31, 30, 31 },
            {0, 31, 29, 31, 30, 31, 30, 31, 30, 31, 30, 31, 30, 31 }
        };

        public Time(int day, int month, int year, int hour, int minute, int second)
        {
            if (year < 1) throw new ArgumentOutOfRangeException();
            if (month < 1 || month > 12) throw new ArgumentOutOfRangeException();
            if (day < 1 || day > DaysOfMonths[Helpers.BoolToInt(IsLeap(year)), month]) throw new ArgumentOutOfRangeException();

            Day    = day;
            Month  = month;
            Year   = year;
            Hour   = hour;
            Minute = minute;
            Second = second;
        }

        public Time(DateTime dt)
        {
            Day    = dt.Day;
            Month  = dt.Month;
            Year   = dt.Year;
            Hour   = dt.Hour;
            Minute = dt.Minute;
            Second = dt.Second;
        }

        public static string ToString(Time t)
        {
            return $"{t.Day}.{t.Month}.{t.Year} {t.Hour}:{t.Minute}:{t.Second}";
        }

        public static void Write(Time t)
        {
            Console.WriteLine(ToString(t));
        }

        public static bool IsLeap(int year)
        {
            int y = year;
            return y % 4 == 0 && y % 100 != 0 || y % 400 == 0;
        }

        public static bool IsLeap(Time time)
        {
            return IsLeap(time.Year);
        }
    }
}