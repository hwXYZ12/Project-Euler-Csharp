// Euler075.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler075
{
    class Euler075
    {
        //static void Main()
        //{
        //    // this whole algorithm is a bit complicated and takes a bit more explanation
        //    // than I'd like to offer here....

        //    BigInteger answer = 0;
        //    int search = 250; //(int)(Math.Pow(10, 3)*0.5);
        //    SortedSet<int> checkSet = new SortedSet<int>();
        //    SortedSet<int> badSet = new SortedSet<int>();
        //    for (int a = 3; a <= search; ++a)
        //    {
        //        for (int b = a + 1; b <= search; ++b)
        //        {
        //            for (int c = b + 1; c <= search; ++c)
        //            {
        //                if (a * a + b * b == c * c)
        //                {
        //                    int sum = a + b + c;
        //                    if (sum <= search)
        //                    {
        //                        if (checkSet.Contains(sum))
        //                        {
        //                            badSet.Add(sum);
        //                        }
        //                        else
        //                        {
        //                            checkSet.Add(sum);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    answer = checkSet.Count - badSet.Count;

        //    foreach(int x in badSet)
        //    {
        //        checkSet.Remove(x);
        //    }
        //    foreach(int x in checkSet)
        //    {
        //        Console.WriteLine(x);
        //    }

        //    Console.WriteLine("Answer: " + answer);

        //    // Keep the console window open in debug mode.
        //    Console.WriteLine("Press any key to exit.");
        //    Console.ReadKey();
        //}

        static void Main()
        {
            // this whole algorithm is a bit complicated and takes a bit more explanation
            // than I'd like to offer here....

            BigInteger answer = 0;
            int search = (int)(Math.Pow(10, 6) * 0.75);
            PrimeGenerator primeGen = new PrimeGenerator((int)Math.Ceiling(Math.Sqrt(search)));
            for (int a = 6; a <= search; ++a)
            {
                List<BigInteger> divisors = primeGen.getDivisors(a);
                int count = 0;
                foreach (BigInteger d in divisors)
                {
                    BigInteger Pd = d;
                    BigInteger bound = (BigInteger)Math.Floor(Math.Sqrt((double)Pd / 2));
                    for (int n = 1; n <= bound; ++n)
                    {
                        double check = Math.Sqrt(n * n + 4 * (double)Pd);
                        if (check % 1 == 0)
                        {
                            double m = (-n + check) / 2;
                            if (m % 1 != 0)
                                continue;
                            BigInteger m2 = (BigInteger)m;
                            if (((m2 - n) % 2 == 1)
                                && (m2 > n)
                                && m2 * (m2 + n) == Pd
                                && (primeGen.isCoprime(m2, n)))
                            {
                                // Here we've found a particular divisor can rescale the
                                // perimeter so as to be used to produce a pythagorean primitive.
                                // We increment the count and break the loop, since pythagorean primitives
                                // are unique and therefore so are their perimeters.
                                ++count;
                                break;
                            }

                        }
                    }

                    // if we've found that this length could
                    // be rescaled to fit more than one pythagorean primitive
                    // then we exit this loop and reset the count to 0 since
                    // we aren't going to tabulate this perimeter length
                    if (count > 1)
                    {
                        count = 0;
                        break;
                    }
                }

                if (count == 1)
                {
                    //Console.WriteLine(a * 2);
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
