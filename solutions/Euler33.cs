// Euler033.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler033
{
    class Euler033
    {
        static void Main()
        {
            SortedSet<BigInteger> s = new SortedSet<BigInteger>();
            for (int a = 1; a <= 9; ++a)
            {
                for (int b = 1; b <= 9; ++b)
                {
                    for(int c = 1; c <=9; ++c)
                    {
                        for(int d = 1; d <= 9; ++d)
                        {
                                
                            if ( (((double) 10*a+b) / (10*c+d) < 1) &&
                                ((isEquivalent(10*a+b, 10*c+d, a, c) && b == d)
                                || (isEquivalent(10*a+b, 10*c+d, a, d) && b == c)
                                || (isEquivalent(10*a+b, 10*c+d, b, c) && a == d)
                                || (isEquivalent(10*a+b, 10*c+d, b, d) && a == c)))
                            {
                                Console.WriteLine((10 * a + b)+ "/" + (10 * c + d));
                            }
                        }
                    }
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // simplification computation, return a / b == x / y
        static bool isEquivalent(int a, int b, int x, int y)
        {
            return ((double) a / b == (double) x / y);
        }

        // inefficient power computation
        static int pow(int x, int y)
        {
            int ret = x;
            for (int i = 1; i < y; ++i)
                ret *= x;
            return ret;
        }
    }
}