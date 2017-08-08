using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exerciseProjectCSharp
{
    class RotateMatrixTest
    {
        static void printMatrix(int[,] m)
        {
            for(int i = 0; i < m.GetLength(0); ++i)
            {
                for(int j = 0; j < m.GetLength(1); ++j)
                {
                    Console.Write(m[i,j]+" ");
                }
                Console.WriteLine();
            }
        }

        static void rotateMatrix(int[,] matrix)
        {
            // error handling
            if (matrix == null)
                return;
            if (matrix.GetLength(0) != matrix.GetLength(1))
                return;

            int n = matrix.GetLength(0);
            int numLoops = (int) Math.Ceiling(n/2.0);
            for(int i = 0; i < numLoops; ++i)
            {
                int end = n - 1 - i;
                for(int j = i; j < end; ++j)
                {
                    // swap rotated elements
                    int element = matrix[i, j];
                    int x = i, y = j;
                    int temp = 0;
                    for(int k = 0; k < 3; ++k)
                    {
                        matrix[x, y] = matrix[n - y - 1, x];
                        temp = y;
                        y = x;
                        x = n - temp - 1;
                    }
                    matrix[x, y] = element;
                }
            }

        }

        static void Main()
        {
            int[,] testMatrix = { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
            int[,] smallerM = { { 0, 1 }, { 2, 3 } };
            int[,] fourByFour = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 1, 2, 3 }, { 4, 5, 6, 7} };
            int[,] bigMatrix = { {0, 0, 0, 0, 0},
                                {1, 1, 1, 1, 1},
                               {2, 2, 2, 2, 2}, 
                               {3, 3, 3, 3, 3}, 
                               {4, 4, 4, 4, 4}};

            printMatrix(smallerM);
            printMatrix(testMatrix);
            printMatrix(fourByFour);
            printMatrix(bigMatrix);

            rotateMatrix(smallerM);
            rotateMatrix(testMatrix);
            rotateMatrix(fourByFour);
            rotateMatrix(bigMatrix);

            Console.WriteLine("After one rotation: ");
            printMatrix(smallerM);
            printMatrix(testMatrix);
            printMatrix(fourByFour);
            printMatrix(bigMatrix);

            rotateMatrix(smallerM);
            rotateMatrix(testMatrix);
            rotateMatrix(fourByFour);
            rotateMatrix(bigMatrix);

            Console.WriteLine("After two rotations: ");
            printMatrix(smallerM);
            printMatrix(testMatrix);
            printMatrix(fourByFour);
            printMatrix(bigMatrix);

            Console.ReadLine();
        }
    }
}
