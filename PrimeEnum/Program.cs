using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PrimeEnum
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.Write("Which number to factorize? ");

                running = int.TryParse(Console.ReadLine(), out int num);
                if (!running) break;
                
                Console.WriteLine($"Prime factorization of {num}:");

                int tmp = num;
                int chars = tmp.ToString().Length;

                while (tmp > 0)
                {
                    IEnumerable<int> primes = new PrimeColl(num);
                    foreach (int i in primes)
                    {
//                        Console.WriteLine($"{tmp % i == 0} for {tmp} % {i}");
                        if (tmp == 1)
                        {
                            string pad2 = Helpers.StringRepeat(" ", chars - 1);
                            Console.WriteLine($"{pad2}{tmp}│");
                            tmp = 0;
                            break;
                        }

                        if (tmp % i == 0)
                        {
                            string pad = Helpers.StringRepeat(" ", chars - tmp.ToString().Length);
                            Console.WriteLine($"{pad}{tmp}│{i}");
                            tmp /= i;
                            break;
                        }
                    }
                }

                Console.WriteLine();
            }
        }
    }

    class Prime
    {
        public static List<int> Sieve(int size)
        {
            // Fill list with numbers using Sieve of Erathostenes
            List<int> numbers = new List<int>();
            for (int i = 2; i < size; i++)
            {
                if (i == 2 || i == 3 || i == 5 || i == 7)
                {
                    numbers.Add(i);
                    continue;
                }

                if (i % 2 != 0 && i % 3 != 0 && i % 5 != 0 && i % 7 != 0)
                {
                    numbers.Add(i);
                }
            }

            return numbers;
        }
    }

    class PrimeColl : IEnumerable<int>
    {
        private readonly int _num;

        public PrimeColl(int n)
        {
            _num = n;
        }

        // Foreach support
        public IEnumerator<int> GetEnumerator()
        {
            return new PrimeEnum(_num);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class PrimeEnum : IEnumerator<int>
    {
        private readonly int _num;
        private int _cur = -1;

        public PrimeEnum(int n)
        {
            _num = n;
        }

        public bool MoveNext()
        {
            if (_cur < _num) _cur++;
            return _cur < _num;
        }

        public void Reset()
        {
            _cur = -1;
        }

        object IEnumerator.Current => Current;

        public int Current
        {
            get
            {
                List<int> numbers = Prime.Sieve(100000);
                return numbers[_cur];
            }
        }

        public void Dispose()
        {
            ;
        }
    }

    class Helpers
    {
        public static string StringRepeat(string value, int count)
        {
            return new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        public static string Ordinal(int number)
        {
            string suffix;

            int ones = number % 10;
            int tens = (int) Math.Floor(number / 10M) % 10;

            if (tens == 1)
            {
                suffix = "th";
            }
            else
            {
                switch (ones)
                {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }

            return String.Format("{0}{1}", number, suffix);
        }
    }
}