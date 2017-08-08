// Euler056.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler056
{
    class Euler056
    {
        static void Main()
        {
            int answer = 35;
            int search = 100;
            BigInteger max = 0;
            for (int a = 0; a < search; ++a)
            {
                for (int b = 0; b < search; ++b )
                {
                    BigInteger x = sumOfDigits(pow(a,b));
                    if (x > max)
                        max = x;
                }
            }
                
            Console.WriteLine("Answer: " + max);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty sum of each digit of a number
        static BigInteger sumOfDigits(BigInteger x)
        {
            BigInteger ret = 0;
            while(x != 0)
            {
                ret += x % 10;
                x /= 10;
            }
            return ret;
        }

        // quick and dirty power function
        static BigInteger pow(BigInteger a, BigInteger b)
        {
            BigInteger ret = a;
            for (; b > 0; --b)
                ret *= a;
            return ret;
        }
    }
}