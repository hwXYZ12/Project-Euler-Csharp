// Euler044.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler044
{
    class Euler044
    {
        static void Main()
        {
            int min = 100000000;
            SortedSet<int> pentagonals = new SortedSet<int>();
            List<int> pentagonalsList = new List<int>();
            for (int i = 1; i < 30000; ++i )
            {
                pentagonals.Add((i * ((3 * i)- 1)) / 2);
                pentagonalsList.Add((i * ((3 * i) - 1)) / 2);
            }

            for(int i=0; i < pentagonalsList.Count; ++i)
            {
                for(int j = 0; j < pentagonalsList.Count; ++j)
                {
                    if(pentagonals.Contains(pentagonalsList[i] + pentagonalsList[j])
                        && pentagonals.Contains(pentagonalsList[i] - pentagonalsList[j]))
                    {
                        Console.WriteLine(pentagonalsList[i]);
                        Console.WriteLine(pentagonalsList[j]);
                        Console.WriteLine(Math.Abs(pentagonalsList[i] - pentagonalsList[j]));
                        if (min > Math.Abs(pentagonalsList[i] - pentagonalsList[j]))
                        {
                            min = Math.Abs(pentagonalsList[i] - pentagonalsList[j]);
                        }
                    }
                }
            }

            Console.WriteLine("Answer: "+min);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}