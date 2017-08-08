// Euler049.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
namespace Euler049
{
    class Euler049
    {
        static void Main()
        {
            int search = 10000;
            List<int> temp = generatePrimes(search);
            List<int> primes = new List<int>();
            foreach(int x in temp)
            {
                if (x >= 1000 && x <= 9999)
                    primes.Add(x);
            }
            temp = null;

            PrimePermutationDict permDict = new PrimePermutationDict(primes);

            foreach(int x in primes)
            {
                List<int> permutations = permDict.getPrimePermutations(x);
                List<List<int>> sequences = getArithmeticSequences(permutations);
                foreach(List<int> seq in sequences)
                {
                    Console.WriteLine("");
                    foreach(int y in seq)
                    {
                        Console.WriteLine(y);
                    }
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private class ListComparer : IEqualityComparer<List<int>>
        {
            // returns true when two lists of ints
            // contain the same numbers
            public bool Equals(List<int> a, List<int> b)
            {
                bool check1 = true;
                foreach (int x in a)
                {
                    if (!b.Contains(x))
                        check1 = false;
                }

                bool check2 = true;
                foreach (int x in b)
                {
                    if (!a.Contains(x))
                        check2 = false;
                }

                return (check1 && check2);
            }

            // I don't know what this does, lol
            public int GetHashCode(List<int> list)
            {
                //Check whether the object is null
                if (Object.ReferenceEquals(list, null)) return 0;

                //Calculate a hash code for your list of numbers
                int code = 0;
                for (int i = 0; i < list.Count; ++i)
                {
                    code += list[i] ^ (i+1);
                }
                return code;
            }
        }

        // given a list of numbers, we seperate the list into
        // yet another list of arithmetic sequences (each represented by
        // a list of numbers)
        static List<List<int>> getArithmeticSequences(List<int> nums)
        {
            // ensure that the numbers are sorted
            nums.OrderBy(x => x);

            List<int> deltas = new List<int>();
            foreach (int x in nums)
            {
                foreach (int y in nums)
                {
                    if (x != y)
                    {
                        if (!deltas.Contains(x - y))
                            deltas.Add(x - y);
                    }
                }
            }

            List<List<int>> sequences = new List<List<int>>();
            foreach (int del in deltas)
            {
                // break the numbers into equivalence classes
                // based on del, where each equivalence class is
                // appended to the list of sequences
                for (int i = 0; i < nums.Count; ++i)
                {
                    // get all the elements that belong to the
                    // equivalence class nums[i] mod del and put
                    // them into a list
                    List<int> numModDel = new List<int>();
                    for (int j = 0; j < nums.Count; ++j)
                    {
                        if (nums[i] % del == nums[j] % del)
                            numModDel.Add(nums[j]);
                    }
                    if (numModDel.Count > 2)
                        sequences.Add(numModDel);
                }
            }
            sequences.Distinct(new ListComparer());

            // now that we have all equivalence classes, we
            // want to remove any equivalence classes that don't
            // also constitute an arithmetic sequence
            List<List<int>> ret = new List<List<int>>();
            foreach(List<int> x in sequences)
            {
                x.OrderBy(a => a);
                int k = x[1] - x[0];
                bool check = true;
                for (int i = 2; i < nums.Count; ++i)
                {
                    if (nums[i] - nums[i - 1] != k)
                    {
                        check = false;
                    }
                }
                if(check)
                {
                    ret.Add(x);
                }
            }
            return ret;
        }

        // Creates a dictionary of permuatations of a prime number
        // using a list of prime numbers as input
        private class PrimePermutationDict
        {
            private Dictionary<int, List<int>> dict = new Dictionary<int,List<int>>();

            public PrimePermutationDict(List<int> primes)
            {
                foreach(int x in primes)
                {
                    List<int> temp = null;                    
                    if(dict.TryGetValue(Int32.Parse(sort(""+x)), out temp))
                    {
                        temp.Add(x);
                    } else{
                        temp = new List<int>();
                        temp.Add(x);
                        dict.Add(Int32.Parse(sort(""+x)), temp);
                    }
                }
            }

            public List<int> getPrimePermutations(int prime)
            {
                List<int> ret = null;
                dict.TryGetValue(Int32.Parse(sort(""+prime)), out ret);
                return ret;
            }

            private string sort(string s)
            {
                return String.Concat(s.OrderBy(x => x));
            }
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