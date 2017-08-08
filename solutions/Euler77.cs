// Euler077.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler077
{
    class Euler077
    {
        static void Main()
        {
            BigInteger answer = 0;

            // our two dynamically generated arrays
            int search = (int)Math.Pow(10, 4);
            PrimeGenerator pGen = new PrimeGenerator(search);
            BigInteger[] f = new BigInteger[search];
            BigInteger[][] alpha = new BigInteger[search][];
            for (int i = 0; i < search; ++i)
            {
                alpha[i] = new BigInteger[search];
            }

            // initial values of f and alpha
            f[0] = 1;
            f[1] = 0;
            f[2] = 1;
            f[3] = 1;
            f[4] = 1;
            alpha[1][1] = 0;
            alpha[1][0] = 1;
            for (int n = 0; n < search; ++n)
            {
                alpha[1][n] = 0;
                alpha[n][1] = 0;
            }

            // generate the values of f and alpha
            List<BigInteger> primes = pGen.getPrimes();
            BigInteger sum = 0;
            for (int n = 2; n < search; ++n)
            {
                // compute alpha needed to compute f                
                for (int i = 0; primes[i] <= n; ++i)
                {
                    int p = (int) primes[i];
                    if (p >= n - p)
                        alpha[p][n-p] = f[n-p];
                    else // we may need something here...
                    {
                        sum = 0;
                        for (int j = 0; primes[j] <= n - p; ++j)
                        {
                            if (!(primes[j] > p))
                                continue;

                            int p2 = (int) primes[j];
                            sum += alpha[p2][n-p-p2];
                        }
                        alpha[p][n-p] = f[n-p] - sum;
                    }
                }

                // compute f
                sum = 0;
                for (int i = 0; primes[i] <= n; ++i)
                {
                    int p = (int)primes[i];
                    sum += alpha[p][n-p];
                }
                f[n] = sum;

                if (f[n] >= 5000)
                {
                    answer = n;
                    break;
                }

            }

            Console.WriteLine("Answer: " + answer);


            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
