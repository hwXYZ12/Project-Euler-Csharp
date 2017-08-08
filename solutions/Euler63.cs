// Euler063.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;

namespace Euler063
{
    class Euler063
    {
        static void Main()
        {
            // there are certain bounds on numbers that are a power of another number
            // AND also have the same number of digits as said power

            // only digits 1 through 9 could even be considered, and as you raise each digit
            // to a greater power you eventually hit a breakpoint after which the digit raised
            // to a power cannot produce enough digits to match the power. This is what our loop is based
            // on.
            BigInteger answer = 0;
            for (int i = 1; i < 10; ++i)
            {
                for(int j = 1; j < 22; ++j)
                {
                    BigInteger it = BigInteger.Pow(i, j);
                    List<BigInteger> digits = Permutator.getDigits(it);
                    if (digits.Count == j)
                        ++answer;
                }
            }            
            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}