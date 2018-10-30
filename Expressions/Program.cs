using System;

namespace Expressions
{
    class Program
    {
        static void Main(string[] args)
        {
            Variable.Collection.Add("x", 1.2);
            
            Expression n1 = new Sum(new Overload(new Number(2.25)), new Number(3.75) );
            Console.WriteLine(n1);

            Console.ReadKey();
        }
    }
}