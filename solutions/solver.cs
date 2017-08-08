using System.Threading;
using System;
using BigNum;

namespace Psychlops
{

	namespace Solver
	{
		internal static class CONST
		{
			public static readonly uint MAX_ARG = 5;
		}

		
		static public class Constants
		{
			public static readonly int LIMIT = 5;
			public delegate double Func0(double x);
			public delegate double Func1(double x, double a);
			public delegate double Func2(double x, double a, double b);
			public delegate double Func3(double x, double a, double b, double c);
			public delegate double Func4(double x, double a, double b, double c, double d);
			public delegate double Func5(double x, double a, double b, double c, double d, double e);
		}


		static public class Binomial
		{
			public static double factorial(int fact)
			{
				double x = 1;
				for (int i = 1; i <= fact; i++)
				{
					x *= i;
				}
				return x;
			}
			public static double combination(int nn, int kk)
			{
				return (double)(Int64)(factorial(nn) / (factorial(kk) * factorial(nn - kk)));
				/*
				BigInt n = new BigInt((Int64)nn, new PrecisionSpec());
				BigInt k = new BigInt((Int64)kk, new PrecisionSpec());
				BigInt n_k = n - k;
				n.Factorial();
				k.Factorial();
				n_k.Factorial();
				return (double)(Int64)(n / (k * n_k) );
				 * */
			}
			public static double likelihood(double pp, int yes, int no)
			{
				double p;
				if (pp < 0) p = 0; else if (pp > 1) p = 1; else p = pp;
				return combination(yes + no, yes) * System.Math.Pow(p, yes) * System.Math.Pow(1 - p, no);
			}

			public static double likelihood(double pp, int yes, int no, double comb)
			{
				double p;
				if (pp < 0) p = 0; else if (pp > 1) p = 1; else p = pp;
				return comb * System.Math.Pow(p, yes) * System.Math.Pow(1 - p, no);
			}

		}


		public class BernoulliProcess
		{
			public struct Data
			{
				public double x;
				public int pos, neg, comb;
				public void set(double cond, int posit, int negat)
				{
					x = cond;
					pos = posit;
					neg = negat;
				}

			};
			public Data[] elems;
			public int length;
			public void set(double[][] num)
			{
				elems = new Data[num.GetLength(0)];
				for (int i = 0; i < elems.GetLength(0); i++)
				{
					elems[i].set(num[i][0], (int)num[i][1], (int)num[i][2]);
				}
			}
		};



		class BinomialLikelihoodThread
		{
			public bool finished;
			public static bool started;
			internal Thread th;
			public static BinomialLikelihoodThread current;

			public Interval[] itvl;
			public double[] step;
			public double[] champ;
			public double champ_like;
			public BernoulliProcess data;


			public Constants.Func0 func0;
			public Constants.Func1 func1;
			public Constants.Func2 func2;
			public Constants.Func3 func3;
			public Constants.Func4 func4;
			public Constants.Func5 func5;


			public BinomialLikelihoodThread()
			{
				itvl = new Interval[CONST.MAX_ARG];
				step = new double[CONST.MAX_ARG];
				champ = new double[CONST.MAX_ARG];
				data = new BernoulliProcess();
			}
			public void waitLoop()
			{
				finished = false;
				for (int i = 0; i < CONST.MAX_ARG; i++)
				{
					champ[i] = 0;
				}
				current = this;
				started = false;
			}

