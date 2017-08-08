// Euler045.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler045
{
    class Euler045
    {
        static void Main()
        {
            int search = 1000000;
            List<BigInteger> triangulars = new List<BigInteger>();
            SortedSet<BigInteger> pentagonals = new SortedSet<BigInteger>();
            SortedSet<BigInteger> hexagonals = new SortedSet<BigInteger>();
            for (BigInteger i = 1; i < search; ++i )
            {
                triangulars.Add(i * (i + 1) / 2);
                pentagonals.Add(i * (3 * i - 1) / 2);
                hexagonals.Add(i * (2*i - 1));
            }

            BigInteger answer = 0;
            for (int i = 285; i < search - 1; ++i)
            {
                if (pentagonals.Contains(triangulars[i])
                    && hexagonals.Contains(triangulars[i]))
                {
                    Console.WriteLine(i+1);
                    answer = triangulars[i];
                    break;
                }
            }

            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
       
    }
}