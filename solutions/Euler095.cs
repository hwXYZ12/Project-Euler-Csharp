// Euler095.cs
using HomeGrown.PrimeNumbers;
using System;
using System.Numerics;
using System.Collections.Generic;

namespace Euler095
{

    class Euler095
    {
		
		static private PrimeGenerator p = null;
        static int BOUND = (int)(Math.Pow(10, 6));
        static int PRIME_BOUND = (int)Math.Sqrt((double)BOUND);

        static BigInteger getDivisorSum(BigInteger n)
		{

            // deal with edge cases
            if (n<=1)
            {
                return 0;
            }

			// populate primegenerator if it already hasn't
			// been populated
			if (p == null)
				p = new PrimeGenerator(PRIME_BOUND);
			
			// get prime factorization of n
			List<Tuple<BigInteger, BigInteger>> factorization;
			factorization = p.getFactorization(n);

            // calculate the sum of the proper divisors
            // of n
            double ret = 1.0;
			for(int x = 0; x < factorization.Count; ++x)
			{
				ret *= (Math.Pow((double)factorization[x].Item1, (double)factorization[x].Item2 + 1)  - 1) / (double)(factorization[x].Item1 - 1);
			}
			return (BigInteger)(ret - (double)n);
		}


        static void Main()
        {
			BigInteger max = 1;
			BigInteger loopStart = 0;
			Dictionary<BigInteger, BigInteger> vals = new Dictionary<BigInteger, BigInteger>();
            HashSet<BigInteger> reachesZero = new HashSet<BigInteger>();
            for (BigInteger a = 1; a <= BOUND; ++a)
			{				
				bool check = true;
				int checkVal = 1;
				vals.Add(a, checkVal);
                BigInteger next = a;
				while(check)
				{
                    next = getDivisorSum(next);
					if(next > BOUND)
					{
						// amicable chain is inadmissable
						// if any of the elements exceed the boundary
						
						// exit chain
						check = false;
						
						// clear dictionary
						vals.Clear();
						
					} else if (reachesZero.Contains(next))
                    {
                        // add a to reachesZero as well
                        reachesZero.Add(a);

                        // exit chain
                        check = false;

                        // clear dictionary
                        vals.Clear();

                    }
                    else if(vals.ContainsKey(next))
					{
						// we've found the start of our loop
						++checkVal;
						BigInteger loopLength = checkVal - vals[next];
						
						// keep track of maximum amicable chain length
						if(loopLength > max)
						{
							max = loopLength;
							loopStart = next;
						}
						
                        // maintain a set of values that are NOT
                        // amicable
                        if(next == 0)
                        {
                            reachesZero.Add(a);
                        }

						// exit chain
						check = false;
						
						// clear dictionary
						vals.Clear();
					} else
					{
						// add value to the dictionary
						++checkVal;
						vals.Add(next, checkVal);
					}
				}
			}
			
			// walk the longest amicable chain and find
			// the smallest element
			BigInteger min = loopStart;
			BigInteger x = getDivisorSum(loopStart);
			while(x != loopStart)
			{
				if(x < min)
				{
					min = x;
				}
				x = getDivisorSum(x);
			}

            Console.WriteLine(min);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}