			public void loop1() { waitLoop(); th = new Thread(new ThreadStart(loop1_)); th.Start(); }
			public void loop2() { waitLoop(); th = new Thread(new ThreadStart(loop2_)); th.Start(); }
			public void loop3() { waitLoop(); th = new Thread(new ThreadStart(loop3_)); th.Start(); }
			public void loop4() { waitLoop(); th = new Thread(new ThreadStart(loop4_)); th.Start(); }
			public void loop5() { waitLoop(); th = new Thread(new ThreadStart(loop5_)); th.Start(); }
			void loop1_()
			{
				started = true;
				double p;
				double like = 1.0;
				champ_like=0.0;
				int L = data.length;
				for (double a = itvl[0].begin.value; a < itvl[0].end.value; a += step[0])
				{
					like = 1.0;
					for(int i=0; i<L; i++) {
						p = func1(data.elems[i].x, a);
						like *= Binomial.likelihood(p, data.elems[i].pos, data.elems[i].neg, data.elems[i].comb);
					}
					if(like > champ_like) {
						champ_like = like;
						champ[0] = a;
					}
				}
				finished = true;
			}
			void loop2_()
			{
				started = true;
				double p = 0.0;
				double like = 1.0;
				champ_like = 0.0;
				int L = data.length;
				for (double a = itvl[0].begin.value; a < itvl[0].end.value; a += step[0])
				{
					for (double b = itvl[1].begin.value; b < itvl[1].end.value; b += step[1])
					{
						like = 1.0;
						for (int i = 0; i < L; i++)
						{
							p = func2(data.elems[i].x, a, b);
							like *= Binomial.likelihood(p, data.elems[i].pos, data.elems[i].neg, data.elems[i].comb);
						}
						if (like > champ_like)
						{
							champ_like = like;
							champ[0] = a;
							champ[1] = b;
						}
					}
				}
				finished = true;
			}
			void loop3_()
			{
				started = true;
				double p;
				double like = 1.0;
				champ_like = 0.0;
				int L = data.length;
				for (double a = itvl[0].begin.value; a < itvl[0].end.value; a += step[0])
				{
					for (double b = itvl[1].begin.value; b < itvl[1].end.value; b += step[1])
					{
						for (double c = itvl[2].begin.value; c < itvl[2].end.value; c += step[2])
						{
							like = 1.0;
							for (int i = 0; i < L; i++)
							{
								p = func3(data.elems[i].x, a, b, c);
								like *= Binomial.likelihood(p, data.elems[i].pos, data.elems[i].neg, data.elems[i].comb);
							}
							if (like > champ_like)
							{
								champ_like = like;
								champ[0] = a;
								champ[1] = b;
								champ[1] = c;
							}
						}
					}
				}
				finished = true;
			}
			void loop4_()
			{
				started = true;
				finished = true;
			}
			void loop5_()
			{
				started = true;
				finished = true;
			}
		};



		public class BinomialLikelihood
		{
			/*
			public static void showWindow(Constants.Func1 f)
			{
				Main.canvas.api_canvas.Dispatcher.BeginInvoke(new Action<Constants.Func1>(showWindow_), f);
			}
			internal static void showWindow_(Constants.Func1 f)
			{
				System.Windows.Controls.ChildWindow page = new PsychlopsSilverlight4.Pages.BinomialSolver(f);
				page.Show();
			}
			public static void showWindow(Constants.Func2 f)
			{
				Main.canvas.api_canvas.Dispatcher.BeginInvoke(new Action<Constants.Func2>(showWindow_), f);
			}
			internal static void showWindow_(Constants.Func2 f)
			{
				System.Windows.Controls.ChildWindow page = new PsychlopsSilverlight4.Pages.BinomialSolver(f);
				page.Show();
			}
			public static void showWindow(Constants.Func3 f)
			{
				Main.canvas.api_canvas.Dispatcher.BeginInvoke(new Action<Constants.Func3>(showWindow_), f);
			}
			internal static void showWindow_(Constants.Func3 f)
			{
				System.Windows.Controls.ChildWindow page = new PsychlopsSilverlight4.Pages.BinomialSolver(f);
				page.Show();
			}
			*/

			public int iteration;

			public Interval[] itvl;
			public double[] step;
			public double[] champ;
			public double champ_like;
			public BernoulliProcess data;

			public Constants.Func0 func0;
			public Constants.Func1 func1;
			public Constants.Func2 func2;
			public Constants.Func3 func3;
			public Constants.Func4 func4;
			public Constants.Func5 func5;

