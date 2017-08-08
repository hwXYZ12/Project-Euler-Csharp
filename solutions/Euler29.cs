// euler29.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler29
{
    class Euler29
    {
        static void Main()
        {
            SortedSet<BigInteger> s = new SortedSet<BigInteger>();
            for (int a = 2; a <= 100; ++a)
            {
                for (int b = 2; b <= 100; ++b)
                {
                    s.Add(pow(a, b));
                }
            }

            Console.WriteLine("Answer: " + s.Count);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // inefficient power computation
        static BigInteger pow(int x, int y)
        {
            BigInteger ret = x;
            for (int i = 1; i < y; ++i)
                ret *= x;
            return ret;
        }
    }
}