// Euler052.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler052
{
    class Euler052
    {
        static void Main()
        {
            BigInteger answer = 0;
            string it = "125874";
            BigInteger start = BigInteger.Parse(it);
            List<string> permutations = computePermutations(it);

            for (int i = 0; i < permutations.Count; ++i )
            {
                bool isAnswer = true;
                for(int j = 1;j<=6;++j)
                {
                    if (!sameDigits(j * BigInteger.Parse(permutations[i]), start))
                    {
                        isAnswer = false;
                        break;
                    }
                }
                if(isAnswer)
                {
                    answer = BigInteger.Parse(permutations[i]);
                    break;
                }
            }
            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty recursive permutation routine
        static List<string> computePermutations(string x)
        {

            List<string> ret = new List<string>();

            if (x.Length == 1)
            {
                ret.Add(x);
                return ret;
            }

            for (int i = 0; i < x.Length; ++i)
            {
                // recreate integer without the picked digit
                string sub = x.Substring(0, i) + x.Substring(i + 1);
                foreach (string z in computePermutations(sub))
                {
                    ret.Add(x.Substring(i, 1) + z);
                }
            }
            return ret;
        }

        // quick and dirty nth digit pandigital check
        static bool isPandigital(BigInteger which, int n)
        {
            if (which >= 1000000000 || n < 2)
                return false;

            // get each digit
            SortedSet<BigInteger> digits = new SortedSet<BigInteger>();
            while (which != 0)
            {
                digits.Add(which % 10);
                which /= 10;
            }

            // remove each digit from 1 to n from
            // the populated set of digits. If a digit cannot be found,
            // return false, else all digits have been found and we check that
            // the set is empty. If the set is also empty, then we return true.
            for (int i = 0; i <= n; ++i)
            {
                if (!digits.Remove(i))
                    return false;
            }

            return (digits.Count == 0);
        }

        // quick and dirty check if two integers contain the same digits
        static bool sameDigits(BigInteger a, BigInteger b)
        {
            // get each set of digits
            SortedSet<BigInteger> digitsA = new SortedSet<BigInteger>();
            SortedSet<BigInteger> digitsB = new SortedSet<BigInteger>();
            while (a != 0)
            {
                digitsA.Add(a % 10);
                a /= 10;
            }
            while (b != 0)
            {
                digitsB.Add(b % 10);
                b /= 10;
            }

            // ensure that each number has the same number
            // of digits
            if (digitsA.Count != digitsB.Count)
                return false;

            // remove each digit that is contained in digitsA from
            // digitsB and check whether digitsB is empty
            foreach(BigInteger i in digitsA)
            {
                if (!digitsB.Remove(i))
                    return false;
            }

            return (digitsB.Count == 0);
        }

    }
}