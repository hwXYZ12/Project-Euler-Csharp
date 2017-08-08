// Euler061.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using PrimeNumbers;

namespace Euler061
{
    class Euler061
    {
        private class CyclicFigurate
        {
            
            // some processing is done at startup to organize the figurate
            // values that we'll be working with
            private static Func<int, int>[] figurateFunc = new Func<int, int>[6] 
            {   (n) => n * (n + 1) / 2,
                (n) => n * n,
                (n) => n * (3*n - 1) / 2,
                (n) => n * (2*n - 1),
                (n) => n * (5*n - 3) / 2,
                (n) => n * (3*n - 2)
            };
            private static int[] low = new int[6], high = new int[6];
            private static int max = 0;
            private static int[][] figurates;
            
            static CyclicFigurate()
            {
                for (int i = 0; i < 6; ++i)
                {
                    for (int j = 0; j < 1000; ++j)
                    {
                        if (figurateFunc[i](j) <= 1000 && figurateFunc[i](j + 1) > 1000)
                            low[i] = j + 1;
                        if (figurateFunc[i](j) < 10000 && figurateFunc[i](j + 1) >= 10000)
                            high[i] = j;
                    }
                }
                for (int i = 0; i < 6; ++i)
                {
                    if (high[i] > max)
                        max = high[i];
                }
                ++max;
                figurates = new int[6][];

                for (int i = 0; i < 6; ++i)
                {
                    figurates[i] = new int[high[i] - low[i] + 1];
                    for (int j = 0; j < high[i] - low[i] + 1; ++j)
                    {
                        figurates[i][j] = figurateFunc[i](j+low[i]);
                    }
                }
            }

            // this class will be used to build the tree that our backtracking
            // algorithm will run on
            // Each node must contain enough information to generate all possible
            // decisions to the next decision node
            private class CandidateNode
            {
                public struct Pair
                {
                    public int whichFigurate;
                    public int index;
                }

                public List<int> nums = new List<int>();
                public Dictionary<int, Pair> figurateDict = new Dictionary<int, Pair>();

                public CandidateNode(List<int> nums, Dictionary<int, Pair> figurateDict)
                {
                    foreach(int x in nums)
                    {
                        this.nums.Add(x);
                    }
                    foreach(int key in figurateDict.Keys)
                    {
                        Pair p;
                        figurateDict.TryGetValue(key, out p);
                        this.figurateDict.Add(key, p);
                    }
                }

                public void print()
                {
                    // we've found the answer and must print it!
                    string s2 = "";
                    foreach (int x in nums)
                        s2 += x + " ";
                    Console.WriteLine("Answer: " + s2);
                }
                
                public bool isSolution()
                {
                    return (nums.Count == 6 && oneArrangementIsCyclic(nums));
                }

                public List<CandidateNode> getNextCandidates()
                {
                    List<int> skipFigurates = new List<int>();
                    foreach(int x in nums)
                    {
                        Pair p;
                        figurateDict.TryGetValue(x, out p);
                        skipFigurates.Add(p.whichFigurate);
                    }

                    List<CandidateNode> newNodes = new List<CandidateNode>();
                    foreach(int a in nums)
                    {
                        for(int x = 0; x < 6; ++x)
                        {
                            // skip figurates we already have
                            if(!skipFigurates.Contains(x))
                            {
                                for(int i = 0; i < figurates[x].Length; ++i)
                                {
                                    if(isCyclicPair(a, figurates[x][i])
                                        && a != figurates[x][i]
                                        && !figurateDict.ContainsKey(figurates[x][i]))
                                    {
                                        // create new node
                                        List<int> t = new List<int>();
                                        foreach(int b in nums)
                                        {
                                            t.Add(b);
                                        }
                                        t.Add(figurates[x][i]);
                                        Dictionary<int, Pair> d = new Dictionary<int,Pair>();
                                        Pair p;
                                        foreach(int key in figurateDict.Keys)
                                        {
                                            figurateDict.TryGetValue(key, out p);
                                            d.Add(key, p);
                                        }
                                        p = new Pair();
                                        p.whichFigurate = x;
                                        p.index = i;
                                        d.Add(figurates[x][i], p);
                                        CandidateNode node = new CandidateNode(t, d);
                                        newNodes.Add(node);
                                    }
                                }
                            }
                        }
                    }
                    return newNodes;
                }

                // checks if the last 2 digits of a number x matches the first
                // 2 digits of another number y
                // OR
                // if the last 2 digits of a number y matches the first 
                // 2 digits of another number y
                // this function makes major assumptions about the input!!!
                // we lose some efficiency here because we get the digits twice....
                private bool isCyclicPair(int x, int y)
                {
                    return isCyclicPairForward(x, y) || isCyclicPairForward(y, x);
                }

