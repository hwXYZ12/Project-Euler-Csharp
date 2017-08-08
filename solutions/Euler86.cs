// Euler084.cs
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

namespace Euler086
{

    class Euler086
    {

        static bool isShortestCuboidPathOfIntegralLength(double a, double b, double c)
        {
            Func<double, double>[] S = new Func<double, double>[3];
            S[0] = delegate(double x) { return Math.Pow(a * a + x * x, 0.5) + Math.Pow((c - x) * (c - x) + b * b, 0.5); };
            S[1] = delegate(double x) { return Math.Pow(c * c + x * x, 0.5) + Math.Pow((a - x) * (a - x) + b * b, 0.5); };
            S[2] = delegate(double x) { return Math.Pow(c * c + x * x, 0.5) + Math.Pow((b - x) * (b - x) + a * a, 0.5); };

            double[] length = new double[3];
            length[0] = (a == b) ? S[0](c / 2) : Math.Min(S[0]((a * c) / (a - b)), S[0]((a * c) / (a + b)));
            length[1] = (b == c) ? S[1](a / 2) : Math.Min(S[1]((a * c) / (c - b)), S[1]((a * c) / (c + b)));
            length[2] = (a == c) ? S[2](b / 2) : Math.Min(S[2]((b * c) / (c - a)), S[2]((b * c) / (c + a)));

            double minLength = Math.Min(Math.Min(length[0], length[1]), length[2]);

            // use a small amount of error correction
            if (Math.Abs(minLength - (int)Math.Round(minLength)) < Math.Pow(10, -4))
                return true;
            return false;
        }

        static void Main()
        {
            int count = 0;
            int searchSpace = 1819;

            // we need sosmething to ensure that we don't repeatedly check rotations
            // we can use a set of strings and check against strings that are already
            // in the set
            SortedSet<string> check = new SortedSet<string>();

            for (int i = 1; i <= searchSpace; ++i)
            {
                for (int j = i + 1; j <= searchSpace; ++j)
                {
                    for (int k = j + 1; k <= searchSpace; ++k)
                    {

                        if (isShortestCuboidPathOfIntegralLength(i, j, k))
                        {
                            int[] temp = new int[3];
                            temp[0] = i;
                            temp[1] = j;
                            temp[2] = k;
                            Array.Sort(temp);
                            string s = "" + temp[0] + "" + temp[1] + "" + temp[2]; 
                            if (!check.Contains(s))
                            {
                                check.Add(s);
                                ++count;
                            }
                        }

                        // stop search and print last used bounds
                        if (count >= 1000000)
                        {
                            Console.WriteLine("reached");
                            Console.WriteLine(i);
                            Console.WriteLine(j);
                            Console.WriteLine(k);
                            i = searchSpace;
                            j = searchSpace;
                            k = searchSpace;
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
