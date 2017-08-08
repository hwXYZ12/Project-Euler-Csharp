// Euler072.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler072
{
    class Euler072
    {
        static void Main()
        {
            BigInteger answer = 0;
            int search = (int) Math.Pow(10, 6);     
            PrimeGenerator primeGen = new PrimeGenerator((int)Math.Ceiling(Math.Sqrt(search)));       
            for (int a = 2; a <= search; ++a)
            {
                answer += primeGen.totient((BigInteger)a);
            }

            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
