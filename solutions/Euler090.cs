// Euler090.cs
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

namespace Euler090
{

    class Euler090
    {

        // checks that the pair of dice can be arranged to show the specified integer pair
        static bool diceCheck(SortedSet<int> dice1, SortedSet<int> dice2, int a, int b)
        {
            return ((dice1.Contains(a) && dice2.Contains(b)) || (dice1.Contains(b) && dice2.Contains(a)));
        }

        static void Main()
        {

            SortedSet<int> dice1 = new SortedSet<int>();
            SortedSet<int> dice2 = new SortedSet<int>();
            int sum = 0;
            for (int i = 0; i < 10 - 5; ++i )
            {
                for(int j = i + 1; j < 10 - 4; ++j)
                {
                    for (int k = j + 1; k < 10 - 3; ++k)
                    {
                        for (int m = k + 1; m < 10 - 2; ++m)
                        {
                            for (int n = m + 1; n < 10 - 1; ++n)
                            {
                                for (int p = n + 1; p < 10; ++p)
                                {


                                    for (int a = 0; a < 10 - 5; ++a)
                                    {
                                        for (int b = a + 1; b < 10 - 4; ++b)
                                        {
                                            for (int c = b + 1; c < 10 - 3; ++c)
                                            {
                                                for (int d = c + 1; d < 10 - 2; ++d)
                                                {
                                                    for (int e = d + 1; e < 10 - 1; ++e)
                                                    {
                                                        for (int f = e + 1; f < 10; ++f)
                                                        {

                                                            // setup dice and check dice conditions
                                                            dice1.Clear();
                                                            dice1.Add(i);
                                                            dice1.Add(j);
                                                            dice1.Add(k);
                                                            dice1.Add(m);
                                                            dice1.Add(n);
                                                            dice1.Add(p);

                                                            dice2.Clear();
                                                            dice2.Add(a);
                                                            dice2.Add(b);
                                                            dice2.Add(c);
                                                            dice2.Add(d);
                                                            dice2.Add(e);
                                                            dice2.Add(f);

                                                            Console.Write(i + " " + j + " " + k + " " + m + " " + n + " " + p + " ");
                                                            Console.Write(", " + a + " " + b + " " + c + " " + d + " " + e + " " + f + " ");
                                                            Console.WriteLine();

                                                            if (diceCheck(dice1, dice2, 0, 1) &&
                                                               diceCheck(dice1, dice2, 0, 4) &&
                                                               (diceCheck(dice1, dice2, 0, 9) || diceCheck(dice1, dice2, 0, 6)) &&
                                                               (diceCheck(dice1, dice2, 1, 6) || diceCheck(dice1, dice2, 1, 9)) &&
                                                               diceCheck(dice1, dice2, 2, 5) &&
                                                               (diceCheck(dice1, dice2, 3, 6) || diceCheck(dice1, dice2, 3, 9)) &&
                                                               (diceCheck(dice1, dice2, 4, 9) || diceCheck(dice1, dice2, 4, 6)) &&
                                                               (diceCheck(dice1, dice2, 6, 4) || diceCheck(dice1, dice2, 9, 4)) &&
                                                               diceCheck(dice1, dice2, 8, 1))
                                                            {
                                                                ++sum;
                                                            }

                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }
            }

            // divide sum due to symmetry
            sum /= 2;

            Console.WriteLine(sum);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