                // checks if the last 2 digits of a number x matches the first
                // 2 digits of another number y
                // this function makes major assumptions about the input!!!
                private bool isCyclicPairForward(int x, int y)
                {
                    List<int> t1 = getDigits(x);
                    List<int> t2 = getDigits(y);
                    return (t1[1] == t2[t2.Count - 1]
                        && t1[0] == t2[t2.Count - 2]);
                }

                // returns a list of the digits of an integer
                private List<int> getDigits(int num)
                {
                    List<int> ret = new List<int>();
                    while (num != 0)
                    {
                        ret.Add(num % 10);
                        num /= 10;
                    }
                    return ret;
                }
            }

            public CyclicFigurate()
            {
            }

            private void backTrack(CandidateNode node)
            {
                // the decision tree is going to be built using recursion
                if(node.isSolution())
                {
                    // we've found the answer and must print it!
                    node.print();
                    return;
                }

                // if we haven't found the solution, then
                // we run our backtracking algorithm
                // Note that we don't prune any nodes early,
                // this algorithm is decidedly faster than the
                // original brute force algorithm that I'd been using
                // since this algorithm will eliminate certain choices
                // by virtue of the 'next candidate' function
                List<CandidateNode> nextCandidates = node.getNextCandidates();
                foreach (CandidateNode next in nextCandidates)
                    backTrack(next);

                
            }

            public void getSolution()
            {
                for(int x = 0; x < 6; ++x)
                {
                    for(int i = 0; i < figurates[x].Length; ++i)
                    {
                        List<int> start = new List<int>();
                        Dictionary<int, CandidateNode.Pair> figurateDict = new Dictionary<int, CandidateNode.Pair>();
                        CandidateNode.Pair p = new CandidateNode.Pair();
                        p.whichFigurate = x;
                        p.index = i;
                        start.Add(figurates[x][i]);
                        figurateDict.Add(figurates[x][i], p);
                        CandidateNode root = new CandidateNode(start, figurateDict);
                        backTrack(root);
                    }
                }

                // we need to traverse a decision tree.... we need a backtracking algorithm!

                // we use each possible starting point as a root node since it would seem
                // a bit arduous to code the recursive calls to handle both the regular
                // recursive parts as well as the initial special case that starts the process off

            //    if(pieces.Count == 0)
            //    {
            //        pieces.Add(figurates[initialFigurate][initialFiguratePos]);
            //        hasPiece[initialFigurate] = true;
            //        thrownAway.Clear();
            //        throwAwayLevel = 0;
            //        return true;
            //    }

            //    // if we have all pieces, then we must check to see
            //    // if the entire set is cyclic
            //    if (pieces.Count == 6)
            //    {
            //        List<int> t = new List<int>();
            //        foreach(int x in pieces)
            //        {
            //            t.Add(x);
            //        }

            //        // debug print
            //        string s = "";
            //        foreach (int x in pieces)
            //            s += x + " ";
            //        Console.WriteLine("Answer: " + s);

            //        if(oneArrangementIsCyclic(t))
            //        {
            //            // we've found the answer and must print it!
            //            string s2 = "";
            //            foreach (int x in pieces)
            //                s2 += x + " ";
            //            Console.WriteLine("Answer: " + s2);
            //            return false;
            //        }
            //        else
            //        {
            //            // we haven't found the answer and must continue
            //            // our search
            //            goto NewSearch;
            //        }
            //    }

                

            //    // if another piece cannot be found then we must
            //    // reset this object, except we must be sure that
            //    // we do not re-use the same starting piece, thus
            //    // we begin the entire process again using the next                
            //    // figurate piece
            //NewSearch:

            //    // we have something of a problem...
            //    // when we cannot find another piece, it's quite
            //    // possible that we need only backtrack one level
            //    // and we will find the answer so that is what we must do

            //    // remove the last element
            //    // and add it to the list of items that
            //    // are to be excluded from future piece
            //    // searches
            //    if(throwAwayLevel == 0)
            //    {
            //        // if the throwAwayLevel is zero then
            //        // we can safely assume the value hasn't been
            //        // set yet (since you must have something to throw away!)
            //        throwAwayLevel = pieces.Count;
            //        thrownAway.Clear();
            //    }
            //    else
            //    {
            //        if(throwAwayLevel != pieces.Count)
            //        {
            //            // here we know that we've backtracked
            //            // to a different number of pieces, and 
            //            // can free the thrown away numbers
            //            throwAwayLevel = pieces.Count;
            //            thrownAway.Clear();
            //        }
            //    }
            //    thrownAway.Add(pieces[pieces.Count - 1]);
            //    pieces.Remove(pieces[pieces.Count - 1]);

            //    // reset flags for which pieces this object holds
            //    for (int i = 0; i < 6; ++i )
            //    {
            //        hasPiece[i] = false;
            //    }
            //    foreach (int x in pieces)
            //    {
            //        for (int i = 0; i < 6; ++i)
            //        {
            //            for (int j = 0; j < figurates[i].Length; ++j)
            //            {
            //                if (figurates[i][j] == x)
            //                {
            //                    hasPiece[i] = true;
            //                    break;
            //                }
            //            }
            //        }
            //    }

            //    // we have a small problem since calling this function
            //    // again will retrieve the piece that we threw away.....

            //    // ... here it becomes a bit more clear that we need something
            //    // of a backtracking algorithm....

            //    // I suppose we only need to track one level, that is if we
            //    // start throwing away pieces when we have 6 of them, then
            //    // we need only keep track of those thrown away as long as
            //    // we are picking up a 6th piece..... I'm not sure if this
            //    // reasoning makes sense but it's worth a shot!

            //    // as it turns out.. NOPE ... the line of reasoning that
            //    // I just attempted doesn't work! It's possible to re-enter
            //    // a previous decision that was found to be fruitless causing
            //    // a loop. You need a genuine backtracking algorithm to 
            //    // handle this problem appropriately
            //    return findAnotherCyclicPiece();
            }
           
