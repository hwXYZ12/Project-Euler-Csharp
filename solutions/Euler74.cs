// Euler074.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler074
{
    class Euler074
    {
        static int nextDigitFactorial(int x)
        {
            int ret = 0;
            List<int> digits = Permutator.getDigits(x);
            Func<int, int> factorial = null;
            factorial = y => y <= 1 ? 1 : y * factorial(y - 1);
            foreach (int a in digits)
                ret += factorial(a);
            return ret;
        }

        static void Main()
        {
            BigInteger answer = 0;
            int search = (int)Math.Pow(10, 6);            
            SortedSet<int> check = new SortedSet<int>();
            int next = 0, count = 0;
            for (int a = 2; a < search; ++a)
            {
                check.Clear();
                check.Add(a);
                count = 1;
                next = nextDigitFactorial(a);
                while(!check.Contains(next))
                {
                    check.Add(next);
                    ++count;
                    next = nextDigitFactorial(next);
                }

                // hit this point when we've found a repeated term!
                // note that this term doesn't get added to the count,
                // so we can use the 'count' variable directly
                if(count == 60)
                {
                    ++answer;
                }
            }

            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
