using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace RPN
{
    class Program
    {
        static double Calc(List<Item> rpn)
        {
            // Operand stack
            Stack<double> st = new Stack<double>();
            // Item queue
            Queue<Item> qu = new Queue<Item>(rpn);

            // Parse queue
            while (qu.Any())
            {
                Item i = qu.Dequeue();
                i.Calc(st);
            }

            if (st.Count != 1) throw new ArgumentException("Too many operands");
            
            return st.Pop();
        }
        
        static void Main(string[] args)
        {

            for (string ln = Console.ReadLine()?.Trim(); ln != null && !ln.Equals(""); ln = Console.ReadLine()?.Trim())
            {
                // Parsing input
                List<Item> rpn = new List<Item>();
                string[] elems = Regex.Split(ln, "\\s+");
                foreach (string elem in elems)
                {
                    try
                    {
                        // If item is a number, add it to numers list
                        double d = Convert.ToDouble(elem);
                        rpn.Add(new Number(d));
                    }
                    catch (FormatException)
                    {
                        try
                        {
                            // If item isn;t a number, it's an operator - execute
                            switch (elem)
                            {
                                case "+":
                                    rpn.Add(new Add());
                                    break;
                                case "-":
                                    rpn.Add(new Sub());
                                    break;
                                case "*":
                                    rpn.Add(new Mult());
                                    break;
                                case "/":
                                    rpn.Add(new Div());
                                    break;
                                case "^":
                                    rpn.Add(new Pow());
                                    break;
                                case "r":
                                    rpn.Add(new Root());
                                    break;
                                default:
                                    throw new ArgumentException("Invalid operator");
                            }
                        }
                        catch (ArgumentException e)
                        {
                            Console.Error.WriteLine(e.Message);
                        }
                    }
                }
                
                // Calculate
                try
                {
                    Console.WriteLine(Calc(rpn));
                }
                catch (Exception)
                {
                    Console.Error.WriteLine("Invalid expression");
                }
            }
            
        }

        // Generic Item class
        abstract class Item
        {
            public abstract void Calc(Stack<double> st);
        }
        
        // Number class
        class Number: Item
        {
            private double num;

            public Number(double d)
            {
                num = d;
            }

            public override void Calc(Stack<double> st)
            {
                st.Push(num);
            }

            public override string ToString()
            {
                return Convert.ToString(num, CultureInfo.InvariantCulture);
            }
        }

        abstract class Function : Item
        {
            public abstract int Arity { get; }
        }

        // Summing class
        class Add : Function
        {
            public override int Arity => 2;

            public override void Calc(Stack<double> st)
            {
                double a = st.Pop(), b = st.Pop();
                st.Push(a + b);
            }
        }

        // Subtraction class
        class Sub : Function
        {
            public override int Arity => 2;

            public override void Calc(Stack<double> st)
            {
                double a = st.Pop(), b = st.Pop();
                st.Push(a - b);
            }
        }

        // Multiplication class
        class Mult : Function
        {
            public override int Arity => 2;

            public override void Calc(Stack<double> st)
            {
                double a = st.Pop(), b = st.Pop();
                st.Push(a * b);
            }
        }

        // Division class
        class Div : Function
        {
            public override int Arity => 2;

            public override void Calc(Stack<double> st)
            {
                double a = st.Pop(), b = st.Pop();
                st.Push(a / b);
            }
        }

        // Power class
        class Pow : Function
        {
            public override int Arity => 2;

            public override void Calc(Stack<double> st)
            {
                double a = st.Pop(), b = st.Pop();
                st.Push(Math.Pow(a, b));
            }
        }

        // Root class (root a of bth degree)
        class Root : Function
        {
            public override int Arity => 2;

            public override void Calc(Stack<double> st)
            {
                double a = st.Pop(), b = st.Pop();
                // Use Math.Pow because nth degree root of x is x to 1/nth power
                st.Push(Math.Pow(a, 1/b));
            }
        }
    }
}