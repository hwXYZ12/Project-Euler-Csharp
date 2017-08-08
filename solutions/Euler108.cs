using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using PrimeNumbersPackage.PrimeNumbers;
using System.Numerics;


namespace exerciseProjectCSharp
{

    class Euler108
    {

        static void Main()
        {
            int n = 10;
            int m = 0;
            PrimeGenerator p = new PrimeGenerator((int)Math.Floor(Math.Sqrt(300000000)));
            while (m <= 1000)
            {

                m = 0;
                List<BigInteger> divisors = p.getDivisors(n);

                // get number of solutions
                for(int x = 0; x < divisors.Count; ++x)
                {
                    for(int j = x; j >= 0; --j)
                    {
                        if (p.isCoprime(divisors[x], divisors[j]))
                        {
                            ++m;
                        }
                    }
                }

                Console.WriteLine("Number: "+n);
                Console.WriteLine("Solutions: "+m);
                ++n;
            }


            Console.WriteLine(n);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

    }
}
        