// Euler066.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using HomeGrown.ContinuedFractions;

namespace Euler066
{
    class Euler066
    {
        static void Main()
        {

            BigInteger answer = 0;
            int search = 1001;
            BigInteger max = 0;

            for (int d = 2; d < search; ++d)
            {
                if (Math.Sqrt(d) % 1 != 0)
                {
                    BigInteger x = getMinSol(d);
                    if (x >= max)
                    {
                        max = x;
                        answer = d;
                    }
                    Console.WriteLine("d: " + d);
                    Console.WriteLine("min x: " + x);
                }
            }
            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // returns the minimal solution of x to the
        // equation x^2 + d*y^2 = 1
        // Makes use of knowledge of Pell's equation!
        // makes some assumptions about the input!
        static BigInteger getMinSol(int d)
        {
            // first we need to know the period of the continued fraction
            // of the square root of d
            RootAsContFraction r = new RootAsContFraction(d);
            List<int> repeating = r.getRepeatingBlock();
            Rational t;
            int i = 1;
            if (repeating.Count % 2 == 0)
            {
                // we do something when the continued fraction has even period
                // when we have even period, then let p = the period and our
                // solution corresponds to the pth convergent
                i = repeating.Count;
                t = r.toRational(i);
                if (t.a * t.a - d * t.b * t.b == 1)
                    return t.a;
            }
            else
            {
                // we do something else when it has odd period
                // When we have odd period, let p = the period and 
                // the solution corresponds to the 2pth convergent
                i = repeating.Count * 2;
                t = r.toRational(i);
                if (t.a * t.a - d * t.b * t.b == 1)
                    return t.a;
            }

            // brute force search all convergents of the square root
            // written as a continued fraction and check for an answer
            // to the pell equation. This is used as a last resort in the
            // case that the above doesn't properly find a solution... which it should...
            while(t.a*t.a - d*t.b*t.b != 1)
            {
                t = r.toRational(++i);
            }

            // debug, this should never be reached
            Console.WriteLine("Period: " + r.getRepeatingBlock().Count);
            Console.WriteLine("Convergent Index: " + i);

            return t.a;
        }
    }
}