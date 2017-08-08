// Euler078.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler078
{
    class Euler078
    {
        static void Main()
        {
            BigInteger answer = 0;

            // After coming up with a particularly slow algorithm that
            // took up alot of space (I ran out of memory a few times!)
            // I'm now going to implement an algorithm that almost certainly should
            // not run out of memory and runs in much better asymptotic time
            List<BigInteger> p = new List<BigInteger>();
            p.Add(1);
            p.Add(1);
            p.Add(2);
            p.Add(3);

            Func<BigInteger, BigInteger> pentagonal = (k => k*(3 * k - 1) / 2);
            BigInteger oneMillion = BigInteger.Pow(10, 6);

            int n = 4;
            while(true)
            {
                int k = 1;
                BigInteger sum = 0;
                while(true)
                {
                    BigInteger g = pentagonal(k);
                    BigInteger sign = (k % 2 == 0 ? -1 : 1);
                    if(k > 0)
                    {
                        // flip the sign of k
                        k *= -1;
                    } else
                    {
                        // flip the sign of k back and increment it
                        k *= -1;
                        ++k;
                    }
                    // g is larger than n so we truncate the sum
                    // (we would be adding zeros indefinitely otherwise)
                    if(n - g < 0)
                    {
                        break;
                    }
                    else
                    {
                        sum += sign*p[n - (int)g];
                    }
                }

                // once we've computed the recurrence sum
                // we add it to the list of partition numbers
                p.Add(sum);

                if(p[n] % oneMillion == 0)
                {
                    answer = n;
                    break;
                }
                ++n;
            }           
            Console.WriteLine("Answer: " + answer);


            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
