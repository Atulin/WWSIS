using System;
using System.Collections.Generic;
using System.Linq;

namespace Prime
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            List<int> numbers = args.Select(arg => Convert.ToInt32(arg.Replace(",", ""))).ToList();

            foreach (int i in numbers)
            {
                if (i.IsPrime())
                {
                    Console.WriteLine(i + " is a prime number");                    
                }
                else
                {
                    Console.WriteLine(i + " is not a prime number (" + string.Join(", ", i.Disassemble()) + ")");
                }
            }
        }

        private static bool IsPrime(this int i)
        {
            if (i <= 1) return false;
            if (i == 2) return true;
            if (i % 2 == 0)  return false;

            var bound = (int)Math.Floor(Math.Sqrt(i));

            for (int j = 3; j <= bound; j+=2)
            {
                if (i % j == 0)  return false;
            }

            return true;  
        }

        private static IEnumerable<int> Disassemble(this int x)
        {
            List<int> output = new List<int>();
            int i = 2;
            int e = (int)Math.Sqrt(x);
            
            while (i <= e)
            {
                while (x % i == 0)
                {
                    x /= i;
                    e = (int)Math.Sqrt(x);
                    output.Add(i);
                }
                i++;
            }
            output.Add(x);

            return output.ToArray();
        }
    }
}