// Euler073.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler073
{
    class Euler073
    {
        static void Main()
        {
            BigInteger answer = 0;
            int search = (int)(Math.Pow(10, 3)*12);
            SortedSet<double> fractions = new SortedSet<double>();
            for (int a = 2; a <= search; ++a)
            {
                for(int b = 1; b < a; ++b)
                {
                    double f = (double) b / a;
                    if(f > (double) 1 / 3
                        && f < (double) 1 / 2
                        && !fractions.Contains(f))
                    {
                        fractions.Add(f);
                    }
                }
            }
            answer = fractions.Count;

            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
