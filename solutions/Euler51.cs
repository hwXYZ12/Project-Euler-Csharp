// Euler051.cs
using System;
using System.Collections.Generic;
using System.Numerics;
namespace Euler051
{
    class Euler051
    {

        //private class CycleInteger
        //{
            
        //    private class CycleMask
        //    {
        //        private List<bool> rotating = new List<bool>();

        //        public CycleMask(int n)
        //        {
        //            for (int i = 0; i < n; ++i)
        //            {
        //                rotating.Add(false);
        //            }
        //        }


        //        public CycleMask(List<bool> bools)
        //        {
        //            for(int i = 0; i < bools.Count; ++i)
        //            {
        //                if(i < rotating.Count)
        //                {
        //                    rotating[i] = bools[i];
        //                }
        //                else
        //                {
        //                    rotating.Add(bools[i]);
        //                }
        //            }
        //        }

        //        public void setMaskToEmpty()
        //        {
        //            for(int i = 0; i < rotating.Count; ++i)
        //            {
        //                rotating[i] = false;
        //            }
        //        }

        //        public void increaseSize()
        //        {
        //            rotating.Add(false);
        //        }

        //        public void increment()
        //        {
        //            incrementRecurse(0);
        //        }

        //        public bool getVal(int n)
        //        {
        //            return rotating[n];
        //        }

        //        public int getSize()
        //        {
        //            return rotating.Count;
        //        }

        //        private void incrementRecurse(int which)
        //        {
        //            if(which >= rotating.Count)
        //            {
        //                rotating.Add(true);
        //                return;
        //            }

        //            if(rotating[which])
        //            {
        //                rotating[which] = false;
        //                incrementRecurse(which + 1);
        //            }
        //            else
        //            {
        //                rotating[which] = true;                    
        //            }
        //        }

        //        public CycleMask getNegation()
        //        {
        //            List<bool> ret = new List<bool>();
        //            for(int i = 0; i < rotating.Count; ++i)
        //            {
        //                ret[i] = !rotating[i];
        //            }
        //            return new CycleMask(ret);
        //        }

        //        public bool isFullMask()
        //        {
        //            for(int i = 0; i < rotating.Count; ++i)
        //            {
        //                if (!rotating[i])
        //                    return false;
        //            }
        //            return true;
        //        }

        //        public bool isEmptyMask()
        //        {
        //            for (int i = 0; i < rotating.Count; ++i)
        //            {
        //                if (rotating[i])
        //                    return false;
        //            }
        //            return true;
        //        }
        //    }

            
        //    private List<int> digits = new List<int>();
        //    private CycleMask mask;

        //    public CycleInteger(int n)
        //    {
        //        int temp = n;
        //        while(temp != 0)
        //        {
        //            digits.Add(temp % 10);
        //            temp /= 10;
        //        }
        //        mask = new CycleMask(digits.Count);
        //        mask.increment(); // doesn't begin at zero
        //    }

        //    public void increment()
        //    {
        //        for(int i = 0; i < digits.Count; ++i)
        //        {
        //            if(mask.getVal(i))
        //                if(digits[i] < 9)
        //                {
        //                    ++digits[i];
        //                }
        //                else
        //                {
        //                    // marks the end of a cycle
        //                    for (int j = 0; j < digits.Count; ++j)
        //                    {
        //                        if (mask.getVal(j))
        //                        {
        //                            digits[j] = 0;
        //                        }
        //                    }
        //                    nextCycle();
        //                    break;
        //                }
        //        }
        //    }

        //    private void nextCycle()
        //    {
        //        nextCycleRecurse(0);
        //    }

        //    public void nextCycleRecurse(int whichDigit)
        //    {
        //        // we increment every digit for which the cycle mask
        //        // is true and we don't increment the digits for which the
        //        // cycle mask is false
        //        // Once we have "cycled" through all 9 digits, we then must
        //        // set all of the mask digits back to 0 and increment the
        //        // unmasked digits. This is the start of a new "cycle".
        //        // Note that when we increment the unmasked digits, we skip over
        //        // the masked digits!
        //        if(whichDigit >= digits.Count)
        //        {
        //            // this is the case that we carry over
        //            // past the digits in the integer
        //            // Here, we check if we need to either
        //            // increment the mask or add another digit AND
        //            // increment the mask
        //            mask.increment();
        //            if (mask.isFullMask()) // we skip the full mask since it is of no use to us!
        //            {
        //                mask.increaseSize();
        //                mask.setMaskToEmpty();
        //                mask.increment();
        //                Console.WriteLine("Mask has been filled!");
        //            }

