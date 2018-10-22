using System;

namespace Time
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("INPUT DATE (dd.mm.yyy hh:ii:ss)");
            
            Time time = new Time(Console.ReadLine());
            
            Console.WriteLine("> It's the " + Time.NumberOfDays(time).Suffix() + " day of the year.");
            Console.WriteLine("> Year " + time.Year + (Time.IsLeap(time) ? " is" : " is not") + " a leap year");
            Console.WriteLine("> In 7 days it'll be " + Time.ToString(Time.Sum(time, new Time(7, 0, 0, 0, 0, 0, false))));
            Console.WriteLine("> 7 days ago it was " + Time.ToString(Time.Difference(time, new Time(7, 0, 0, 0, 0, 0, false))));
            Console.ReadKey();
        }
    }
}