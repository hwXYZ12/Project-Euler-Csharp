// Euler088.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using HomeGrown.PrimeNumbers;
using System.Text;
using HomeGrown.Permutations;
using System.Linq;
using HomeGrown.ContinuedFractions;
using HomeGrown.TextHandling;
using HomeGrown.DirectedGraph;

namespace Euler088
{

    class Euler088
    {

        static int searchSpace = 12000;
        static int primeSearchSpace = searchSpace * 2;
        static PrimeGenerator primes = new PrimeGenerator(primeSearchSpace);

        // used to dynamically compute solutions
        static Dictionary<int, SortedSet<Tuple<int, int>>> divisorSums = new Dictionary<int, SortedSet<Tuple<int, int>>>();

        // I'm not going to explain what this function does. It is rather confusing.
        // It is intended to compute the possible divisor sums given an integer greater than 1.
        static SortedSet<Tuple<int, int>> computeDivisorSumValues(int n)
        {

            // divisor sum values may already be computed
            SortedSet<Tuple<int, int>> r;
            if(divisorSums.TryGetValue(n,out r))
            {
                return r;
            }

            // if n is prime then we can compute the list directly. This represents
            // the base case.
            if(primes.isPrime(n))
            {
                SortedSet<Tuple<int, int>> list = new SortedSet<Tuple<int, int>>();
                list.Add(new Tuple<int, int>(n, 1));
                return list;
            }

            // if n isn't prime then we can recursively compute its divisor sums
            List<BigInteger> divisors = primes.getDivisors(n);
            SortedSet<Tuple<int, int>> ret = new SortedSet<Tuple<int, int>>();
            int start = 1; // skip first element, which is 1 in every divisor list
            ret.Add(new Tuple<int, int>(n, 1)); // last element is a special case and is skipped
            int end = divisors.Count - 2;
            for(int i = start; i <= end; ++i)
            {
                foreach(Tuple<int,int> t in computeDivisorSumValues(n / (int)divisors[i]))
                {
                    Tuple<int, int> t2 = new Tuple<int, int>(t.Item1 + (int)divisors[i], t.Item2+1);
                    // ensure the set doesn't contain duplicates
                    if(!ret.Contains(t2))
                        ret.Add(t2);
                }
            }

            // store the result
            divisorSums.Add(n, ret);
            return ret;


        }

        // A somewhat difficult to explain recursive function that generates a list of
        // possible sums of factors given an integer and the number of divisors
        // that will be used to build the sum. The depth represents the number of divisors
        // used. The divisors used in the sum must multiply to the original integer, x.
        // Originally this function would compute every possible sum of divisors but 
        // computationally that is extremely expensive and so I've edited the function a bit
        // to prune for the desired result
        // we use some memory here as well to store results that way we don't need to recursively
        // recalculate a result every time we want to use the result. This will probably speed up
        // this function dramatically
        //static Dictionary<Tuple<int, int>, SortedSet<int> > sumsSortedSets = new Dictionary<Tuple<int, int>, SortedSet<int>>();

        //static SortedSet<int> possibleSums(int x, int depth)
        //{

        //    SortedSet<int> r;
        //    if (sumsSortedSets.TryGetValue(new Tuple<int, int>(x, depth), out r))
        //        return r;

        //    SortedSet<BigInteger> divisors = primes.getDivisors(x);
        //    if(depth == 2)
        //    {
        //        SortedSet<int> sums = new SortedSet<int>();
        //        int t = (int)Math.Floor((divisors.Count - 1) / 2.0);
        //        for(int i = 0; i <= t; ++i)
        //        {
        //            int temp = (int)divisors[i] + (int)divisors[divisors.Count - 1 - i];
        //            if (temp <= x)
        //            {
        //                sums.Add(temp);
        //            }
        //        }

        //        // place the sum list in the dictionary for faster reference later
        //        sumsSortedSets.Add(new Tuple<int, int>(x, depth), sums);

        //        return sums;

        //    } else{
               
