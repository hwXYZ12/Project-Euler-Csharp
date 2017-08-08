// Euler038.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler038
{
    class Euler038
    {
        static void Main()
        {
            int max = 918273645;
            for (int i = 1; i <= 20000; ++i)
            {
                for (int j = 1; j <= 9; ++j)
                {
                    string t = "";
                    for(int k = 1; k <= j; ++k)
                    {
                        t += (i * k);
                    }                    
                    if(BigInteger.Parse(t) < 1000000000 && isPandigital(Int32.Parse(t)))
                    {
                        Console.WriteLine("i = " + i);
                        Console.WriteLine("n = " + j);
                        Console.WriteLine(t);
                        if (Int32.Parse(t) > max)
                            max = Int32.Parse(t);
                    }
                }
            }

            Console.WriteLine("Answer: "+max);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty 9-digit pandigital check
        static bool isPandigital(int n)
        {
            if (n >= 1000000000)
                return false;

            // get each digit
            SortedSet<int> digits = new SortedSet<int>();
            while (n != 0)
            {
                digits.Add(n % 10);
                n /= 10;
            }

            // remove each digit from 1 to 9 from
            // the populated set of digits. If a digit cannot be found,
            // return false, else all digits have been found and we check that
            // the set is empty. If the set is also empty, then we return true.
            for (int i = 1; i <= 9; ++i )
            {
                if (!digits.Remove(i))
                    return false;
            }

            return (digits.Count == 0);
        }

    }
}