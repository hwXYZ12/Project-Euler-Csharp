// Euler091.cs
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

namespace Euler091
{

    class Euler091
    {

        static int BOUND = 50;

        static void Main()
        {
            int count = 0;

            for (int x1 = 0; x1 <= BOUND; ++x1)
            {
                for (int y1 = 0; y1 <= BOUND; ++y1)
                {
                    for (int x2 = 0; x2 <= BOUND; ++x2)
                    {
                        for (int y2 = 0; y2 <= BOUND; ++y2)
                        {
                            if(!(x1 == 0 && y1 == 0)
                                && !(x2 == 0 && y2 == 0)
                                && !(x1 == x2 && y1 == y2)
                                && ((x1*x1 + y1*y1 == x1*x2 + y1*y2)
                                    || (x1 == 0 && y2 == 0)))
                            {
                                Console.WriteLine(x1 + " " + y1 + " " + x2 + " " + y2);
                                ++count;
                            }
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
