// Euler083.cs
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

namespace Euler084
{

    class Euler084
    {

        static int FACES_ON_DIE = 4;
        static int BOARD_SIZE = 40;
        static string[] TILES = { "GO", "A1", "CC1", "A2", "T1", "R1", "B1", "CH1", "B2", "B3", "JAIL",
                                    "C1", "U1", "C2", "C3", "R2", "D1", "CC2", "D2", "D3", "FP", 
                                    "E1", "CH2", "E2", "E3", "R3", "F1", "F2", "U2", "F3", "G2J",
                                    "G1", "G2", "CC3", "G3", "R4", "CH3", "H1", "T2", "H2"};

        static string[] COMMUNITY_CHEST_CARDS = { "Goto GO", "Goto JAIL", "Do Nothing", "Do Nothing", "Do Nothing",
                                                    "Do Nothing", "Do Nothing", "Do Nothing", "Do Nothing", "Do Nothing",
                                                    "Do Nothing", "Do Nothing", "Do Nothing", "Do Nothing", "Do Nothing",
                                                    "Do Nothing"
                                                };

        static string[] CHANCE_PILE_CARDS = { "Goto GO", "Goto JAIL", "Goto C1", "Goto E3", "Goto H2",
                                                    "Goto R1", "Goto next R", "Goto next R", "Goto next U", "Go back 3 squares",
                                                    "Do Nothing", "Do Nothing", "Do Nothing", "Do Nothing", "Do Nothing",
                                                    "Do Nothing"
                                                };
        static int NUMBER_OF_GAMES = 500000;
        static int NUMBER_OF_ROLLS = 20000;

        static int[] TILE_END_COUNT = new int[BOARD_SIZE];

        static LinkedList<string> COMMUNITY_CHEST = new LinkedList<string>();
        static LinkedList<string> CHANCE_PILE = new LinkedList<string>();

