// Euler068.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;

namespace Euler068
{
    class Euler068
    {
        // to search for the solution we must
        // find the 126 possible selections for X
        // and determine the set difference set Y. We then must test
        // the various orders of X and Y. In total we
        // end up checking about 10^6 combinations.        

        static void Main()
        {
            List<int> t = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            for(int i = 1; i <= 9; ++i)
            {
                for(int j = i + 1; j<=9;++j)
                {
                    for(int k=j+1; k<=9; ++k)
                    {
                        for(int m=k+1; m<=9;++m)
                        {
                            for(int n=m+1; n<=9;++n)
                            {
                                // for each set of i,j,k,l,m, and n
                                // we want to check the linear solution
                                // using all possible permutations of these numbers
                                // as well as the possible permutations of the difference
                                // set with [1,10]
                                List<int> dSet = new List<int>();
                                List<int> X = new List<int>();
                                X.Add(i);
                                X.Add(j);
                                X.Add(k);
                                X.Add(m);
                                X.Add(n);
                                foreach(int x in t)
                                {
                                    if(x != i
                                        && x != j
                                        && x != k
                                        && x != m
                                        && x != n)
                                    {
                                        dSet.Add(x);
                                    }
                                }

                                // now that we have the difference set, we need
                                // to operate on all the permutations of these numbers
                                // and compute the constant we need for our computations
                                int constant = 0;
                                foreach (int item in dSet)
	                            {
		                             constant += item;
	                            }
                                foreach (int item in X)
	                            {
		                             constant += 2*item;
	                            }
                                constant /= 5;

                                List<List<int>> permsX = Permutator.getIntPermutations(X);
                                List<List<int>> permsDSet = Permutator.getIntPermutations(dSet);
                                foreach(List<int> u in permsDSet)
                                {
                                    foreach(List<int> v in permsX)
                                    {
                                        if(u[0] + v[0] + v[1] == constant
                                            && u[1] + v[1] + v[2] == constant
                                            && u[2] + v[2] + v[3] == constant
                                            && u[3] + v[3] + v[4] == constant
                                            && u[4] + v[4] + v[0] == constant)
                                        {
                                            // print the solution sets, but print
                                            // the solution in the format as described in the problem description
                                            int min = 10, index = 0;
                                            for (int x1 = 0; x1 < u.Count; ++x1)
                                            {
                                                if(u[x1] <= min)
                                                {
                                                    min = u[x1];
                                                    index = x1;
                                                }
                                            }
                                            for(int x1 = 0; x1 < u.Count; ++x1)
                                            {
                                                int x2 = (x1 + index) % u.Count;
                                                Console.Write(u[x2]+","+v[x2 % 5]+","+v[(x2+1)%5]+"; ");                                                
                                            }
                                            Console.WriteLine();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


    }
}
