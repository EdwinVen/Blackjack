using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary {
    public class Deck {

        private const int DECK_SIZE = 52;
        //public enum RankType { Ace = 11, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 10, Queen = 10, King = 10 };
        //public enum SuitType { Hearts, Diamond, Spades, Clubs };

        private Card[] cards;
        private bool[] drawn;

        /// <summary>
        /// Default constructor
        /// Initializes and generates a Deck
        /// </summary>
        public Deck() {
            cards = new Card[DECK_SIZE];
            drawn = new bool[DECK_SIZE];
            //Combine ranks and suits to fill the deck
            int i = 0;

            foreach(string rank in Enum.GetNames(typeof(RankType))) {
                foreach(string suit in Enum.GetNames(typeof(SuitType))) {
                    cards[i] = new Card((RankType)Enum.Parse(typeof(RankType), rank), (SuitType)Enum.Parse(typeof(SuitType),suit));
                    cards[i].Pic = CardPics.CardPics.GetBitmapImageResource($"CardPics.CardPics.{GetCardValueForImage(cards[i]).Item2}-{GetCardValueForImage(cards[i]).Item1}.png");
                    drawn[i] = false;
                    i++;
                }
            }
        }

        /// <summary>
        /// Match the values of a card to the equivalent on the CardPics image file name
        /// So "CardPics.CardPics.Spade-A.png" can be generated as $"CardPics.CardPics.{Item2}-{Item1}.png"
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Pair of values to generate the filename of the Card Image</returns>
        private Tuple<string, string> GetCardValueForImage(Card c) {
            if (c != null) {
                string rank, suit;

                // Convert the Suit
                // Remove the 's' at the end 
                // Check the SuitType enum definition
                suit = c.Suit.ToString().TrimEnd('s');    
                
                // Convert the Rank
                
                if(c.Rank > RankType.Ace && c.Rank <= RankType.Ten) {
                    rank = Convert.ToString( (int)c.Rank + 1);
                }
                else {
                    rank = c.Rank.ToString().Remove(1);
                }

                return new Tuple<string, string>(rank, suit);
            }
            return null;
        }

        /// <summary>
        /// Sets all the cards to not drawn
        /// </summary>
        public void Shuffle() {
            for (int i = 0; i < DECK_SIZE; i++) {
                drawn[i] = false;
            }
        }

        /// <summary>
        /// Chooses a random card from the deck
        /// Note: If all cards are drawn the loop will be infinite
        /// </summary>
        /// <returns>Random Card from the deck</returns>
        public Card Draw() {
            Random r = new Random();
            int n;
            while (true) {
                n = r.Next(DECK_SIZE);
                if (!drawn[n]) {
                    drawn[n] = true;
                    return cards[n];
                }
            }
        }

        /// <summary>
        /// ToString() overrride
        /// </summary>
        /// <returns>One card per line</returns>
        override
        public string ToString() {
            string s = "";

            for (int i = 0; i < DECK_SIZE; i++) {
                s += $"{i}. {cards[i].ToString()} Drawn: {(drawn[i] ? "True" : "False")}\n";
            }
            return s;
        }

    }
}
