// Euler058.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler058
{
    class Euler058
    {
        static void Main()
        {
            int answer = 0;
            int search = 100000;
            List<int> primes = generatePrimes(search);
            int sideLength = 3;
            int pX = 0, diagonal = 1, inc = 2;
            while(true)
            {
                for(int t = 0; t < 4; ++t)
                {
                    diagonal += inc;

                    if (isPrime(primes, diagonal))
                    {
                        ++pX;
                    }
                }         
                
                double check = ((double)pX / (2*sideLength - 1)); 
                if(check < 0.1)
                {
                    answer = sideLength;
                    break;
                }

                // increases the sideLength and the 
                // difference between diagonals of the next spiral
                inc += 2;
                sideLength+=2;
            }

            Console.WriteLine("Answer: " + answer );

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