        //            if(mask.getSize() > digits.Count)
        //            {
        //                // increment the mask as usual, but here
        //                // we also must append another digit to the
        //                // integer
        //                // We assume here that the incremented mask will also
        //                // add another mask bit internally
        //                digits.Add(1);
        //            }
        //            else
        //            {
        //                // this is the case that we DID NOT
        //                // overextend and need an extra digit,
        //                // more clearly this is the case in which
        //                // we reset the previous digit to 1 and only
        //                // increment the mask
        //                digits[digits.Count - 1] = 1;
        //            }
        //            return;
        //        }

        //        if(!mask.getVal(whichDigit)) // skip masked values
        //        {
        //            if (digits[whichDigit] < 9)
        //                ++digits[whichDigit];
        //            else
        //            {
        //                // carry over
        //                digits[whichDigit] = 0;
        //                nextCycleRecurse(whichDigit + 1);
        //            }

        //        }
        //        else
        //        {
        //            nextCycleRecurse(whichDigit + 1);
        //        }
        //    }

        //    public int getInt()
        //    {
        //        int ret = 0;
        //        for(int i = 0; i < digits.Count; ++i)
        //        {
        //            ret += digits[i] * Convert.ToInt32(Math.Pow(10, i));
        //        }
        //        return ret;
        //    }
        //}

        private class Family
        {
            private List<int> members = new List<int>();
            private List<int> checkDigits = new List<int>();

            public Family(List<int> format)
            {
                for (int i = 0; i < format.Count; ++i)
                {
                    checkDigits.Add(format[i]);
                }
            }

            public bool Add(int x)
            {
                if (isMember(x))
                {
                    members.Add(x);
                    return true;
                }
                return false;
            }

            public int getSize()
            {
                return members.Count;
            }

            public int getMaskSize()
            {
                return checkDigits.Count;
            }

            public int getMember(int i)
            {
                return members[i];
            }

            private bool isMember(int x)
            {
                List<int> digits = new List<int>();
                while (x != 0)
                {
                    digits.Add(x % 10);
                    x /= 10;
                }
                if (digits.Count != checkDigits.Count)
                    return false;

                // make sure the independent digits follow the family format
                int k = 0;
                for (int i = 0; i < digits.Count; ++i)
                {
                    if (checkDigits[i] != -1 && checkDigits[i] != digits[i])
                        return false;
                    else if (checkDigits[i] == -1)
                    {
                        // get the dependent digit value
                        k = digits[i];
                    }
                }

                // make sure that all of the dependent digits are the same
                for (int i = 0; i < digits.Count; ++i)
                {
                    if (checkDigits[i] == -1 && digits[i] != k)
                        return false;
                }

                // if all the above conditions are passed then
                // we can affirm that the number conforms to the family
                // condition and is a member of it
                return true;
            }
        }

        private class FamilyFactory
        {
            
            private List<int> workingFormat = new List<int>();
            private List<bool> dependencyMask = new List<bool>();

            public FamilyFactory()
            {
                // start with format 0*
                workingFormat.Add(-1);
                workingFormat.Add(0);

                // start the mask at 01
                dependencyMask.Add(true);
                dependencyMask.Add(false);

                // we start the mask at 01 but the format
                // at 0* since the first call to next family
                // will automatically return the next format,
                // that is 1* (note that the mask still
                // ought to be initialized as 01!)
            }

            public Family getNextFamily()
            {
                return new Family(nextFormat());
            }

            private List<int> nextFormat()
            {
                nextFormatRecurse(0);
                return workingFormat;
            }

            private void getNextMask()
            {
                maskRecurse(0);
            }

            private bool fullMask()
            {
                for(int i = 0; i < dependencyMask.Count; ++i)
                {
                    if (!dependencyMask[i])
                        return false;
                }
                return true;
            }

            private void maskRecurse(int whichBit)
            {
                if (!dependencyMask[whichBit])
                {
                    dependencyMask[whichBit] = true;

                    // we have no need for full or empty masks
                    // when the mask is full, we add another digit and
                    // reset all of the bits to false except the last one
                    // in order to skip an all zero mask
                    if (fullMask())
                    {
                        for(int i = 0; i<dependencyMask.Count;++i)
                        {
                            dependencyMask[i] = false;
                        }
                        dependencyMask.Add(false);
                        dependencyMask[0] = true;
                    }

                }                   
                else
                {
                    // reset the bit to 0 and increment the next bit
                    dependencyMask[whichBit] = false;
                    maskRecurse(whichBit + 1);
                }
            }

