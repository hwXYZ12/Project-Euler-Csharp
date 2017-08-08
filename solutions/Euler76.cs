// Euler076.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler076
{
    class Euler076
    {
        static void Main()
        {
            BigInteger answer = 0;

            // our two dynamically generated arrays
            int search = 101;
            BigInteger[] f = new BigInteger[search];
            BigInteger[][] alpha = new BigInteger[search][];
            for (int i = 0; i < search; ++i)
            {
                alpha[i] = new BigInteger[search];
            }

            // initial values of f and alpha
            f[0] = 1;
            f[1] = 1;
            alpha[1][1] = 1;
            alpha[1][0] = 1;
            for (int n = 0; n < search; ++n)
            {
                alpha[1][n] = 1;
                alpha[n][1] = 1;
                alpha[n][0] = 1;
            }

            // find the smallest partition number n such that
            // n is divisible by 10^6
            BigInteger million = (BigInteger)Math.Pow(10, 6);

            // generate the values of f and alpha
            BigInteger sum = 0;
            for (int n = 2; n < search; ++n)
            {
                // compute alpha needed to compute f
                for (int i = 0; i < n; ++i)
                {
                    if (n - i >= i)
                        alpha[n - i][i] = f[i];
                    else if (n - i != 1)
                    {
                        sum = 0;
                        for (int j = 0; j <= 2 * i - n - 1; ++j)
                        {
                            sum += alpha[i - j][j];
                        }
                        alpha[n - i][i] = f[i] - sum;
                    }
                }

                // compute f
                sum = 0;
                for (int i = 0; i < n; ++i)
                {
                    sum += alpha[n - i][i];
                }
                f[n] = sum;

            }

            Console.WriteLine("Answer: " + (f[100] - 1));


            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
