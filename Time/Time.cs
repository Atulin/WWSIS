using System;

namespace Time
{
    public class Time
    {
        public int Day, Month, Year, Hour, Minute, Second;

        private static readonly int[,] DaysOfMonths =
        {
            {0, 31, 28, 31, 30, 31, 30, 31, 30, 31, 30, 31, 30, 31 },
            {0, 31, 29, 31, 30, 31, 30, 31, 30, 31, 30, 31, 30, 31 }
        };

        
        /* ********************** *\
         * CONSTRUCTORS
         * no witty oneliner here
        \* ********************** */

        public Time(int day, int month, int year, int hour, int minute, int second, bool strict = true)
        {
            if (strict)
            {
                if (year < 1) throw new ArgumentOutOfRangeException();
                if (month < 1 || month > 12) throw new ArgumentOutOfRangeException();
                if (day < 1 || day > DaysOfMonths[Helpers.BoolToInt(IsLeap(year)), month]) throw new ArgumentOutOfRangeException();
            }

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

        public Time(string datetime)
        {
            string[] datetimesplit = datetime.Split(' ');
            
            string date = datetimesplit[0];
            string time = datetimesplit[1];

            int[] datesplit = Array.ConvertAll(date.Split('.'), int.Parse);
            int[] timesplit = Array.ConvertAll(time.Split(':'), int.Parse);
            
            Day    = datesplit[0];
            Month  = datesplit[1];
            Year   = datesplit[2];
            Hour   = timesplit[0];
            Minute = timesplit[1];
            Second = timesplit[2];
        }
        
        
        /* ********************** *\
         * TRANSFORMERS
         * robots in disguise!
        \* ********************** */

        public static string ToString(Time t)
        {
            return $"{t.Day:D2}.{t.Month:D2}.{t.Year:D4} {t.Hour:D2}:{t.Minute:D2}:{t.Second:D2}";
        }

        public static DateTime ToDateTime(Time t)
        {
            return new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, t.Second);
        }

        
        /* ********************** *\
         * QUICK MATHS
         * two plus two is four
        \* ********************** */
        
        public static Time Sum(Time t1, Time t2)
        {
            int days = t1.Day + t2.Day;
            int months = t1.Month + t2.Month;
            int years = t1.Year + t2.Year;
            int hours = t1.Hour + t2.Hour;
            int minutes = t1.Minute + t2.Minute;
            int seconds = t1.Second + t2.Second;

            if (seconds >= 60)
            {
                minutes += seconds / 60;
                seconds = seconds % 60;
            }
            if (minutes >= 60)
            {
                hours += minutes / 60;
                minutes = minutes % 60;
            }
            if (hours >= 24)
            {
                days += hours / 24;
                hours = hours % 24;
            }

            while (days > DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months])
            {
                if (days >= DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months])
                {
                    months += days / DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months];
                    days = days % DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months];
                }
            }

            if (months >= 12)
            {
                years += months / 12;
                months = months / 12;
            }
            
            return new Time(days, months, years, hours, minutes, seconds);
        }

        public static Time Difference(Time t1, Time t2)
        {
            int days = t1.Day - t2.Day;
            int months = t1.Month - t2.Month;
            int years = t1.Year - t2.Year;
            int hours = t1.Hour - t2.Hour;
            int minutes = t1.Minute - t2.Minute;
            int seconds = t1.Second - t2.Second;

            if (seconds >= 60)
            {
                minutes -= seconds / 60;
                seconds = seconds % 60;
            }
            if (minutes >= 60)
            {
                hours -= minutes / 60;
                minutes = minutes % 60;
            }
            if (hours >= 24)
            {
                days -= hours / 24;
                hours = hours % 24;
            }

            while (days > DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months])
            {
                if (days >= DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months])
                {
                    months -= days / DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months];
                    days = days % DaysOfMonths[Helpers.BoolToInt(IsLeap(t1)), months];
                }
            }

            if (months >= 12)
            {
                years -= months / 12;
                months = months / 12;
            }
            
            return new Time(days, months, years, hours, minutes, seconds);
        }

        public static Time Difference(Time t)
        {
            Time t2 = new Time(DateTime.Now);
            return Difference(t, t2);
        }

        public static int NumberOfDays(Time t)
        {
            int days = 0;
            for (int i = 0; i < t.Month; i++)
            {
                days += DaysOfMonths[Helpers.BoolToInt(IsLeap(t)), i];
            }

            days += t.Day;

            return days;
        }

        
        /* ********************** *\
         * CHECKERS
         * not the board game
        \* ********************** */
        
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