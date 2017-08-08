// Euler065.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;

namespace Euler065
{
    class Euler065
    {
        private struct Pair
        {
            public BigInteger a;
            public BigInteger b;
        }

        private class EAsContFraction
        {
            public BigInteger initial;
            public List<BigInteger> repeatingBlock = new List<BigInteger>();

            public EAsContFraction(int n)
            {
                // the initial value and repeating sequence for E was given in the
                // problem statement

                initial = 2;

                // store the values of the repeating block
                BigInteger t = n / 3;
                for(BigInteger i = 1; i <= t; ++i)
                {
                    repeatingBlock.Add(1);
                    repeatingBlock.Add(2*i);
                    repeatingBlock.Add(1);
                }
                if(n % 3 == 1)
                {
                    repeatingBlock.Add(1);
                } else if(n % 3 == 2)
                {
                    repeatingBlock.Add(1);
                    repeatingBlock.Add(2*(n / 3 + 1));
                }
            }

            public BigInteger getInitialValue()
            {
                return initial;
            }

            public List<BigInteger> getRepeatingBlock()
            {
                return repeatingBlock;
            }            

            public void print()
            {
                if (repeatingBlock.Count <= 0)
                    return;

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < repeatingBlock.Count - 1; ++i)
                {
                    sb.Append(repeatingBlock[i]);
                    sb.Append(',');
                }
                sb.Append(repeatingBlock[repeatingBlock.Count - 1]);
                Console.Write("[" + initial + ";(" + sb.ToString() + ")]");
            }

            public Pair toRational()
            {
                BigInteger numerator = 1;
                BigInteger denominator = repeatingBlock[repeatingBlock.Count - 1];
                for(int i = repeatingBlock.Count - 2; i >= 0; --i)
                {
                    BigInteger x = repeatingBlock[i];
                    BigInteger a = numerator;
                    BigInteger b = denominator;
                    numerator = b;
                    denominator = x * b + a;
                }

                // finally add the initial value to the pair                
                numerator = initial * denominator + numerator;

                Pair p;
                p.a = numerator;
                p.b = denominator;
                return p;
            }

        }

        static void Main()
        {
            BigInteger answer = 0;
            int search = 10;
            BigInteger max = 0;
            EAsContFraction e;
            for (int a = 1; a < search; ++a)
            {
                e = new EAsContFraction(a);
                e.print();
                Console.WriteLine();

                Pair p;
                p = e.toRational();
                Console.WriteLine(p.a + "/"+p.b);
                Console.WriteLine(Permutator.sumOfDigits(p.a));
            }

            e = new EAsContFraction(99);
            Pair p2 = e.toRational();
            answer = Permutator.sumOfDigits(p2.a);

            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}