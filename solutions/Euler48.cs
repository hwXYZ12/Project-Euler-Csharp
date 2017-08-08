// Euler048.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler048
{
    class Euler048
    {
        static void Main()
        {
            BigInteger sum = 0;
            BigInteger a = pow(10, 10);
            for (int i = 1; i <= 1000; ++i )
            {
                sum += (pow(i, i) % a);
            }

            sum = (sum % a);
            Console.WriteLine("Answer: " + sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty power calculation using
        // a biginteger class
        static BigInteger pow(int i, int j)
        {
            BigInteger ret = 1;
            for(int x = j; x > 0; --x)
            {
                ret = ret * i;
            }
            return ret;
        }

        
    }
}