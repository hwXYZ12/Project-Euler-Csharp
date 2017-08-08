// Euler047.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler047
{
    class Euler047
    {
        static void Main()
        {
            int i = 35;
            int search = 10000;
            List<int> primes = generatePrimes(search);
            while (i < search*search)
            {
                bool check = true;
                for (int j = 0; j < 4; ++j )
                {
                    if (!has4DistinctPrimeFactors(primes, i + j))
                        check = false;
                }
                if(check)
                {
                    break;
                }
                i++;
            }
            Console.WriteLine("Answer: " + i);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // checks if a number has 4 distinct prime factors
        static bool has4DistinctPrimeFactors(List<int> primes, int num)
        {
            if (isPrime(primes, num))
                return false;

            // factorize the number and dump its contents into
            // a sorted set. Any duplicates will not be placed in the set.
            List<int> factors = factorize(primes, num);
            SortedSet<int> distinctFactors = new SortedSet<int>();
            foreach(int x in factors)
            {
                distinctFactors.Add(x);
            }

            return (distinctFactors.Count == 4);

        }

        // returns a list of each prime factor of the given number
        // in order of determination
        static List<int> factorize(List<int> primes, int num)
        {
            List<int> ret = new List<int>();
            foreach(int x in primes)
            {
                while(num % x == 0)
                {
                    ret.Add(x);
                    num /= x;
                }
            }
            return ret;
        }

        // determines if a number is prime given a list of
        // primes less than or equal to the square root of the 
        // number in question
        // Note that this code makes some assumptions about its inputs!
        static bool isPrime(List<int> primes, int num)
        {
            if (num <= 1)
                return false;

            if (primes.Contains(num))
                return true;

            foreach (int x in primes)
            {
                if (num % x == 0)
                    return false;
            }
            return true;
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