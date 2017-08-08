// Euler034.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler034
{
    class Euler034
    {
        static void Main()
        {
            int sum = 0;
            for (int x = 3; x <= 10000000; ++x)
            {
                if (isEqualToSumOfDigitsFactorials(x))
                    sum += x;
            }

            Console.WriteLine("Answer: " + sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // inefficient sum of factorial of digits check
        static bool isEqualToSumOfDigitsFactorials(int n)
        {
            // compute the sum of each digit factorial in the number
            int digit, sum = 0, temp = n;
            while (n != 0)
            {
                digit = n % 10;
                sum += fact(digit);
                n /= 10;
            }
            return (sum == temp);
        }

        // inefficient factorial computation
        static int fact(int x)
        {
            int ret = 1;
            for (int i = x; i > 0; --i)
                ret *= i;
            return ret;
        }
    }
}