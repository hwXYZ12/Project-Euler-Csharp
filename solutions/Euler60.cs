// Euler060.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;

namespace Euler060
{
    class Euler060
    {
        struct Pair
        {
            public BigInteger a;
            public BigInteger b;
        }

        static void Main()
        {

            List<Pair> pairs = new List<Pair>();
            int search = 10000;
            PrimeGenerator primeSet = new PrimeGenerator(search);
            for (int i = 0; i < primeSet.getSize(); ++i)
            {
                for (int j = 0; j < primeSet.getSize(); ++j)
                {
                    if (isPrimePair(primeSet, primeSet[i], primeSet[j]))
                    {
                        Pair p = new Pair();
                        p.a = primeSet[i];
                        p.b = primeSet[j];
                        pairs.Add(p);
                    }
                }
            }            

            List<BigInteger> left = new List<BigInteger>();
            foreach(Pair p in pairs)
            {
                if(!left.Contains(p.a))
                    left.Add(p.a);
                Console.WriteLine("(" + p.a + ", " + p.b + ")");
            }
            Dictionary<BigInteger, List<BigInteger>> friendlyPrimes = new Dictionary<BigInteger, List<BigInteger>>();
            for (int i = 0; i < left.Count; ++i )
            {
                List<BigInteger> t = new List<BigInteger>();
                foreach (Pair p in pairs)
                {
                    if (p.a == left[i] && !t.Contains(p.b))
                        t.Add(p.b);
                }
                friendlyPrimes.Add(left[i], t);
            }

            // we're searching for 4 numbers in our 'left' list
            // of primes for which each number can be found in the others
            // 'right' list of primes
            // How to go about this?
            // To start, any number on the right list is already a pair
            // with the number on the left. Let's call this number prime 1.
            // Any number on the right list of the number could potentially 
            // be a prime 1 number. The tricky part would be finding prime 2
            // and prime 3, for which prime 2 can be found in the right list
            // of prime 1 and prime 3 and similarly prime 3 can be found in the
            // right list of prime 2 and prime 1.

            // We now extend this method to include a fourth prime,
            // specifically we need to find prime 2, 3, and 4 in the rightlist
            // of prime 1, primes 1,3, and 4 in the rightlist of prime 2,
            // primes 1,2, and 4 in the rightlist of prime 3, and finally
            // primes 1,2, and 3 in the rightlist of prime 4
            for (int i = 0; i < left.Count; ++i )
            {
                List<BigInteger> rightList;
                friendlyPrimes.TryGetValue(left[i], out rightList);
                for(int j = 0; j < rightList.Count; ++j)
                {
                    // rightList[j] represents prime 1
                    for(int k = j + 1; k < rightList.Count; ++k)
                    {
                        // rightList[k] represents prime 2
                        for(int m = k + 1; m < rightList.Count; ++m)
                        {
                            // rightList[m] represents prime 3
                            for(int n = m + 1; n < rightList.Count; ++n)
                            {
                                // rightList[n] represents prime 4

                                // ensure that prime 2 and prime 3 can
                                // be found on the right list of prime 1
                                // and similarly that prime 1 and prime 3 can
                                // be found on the right list of prime 2 and
                                // prime 1 and prime 2 can be found on the right list
                                // of prime 3
                                bool check = true;
                                List<BigInteger> whichRightList;
                                if( friendlyPrimes.TryGetValue(rightList[j], out whichRightList) )
                                {
                                    if (!(whichRightList.Contains(rightList[k])
                                        && whichRightList.Contains(rightList[m])
                                        && whichRightList.Contains(rightList[n])))
                                    {
                                        check = false;
                                        continue;
                                    }
                                }
                                else
                                {
                                    check = false;
                                    continue;
                                }
                            
                                if(friendlyPrimes.TryGetValue(rightList[k], out whichRightList))
                                {
                                    if (!(whichRightList.Contains(rightList[j])
                                        && whichRightList.Contains(rightList[m])
                                        && whichRightList.Contains(rightList[n])))
                                    {
                                        check = false;
                                        continue;
                                    }
                                }
                                else
                                {
                                    check = false;
                                    continue;
                                }
                            
                                if(friendlyPrimes.TryGetValue(rightList[m], out whichRightList))
                                {
                                    if (!(whichRightList.Contains(rightList[j])
                                        && whichRightList.Contains(rightList[k])
                                        && whichRightList.Contains(rightList[n])))
                                    {
                                        check = false;
                                        continue;
                                    }
                                }
                                else
                                {
                                    check = false;
                                    continue;
                                }

                                if (friendlyPrimes.TryGetValue(rightList[n], out whichRightList))
                                {
                                    if (!(whichRightList.Contains(rightList[j])
                                        && whichRightList.Contains(rightList[k])
                                        && whichRightList.Contains(rightList[m])))
                                    {
                                        check = false;
                                        continue;
                                    }
                                }
                                else
                                {
                                    check = false;
                                    continue;
                                }
                                                        
                                if(check)
                                {
                                    // found our prime set

                                    Console.WriteLine("" + left[i] + ", " +
                                                        rightList[j] + ", " +
                                                        rightList[k] + ", " +
                                                        rightList[m] + ", " +
                                                        rightList[n]);

                                    // Keep the console window open in debug mode.
                                    Console.WriteLine("Press any key to exit.");
                                    Console.ReadKey();
                                    return;
                                }
                            }                            
                        }
                    }
                }
            }


            Console.WriteLine("No answer");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // checks if a list of ints (assumed to be prime!)
        // constitute a prime pair set
        static bool isPrimePairSet(PrimeGenerator pGen, BigInteger[] primes)
        {
            // we need to check that for each possible pair
            // both possible concatenations also represent a prime number
            for(int i = 0; i < primes.Length; ++i)
            {
                for(int j = i + 1; j < primes.Length; ++j)
                {
                    if (!(pGen.isPrime(BigInteger.Parse("" + primes[i] + primes[j]))
                       && pGen.isPrime(BigInteger.Parse("" + primes[j] + primes[i]))))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // checks if two primes form a "prime pair"
        static bool isPrimePair(PrimeGenerator pGen, BigInteger a, BigInteger b)
        {
            // we need to check that for each possible pair
            // both possible concatenations also represent a prime number
            return (pGen.isPrime(BigInteger.Parse("" + a + b))
                && pGen.isPrime(BigInteger.Parse("" + b + a)));
        }

    }
}