			public BinomialLikelihood()
			{
				itvl = new Interval[CONST.MAX_ARG];
				step = new double[CONST.MAX_ARG];
				champ = new double[CONST.MAX_ARG];
				iteration = 2;
			}

			public void begin(Constants.Func0 d_func)
			{ }
			public void begin(Constants.Func1 d_func)
			{
				func1 = d_func;

				BinomialLikelihoodThread[] l = new BinomialLikelihoodThread[4];
				for (int i = 0; i < 4; i++) { l[i] = new BinomialLikelihoodThread(); }

				for (int k = 0; k < iteration; k++)
				{
					begin_base(l);
					for (int i = 0; i < 4; i++)
					{
						l[i].data = data;
						l[i].func1 = d_func;
						l[i].loop1();
					}
					end_base(l);
				}
			}
			public void begin(Constants.Func2 d_func)
			{
				func2 = d_func;

				BinomialLikelihoodThread[] l = new BinomialLikelihoodThread[4];
				for (int i = 0; i < 4; i++) { l[i] = new BinomialLikelihoodThread(); }

				for (int k = 0; k < iteration; k++)
				{
					begin_base(l);
					for (int i = 0; i < 4; i++)
					{
						l[i].data = data;
						l[i].func2 = d_func;
						l[i].loop2();
					}
					end_base(l);
				}
			}
			public void begin(Constants.Func3 d_func)
			{
				func3 = d_func;

				BinomialLikelihoodThread[] l = new BinomialLikelihoodThread[4];
				for (int i = 0; i < 4; i++) { l[i] = new BinomialLikelihoodThread(); }

				for (int k = 0; k < iteration; k++)
				{
					begin_base(l);
					for(int i=0; i<4; i++) {
						l[i].data = data;
						l[i].func3 = d_func;
						l[i].loop3();
					}
					end_base(l);
				}
			}

			public void begin(Constants.Func4 d_func)
			{
			}
			public void begin(Constants.Func5 d_func)
			{
			}

			void begin_base(BinomialLikelihoodThread[] l)
			{
				champ_like = 0;

				data.length = data.elems.GetLength(0);
				for (int i = 0; i < data.elems.GetLength(0); i++)
				{
					data.elems[i].comb = (int)Binomial.combination(data.elems[i].pos + data.elems[i].neg, data.elems[i].pos);
				}

				for (int j = 0; j < Constants.LIMIT; j++) { step[j] = (itvl[j].end.value - itvl[j].begin.value) / 256.0; }
				for (int i = 0; i < 4; i++)
				{
					l[i].itvl[0] = new Interval((itvl[0].begin.value) + (i * step[0] * 64), (itvl[0].begin.value) + ((i + 1) * step[0] * 64));
					l[i].step[0] = step[0];
					for (int j = 1; j < Constants.LIMIT; j++)
					{
						l[i].itvl[j] = itvl[j];
						l[i].step[j] = step[j];
					}
					l[i].data = data;
				}
			}

			void end_base(BinomialLikelihoodThread[] l)
			{
				//for (int i = 0; i < 4; i++) { l[i].th.Join(); }
				while (!l[0].finished || !l[1].finished || !l[2].finished || !l[3].finished) { Thread.Sleep(100);  } 

				for(int j=0; j<Constants.LIMIT; j++) { champ[j] = 0; }
				champ_like = 0.0;
				for(int i=0; i<4; i++) {
					if(champ_like < l[i].champ_like) {
						champ_like = l[i].champ_like;
						for(int j=0; j<Constants.LIMIT; j++) { champ[j] = l[i].champ[j]; }
					}
				}

				double r, low, high;
				for (int j = 0; j < Constants.LIMIT; j++)
				{
					r = itvl[j].end.value - itvl[j].begin.value;
					low = champ[j] - r / 8.0 < itvl[j].begin.value ? itvl[j].begin.value : champ[j] - r / 8.0;
					high = champ[j] + r / 8.0 > itvl[j].end.value ? itvl[j].end.value : champ[j] + r / 8.0;
					itvl[j] = new Interval(low, high);
				}

			}


		}


	}

}