// Euler054.cs
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
namespace Euler054
{
    

    class Euler054
    {

        private class Card
        {
            char value, suit;

            public Card(char value, char suit)
            {
                this.value = value;
                this.suit = suit;
            }

            public bool Equals(Card c)
            {
                return this.value == c.value
                    && this.suit == c.suit;
            }

            public char getValue()
            {
                return value;
            }

            public int getRelativeValue()
            {
                switch(value)
                {
                    case 'A':
                        return 1;                        
                    case '2':
                        return 2;
                    case '3':
                        return 3;
                    case '4':
                        return 4;
                    case '5':
                        return 5;
                    case '6':
                        return 6;
                    case '7':
                        return 7;
                    case '8':
                        return 8;
                    case '9':
                        return 9;
                    case 'T':
                        return 10;
                    case 'J':
                        return 11;
                    case 'Q':
                        return 12;
                    case 'K':
                        return 13;
                }
                return -1;
            }

            public int getRelativeValueAceHigh()
            {
                if (getRelativeValue() == 1)
                    return 14;
                else
                    return getRelativeValue();
            }

            public char getSuit()
            {
                return suit;
            }
        }

        private class Hand
        {
            private class Pair
            {
                public Card a, b;

                public Pair(Card a, Card b)
                {
                    this.a = a;
                    this.b = b;
                }
            }

            private List<Card> cards = new List<Card>();

            public Hand(Card[] cards)
            {
                for(int i = 0; i < 5; ++i)
                {
                    this.cards.Add(cards[i]);
                }
            }

            private bool hasCardWithValue(char value)
            {
                for(int i = 0; i < 5; ++i)
                {
                    if (cards[i].getValue() == value)
                        return true;
                }
                return false;
            }

            private bool hasCardWithRelativeValue(int value)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (cards[i].getRelativeValue() == value)
                        return true;
                }
                return false;
            }

