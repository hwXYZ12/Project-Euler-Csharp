using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HomeGrown.TextHandling
{
    class TextHandling<T>
    {        
        // reads a string of input and returns a matrix of the input values
        // note that this function assumes that the input is appropriately formatted!
        public static string[,] readMatrix(string input, int rows, int columns, char separatingCharRows, char separatingCharCols)
        {
            string[,] ret = new string[rows, columns];

            for(int i = 0; i < rows; ++i)
            {
                for(int j = 0; j < columns; ++j)
                {
                    // use the next index of the character that separates each column unless
                    // the parser has reached the final element of the row
                    int nextIndex = (j == columns - 1) ? input.IndexOf(separatingCharRows)
                                                        : input.IndexOf(separatingCharCols);
                    if (nextIndex == -1)
                    {
                        // the last element of the input is all that is leftover
                        ret[i, j] = input;
                    }
                    else
                    {
                        string convert = input.Substring(0, nextIndex);
                        ret[i, j] = convert;
                        input = input.Substring(nextIndex + 1);
                    }
                }

            }

            return ret;
        }

        // prints all of the contents of a string matrix to the console
        public static void printMatrixToConsole(string[,] matrix, int rows, int cols)
        {
            for(int i = 0; i < rows; ++i)
            {
                for(int j = 0; j < cols; ++j)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        // reads in line-by-line some text and performs a function on each line
        // then the result of each operation is stored in an array of type T (generic)
        // and the resulting array is returned
        // this function assumes that the input is formatted correctly!
        public static T[] getListOfOperationResultsOnInput(string input, int inputSize, char separatingCharInput, Func<string, T> op)
        {
            T[] ret = new T[inputSize];

            for (int i = 0; i < inputSize; ++i)
            {
                // get the index of the next separating character of the input
                int nextIndex = input.IndexOf(separatingCharInput);
                if (nextIndex == -1)
                {
                    // the last element of the input is all that is leftover
                    ret[i] = op(input);
                }
                else
                {
                    string convert = input.Substring(0, nextIndex);
                    ret[i] = op(convert);
                    input = input.Substring(nextIndex + 1);
                }

            }

            return ret;

        }

        // reads in line-by-line some text and performs a function on each line
        // then the result of each function is added to a running sum
        // and the resulting sum is returned
        // this function assumes that the input is formatted correctly!
        public static BigInteger getSumOfOperationResultsOnInput(string input, int inputSize, char separatingCharInput, Func<string, BigInteger> op)
        {
            BigInteger ret = 0;

            for (int i = 0; i < inputSize; ++i)
            {
                // get the index of the next separating character of the input
                int nextIndex = input.IndexOf(separatingCharInput);
                if (nextIndex == -1)
                {
                    // the last element of the input is all that is leftover
                    ret += op(input);
                }
                else
                {
                    string convert = input.Substring(0, nextIndex);
                    ret += op(convert);
                    input = input.Substring(nextIndex + 1);
                }

            }

            return ret;

        }
    }
   
}