        static void Main()
        {

            // create an simulation of a monopoly board,
            // run 10000 games on the board that each consist of 10000 moves,
            // then record the tile that each game ended on
            // Once recorded, we can approximate, for each tile, the odds
            // that the game will end on that tile

            for(int i = 0; i < NUMBER_OF_GAMES; ++i)
            {
                // init the game piece
                int gamePiece = 0;

                // start a new game by seeding the random class
                Random rndGen = new Random((int)(DateTime.Now.Ticks % int.MaxValue));

                // generate a random order for the community and chance cards
                List<int> communityOrder = new List<int>();
                for(int j = 0; j < COMMUNITY_CHEST_CARDS.Count(); ++j)
                {
                    communityOrder.Add(j);
                }

                List<int> chanceOrder = new List<int>();
                for (int j = 0; j < CHANCE_PILE_CARDS.Count(); ++j)
                {
                    chanceOrder.Add(j);
                }

                // using some code from the internet to quickly randomize the list of numbers
                int n = communityOrder.Count();
                while (n > 1)
                {
                    n--;
                    int k = rndGen.Next(n + 1);
                    int value = communityOrder[k];
                    communityOrder[k] = communityOrder[n];
                    communityOrder[n] = value;
                }

                n = chanceOrder.Count();
                while (n > 1)
                {
                    n--;
                    int k = rndGen.Next(n + 1);
                    int value = chanceOrder[k];
                    chanceOrder[k] = chanceOrder[n];
                    chanceOrder[n] = value;
                }

                // now use the random orders to create the community
                // and chance card piles
                COMMUNITY_CHEST.Clear();
                for(int j = 0; j < communityOrder.Count; ++j)
                {
                    COMMUNITY_CHEST.AddLast(COMMUNITY_CHEST_CARDS[communityOrder[j]]);
                }

                CHANCE_PILE.Clear();
                for (int j = 0; j < chanceOrder.Count; ++j)
                {
                    CHANCE_PILE.AddLast(CHANCE_PILE_CARDS[chanceOrder[j]]);
                }

                // now we start rolling and playing the game
                for(int j = 0; j < NUMBER_OF_ROLLS; ++j)
                {
                    // we also need to track consecutive double rolls of the dice
                    // if we have 3 consecutive rolls of doubles then we need to
                    // move back to JAIL (speeding!)

                    int consecutiveDoubles = 0;
                    int roll1 = rndGen.Next(1, FACES_ON_DIE);
                    int roll2 = rndGen.Next(1, FACES_ON_DIE);
                    if (roll1 == roll2)
                        ++consecutiveDoubles;
                    if (consecutiveDoubles == 3)
                    {
                        consecutiveDoubles = 0;
                        gamePiece = 10;
                    }
                    else
                    {
                        // move to the next tile
                        gamePiece += (roll1 + roll2);
                        gamePiece %= BOARD_SIZE;

                        // we need a looping structure for when the gamepiece
                        // is moved 3 spaces backwards onto another tile that
                        // might cause another movement
                        bool checkLooping = true;
                        while (checkLooping)
                        {
                            // if we land on the CH tile
                            // or the CC tile then we do something differently
                            if (TILES[gamePiece].Equals("CC1")
                                || TILES[gamePiece].Equals("CC2")
                                || TILES[gamePiece].Equals("CC3"))
                            {
                                // pull a card from the community chest pile
                                string nextCard = COMMUNITY_CHEST.First.Value;
                                if (nextCard.Equals("Goto GO"))
                                {
                                    gamePiece = 0;
                                }
                                else if (nextCard.Equals("Goto JAIL"))
                                {
                                    gamePiece = 10;
                                }
                                else if (nextCard.Equals("Do Nothing"))
                                {
                                    // don't do anything!
                                }

                                // place the card at the bottom of the chest pile
                                COMMUNITY_CHEST.RemoveFirst();
                                COMMUNITY_CHEST.AddLast(nextCard);

                            }

                            // set the default to exit the looping structure
                            checkLooping = false;

                            // note, you cannot be on a CC tile as well as a CH
                            // tile, so we can put these statements next to eachother
                            // without an 'else if' structure
                            if (TILES[gamePiece].Equals("CH1")
                                || TILES[gamePiece].Equals("CH2")
                                || TILES[gamePiece].Equals("CH3"))
                            {
                                // pull a card from the chance cards pile
                                string nextCard = CHANCE_PILE.First.Value;
                                switch (nextCard)
                                {
                                    case "Goto GO":
                                        gamePiece = 0;
                                        break;
                                    case "Goto JAIL":
                                        gamePiece = 10;
                                        break;
                                    case "Goto C1":
                                        gamePiece = 11;
                                        break;
                                    case "Goto E3":
                                        gamePiece = 24;
                                        break;
                                    case "Goto H2":
                                        gamePiece = 39;
                                        break;
                                    case "Goto R1":
                                        gamePiece = 5;
                                        break;
                                    case "Goto next R":
                                        if (gamePiece >= 35 || gamePiece <= 4)
                                        {
                                            gamePiece = 5;
                                        }
                                        else if (gamePiece >= 5 && gamePiece <= 14)
                                        {
                                            gamePiece = 15;
                                        }
                                        else if (gamePiece >= 15 && gamePiece <= 24)
                                        {
                                            gamePiece = 25;
                                        }
                                        else if (gamePiece >= 25 && gamePiece <= 34)
                                        {
                                            gamePiece = 35;
                                        }
                                        break;
                                    case "Goto next U":
                                        if (gamePiece >= 28 || gamePiece < 12)
                                        {
                                            gamePiece = 12;
                                        }
                                        else if (gamePiece >= 12 && gamePiece < 28)
                                        {
                                            gamePiece = 28;
                                        }
                                        break;
                                    case "Go back 3 squares":
                                        gamePiece -= 3;
                                        gamePiece %= BOARD_SIZE;
                                        checkLooping = true;
                                        // loop again in case the gamepiece lands
                                        // on a CC tile
                                        break;
                                    default:
                                        // do nothing!
                                        break;

                                }

                                // place the chance card back on the bottom of the pile
                                CHANCE_PILE.RemoveFirst();
                                CHANCE_PILE.AddLast(nextCard);


                            }

                            // again, if we land on G2J then we don't need to
                            // worry about landing on either of the chance / community piles
                            // if the gamePiece lands on the G2J tile then
                            // move the gamePiece to the JAIL tile
                            if (gamePiece == 30)
                                gamePiece = 10;
                    

                        } // end of looping structure

                    } // end else to if (consecutiveDoubles == 3)

                } // end of roll
                
                // after rolling for the whole game
                // we take the tile that the game piece ends on
                // and increment its count
                TILE_END_COUNT[gamePiece]++;


            } // end of game

            // after all the games have been processed, display the
            // approximated chances of ending on each tile using
            // the results of the simulation
            for (int i = 0; i < BOARD_SIZE; ++i)
            {
                Console.WriteLine("Tile Piece " + i + ": %" + ((double)TILE_END_COUNT[i] / NUMBER_OF_GAMES * 100));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}
