// Euler039.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler039
{
    class Euler039
    {
        static void Main()
        {
            int[] solns = new int[1001];
            for (int i = 0; i < 1001; ++i )
            {
                solns[i] = 0;
            }

            for (int a = 1; a <= 1000; ++a)
            {
                for (int b = 1; b <= 1000; ++b)
                {
                    for (int c = 1; c <= 1000; ++c)
                    {
                        if (a + b + c <= 1000 && a * a + b * b == c * c)
                        {
                            ++solns[a + b + c];
                        }
                    }
                }
            }
            int max = solns[0], answer = 0;
            for (int i = 0; i < 1001; ++i)
            {
                if (solns[i] > max)
                {
                    max = solns[i];
                    answer = i;
                }
            }
            

            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}