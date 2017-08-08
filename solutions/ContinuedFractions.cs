using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HomeGrown.ContinuedFractions
{
    public struct Rational
    {
        public BigInteger a;
        public BigInteger b;
    }

    public class EAsContFraction
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
            for (BigInteger i = 1; i <= t; ++i)
            {
                repeatingBlock.Add(1);
                repeatingBlock.Add(2 * i);
                repeatingBlock.Add(1);
            }
            if (n % 3 == 1)
            {
                repeatingBlock.Add(1);
            }
            else if (n % 3 == 2)
            {
                repeatingBlock.Add(1);
                repeatingBlock.Add(2 * (n / 3 + 1));
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

        public Rational toRational()
        {
            BigInteger numerator = 1;
            BigInteger denominator = repeatingBlock[repeatingBlock.Count - 1];
            for (int i = repeatingBlock.Count - 2; i >= 0; --i)
            {
                BigInteger x = repeatingBlock[i];
                BigInteger a = numerator;
                BigInteger b = denominator;
                numerator = b;
                denominator = x * b + a;
            }

            // finally add the initial value to the pair                
            numerator = initial * denominator + numerator;

            Rational p;
            p.a = numerator;
            p.b = denominator;
            return p;
        }

    }

    public class RootAsContFraction
    {
        private int initial;
        private int which;
        private List<int> repeatingBlock = new List<int>();
        private Rational pellEqnSoln;

        public RootAsContFraction(int x)
        {
            initial = getClosestRoot(x);
            which = x;
            pellEqnSoln = new Rational();
            pellEqnSoln.a = 0;
            pellEqnSoln.b = 0;

            // compute the repeating block
            // of the root
            // start with the first element
            // of the repeating block
            int c = 1;
            int k = initial;
            int d = x - k * k;
            int a = Convert.ToInt32(Math.Floor(((double)((initial + k) * c) / d)));
            int k2 = a * d / c - k;
            repeatingBlock.Add(a);

            // get the next element of the repeating block
            while (true)
            {
                // this is where the pattern begins to repeat
                if (d == 1 && k2 == initial)
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

        // determines the minimal solution of x & y to the
        // equation x^2 + d*y^2 = 1
        // Makes use of knowledge of Pell's equation!
        // makes some assumptions about the input!
        public Rational getPellEquationSolution()
        {
            // first we need to know the period of the continued fraction
            List<int> repeating = repeatingBlock;
            Rational t;
            int i = 1;
            if (repeating.Count % 2 == 0)
            {
                // we do something when the continued fraction has even period
                // when we have even period, then let p = the period and our
                // solution corresponds to the pth convergent
                i = repeating.Count;
                t = toRational(i);
                if (t.a * t.a - which * t.b * t.b == 1)
                {
                    pellEqnSoln = t;
                    return t;
                }
            }
            else
            {
                // we do something else when it has odd period
                // When we have odd period, let p = the period and 
                // the solution corresponds to the 2pth convergent
                i = repeating.Count * 2;
                t = toRational(i);
                if (t.a * t.a - which * t.b * t.b == 1)
                {
                    pellEqnSoln = t;
                    return t;
                }
            }

            // brute force search all convergents of the square root
            // written as a continued fraction and check for an answer
            // to the pell equation. This is used as a last resort in the
            // case that the above doesn't properly find a solution... which it should...
            while (t.a * t.a - which * t.b * t.b != 1)
            {
                t = toRational(++i);
            }

            // debug, this should never be reached
            Console.WriteLine("Period: " + repeatingBlock.Count);
            Console.WriteLine("Convergent Index: " + i);

            pellEqnSoln = t;
            return t;
        }

        // returns the next solution to the Pell equation
        // x^2 - d*y^2 = 1 where x,y are both integer solutions
        // and d is the integer such that this class represents the
        // continued fraction of the square root of d
        // Note that this method makes some major assumptions about the input!!!
        // We're assuming that the "current" field is indeed a solution
        // to the above Pell equation
        public Rational getNextPellEquationSoln(Rational current)
        {
            // ensure that we have the fundamental solution
            if(pellEqnSoln.a == 0 && pellEqnSoln.b == 0)
            {
                getPellEquationSolution();
            }

            // perform a recurrence relation computation to
            // get the next solutions based off the fundamental solution
            BigInteger x = pellEqnSoln.a * current.a + which * pellEqnSoln.b * current.b;
            BigInteger y = pellEqnSoln.a * current.b + pellEqnSoln.b * current.a;
            Rational ret = new Rational();
            ret.a = x;
            ret.b = y;
            return ret;
        }

        // returns the nth convergent of the continued fraction
        public Rational toRational(int n)
        {
            // we need a starting point, if we 
            // are only interested in the first element of
            // the fraction then we return it as such
            if(n<=1)
            {
                Rational r;
                r.a = initial;
                r.b = 1;
                return r;
            }

            // otherwise, we are going to need to loop through the
            // repeating block of continued fraction elements until we
            // have computed the entire continued fraction
            // we deal with n-2, since the nth convergent corresponds
            // to the n-1th element of the repeatingBlock list and 
            // we also hold the repeating digits in a separate data
            // structure from the intitial digit (ie, repeatingBlock[0] isn't
            // useful when computed n=1, but only for n>=2)
            int x = (n-2) % repeatingBlock.Count;

            // once we have a starting point we can step through
            // the computation of the simple continued fraction
            BigInteger numerator = 1;
            BigInteger denominator = repeatingBlock[x];
            while (n > 2) // we terminate here, following similar reasoning to the above
            {
                --n;    // decrement n to use the next element of the
                        // repeating block
                x = (n - 2) % repeatingBlock.Count;
                BigInteger a = numerator;
                BigInteger b = denominator;

                // we add the next element to the recorded fraction, then
                // swap to emulate taking a reciprocal. We end up with the next
                // partial computation of the continued fraction
                numerator = b;
                denominator = repeatingBlock[x] * b + a;
            }

            // add the initial value to the rest
            // of the continued fraction
            numerator = initial * denominator + numerator;

            Rational p;
            p.a = numerator;
            p.b = denominator;
            return p;
        }

    }

    // class that handles the decimal expansion of
    // a rational number, specifically if it's finite
    // or not, how long it's period is if it has one,
    // and the digits of the expansion are stored using
    // an arbitrary amount of memory such that large
    // expansions can be represented and utilized
    public class BigDecimalExpansion
    {
        private List<int> digits = new List<int>();
        private BigInteger wholePart;
        private bool repeating;
        private int whereRepeatBegins;

        // use this constructor if you know that the digit expansion
        // might cause an overflow error and you don't need all of the
        // digits of the expansion
        // NOTE - We won't know if the expansion terminates or repeats
        // indefinitely if this option is chosen
        public BigDecimalExpansion(Rational ratio, int limit)
        {
            // calculate the big decimal expansion of the
            // given ratio

            // first, we seperate the whole part from the
            // decimal expansion
            wholePart = ratio.a / ratio.b; // integer division

            // now we need to compute the digits of the expansion
            // one-by-one
            // We also need to perform a check on these digits to ensure that
            // they aren't repeating, otherwise we'll end up in an infinite loop!
            BigInteger numerator = ratio.a % ratio.b;
            BigInteger divisor = ratio.b;
            BigInteger[] usefulNumbers = new BigInteger[10];
            for (int i = 0; i < usefulNumbers.Length; ++i)
            {
                usefulNumbers[i] = divisor * i;
            }

            // by construction, we know that all of the numerator
            // is strictly less than the divisor
            // from here, we start the process of long division
            // note that we don't account for the possibility of a repeating decimal!
            BigInteger currentNumber = numerator * 10;
            Dictionary<BigInteger, int> previousPartials = new Dictionary<BigInteger, int>();
            repeating = false; // doesn't repeat by default
            while (currentNumber != 0)
            {
                int nextDigit = (int)(currentNumber / divisor);
                currentNumber -= usefulNumbers[nextDigit];

                // here we check for a repeating pattern
                int which = 0;
                if (previousPartials.TryGetValue(currentNumber, out which))
                {
                    // if we're looking at a partial value that we've seen before
                    // then we DON'T add the next digit, we actually terminate the loop
                    // because we've found where it begins to repeat itself
                    // We mark that point here
                    whereRepeatBegins = which;
                    repeating = true;
                    break;
                }
                else
                {
                    // if we haven't seen this partial value
                    // yet, then we add it to the list along with its index
                    previousPartials.Add(currentNumber, digits.Count);
                }

                digits.Add(nextDigit);
                currentNumber *= 10;

                if(digits.Count >= limit)
                {
                    // since we terminate here we don't know if
                    // the expansion repeats
                    break;
                }
            }

        }

        public BigDecimalExpansion(Rational ratio)
        {
            // calculate the big decimal expansion of the
            // given ratio
            
            // first, we seperate the whole part from the
            // decimal expansion
            wholePart = ratio.a / ratio.b; // integer division

            // now we need to compute the digits of the expansion
            // one-by-one
            // We also need to perform a check on these digits to ensure that
            // they aren't repeating, otherwise we'll end up in an infinite loop!
            BigInteger numerator = ratio.a % ratio.b;
            BigInteger divisor = ratio.b;
            BigInteger[] usefulNumbers = new BigInteger[10];
            for(int i = 0; i < usefulNumbers.Length; ++i)
            {
                usefulNumbers[i] = divisor * i;
            }

            // by construction, we know that all of the numerator
            // is strictly less than the divisor
            // from here, we start the process of long division
            // note that we don't account for the possibility of a repeating decimal!
            BigInteger currentNumber = numerator * 10;
            Dictionary<BigInteger, int> previousPartials = new Dictionary<BigInteger, int>();
            repeating = false; // doesn't repeat by default
            while(currentNumber != 0)
            {
                int nextDigit = (int)(currentNumber / divisor);
                currentNumber -= usefulNumbers[nextDigit];

                // here we check for a repeating pattern
                int which = 0;
                if(previousPartials.TryGetValue(currentNumber, out which))
                {
                    // if we're looking at a partial value that we've seen before
                    // then we DON'T add the next digit, we actually terminate the loop
                    // because we've found where it begins to repeat itself
                    // We mark that point here
                    whereRepeatBegins = which;
                    repeating = true;
                    break;
                }
                else
                {
                    // if we haven't seen this partial value
                    // yet, then we add it to the list along with its index
                    previousPartials.Add(currentNumber, digits.Count);
                }

                digits.Add(nextDigit);
                currentNumber *= 10;
            }

        }

        public List<int> getDecimalExpansion()
        {
            return digits;
        }

        public BigInteger getWholePart()
        {
            return wholePart;
        }

        public bool isRepeating()
        {
            return repeating;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(wholePart);
            sb.Append(".");
            if(!repeating)
            {
                for(int i = 0; i < digits.Count; ++i)
                {
                    sb.Append(digits[i]);
                }
            }
            else
            {
                for(int i = 0; i < 100; ++i)
                {
                    sb.Append(digits[i % digits.Count + whereRepeatBegins]);
                }
            }
            
            return sb.ToString();
        }
    }
}
