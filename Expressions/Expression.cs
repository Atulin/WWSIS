using System;
using System.Collections.Generic;
using System.Globalization;

namespace Expressions
{
    public abstract class Expression
    {
        public abstract double Calculate();
    }

    // Number
    // Number
    class Number : Expression
    {
        private readonly double _number;

        public Number(double d)
        {
            _number = d;
        }

        public override double Calculate()
        {
            return _number;
        }

        public override string ToString()
        {
            return Convert.ToString(_number, CultureInfo.InvariantCulture);
        }
    }
    
    class Variable : Expression
    {
        public static Dictionary<string, double> Collection = new Dictionary<string, double>(); 
        
        private readonly string _variable;

        public Variable(string d)
        {
            _variable = d;
        }

        public override double Calculate()
        {
            return Collection[_variable];
        }

        public override string ToString()
        {
            return Convert.ToString(_variable, CultureInfo.InvariantCulture);
        }
    }

    // Operator
    abstract class Operator1Arg : Expression
    {
        protected Expression exp1;

        public Operator1Arg(Expression exp1)
        {
            this.exp1 = exp1 ?? throw new ArgumentNullException(nameof(exp1));
        }
    }

    abstract class Operator2Arg : Operator1Arg
    {
        protected Expression exp2;

        public Operator2Arg(Expression exp1, Expression exp2) : base(exp1)
        {
            this.exp2 = exp2 ?? throw new ArgumentNullException(nameof(exp2));
        }
    }

    // Overload
    class Overload : Operator1Arg
    {
        public Overload(Expression exp1) : base(exp1)
        {
        }

        public override double Calculate()
        {
            return -exp1.Calculate();
        }

        public override string ToString()
        {
            return string.Format($"-({exp1})");
        }
    }

    // Sum
    class Sum : Operator2Arg
    {
        public Sum(Expression exp1, Expression exp2) : base(exp1, exp2)
        {
        }

        public override double Calculate()
        {
            return exp1.Calculate() + exp2.Calculate();
        }

        public override string ToString()
        {
            string s1 = exp1.ToString();
            if (exp1 is Operator2Arg) s1 = String.Format($"({s1})");
            string s2 = exp2.ToString();
            if (exp2 is Operator2Arg) s2 = String.Format($"({s2})");
            return string.Format($"{s1} + {s2}");
        }
    }
}