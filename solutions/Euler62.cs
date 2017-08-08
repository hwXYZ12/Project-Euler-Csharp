// Euler062.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using PrimeNumbers;
using Permutations;

namespace Euler062
{
    class Euler062
    {
        static void Main()
        {
            // We're going to create a permutation dictionary of all
            // cubic values up to a certain point. The purpose here is to
            // be able to attach a list of all discovered cubic permutations
            // to any particular cubic value that when sorted acts as a key
            // to this dictionary
            BigInteger answer = 0;
            Dictionary<BigInteger, List<BigInteger>> cubicDict = new Dictionary<BigInteger, List<BigInteger>>();
            int search = 100000;
            for(int i = 0; i < search; ++i)
            {
                // get the cube of the index, convert it to a string,
                // sort it, and use the sorted permutation of it's digits
                BigInteger it = BigInteger.Pow(i, 3);
                List<BigInteger> digits = Permutator.getDigits(it);
                digits.Sort();
                BigInteger sorted = 0;
                for(int j = 0; j < digits.Count; ++j)
                {
                    sorted += BigInteger.Pow(10, j) * digits[j];
                }
                List<BigInteger> knownPerms;
                
                // we want to enforce the rule that associated permutations
                // have the same number of digits
                List<BigInteger> sortedDigits = Permutator.getDigits(sorted);
                if (sortedDigits.Count == digits.Count)
                {
                    if (cubicDict.TryGetValue(sorted, out knownPerms))
                    {
                        knownPerms.Add(it);
                        if (knownPerms.Count == 5)
                        {
                            foreach (BigInteger x in knownPerms)
                            {
                                Console.WriteLine(x);
                            }
                            answer = i;
                            break;
                        }
                    }
                    else
                    {
                        knownPerms = new List<BigInteger>();
                        knownPerms.Add(it);
                        cubicDict.Add(sorted, knownPerms);
                    }
                }                
            }
            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}