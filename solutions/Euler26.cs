// euler26.cs
using System;
using System.Collections.Generic;
namespace Euler26
{
    class Euler26
    {
        static void Main() 
        {
            int t = 1, digit = 0, length = 1, maxLength = 1, whichDiv = 2;
            SortedSet<int> ts = new SortedSet<int>();
            List<int> digits = new List<int>();

            for(int div = 2; div < 1000; ++div)
            {
                // init t to 1 and init binary search tree to a head with value 1
                t = 1;
                ts.Clear();
                ts.Add(1);
                digits.Clear();
                length = 0;

                while (t != 0)
                {
                    if (div > t)
                    {
                        // carry
                        t *= 10;
                    }
                    // find greatest value of x such that div * x < t
                    digit = t / div;
                    t -= (div * digit);

                    // search for t in the binary search tree
                    /* if t is found, then we have found our recurring
                     * pattern and need to find the length of the cycle and check the length
                     * and value of div against the current maximum
                     * 
                     * How do we know the length of the recurring pattern?
                     *  We have the digits stored in an array and we have the respective "t"s of each
                     *  loop stored in a BST. Thus we can search the BST to determine whether the cycle has
                     *  been completed or not in O(logn) time. Once we know that the cycle has been completed, 
                     *  then we search digit list for the first occurrence of the digit in the list and assume that
                     *  it appended with the following digits makes up the cycle.
                     */
                    if(ts.Contains(t))
                    {
                        length = digits.Count - digits.FindIndex((int x) => (x == digit));
                        if (length > maxLength)
                        {
                            maxLength = length;
                            whichDiv = div;
                        }
                        t = 0;
                    } else
                    {

                        // in the case that we haven't found the end of the cycle we
                        // add the "t" to the sortedset and we add the digit to the list
                        digits.Add(digit);
                        ts.Add(t);
                    }
                }
            }

            Console.WriteLine("Answer: "+whichDiv);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}