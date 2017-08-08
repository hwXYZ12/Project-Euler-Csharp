// euler27.cs
using System;
using System.Collections.Generic;
namespace Euler27
{
    class Euler27
    {
        static void Main()
        {
            int maxPrimes = 40, result = 41;
            int ret1=0, ret2=0;
            for (int a = -999; a < 1000; ++a )
            {
                for (int b = 1; b < 1000; ++b )
                {
                    int n = 0;
                    while((n*n+a*n+b)>0 && isPrime(n*n+a*n+b))
                    {
                        ++n;
                    }
                    int p = n + 1;
                    if (p > maxPrimes)
                    {
                        maxPrimes = p;
                        result = a * b;
                        ret1 = a;
                        ret2 = b;
                    }
                }
            }

            Console.WriteLine("Answer: " + result);
            Console.WriteLine("Answer: " + maxPrimes);
            Console.WriteLine("Answer: " + ret1);
            Console.WriteLine("Answer: " + ret2);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // extremely inefficient but simple prime check
        static bool isPrime(int x)
        {
            for(int i = 2; i < x; ++i)
            {
                if (x % i == 0)
                    return false;
            }
            return true;
        }
    }
}