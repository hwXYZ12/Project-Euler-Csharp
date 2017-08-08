// Euler089.cs
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

namespace Euler089
{

    class Euler089
    {

        static string FILE_PATH = "C://Users//William//Desktop//Exercise Solutions//exerciseProjectCSharp//exerciseProjectCSharp//p089_roman.txt";
        static int INPUT_SIZE = 1000;

        // returns the value of a roman numeral character
        static int romanNumeralValue(char numeral)
        {
            switch(numeral)
            {
                case 'I':
                    return 1;
                case 'V':
                    return 5;
                case 'X':
                    return 10;
                case 'L':
                    return 50;
                case 'C':
                    return 100;
                case 'D':
                    return 500;
                case 'M':
                    return 1000;
            }
            return -1;
        }

        // determines the value of a number expressed in roman numerals
        static BigInteger convertRomanNumber(string romanNumber)
        {
            // note that, for this entire problem, we have assumed
            // that all the inputs are valid, thus we can ignore error checking
            // and make certain assumptions

            // in the case that the roman numerals at no point ascend in value
            // from symbol to symbol then
            // we can simply sum the value of each numeral
            // but once we find a roman numeral that is larger then we actually
            // subtract the value of the last numeral
            int lastValue = romanNumeralValue(romanNumber[0]);
            BigInteger sum = lastValue;
            for(int i = 1; i < romanNumber.Count(); ++i)
            {
                int currValue = romanNumeralValue(romanNumber[i]);
                if (currValue > lastValue)
                {
                    sum -= 2*lastValue; // last value is meant to be subtracted, not added, so we subtract it twice
                }
                sum += currValue;
                lastValue = currValue;
            }

            return sum;
        }

        // determines the minimum number of symbols required to express 'num'
        // using roman numerals
        static BigInteger minRomanSymbols(BigInteger num)
        {
            // It may be possible to show that the minimum number of symbols required to express 
            // a number in roman numerals follows a relatively simple pattern....
            // If you are to break up the decimal expansion of the number in question, for example
            // the number 49, if you can express 40 and 9 in their minimal forms and then append
            // them respectively then you'll get the minimal representation of 49. Why is this? I'm
            // not entirely certain but it seems to be true.
            // It's more or less obviously true when you consider numbers for which the minimal representation
            // doesn't have any subtractive pairs, since all of the symbols in such representations will
            // only exist from left to right in descending order of value
            // Ok, so what about the numbers that DO include subtractive pairs? Well, one approach to
            // proving this is to prove it for all numbers from 1 to 999. This can be done directly, case by
            // case, or with some more general reasoning. The reason we only need to prove it for 1 to 999 is that all the number
            // representations greater than or equal to 1000 simply append 'M's to the left side since the
            // roman numeral system doesn't have any symbols for things greater than 1000.
            // Alright, so let's try to prove it. Suppose that this approach will NOT result in a minimal
            // representation. Note that this number is between 1 and 999, as described above. We can then represent
            // this number as a1 + a2 + a3 where 900 >= a1 >= 100 is a multiple of 100, 90 >= a2 >= 10 is a multiple of 10, and 9 >= a1 >= 1.
            // We assume that the minimal representation is NOT the concatenation of the minimal representation of each these numbers. This
            // implies that either concatenating the representations of (a1 + a2) and a3, a1 + (a2 + a3), or (a1 + a2 + a3) would result in
            // a smaller number of symbols. I believe that it can be shown case by case that each of these three cases will result in a contradiction
            // due to each of the rules as the rules make it impossible for a smaller combination of symbols to result in either of the cases, one case
            // in particular if you consider 999, you could have IM, except that combination has been ruled out. Put another way, each of the rules
            // require that at least one symbol exist for each non-zero value of a1, a2, and a3 and therefore the other possible combinations
            // are ruled out.

            // take care of all the 'M's at the left of the representation
            BigInteger ret = 0;
            ret += (num / 1000);
            num %= 1000;

            // now break up the remaining number into 3 parts
            int a1, a2, a3;
            a1 = (int) (num / 100) * 100;
            num %= 100;
            a2 = (int) (num / 10) * 10;
            num %= 10;
            a3 = (int) num;

            // find the minimal number of symbols needed for each of a1, a2, and a3     
            int x = 0;
            switch(a1)
            {
                case 900:
                    x = 2;
                    break;
                case 800:
                    x = 4;
                    break;
                case 700:
                    x = 3;
                    break;
                case 600:
                    x = 2;
                    break;
                case 500:
                    x = 1;
                    break;
                case 400:
                    x = 2;
                    break;
                case 300:
                    x = 3;
                    break;
                case 200:
                    x = 2;
                    break;
                case 100:
                    x = 1;
                    break;
            }
            ret += x;
                        
            x = 0;
            switch (a2)
            {
                case 90:
                    x = 2;
                    break;
                case 80:
                    x = 4;
                    break;
                case 70:
                    x = 3;
                    break;
                case 60:
                    x = 2;
                    break;
                case 50:
                    x = 1;
                    break;
                case 40:
                    x = 2;
                    break;
                case 30:
                    x = 3;
                    break;
                case 20:
                    x = 2;
                    break;
                case 10:
                    x = 1;
                    break;
            }
            ret += x;

            x = 0;
            switch (a3)
            {
                case 9:
                    x = 2;
                    break;
                case 8:
                    x = 4;
                    break;
                case 7:
                    x = 3;
                    break;
                case 6:
                    x = 2;
                    break;
                case 5:
                    x = 1;
                    break;
                case 4:
                    x = 2;
                    break;
                case 3:
                    x = 3;
                    break;
                case 2:
                    x = 2;
                    break;
                case 1:
                    x = 1;
                    break;
            }
            ret += x;

            return ret;
        }

        // takes a roman numeral expression and determines it's value
        // once its value is determined, the minimal number of symbols needed to
        // express the same number is determined. This function then returns the
        // difference between the number of symbols given in the original expression
        // and the number of symbols required by the minimal expression.
        static BigInteger diffRomanNumerals(string romanNumber)
        {
            return romanNumber.Count() - minRomanSymbols(convertRomanNumber(romanNumber));
        }

        static void Main()
        {

            string input = System.IO.File.ReadAllText(FILE_PATH);
            BigInteger count = TextHandling<int>.getSumOfOperationResultsOnInput(input, INPUT_SIZE, '\n', diffRomanNumerals);

            Console.WriteLine(count);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
