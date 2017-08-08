// Euler032.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler032
{
    class Euler032
    {
        static void Main()
        {
            int sum = 0;
            SortedSet<int> s = new SortedSet<int>();
            for (int a = 1; a <= 10000; ++a)
            {
                for (int b = 1; b <= 10000; ++b)
                {
                    if (isPandigital(a, b))
                    {
                        if(!s.Contains(a*b))
                        {
                            sum += a * b;
                            s.Add(a * b);
                        }
                    }
                }
            }

            Console.WriteLine("Answer: " + sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // inefficient narcissism calculation
        static bool isPandigital(int x, int y)
        {
            // check that the multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital
            SortedSet<int> check = new SortedSet<int>();
            for(int i = 1; i < 10; ++i)
            {
                check.Add(i);
            }

            int temp = x, digit = 0;
            while(temp != 0)
            {
                digit = temp % 10;
                temp /= 10;
                if(!check.Remove(digit))
                {
                    return false;
                }
            }

            temp = y;
            while (temp != 0)
            {
                digit = temp % 10;
                temp /= 10;
                if (!check.Remove(digit))
                {
                    return false;
                }
            }

            temp = x * y;
            while (temp != 0)
            {
                digit = temp % 10;
                temp /= 10;
                if (!check.Remove(digit))
                {
                    return false;
                }
            }

            if (check.Count == 0)
                return true;

            return false;
        }
    }
}