// Euler071.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler071
{
    class Euler071
    {
        static void Main()
        {
            int search = (int) Math.Pow(10, 6);
            double diff = (double) 3 / 7 - ((double) 428571 / search);
            int answer = 428571;
            search = (int)Math.Round((double)search * 3 / 7) - 1;
            for (int i = 1; i <= search; ++i)
            {
                int denom = (int) Math.Round((double)i * 7 / 3);
                double check = (double)3 / 7 - (double) i / denom;

                if(diff >= check
                    && check > 0.0)
                {
                    // found our new difference
                    diff = check;
                    answer = i;
                    continue;
                }

                // check around this fraction as well (denom - 1)
                --denom;
                check = (double)3 / 7 - (double)i / denom;
                if (diff >= check
                    && check > 0.0)
                {
                    // found our new difference
                    diff = check;
                    answer = i;
                    continue;
                }

                // check around this fraction as well (denom + 1)
                denom += 2;
                check = (double)3 / 7 - (double)i / denom;
                if (diff >= check
                    && check > 0.0)
                {
                    // found our new difference
                    diff = check;
                    answer = i;
                    continue;
                }
            }
            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


    }
}