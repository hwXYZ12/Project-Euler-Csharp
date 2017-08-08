// Euler30.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler30
{
    class Euler30
    {
        static void Main()
        {
            int sum = 0;
            SortedSet<BigInteger> s = new SortedSet<BigInteger>();
            for (int x = 2; x <= 1000000; ++x)
            {
                if (isNarcissistic(x))
                {
                    sum += x;
                    Console.WriteLine(x);
                }
            }

            Console.WriteLine("Answer: " + sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // inefficient narcissism calculation
        static bool isNarcissistic(int n)
        {
            // compute the sum of each digit in the number
            // raised to the 5th power
            int digit, sum = 0, temp = n;
            while(n != 0)
            {
                digit = n % 10;
                sum += pow(digit, 5);
                n /= 10;
            }
            return (sum == temp);
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