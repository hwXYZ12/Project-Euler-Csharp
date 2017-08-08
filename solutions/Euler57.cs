// Euler057.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler057
{
    class Euler057
    {
        static void Main()
        {
            int answer = 0;
            int search = 1000;
            BigInteger a = 1, b = 1;
            for (int i = 1; i <= search; ++i)
            {
                BigInteger t1, t2;
                t1 = a + 2 * b;
                t2 = a + b;
                a = t1;
                b = t2;
                if (numDigits(a) > numDigits(b))
                {
                    Console.WriteLine(true);
                    ++answer;
                } else
                {
                    Console.WriteLine(false);
                }
                Console.WriteLine(a + "/" + b);
            }

            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty get number of digits
        static BigInteger numDigits(BigInteger x)
        {
            BigInteger ret = 0;
            while (x != 0)
            {
                ++ret;
                x /= 10;
            }
            return ret;
        }
    }
}