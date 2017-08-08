// Euler055.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
namespace Euler055
{
    class Euler055
    {
        static void Main()
        {
            int answer = 0;
            int search = 10000;
            for (int i = 0; i < search; ++i)
            {
                BigInteger gpti = i;
                for (int j = 0; j < 50; ++j)
                {
                    gpti = (gpti + BigInteger.Parse(reverse("" + gpti)));
                    if(isPalindrome(""+gpti))
                    {
                        ++answer;
                        break;
                    }
                }
            }
            answer = search - answer;
            Console.WriteLine("Answer: " + answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // quick and dirty string reversal
        static string reverse(string s)
        {
            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = s.Length - 1; i >= 0; --i)
                sb.Append(s[i]);
            return sb.ToString();
        }

        // checks if a string is a palindrome
        static bool isPalindrome(string s)
        {
            return s.Equals(reverse(s));
        }

    }
        
}