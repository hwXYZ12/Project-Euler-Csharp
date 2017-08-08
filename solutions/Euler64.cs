// Euler064.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;

namespace Euler064
{
    class Euler064
    {
        private class RootAsContFraction
        {
            public int initial;
            public List<int> repeatingBlock = new List<int>();

            public RootAsContFraction(int x)
            {
                initial = getClosestRoot(x);

                // compute the repeating block
                // of the root
                // start with the first element
                // of the repeating block
                int c = 1;
                int k = initial;
                int d = x - k*k;
                int a = Convert.ToInt32(Math.Floor(( (double) ((initial + k) * c) / d)));
                int k2 = a * d / c - k;
                repeatingBlock.Add(a);

                // get the next element of the repeating block
                while(true)
                {                    
                    // this is where the pattern begins to repeat
                    if(d == 1 && k2 == initial)
                    {
                        break;
                    }

                    c = d;
                    k = k2;
                    d = x - k * k;
                    a = Convert.ToInt32(Math.Floor(((double)((initial + k) * c) / d)));
                    k2 = a * d / c - k;
                    d /= c;
                    repeatingBlock.Add(a);
                }
            }

            public int getInitialValue()
            {
                return initial;
            }

            public List<int> getRepeatingBlock()
            {
                return repeatingBlock;
            }

            // returns the greatest square root
            // that is a whole number and less than
            // the number x
            private static int getClosestRoot(int x)
            {
                return Convert.ToInt32(Math.Floor(Math.Sqrt(x)));
            }    
        
            public void print()
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < repeatingBlock.Count - 1; ++i)
                {
                    sb.Append(repeatingBlock[i]);
                    sb.Append(',');
                }
                sb.Append(repeatingBlock[repeatingBlock.Count - 1]);
                Console.Write("[" + initial + ";(" + sb.ToString() + ")]");
            }

        }

        static void Main()
        {
            int answer = 0;
            int search = 10001;
            BigInteger max = 0;
            RootAsContFraction r;
            for (int a = 0; a < search; ++a)
            {               
                if(Math.Sqrt(a) % 1 != 0)
                {
                    r = new RootAsContFraction(a);
                    r.print();
                    Console.WriteLine();
                    if (r.repeatingBlock.Count % 2 == 1)
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