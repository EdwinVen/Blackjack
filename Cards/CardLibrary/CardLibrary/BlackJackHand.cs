using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLibrary {
    public class BlackJackHand {

        //Order Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
        private int[] values = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
        private List<Card> cards;

        /// <summary>
        /// Default contsructors
        /// Initializes the cards list
        /// </summary>
        public BlackJackHand() {
            cards = new List<Card>();
        }

        /// <summary>
        /// Adds a card to the Hand
        /// </summary>
        /// <param name="c">Card to be added</param>
        public void Add(Card c) {
            cards.Add(c);
        }

        /// <summary>
        /// Removes all cards from the Hand
        /// </summary>
        public void Clear() {
            cards.Clear();      
        }

        /// <summary>
        /// Matches the cards ranks to their value
        /// </summary>
        /// <returns>Calculated score</returns>
        public int Score() {
            int score = 0;
            foreach (Card c in cards) {
                score += values[(int)c.Rank];
            }
            
            return score;
        }

        public List<Card> GetHand() {
            return cards;         
        }

        /// <summary>
        /// Prints the score and the cards in the hand
        /// </summary>
        public void Show() {
            Console.WriteLine($"Score: {Score()}");
            foreach (Card item in cards) {
                Console.WriteLine(item);
            }
        }
    }
}
