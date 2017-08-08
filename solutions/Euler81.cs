// Euler081.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;
using System.Text;
using Permutations;
using System.Linq;

namespace Euler081
{
    class Euler081
    {
        static void Main()
        {
            // uses dynamic programming to search through an n by n matrix
            // to find the minimum path moving only from left to right
            int n = 80;
            int[][] matrix = new int[n][];
            for(int i = 0; i < n; ++i)
            {
                matrix[i] = new int[n];
            }

            // here we parse the values into the matrix
            // parse the matrix text file
            string filePath = "C:\\Users\\William\\Desktop\\Exercise Solutions"
                                + "\\exerciseProjectCSharp\\exerciseProjectCSharp\\"
                                + "p081_matrix.txt";
            //string filePath = "C:\\Users\\William\\Desktop\\Exercise Solutions"
            //                    + "\\exerciseProjectCSharp\\exerciseProjectCSharp\\"
            //                    + "testMatrix.txt";
            string textToParse = System.IO.File.ReadAllText(filePath);
            int x1 = 0, y1 = 0;
            while(!textToParse.Equals(""))
            {
                // work one line at a time
                int nextLineIndex = textToParse.IndexOf('\n');

                // found the end of the input
                string nextLine;
                if (nextLineIndex < 0)
                {
                    nextLine = textToParse.Substring(0);
                    textToParse = "";
                }
                else
                {
                    nextLine = textToParse.Substring(0, nextLineIndex);
                    // remove the line from our working string
                    textToParse = textToParse.Substring(nextLineIndex + 1);
                }


                while(!nextLine.Equals(""))
                {
                    // move through the line one matrix element
                    // at a time
                    int nextElementIndex = nextLine.IndexOf(',');

                    // found the end of the input
                    string nextElement;
                    if (nextElementIndex < 0)
                    {
                        nextElement = nextLine.Substring(0);
                        nextLine = "";
                    }
                    else
                    {
                        nextElement = nextLine.Substring(0, nextElementIndex);
                        // remove the element from our working string
                        nextLine = nextLine.Substring(nextElementIndex + 1);
                    }
                        
                    Int32.TryParse(nextElement, out matrix[x1][y1]);
                    ++y1;
                    if (y1 >= n)
                    {
                        y1 = 0;
                        ++x1;
                    }
                }
            }


            // we're going to dynamically calculate the
            // value of the minimum path to each element of the
            // matrix            
            int[][] minMatrix = new int[n][];
            for(int i = 0; i < n; ++i)
            {
                minMatrix[i] = new int[n];
            }

            // initialize and skip the first diagonal
            minMatrix[0][0] = matrix[0][0];
            for(int i = 1; i < 2*n; ++i)
            {
                // calculate the minimum paths
                // of each element on the diagonal
                // let (j, k) be the starting point from the
                // top right part of the diagonal
                int j = 0, k = 0;
                if(i >= n)
                {
                    j = n - 1;
                    k = i - n + 1;
                } else
                {
                    j = i;
                    k = 0;
                }
                for(int m = 0; j - m >= 0 && k + m < n; ++m)
                {
                    // we now redefine x and y in terms of
                    // j and k in order to work on each element
                    // of the diagonal
                    int x = j - m, y = k + m;
                    int sum1 = int.MaxValue, sum2 = int.MaxValue; // use max value to bounce off walls
                    if(x > 0)
                    {
                        sum1 = minMatrix[x-1][y];
                    }
                    if(y > 0)
                    {
                        sum2 = minMatrix[x][y-1];
                    }
                    minMatrix[x][y] = (sum1 <= sum2) ? sum1 : sum2;
                    minMatrix[x][y] += matrix[x][y];
                }
            }

            // this algorithm ought to be correct and visits each element
            // of the matrix at least once but not more than 3 times, so
            // this algorithm is O(n*n)
            Console.WriteLine("Answer: " + minMatrix[n-1][n-1]);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
