// Euler042.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
namespace Euler042
{
    class Euler042
    {
        private const string fileName = "words.txt";

        static void Main()
        {

            // open the file
            // then we're going to parse it into memory and break it into
            // a list of words
            List<string> words = new List<string>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string contents = sr.ReadToEnd();
                int t = contents.IndexOf(',');
                while(t != -1)
                {
                    string temp = contents.Substring(0, t);
                    temp = temp.Substring(1, temp.Length-2); // remove "" characters included in the file
                    words.Add(temp);
                    contents = contents.Substring(t+1);
                    t = contents.IndexOf(',');
                }
                sr.Close();
            }

            int sum = 0;
            foreach(string s in words)
            {
                int val = 0;
                char[] x = s.ToCharArray();
                for(int i = 0; i < s.Length; ++i)
                {
                    val += getAlphabeticalPosition(x[i]);
                }
                if (isTriangleNumber(val))
                    ++sum;

                Console.WriteLine(s);
                Console.WriteLine(val);
            }

            Console.WriteLine("Answer: " + sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        // determines if a number is a triangle number
        static bool isTriangleNumber(int x)
        {
            double ret = (-1+Math.Sqrt(1+8*x)) / 2;
            return (ret % 1 == 0);
        }

        // returns the alphabetical position of the given character
        // assuming the character is a letter of the alphabet
        static int getAlphabeticalPosition(char c)
        {
            return (""+c).ToLower()[0] - 'a' + 1;
        }

    }
}