// Euler070.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler070
{
    class Euler070
    {        

        static void Main()
        {
            int search = Convert.ToInt32(Math.Ceiling(Math.Sqrt(Math.Pow(10, 7))));
            int search2 = Convert.ToInt32(Math.Ceiling(Math.Pow(10, 7)));
            PrimeGenerator primeGen = new PrimeGenerator(search);
            List<BigInteger> primes = primeGen.getPrimes();

            double min = search2;
            int tot = 0;
            for (int i = 2; i <= search2; ++i )
            {
                tot = totient(i, primeGen);
                if (Permutator.isPermutation(i, tot) 
                    && min >= ((double) i) / tot)
                {
                    Console.WriteLine(i);
                    min = ((double) i) / tot;
                }
            }

            Console.WriteLine("Answer: ");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static int totient(int x, PrimeGenerator primes)
        {
            if (primes.isPrime((BigInteger)x))
            {
                return x - 1;
            }

            // get the factors of x and make
            // the list unique
            List<BigInteger> factors = primes.getFactors((BigInteger)x);
            factors = factors.Distinct().ToList();

            double multiplier = 1;
            foreach(BigInteger f in factors)
            {
                multiplier *= (1 - (1 / (double)f));
            }

            return ((int)Math.Round(x * multiplier));           
        }
    }
}