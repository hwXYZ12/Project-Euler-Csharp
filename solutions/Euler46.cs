// Euler046.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler046
{
    class Euler046
    {
        static void Main()
        {
            int odd = 35;
            int search = 10000;
            List<int> primes = generatePrimes(search);
            while(odd < search)
            {
                if(!isPrime(primes, odd))
                {
                    if (!isSumOfPrimeAndTwiceSquare(primes, odd))
                        break;
                }
                odd += 2;
            }
            Console.WriteLine("Answer: " + odd);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // checks if the number is potentially the sum of
        // a prime and twice a square
        static bool isSumOfPrimeAndTwiceSquare(List<int> primes, int num)
        {
            foreach(int x in primes)
            {
                if (Math.Sqrt((num - x) / 2) % 1 == 0)
                    return true;
            }
            return false;
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