            // checks if the last 2 digits of a number x matches the first
            // 2 digits of another number y
            // OR
            // if the last 2 digits of a number y matches the first 
            // 2 digits of another number y
            // this function makes major assumptions about the input!!!
            // we lose some efficiency here because we get the digits twice....
            private bool isCyclicPair(int x, int y)
            {
                return isCyclicPairForward(x, y) || isCyclicPairForward(y, x);
            }

            // checks if the last 2 digits of a number x matches the first
            // 2 digits of another number y
            // this function makes major assumptions about the input!!!
            private bool isCyclicPairForward(int x, int y)
            {
                List<int> t1 = getDigits(x);
                List<int> t2 = getDigits(y);
                return (t1[1] == t2[t2.Count - 1]
                    && t1[0] == t2[t2.Count - 2]);
            }

            // returns a list of the digits of an integer
            private List<int> getDigits(int num)
            {
                List<int> ret = new List<int>();
                while (num != 0)
                {
                    ret.Add(num % 10);
                    num /= 10;
                }
                return ret;
            }

        }

        static void Main()
        {
            CyclicFigurate theThing = new CyclicFigurate();

            theThing.getSolution();

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // generates all possible re-arrangements of a list of ints
        // and returns them as a list of lists
        static List<List<int>> arrangements(List<int> nums)
        {

            if(nums.Count == 1)
            {
                List<List<int>> ret = new List<List<int>>();
                List<int> onlyList = new List<int>();
                onlyList.Add(nums[0]);
                ret.Add(onlyList);
                return ret;
            }
            else if (nums.Count < 1)
            {
                // it's possible to reach this block in the
                // case that the caller tries to make an arrangement
                // of elements with duplicates!
                List<List<int>> ret = new List<List<int>>();
                List<int> onlyList = new List<int>();
                ret.Add(onlyList);
                // we add a single empty list and return
                return ret;
            }

            List<List<int>> totalArrangements = new List<List<int>>();
            for(int i = 0; i < nums.Count; ++i)
            {
                List<int> t = new List<int>();
                for(int j = 0; j < nums.Count; ++j)
                {
                    if(nums[i] != nums[j])
                    {
                        t.Add(nums[j]);
                    }
                }
                List<List<int>> oneLessElement = arrangements(t);
                foreach(List<int> x in oneLessElement)
                {
                    x.Add(nums[i]);
                    totalArrangements.Add(x);
                }
            }
            return totalArrangements;
        }

        // returns true if at least one re-arrangement
        // of the ints represents a cyclic ordering
        static bool oneArrangementIsCyclic(List<int> nums)
        {
            List<List<int>> t = arrangements(nums);
            foreach(List<int> x in t)
            {
                if (isCyclic(x))
                    return true;
            }
            return false;
        }

        // returns a list of the digits of an integer
        static List<int> getDigits(int num)
        {
            List<int> ret = new List<int>();
            while (num != 0)
            {
                ret.Add(num % 10);
                num /= 10;
            }
            return ret;
        }

        // checks if an array of 4 digit numbers is cyclic
        // in the current order it's in
        // this function makes major assumptions about the input!!!
        static bool isCyclic(List<int> nums)
        {
            for(int i = 0; i < nums.Count; ++i)
            {
                List<int> t1 = getDigits(nums[i % nums.Count]);
                List<int> t2 = getDigits(nums[(i+1) % nums.Count]);
                if(!(t1[t1.Count - 1] == t2[1]
                    && t1[t1.Count - 2] == t2[0]))
                {
                    return false;
                }
            }
            return true;
        }

        
    }
}