        //        // here we run more or less the same algorithm except we
        //        // use recursion
        //        SortedSet<int> sums = new SortedSet<int>();
        //        int t = (int)Math.Floor((divisors.Count - 1) / 2.0);
        //        for (int i = 0; i <= t; ++i)
        //        {
        //            foreach (int n in possibleSums((int)divisors[divisors.Count - 1 - i], depth - 1))
        //            {
        //                int temp = (int)divisors[i] + n;
        //                if(temp <= x)
        //                    sums.Add(temp);  
        //            }
        //        }

        //        // place the sum list in the dictionary for faster reference later
        //        sumsSortedSets.Add(new Tuple<int, int>(x, depth), sums);

        //        return sums;
        //    }

        //}

        // computes the minimum product-sum value for which
        // there are k integers in the sum. A product-sum value
        // is a number such that the product and sum of particular
        // set of numbers X is both equal to eachother and equal to the
        // product-sum. This function determines the smallest product-sum
        // value of any product-sum value for which the size of the set X
        // is exactly k.
        // Note that this loop has the potential to never terminate, in which
        // case you're on your own!
        //static int getMinProductSum(int k)
        //{
        //    int offset = 0;
        //    while(true)
        //    {

        //        SortedSet<int> sums = possibleSums(k + offset, k);
        //        foreach(int s in sums)
        //        {
        //            if(k + offset == s)
        //            {
        //                // found one!
        //                return k + offset;
        //            }
        //        }
        //        ++offset;
        //    }
        //}
        
        static void Main()
        {
            //SortedSet<int> minProductSums = new SortedSet<int>();
            //for (int i = 2; i <= searchSpace; ++i )
            //{
            //    if (i == 170)
            //        Console.WriteLine();

            //    int x = getMinProductSum(i);
            //    if (!minProductSums.Contains(x))
            //        minProductSums.Add(x);
            //}

            //// final sum
            //int sum = 0;
            //foreach(int i in minProductSums)
            //{
            //    sum += i;
            //}

            // we're going to use our divisor-sum function to
            // help compute the minimum product-sum values for each k
            // where k is the number of integers in the sum portion of the
            // product-sum
            int maxk = 12000;
            int searchSpace = maxk * 2;
            Dictionary<int, int> kToProductSumValue = new Dictionary<int, int>();

            // note that the first value in this dictionary is necessarily
            kToProductSumValue.Add(2, 4);


            for (int i = 2; i <= searchSpace; ++i )
            {
                SortedSet<Tuple<int, int>> x = computeDivisorSumValues(i);
                foreach(Tuple<int, int> t in x)
                {
                    // a few cases to check through
                    if(t.Item1 < i)
                    {
                        // may need to update a value of k
                        int k = t.Item2 + (i - t.Item1);
                        int check = 0;
                        if (kToProductSumValue.TryGetValue(k, out check))
                        {
                            // if there is already a candidate for the min product sum
                            // with k integers in the sum
                            // then we check it against this newfound min product sum
                            if (i < check)
                                kToProductSumValue[k] = i;
                        } else
                        {
                            // there was no candidate for the min product sum
                            // with k integers in the sum present
                            // so by default we set it here
                            kToProductSumValue.Add(k, i);
                        }
                    } else if(t.Item1 == i)
                    {
                        // I suspect that this case is only relevant once...
                        // I'm not going to place anything here
                    }
                }
            }

            // print the results
            //for (int i = 2; i <= maxk; ++i )
            //{
            //    if(kToProductSumValue.ContainsKey(i))
            //        Console.WriteLine("k = " + i + " : " + kToProductSumValue[i]);
            //}

            // compute final sum
            SortedSet<int> tempSet = new SortedSet<int>();
            for (int i = 2; i <= maxk; ++i )
            {
                if(kToProductSumValue.ContainsKey(i))
                {
                    if (!tempSet.Contains(kToProductSumValue[i]))
                        tempSet.Add(kToProductSumValue[i]);
                }
            }
            int sum = 0;
            foreach (int x in tempSet)
                sum += x;

            Console.WriteLine(sum);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
