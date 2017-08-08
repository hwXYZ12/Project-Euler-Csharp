// Euler041.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler041
{
    class Euler041
    {
        static void Main()
        {
            int max = 1423;
            for(int i = 4; i <= 7; ++i)
            {
                List<int> primes = generatePrimes(Convert.ToInt32(Math.Ceiling(Math.Sqrt(Math.Pow(10, i+1)))));
                
                // generate a list of pandigital numbers with i digits
                // and check primality of each
                int sum = 0;
                string s = "";
                for(int j = 1; j <= i; ++j)
                {
                    s += j;
                }
                sum = Int32.Parse(s);

                foreach(int x in computePermutations(sum))
                {
                    if(isPrime(primes, x) && isPandigital(x, i))
                    {
                        if (x > max)
                            max = x;
                    }
                }

            }

            Console.WriteLine("Answer: " + max);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty n-digit pandigital check
        static bool isPandigital(int which, int n)
        {
            if (which >= 1000000000 || n < 2)
                return false;

            // get each digit
            SortedSet<int> digits = new SortedSet<int>();
            while (which != 0)
            {
                digits.Add(which % 10);
                which /= 10;
            }

            // remove each digit from 1 to n from
            // the populated set of digits. If a digit cannot be found,
            // return false, else all digits have been found and we check that
            // the set is empty. If the set is also empty, then we return true.
            for (int i = 1; i <= n; ++i)
            {
                if (!digits.Remove(i))
                    return false;
            }

            return (digits.Count == 0);
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

        // determines if a number is prime given a list of
        // primes less than or equal to the square root of the 
        // number in question
        // Note that this code makes some assumptions about its inputs!
        static bool isPrime(List<int> primes, int num)
        {
            if (primes.Contains(num))
                return true;

            foreach (int x in primes)
            {
                if (num % x == 0)
                    return false;
            }
            return true;
        }

        // quick and dirty recursive permutation routine
        static List<int> computePermutations(int x)
        {

            List<int> ret = new List<int>();

            if (("" + x).Length == 1)
            {
                ret.Add(x);
                return ret;
            }

            for(int i = 0; i < (""+x).Length; ++i)
            {
                // recreate integer without the picked digit
                int sub = Int32.Parse(("" + x).Substring(0, i) + ("" + x).Substring(i+1));
                foreach(int z in computePermutations(sub))
                {
                    ret.Add(Int32.Parse("" + ("" + x).Substring(i,1) + z));
                }
            }
            return ret;
        }
    }
}