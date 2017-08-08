// Euler080.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;
using HomeGrown.ContinuedFractions;

namespace Euler080
{
    class Euler080
    {
        static void Main()
        {            
            BigInteger answer = 0;
            int search = (int)Math.Pow(10, 2);
            int search2 = (int)Math.Pow(10, 2);
            for (int a = 2; a <= search; ++a)
            {
                if(Math.Sqrt(a) % 1 != 0)
                {
                    RootAsContFraction r = new RootAsContFraction(a);
                    Rational soln = r.getPellEquationSolution();
                    BigDecimalExpansion bde;
                    for (int i = 0; i < search2; ++i )
                    {
                        soln = r.getNextPellEquationSoln(soln);
                    }
                    bde = new BigDecimalExpansion(soln, 100);

                    List<int> digits = bde.getDecimalExpansion();
                    BigInteger wholePart = bde.getWholePart();
                    int sizeOfWholePart = (int)Math.Floor(BigInteger.Log10(wholePart)) + 1;
                    answer += wholePart;
                    Console.WriteLine(bde.ToString());
                    for (int i = 0; i < (100 - sizeOfWholePart); ++i )
                    {
                        answer += digits[i];
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

