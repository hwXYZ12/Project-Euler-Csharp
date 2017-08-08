// Euler082.cs
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

namespace Euler083
{

    class Euler083
    {

        static int MATRIX_SIZE = 80;
        static string FILE_PATH = "C:\\Users\\William\\Desktop\\Exercise Solutions"
                            + "\\exerciseProjectCSharp\\exerciseProjectCSharp\\"
                            + "p083_matrix.txt";

        static void Main()
        {
            // read matrix into an array
            string textToParse = System.IO.File.ReadAllText(FILE_PATH);
            string[,] temp = TextHandling.readMatrix(textToParse, MATRIX_SIZE, MATRIX_SIZE, '\n', ',');

            // convert each element of the string matrix to an int
            // and place it in an int matrix
            int[,] matrix = new int[MATRIX_SIZE, MATRIX_SIZE];
            for (int i = 0; i < MATRIX_SIZE; ++i )
            {
                for (int j = 0; j < MATRIX_SIZE; ++j)
                {
                    matrix[i, j] = Convert.ToInt32(temp[i, j]);
                }
            }

            // print matrix
            TextHandling.printMatrixToConsole(temp, MATRIX_SIZE, MATRIX_SIZE);

            // build a directed graph from the input matrix
            // each entry in the matrix is going to be another node
            // and for each node, the element(s) directly to the right,
            // beneath, and above are going to be children of the given
            // node, if each child exists.
            MinDirectedGraph.MinGraphNode[,] matrixOfNodes = new MinDirectedGraph.MinGraphNode[MATRIX_SIZE, MATRIX_SIZE];
            for (int i = 0; i < MATRIX_SIZE; ++i)
            {
                for (int j = 0; j < MATRIX_SIZE; ++j)
                {
                    matrixOfNodes[i, j] = new MinDirectedGraph.MinGraphNode(matrix[i,j], null);
                }
            }

            // now add children to each node
            for (int i = 0; i < MATRIX_SIZE; ++i)
            {
                for (int j = 0; j < MATRIX_SIZE; ++j)
                {
                    List<MinDirectedGraph.MinGraphNode> children = new List<MinDirectedGraph.MinGraphNode>();
                    if (j - 1 >= 0)
                    {
                        children.Add(matrixOfNodes[i, j - 1]);
                    }
                    if(j + 1 < MATRIX_SIZE)
                    {
                        children.Add(matrixOfNodes[i, j + 1]);
                    }
                    if(i - 1 >= 0)
                    {
                        children.Add(matrixOfNodes[i - 1, j]);
                    }
                    if(i + 1 < MATRIX_SIZE)
                    {
                        children.Add(matrixOfNodes[i + 1, j]);
                    }
                    matrixOfNodes[i, j].Children = children;
                }
            }

            // finally build the directed graph using the graph nodes
            List<MinDirectedGraph.MinGraphNode> nodeList = new List<MinDirectedGraph.MinGraphNode>();
            for (int i = 0; i < MATRIX_SIZE; ++i)
            {
                for (int j = 0; j < MATRIX_SIZE; ++j)
                {
                    nodeList.Add(matrixOfNodes[i,j]);
                }
            }

            MinDirectedGraph graph = new MinDirectedGraph(nodeList);

            //int min = int.MaxValue;
            //for(int i = 0; i < MATRIX_SIZE; ++i)
            //{
            //    for(int j = 0; j < MATRIX_SIZE; ++j)
            //    {
            //        int x = graph.BFS_MinPathLength(matrixOfNodes[i, 0], matrixOfNodes[j, MATRIX_SIZE - 1]);
            //        Console.WriteLine(x);
            //        if (x < min)
            //            min = x;
            //    }
            //}
            int min = graph.BFS_MinPathLength(matrixOfNodes[0, 0], matrixOfNodes[MATRIX_SIZE - 1, MATRIX_SIZE - 1]);
            Console.WriteLine("Minimum Path between selected nodes: " + min);


            //for (int i = 0; i < MATRIX_SIZE; ++i)
            //{
            //    for (int j = 0; j < MATRIX_SIZE; ++j)
            //    {
            //        Console.Write(matrixOfNodes[i, j].MinPath + ", ");
            //    }
            //    Console.WriteLine();
            //}
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