            private bool hasCardWithSuit(char suit)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (cards[i].getSuit() == suit)
                        return true;
                }
                return false;
            }

            private bool hasCard(char value, char suit)
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (cards[i].getValue() == value
                        && cards[i].getSuit() == suit)
                        return true;
                }
                return false;
            }

            private bool isFlush()
            {
                for (int i = 0; i < 5; ++i)
                {
                    if (cards[0].getSuit() != cards[i].getSuit())
                        return false;
                }
                return true;
            }

            private bool isStraight(out int highRelativeValue)
            {
                // find the lowest non-ace card
                int low = 13;
                bool hasAce = false;
                for (int i = 0; i < 5; ++i)
                {
                    if (cards[i].getRelativeValue() != 1 && cards[i].getRelativeValue() < low)
                    {
                        low = cards[i].getRelativeValue();
                    }
                    else if (cards[i].getRelativeValue() == 1)
                        hasAce = true;
                }

                if (!hasAce)
                {
                    highRelativeValue = low + 4;
                    return (hasCardWithRelativeValue(low)
                            && hasCardWithRelativeValue(low + 1)
                            && hasCardWithRelativeValue(low + 2)
                            && hasCardWithRelativeValue(low + 3)
                            && hasCardWithRelativeValue(low + 4));
                }

                // has an ace in hand
                if (hasCardWithRelativeValue(1)
                            && hasCardWithRelativeValue(2)
                            && hasCardWithRelativeValue(3)
                            && hasCardWithRelativeValue(4)
                            && hasCardWithRelativeValue(5))
                {
                    highRelativeValue = 5;
                    return true;
                }                
                else if ((hasCardWithRelativeValue(10)
                          && hasCardWithRelativeValue(11)
                          && hasCardWithRelativeValue(12)
                          && hasCardWithRelativeValue(13)
                          && hasCardWithRelativeValue(14)))
                {
                    // note that we count a royal flush as a flush!
                    highRelativeValue = 14;
                    return true;
                }
                highRelativeValue = -1;
                return false;
            }

            private bool isStraightFlush(out int straightValue)
            {
                int x = 0;
                if(isFlush() && isStraight(out x))
                {
                    straightValue = x;
                    return true;
                }
                straightValue = x;
                return false;
            }

            private bool isRoyalFlush()
            {
                if (!isFlush())
                    return false;

                return (hasCardWithValue('T')
                        && hasCardWithValue('J')
                        && hasCardWithValue('Q')
                        && hasCardWithValue('K')
                        && hasCardWithValue('A'));
            }

            private bool hasPair(out Pair p)
            {
                for(int i=0; i < 5; ++i)
                {
                    for(int j=i+1; j < 5; ++j)
                    {
                        if (cards[i].getRelativeValue() == cards[j].getRelativeValue())
                        {
                            p = new Pair(cards[i], cards[j]);
                            return true;
                        }                            
                    }
                }
                p = null;
                return false;
            }

            private bool hasMultiplePairs(out List<Pair> pairs)
            {
                pairs = new List<Pair>();
                for (int i = 0; i < 5; ++i)
                {
                    for (int j = i+1; j < 5; ++j)
                    {
                        if (cards[i].getRelativeValue() == cards[j].getRelativeValue())
                        {
                            // ensure that part of the pair hasn't already
                            // been recorded
                            bool check = false;
                            for (int k = 0; k < pairs.Count; ++k )
                            {
                                if(pairs[k].a.Equals(cards[i])
                                    || pairs[k].a.Equals(cards[j])
                                    || pairs[k].b.Equals(cards[i])
                                    || pairs[k].b.Equals(cards[j]))
                                {
                                    check = true;
                                }
                            }
                            if(!check)
                                pairs.Add(new Pair(cards[i], cards[j]));
                        }
                    }
                }
                if (pairs.Count < 2)
                    return false;
                else 
                    return true;
            }


            // both of the following routines are hard-coded
            // for 5 card hands. I realize this is not ideal, but
            // for our purposes it will work OK.

            private bool threeOfAKind(out int relativeValue)
            {
                int[] count = new int[14];
                for(int i=0; i<14;++i)
                {
                    count[i] = 0;
                }
                for(int i=0; i<5;++i)
                {
                    if(++count[cards[i].getRelativeValue()] == 3)
                    {
                        relativeValue = cards[i].getRelativeValueAceHigh();
                        return true;
                    }
                }
                relativeValue = -1;
                return false;
            }

            private bool fourOfAKind(out int relativeValue)
            {
                int[] count = new int[14];
                for (int i = 0; i < 14; ++i)
                {
                    count[i] = 0;
                }
                for (int i = 0; i < 5; ++i)
                {
                    if (++count[cards[i].getRelativeValue()] == 4)
                    {
                        relativeValue = cards[i].getRelativeValueAceHigh();
                        return true;
                    }
                }
                relativeValue = -1;
                return false;
            }

            private bool fullHouse(out int threeValue, out int pairValue)
            {
                int x;
                // due to how 'pair' and 'threeOfAKind' were
                // programmed we can't simply use
                // if (threeOfAKind(out x) && hasPair(out p))
                // where p is a Pair variable
                if(threeOfAKind(out x))
                {
                    // check if there is also a pair
                    threeValue = x;
                    bool[] check = new bool[15];
                    for (int i = 0; i < 15;++i )
                    {
                        check[i] = false;
                    }
                    for (int i = 0; i < 5; ++i )
                    {
                        if (cards[i].getRelativeValueAceHigh() != threeValue)
                        {
                            if (check[cards[i].getRelativeValueAceHigh()])
                            {
                                pairValue = cards[i].getRelativeValueAceHigh();
                                return true;
                            }
                            else
                                check[cards[i].getRelativeValueAceHigh()] = true;
                        }
                    }
                }
                pairValue = -1;
                threeValue = -1;
                return false;
            }

            private int getRelativeHandRank()
            {
                int temp = 0, temp2 = 0;
                Pair tempPair = null;
                List<Pair> tempPairs = null;
                if (isRoyalFlush())
                {
                    return 10;
                }
                else if (isStraightFlush(out temp))
                    return 9;
                else if(fourOfAKind(out temp))
                    return 8;
                else if(fullHouse(out temp, out temp2))
                    return 7;
                else if(isFlush())
                    return 6;
                else if(isStraight(out temp))
                    return 5;
                else if(threeOfAKind(out temp))
                    return 4;
                else if(hasMultiplePairs(out tempPairs))
                    return 3;
                else if(hasPair(out tempPair))
                    return 2;
                else
                    return 1;
            }


            // when two hands are the same relative rank AND also
            // the same value, we defer to comparing the highest
            // card(s) in each hand
            private bool highestCardWins(Hand a)
            {
                // sort both hands by value
                // and compare card-by-card starting
                // with the highest value cards
                // If both hands have the same values,
                // return true by default
                IEnumerable<Card> query1 = cards.OrderByDescending(card => card.getRelativeValueAceHigh());
                IEnumerable<Card> query2 = a.cards.OrderByDescending(card => card.getRelativeValueAceHigh());
                List<Card> l1 = new List<Card>();
                List<Card> l2 = new List<Card>();
                foreach (Card c in query1)
                {
                    l1.Add(c);
                }
                foreach (Card c in query2)
                {
                    l2.Add(c);
                }
                for (int i = 0; i < 5; ++i)
                {
                    // ace counts as high!
                    if (l1[i].getRelativeValueAceHigh() > l2[i].getRelativeValueAceHigh())
                    {
                        return true;
                    }
                    else if (l1[i].getRelativeValueAceHigh() < l2[i].getRelativeValueAceHigh())
                    {
                        return false;
                    }
                }
                return true;
            }

            // returns true if this hand is of higher or equal value than hand a
            // assuming both are the same relative hand rank
            private bool higherOrEqualValueHand(Hand a, int whichRelativeRank)
            {
                // handles 10 different possible hand rankings
                switch(whichRelativeRank)
                {
                    case 1:
                        return highestCardWins(a);
                    case 2:
                        Pair p1, p2;
                        hasPair(out p1);
                        a.hasPair(out p2);
                        if (p1.a.getRelativeValueAceHigh() > p2.a.getRelativeValueAceHigh())
                            return true;
                        else if (p1.a.getRelativeValueAceHigh() < p2.a.getRelativeValueAceHigh())
                            return false;
                        else
                        {
                            // we have the same pairs, we now compare highest card
                            return highestCardWins(a);
                        }
                    case 3:
                        // sort both sets of pairs
                        // and compares pair-by-pair
                        // with the highest value pairs
                        // If both pairs have the same values,
                        // we defer to 'highest card wins'
                        List<Pair> pairs1, pairs2;
                        hasMultiplePairs(out pairs1);
                        a.hasMultiplePairs(out pairs2);
                        IEnumerable<Pair> query1 = pairs1.OrderByDescending(p => p.a.getRelativeValueAceHigh());
                        IEnumerable<Pair> query2 = pairs2.OrderByDescending(p => p.a.getRelativeValueAceHigh());
                        List<Pair> o1 = new List<Pair>();
                        List<Pair> o2 = new List<Pair>();
                        foreach (Pair p in query1)
                        {
                            o1.Add(p);
                        }
                        foreach (Pair p in query2)
                        {
                            o2.Add(p);
                        }
                        for (int i = 0; i < 2; ++i)
                        {
                            if (o1[i].a.getRelativeValueAceHigh() > o2[i].a.getRelativeValueAceHigh())
                                return true;
                            else if (o1[i].a.getRelativeValueAceHigh() < o2[i].a.getRelativeValueAceHigh())
                                return false;
                        }
                        return highestCardWins(a);
                    case 4:
                        int v1, v2;
                        threeOfAKind(out v1);
                        a.threeOfAKind(out v2);
                        if (v1 > v2)
                            return true;
                        else if (v1 < v2)
                            return false;
                        return highestCardWins(a);
                    case 5:
                        int x1, x2;
                        isStraight(out x1);
                        a.isStraight(out x2);
                        if (x1 > x2)
                            return true;
                        else if (x1 < x2)
                            return false;
                        return highestCardWins(a);
                    case 6:
                        // in the case of both players having a flush,
                        // we automatically defer to highest card
                        // since it is the same as checking
                        // the highest cards of the flush against the
                        // highest cards of the other flush
                        return highestCardWins(a);
                    case 7:
                        // when comparing full houses, we compare the
                        // three of a kind and defer to the pair
                        // if neither win, then the hands tie
                        int first3, second3, firstPair, secondPair;
                        fullHouse(out first3, out firstPair);
                        a.fullHouse(out second3, out secondPair);

                        if (first3 > second3)
                            return true;
                        else if (first3 < second3)
                            return false;
                        else
                        {
                            // defer to pair values
                            if (firstPair > secondPair)
                                return true;
                            else if(firstPair < secondPair)
                                return false;
                        }
                        // the hands are equal
                        return true;
                    case 8:
                        // two four of a kind hands
                        int val1, val2;
                        fourOfAKind(out val1);
                        a.fourOfAKind(out val2);
                        if(val1 > val2)
                            return true;
                        else if(val1 > val2)
                            return false;
                        else
                            return highestCardWins(a);
                    case 9:
                        // comparing straight flushes
                        // straight flushes are curious in the sense that
                        // both a flush and a straight comparison can be deduced
                        // to a highest card check
                        int s1, s2;
                        isStraightFlush(out s1);
                        a.isStraightFlush(out s2);
                        return (s1 >= s2);
                    case 10:
                        // comparing two royal flushes result in a tie
                        return true;
                    default:
                        return true;
                }
            }

            public static bool operator>=(Hand a, Hand b)
            {
                if (a.getRelativeHandRank() > b.getRelativeHandRank())
                    return true;
                else if (a.getRelativeHandRank() == b.getRelativeHandRank())
                    return a.higherOrEqualValueHand(b, a.getRelativeHandRank());
                else
                    return false;
            }

            public static bool operator<(Hand a, Hand b)
            {
                return !(a >= b);
            }

            public static bool operator ==(Hand a, Hand b)
            {
                return (a >= b) && (b >= a);
            }

            public static bool operator <=(Hand a, Hand b)
            {
                return (b >= a);
            }

            public static bool operator >(Hand a, Hand b)
            {
                return !(a <= b);
            }

            public static bool operator !=(Hand a, Hand b)
            {
                return !(a == b);
            }


        }

        static void Main()
        {
            
            // parse the poker text file
            string filePath = "C:\\Users\\William\\Desktop\\Exercise Solutions"
                                +"\\exerciseProjectCSharp\\exerciseProjectCSharp\\"
                                +"p054_poker.txt";
            string textToParse = System.IO.File.ReadAllText(filePath);
            int twoHands = 10, answer = 0;
            string card = "";
            Hand h1, h2;
            Card[] cards1 = new Card[5];
            Card[] cards2 = new Card[5];
            int cardCount1 = 0, cardCount2 = 0;
            while(true)
            {
                if(twoHands > 0)
                {
                    if (textToParse.Equals(""))
                    {
                        break;
                    }
                    else
                    {
                        card = textToParse.Substring(0, 2);
                        char[] c = card.ToCharArray();
                        if(twoHands > 5)
                        {
                            cards1[cardCount1] = new Card(c[0], c[1]);
                            ++cardCount1;
                        }
                        else
                        {
                            cards2[cardCount2] = new Card(c[0], c[1]);
                            ++cardCount2;
                        }
                        textToParse = textToParse.Substring(3); // removes indiced 0, 1 and 2
                        --twoHands;
                    }                    
                }
                else
                {
                    h1 = new Hand(cards1);
                    h2 = new Hand(cards2);
                    if (h1 > h2)
                    {
                        ++answer;
                        Console.WriteLine("Player 1 wins");
                    }
                    else
                    {
                        Console.WriteLine("Player 2 wins");
                    }
                    twoHands = 10;
                    cardCount1 = 0;
                    cardCount2 = 0;
                }
            }

            // Show answer
            Console.WriteLine("Answer: "+answer);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}