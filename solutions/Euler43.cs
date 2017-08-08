// Euler043.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler043
{
    class Euler043
    {
        static void Main()
        {
            List<int> primes = generatePrimes(18);
            List<string> pandigitals = computePermutations("9876543210");

            BigInteger sum = 0;
            foreach(string z in pandigitals)
            {
                bool hasProperty = true;
                for (int i = 1; i < primes.Count+1; ++i )
                {
                    if((Int32.Parse(z.Substring(i, 3)) % primes[i-1] != 0))
                    {
                        hasProperty = false;
                        break;
                    }
                }
                if(hasProperty)
                {
                    sum += BigInteger.Parse(z);
                    Console.WriteLine(z);
                }
            }

            Console.WriteLine("Answer: " + sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty recursive permutation routine
        static List<string> computePermutations(string x)
        {

            List<string> ret = new List<string>();

            if (x.Length == 1)
            {
                ret.Add(x);
                return ret;
            }

            for (int i = 0; i < x.Length; ++i)
            {
                // recreate integer without the picked digit
                string sub = x.Substring(0, i) + x.Substring(i + 1);
                foreach (string z in computePermutations(sub))
                {
                    ret.Add(x.Substring(i, 1) + z);
                }
            }
            return ret;
        }

        // prime computation via Sieve of Eratosthenes
        static List<int> generatePrimes(int n)
        {
            List<int> ret = new List<int>();
            int prime = 2;
            ret.Add(2);

            // populate the sieve
            for (int i = 3; i <= n; i += 2)
            {
                ret.Add(i);
            }

            while (prime != ret[ret.Count - 1])
            {
                // get next prime to work with
                // (smallest number still in the sieve such
                // that it is greater than the previous prime that
                // had been used)
                // the loop terminates when the next such prime doesn't exist,
                // that is the last used prime is also the last element in the list
                for (int i = 0; i < ret.Count; ++i)
                {
                    if (ret[i] > prime)
                    {
                        prime = ret[i];
                        break;
                    }
                }

                // perform the sieve: remove all the elements that
                // are multiples of the chosen prime
                int j = 2;
                int max = ret[0];
                for (int i = 0; i < ret.Count; ++i)
                {
                    if (max < ret[i])
                        max = ret[i];
                }
                while (prime * j <= max)
                {
                    ret.Remove(prime * j);
                    ++j;
                }
            }

            return ret;
        }
    }
}