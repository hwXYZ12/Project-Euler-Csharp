// Euler036.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler036
{
    class Euler036
    {
        static void Main()
        {
            int sum = 0;
            int sum2 = 0;
            string p = "1";
            while (Int32.Parse(p) < 1000000)
            {
                if (isPalindrome(p) && isPalindrome(toBase2(Int32.Parse(p))))
                {
                    Console.WriteLine(p + " = " + toBase2(Int32.Parse(p)));
                    sum += Int32.Parse(p);
                }
                p = nextPalindrome(p);
            }

            for (int i = 1; i < 1000000; ++i)
            {
                if (isPalindrome("" + i) && isPalindrome(toBase2(i)))
                {
                    Console.WriteLine(i + " = " + toBase2(i));
                    sum2 += i;
                }
            }

            Console.WriteLine("Answer: " + sum);
            Console.WriteLine("Answer: " + sum2);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }



        private static char minc = '0';
        private static char mincPlusOne = '1';
        private static char maxc = '9';

        // get nth palindrome
        static string getNthPalindrome(int n)
        {
            string s = "1";
            for (int i = 1; i < n; ++i)
            {
                s = nextPalindrome(s);
            }
            return s;
        }

        // returns the next palindrome in a lexicographical ordering
        // of palindromes (assumes that the input is a palindrome)
        private static string nextPalindrome(string s)
        {
            int len = s.Length;
            for (int i = 0; i < len / 2; ++i)
            {
                if (s[i] < maxc)
                {
                    s = replaceCharAtIndex(s, len - i - 1, Convert.ToChar(s[i] + 1));
                    s = replaceCharAtIndex(s, i, Convert.ToChar(s[i] + 1));
                    return s;
                }
                else
                {
                    // reset to the minc, except reset to mincPlusOne iff the character is at
                    // the beginning / end of the string
                    if(i==0)
                    {
                        s = replaceCharAtIndex(s, len - i - 1, mincPlusOne);
                        s = replaceCharAtIndex(s, i, mincPlusOne);
                    }
                    else
                    {
                        s = replaceCharAtIndex(s, len - i - 1, minc);
                        s = replaceCharAtIndex(s, i, minc);
                    }
                }

            }

            // if this point is reached then the palindrome is either "full"
            // in the sense that it is a palindrome of only 9's
            // or it must be wrapped
            if(len % 2 == 1 && Convert.ToChar(s[len/2]) < maxc )
            {
                // to wrap the palindrome we incrememnt the innermost character and
                // reset the other characters to 0 and the outer characters to 1
                s = replaceCharAtIndex(s, len / 2, Convert.ToChar(s[len / 2] + 1));
                for (int i = (len + 1) / 2; i < len; ++i)
                {
                    if (s[i] == maxc)
                    {
                        if(i == len - 1)
                        {
                            s = replaceCharAtIndex(s, len - i - 1, minc);
                            s = replaceCharAtIndex(s, i, minc);
                        } else
                        {
                            s = replaceCharAtIndex(s, len - i - 1, mincPlusOne);
                            s = replaceCharAtIndex(s, i, mincPlusOne);
                        }
                    }
                }
                return s;
            }

            for (int i = 0; i < len; ++i )
                s = replaceCharAtIndex(s, i, minc);
            s = replaceCharAtIndex(s, 0, mincPlusOne);
            s += mincPlusOne;

            return s;
        }

        // helper method to do in c# what would normally be done with ease
        // in c++
        static string replaceCharAtIndex(string s, int index, char x)
        {
            return s.Substring(0, index) + x + s.Substring(index+1);
        }

        // helper to determine if a string represents a palindrome
        static bool isPalindrome(string s)
        {
            return s.Equals(reverse(s));
        }

        // helper taken from the stack exchange to reverse strings
        static string reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        // convert a decimal number to a base 2 number (as a string)
        static string toBase2(int n)
        {
            if (n < 0)
                return "-1";

            string ret = "";
            while(n != 0)
            {
                ret = n % 2 + ret;
                n /= 2;
            }
            return ret;
        }

    }
}