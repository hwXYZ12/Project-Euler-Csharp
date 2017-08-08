using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;


namespace exerciseProjectCSharp
{

    class Euler101
    {

        static double evalBOP(Vector<double> p, int degree, int n)
        {
            double ret = 0;
            for(int i = 0; i <= degree; ++i)
            {
                ret += p.At(i) * Math.Pow(n, i);
            }
            return ret;
        }

        static void Main()
        {

            // generate the first 11 numbers in the sequence
            Func<int, double> seq = n => 1 - (double)Math.Pow(n, 1) + (double)Math.Pow(n, 2)
                                        - (double)Math.Pow(n, 3) + (double)Math.Pow(n, 4)
                                        - (double)Math.Pow(n, 5) + (double)Math.Pow(n, 6)
                                        - (double)Math.Pow(n, 7) + (double)Math.Pow(n, 8)
                                        - (double)Math.Pow(n, 9) + (double)Math.Pow(n, 10);

            double[] nums = new double[12];
            for(int i=0; i <= 11; ++i)
            {
                nums[i] = seq(i+1);
            }
            
            Vector<double>[] parameters = new Vector<double>[12];
            parameters[0] = CreateVector.Dense<double>(1, 0);

            /* setup the vectors that will represent the 
             * vectors of the first k elements of the sequence
             */
            Vector<double>[] seqVals = new Vector<double>[12];
            for(int i=1; i<=11; ++i)
            {
                seqVals[i] = CreateVector.Dense<double>(i + 1);
                for(int x=0; x < i + 1; ++x)
                {
                    seqVals[i].At(x, nums[x]);
                }
            }

            /* generate the kth vandermonde matrix
                and compute it's inverse then 
                apply it's inverse to the first k
                elements in the sequence as a vector
                and store the resultant vector as
                a parameter list
            */

            // create vandermonde matrices
            Matrix<double>[] V = new Matrix<double>[12];
            for (int k = 2; k <= 12; ++k)
            {
                V[k - 1] = CreateMatrix.Dense<double>(k, k, 1);

                for (int i = 1; i <= k; ++i)
                {
                    for (int j = 1; j <= k; ++j)
                    {
                        V[k - 1].At(i - 1, j - 1, (double)Math.Pow(i, j - 1));
                    }
                }                
            }

            // invert vandermonde matrices
            for(int i = 1; i <= 11; ++i)
            {
                V[i] = V[i].Inverse();
            }

            // multiply each sequence vector by it's respective
            // inverted vandermonde matrix
            for(int i = 1; i <= 11; ++i)
            {
                parameters[i] = V[i].Multiply(seqVals[i]);
            }

            // continuously evaluate every value generated
            // by each BOP and compare it to the sequence value.
            // Once the values do not match we add the value
            // to the fit sum
            double sum = 1;
            double fit = 0;
            for(int i = 1; i<=9; ++i)
            {
                bool loop = true;
                int n = 1;
                while(loop)
                {
                    double val = evalBOP(parameters[i], i, n);
                    val = Math.Round(val, 0);             
                    Console.WriteLine("Seq: "+nums[n - 1]+" BOP: "+ val);
                    if (val != nums[n-1])
                    {
                        fit = val;
                        loop = false;
                    }
                    ++n;
                }
                sum += fit;
            }

            Console.WriteLine(sum);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
