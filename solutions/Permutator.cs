using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HomeGrown.Permutations
{
    class Permutator
    {
        // quick and dirty recursive permutation routine
        public static List<string> computePermutations(string x)
        {

            List<string> ret = new List<string>();

            if (x.Length == 1)
            {
                ret.Add(x);
                return ret;
            }

            for (int i = 0; i < x.Length; ++i)
            {
                // recreate integer without the picked digit
                string sub = x.Substring(0, i) + x.Substring(i + 1);
                foreach (string z in computePermutations(sub))
                {
                    ret.Add(x.Substring(i, 1) + z);
                }
            }
            return ret;
        }

        // returns a list of permutations of a list of numbers, resulting in a list
        // of lists
        public static List<List<int>> getIntPermutations(List<int> nums)
        {
            List<List<int>> ret = new List<List<int>>();

            if (nums.Count == 1)
            {
                ret.Add(nums);
                return ret;
            }

            for (int i = 0; i < nums.Count; ++i)
            {
                // recreate integer list without the picked integer
                List<int> without = new List<int>();
                for(int j = 0; j < nums.Count; ++j)
                {
                    if(j != i)
                        without.Add(nums[j]);
                }
                foreach(List<int> z in getIntPermutations(without))
                {
                    // add the picked integer and add the new list
                    // to the list of permutations
                    z.Add(nums[i]);
                    ret.Add(z);
                }
            }
            return ret;
        }


        public static List<BigInteger> getDigits(BigInteger num)
        {
            List<BigInteger> ret = new List<BigInteger>();
            while(num != 0)
            {
                ret.Add(num % 10);
                num /= 10;
            }
            return ret;
        }

        public static List<int> getDigits(int num)
        {
            List<int> ret = new List<int>();
            while (num != 0)
            {
                ret.Add(num % 10);
                num /= 10;
            }
            return ret;
        }

        public static int sumOfDigits(int x)
        {
            int ret = 0;
            List<int> digits = getDigits(x);
            foreach(int y in digits)
                ret += y;
            return ret;
        }

        public static BigInteger sumOfDigits(BigInteger x)
        {
            BigInteger ret = 0;
            List<BigInteger> digits = getDigits(x);
            foreach (BigInteger y in digits)
                ret += y;
            return ret;
        }

        // returns true when x is a permutation of y and false
        // otherwise
        public static bool isPermutation(int x, int y)
        {
            List<int> digitsX = getDigits(x);
            List<int> digitsY = getDigits(y);
            digitsX = digitsX.OrderBy(x1 => x1).ToList();
            digitsY = digitsY.OrderBy(x1 => x1).ToList();
            if (digitsX.Count != digitsY.Count)
                return false;

            for(int i = 0; i < digitsX.Count; ++i)
            {
                if (digitsX[i] != digitsY[i])
                    return false;
            }
            return true;
        }

        // returns true when x is a permutation of y and false
        // otherwise
        public static bool isPermutation(BigInteger x, BigInteger y)
        {
            List<BigInteger> digitsX = getDigits(x);
            List<BigInteger> digitsY = getDigits(y);
            digitsX = digitsX.OrderBy(x1 => x1).ToList();
            digitsY = digitsY.OrderBy(x1 => x1).ToList();
            if (digitsX.Count != digitsY.Count)
                return false;

            for (int i = 0; i < digitsX.Count; ++i)
            {
                if (digitsX[i] != digitsY[i])
                    return false;
            }
            return true;
        }
    }
}
