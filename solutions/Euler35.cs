// Euler035.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler035
{
    class Euler035
    {
        static void Main()
        {
            int total = 0;
            List<int> primes = generatePrimes(1000);

            // we're going to check for primality of each
            // number from 2 to 999999 and then we will check
            // circularity of said prime
            for (int x = 2; x < 1000000; ++x)
            {

                if (isPrime(primes, x) && isCircularPrime(primes,x))
                {
                    Console.WriteLine(x);
                    ++total;
                }
            }

            Console.WriteLine("Answer: " + total);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
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

        // determines if a number is prime given a list of
        // primes less than or equal to the square root of the 
        // number in question
        // Note that this code makes some assumptions about its inputs!
        static bool isPrime(List<int> primes, int num)
        {
            if (primes.Contains(num))
                return true;

            foreach(int x in primes)
            {
                if (num % x == 0)
                    return false;
            }
            return true;
        }

        // determines if a prime is circular
        // Note that this code makes some assumptions about its inputs!
        // Specifically, we're assuming we have all the primes listed up to
        // and including the square root of the number in question as well
        // as all of its possible rotations
        static bool isCircularPrime(List<int> primes, int which)
        {

            // get the digits of the prime
            List<int> digits = new List<int>();
            int digit, temp = which;
            while(temp != 0)
            {
                digit = temp % 10;
                temp /= 10;
                digits.Add(digit);
            }

            // for each rotation of the digits, check whether
            // the number formed is a prime number
            for(int i = 0; i < digits.Count; ++i)
            {
                // get the next rotation
                int swap = digits[0];
                for (int j = 0; j < digits.Count - 1; ++j)
                {
                    digits[j] = digits[j + 1];
                }
                digits[digits.Count-1] = swap;
                
                // compute the number formed by the rotation
                int num = 0;
                for (int j = 0; j < digits.Count; ++j)
                {
                    num += pow(10, j) * digits[j];
                }

                // check if that number is prime against the input list
                if (!isPrime(primes, num))
                    return false;
            }

            // in the case that every rotation of the prime is also prime
            // we know that the input prime is circular and we return true
            return true;

        }

        // inefficient power computation
        static int pow(int x, int y)
        {
            if (y == 0)
                return 1;

            int ret = x;
            for (int i = 1; i < y; ++i)
                ret *= x;
            return ret;
        }
    }
}