            private void nextFormatRecurse(int x)
            {
                if(x >= workingFormat.Count)
                {
                    // here we either must add an additional
                    // digit and change the dependency digits
                    // or simply change the dependency digits

                    // the dependency digit pattern follows a binary
                    // pattern, that is the first is 01, the next is
                    // 01 (we exclude 00 and 11 as they aren't useful)
                    // and become 001, 010, 011, 100, 101, 110 and so on....
                    // we want a call here that increments the pattern and will
                    // cause an additional digit to be appended when the pattern
                    // adds another digit
                    getNextMask();

                    // now we apply the new mask to our digit format
                    // in the case that we need an extra digit, we will handle that
                    // as well
                    for (int i = 0; i < workingFormat.Count; ++i)
                    {
                        if (dependencyMask[i])
                            workingFormat[i] = -1;
                        else
                        {
                            workingFormat[i] = 0;
                        }
                    }

                    if(dependencyMask.Count > workingFormat.Count)
                    {
                        // must add another digit to our format
                        workingFormat.Add(1);
                    }

                    // ensure that the last digit of the format is either a dependent
                    // digit or at least a 1
                    if (workingFormat[workingFormat.Count - 1] != -1)
                        workingFormat[workingFormat.Count - 1] = 1;
                    
                    return;
                }

                // skip the dependent digits, for now
                if(workingFormat[x] == -1 )
                {
                    nextFormatRecurse(x + 1);
                    return;
                }

                if (workingFormat[x] < 9)
                    ++workingFormat[x];
                else if(workingFormat[x] == 9)
                {
                    // reset and increment the next digit
                    workingFormat[x] = 0;
                    nextFormatRecurse(x + 1);
                }
            }
        }

        static void Main()
        {            
            int search = 6, searchValue = Convert.ToInt32(Math.Pow(10, search));
            List<int> primes = generatePrimes(searchValue);            
            FamilyFactory creator = new FamilyFactory();
            List<Family> families = new List<Family>();
            for (int i = 0; i < searchValue; ++i)
            {
                families.Add(creator.getNextFamily());
                for (int j = 0; j < primes.Count; ++j)
                {
                    families[i].Add(primes[j]);
                    if (families[i].getSize() == 8)
                    {
                        i = searchValue;
                        break;
                    }
                }
            }

            // get the first prime family that has 8 values in it
            int find = 0;
            for (int i = 0; i < families.Count; ++i )
            {
                if(families[i].getSize() == 8)
                {
                    find = i;
                    break;
                }
            }


            Console.WriteLine("Number of digits: " + families[families.Count - 1].getMaskSize());
            for (int i = 0; i < families[find].getSize(); ++i )
            {
                Console.WriteLine(families[find].getMember(i));
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        // determines if a number is prime given a list of
        // primes less than or equal to the square root of the 
        // number in question
        // Note that this code makes some assumptions about its inputs!
        static bool isPrime(List<int> primes, int num)
        {
            if (num <= 1)
                return false;

            if (primes.Contains(num))
                return true;

            foreach (int x in primes)
            {
                if (num % x == 0)
                    return false;
            }
            return true;
        }

        // prime computation via Sieve of Eratosthenes
        static List<int> generatePrimes(int n)
        {
            List<int> ret = new List<int>();
            int prime = 2;
            ret.Add(2);

            // populate the sieve
            for (int i = 3; i <= n; i += 2)
            {
                ret.Add(i);
            }

            while (prime != ret[ret.Count - 1])
            {
                // get next prime to work with
                // (smallest number still in the sieve such
                // that it is greater than the previous prime that
                // had been used)
                // the loop terminates when the next such prime doesn't exist,
                // that is the last used prime is also the last element in the list
                for (int i = 0; i < ret.Count; ++i)
                {
                    if (ret[i] > prime)
                    {
                        prime = ret[i];
                        break;
                    }
                }

                // perform the sieve: remove all the elements that
                // are multiples of the chosen prime
                int j = 2;
                int max = ret[0];
                for (int i = 0; i < ret.Count; ++i)
                {
                    if (max < ret[i])
                        max = ret[i];
                }
                while (prime * j <= max)
                {
                    ret.Remove(prime * j);
                    ++j;
                }
            }

            return ret;
        }
    }
}