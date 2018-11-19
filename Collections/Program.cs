using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Collections
{
    class Program
    {
        private static void Main(string[] args)
        {
            bool b = true;
            foreach (var i in new IntColl(5))
            {
                foreach (var j in new IntColl(3)) Console.Write("* ");
                Console.WriteLine();
                
            }
        }
    }

    class IntColl : IEnumerable<int>
    {
        private readonly int _num;

        public IntColl(int n)
        {
            _num = n;
        }

        // Foreach support
        public IEnumerator<int> GetEnumerator()
        {
            return new MyIntEnum(_num);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class MyIntEnum : IEnumerator<int>
    {
        private readonly int _num;
        private int _cur = -1;

        public MyIntEnum(int n)
        {
            _num = n;
        }

        public bool MoveNext()
        {
            if(_cur < _num) _cur++;
            return _cur < _num;
        }

        public void Reset()
        {
            _cur = 0;
        }

        object IEnumerator.Current => Current;

        public int Current => _cur;

        public void Dispose()
        {
            ;
        }
    }
}