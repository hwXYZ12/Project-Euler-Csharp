// Euler087.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using HomeGrown.PrimeNumbers;
using System.Text;
using HomeGrown.Permutations;
using System.Linq;
using HomeGrown.ContinuedFractions;
using HomeGrown.TextHandling;
using HomeGrown.DirectedGraph;

namespace Euler087
{

    class Euler087
    {
        static int search = 50 * (int) Math.Pow(10, 6);
        static int squareSearch = (int)Math.Ceiling(Math.Sqrt(search));
        static int cubeSearch = (int)Math.Ceiling(Math.Pow(search, (1.0/3)));
        static int fourthPowerSearch = (int)Math.Ceiling(Math.Pow(search, (1.0 / 4)));
 
        static void Main()
        {
            PrimeGenerator primes = new PrimeGenerator(squareSearch);
            int count = 0;
            int numPrimes = primes.getSize();
            SortedSet<int> check = new SortedSet<int>();

            for (int i = 0; i < numPrimes && primes[i] <= squareSearch; ++i )
            {
                for (int j = 0; j < numPrimes && primes[j] <= cubeSearch; ++j)
                {
                    for (int k = 0; k < numPrimes && primes[k] <= fourthPowerSearch; ++k)
                    {
                        int x = (int)(Math.Pow((double)primes[i], 2) + 
                                    Math.Pow((double)primes[j], 3) + 
                                    Math.Pow((double)primes[k], 4));
                        if (x <= search && !check.Contains(x))
                        {
                            check.Add(x);
                            count++;
                        }
                    }
                }
            }

            Console.WriteLine(count);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
