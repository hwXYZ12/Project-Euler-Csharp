// Euler037.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler037
{
    class Euler037
    {
        static void Main()
        {

            List<int> primes = generatePrimes(1000);
            int sum = 0;

            for (int i = 11; i < 1000000; ++i )
            {
                if (isTruncatablePrime(primes, i))
                {
                    Console.WriteLine(i);
                    sum += i;
                }
            }
                
            Console.WriteLine("Answer: " + sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
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

            foreach(int x in primes)
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
            for (int i = 3; i <= n; i+=2)
            {
                ret.Add(i);
            }

            while(prime != ret[ret.Count - 1])
            {
                // get next prime to work with
                // (smallest number still in the sieve such
                // that it is greater than the previous prime that
                // had been used)
                // the loop terminates when the next such prime doesn't exist,
                // that is the last used prime is also the last element in the list
                for (int i = 0; i < ret.Count; ++i )
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
                for(int i = 0; i < ret.Count; ++i)
                {
                    if (max < ret[i])
                        max = ret[i];
                }
                while(prime * j <= max)
                {
                    ret.Remove(prime*j);
                    ++j;
                }
            }

            return ret;
        }

        // determines if a prime is left to right and right
        // to left truncatable. This means that the prime represents a
        // list of primes if you were to delete the leftmost character
        // to generate the next prime in the series and similarly deleting
        // the rightmost character, ie 3797 -> 797, 97, 7 and 379, 37, 3
        static bool isTruncatablePrime(List<int> primes, int which)
        {
            if (!isPrime(primes, which))
                return false;

            // check left to right truncations
            string temp = "" + which;
            int len = temp.Length;
            for (int i = 0; i < len - 1; ++i)
            {
                temp = temp.Substring(1);
                if (!isPrime(primes, Int32.Parse(temp)))
                    return false;
            }

            // check right to left truncations
            temp = "" + which;
            for (int i = 0; i < len - 1; ++i)
            {
                temp = temp.Substring(0, temp.Length - 1);
                if (!isPrime(primes, Int32.Parse(temp)))
                    return false;
            }

            // in the case that every truncation of the prime is prime
            // then we know that the prime is itself a truncatable prime
            return true;

        }

    }
}