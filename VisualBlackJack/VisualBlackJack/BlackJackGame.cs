using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Collections.Generic;

namespace VisualBlackJack {
    class BlackJackGame {
        public event EventHandler<GameFinishedArgs> GameFinished;

        private Deck deck;
        private BlackJackHand player;
        private BlackJackHand house;

        /// <summary>
        /// Initialize deck, hands and start drawing
        /// </summary>
        public BlackJackGame() {
            deck = new Deck();
            player = new BlackJackHand();
            house = new BlackJackHand();

            player.Add(deck.Draw());
            player.Add(deck.Draw());
        }

        /// <summary>
        /// Return readonly list of cards in player hand
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<Card> GetPlayerHand() {
            return player.GetHand().AsReadOnly();
        }


        /// <summary>
        /// return readonly list of cards in house hand
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<Card> GetHouseHand() {
            return house.GetHand().AsReadOnly();
        }

        /// <summary>
        /// Resets the game and begin a new one
        /// </summary>
        public void NewGame() {
            player.Clear();
            house.Clear();
            deck.Shuffle();

            player.Add(deck.Draw());
            player.Add(deck.Draw());
        }

        public int GetPlayerScore() { return player.Score(); }
        public int GetHouseScore() { return house.Score(); }

        /// <summary>
        /// Player draws a new card
        /// Player loses if score goes over 21
        /// </summary>
        public void Hit() {
            if (player.Score() < 21) {
                player.Add(deck.Draw());
            }

            if (player.Score() > 21) {
                // House wins
                onGameFinished("house");
            }
        }

        /// <summary>
        /// House draws its cards 
        /// ongamefinished event is fired with winner as an argumen
        /// </summary>
        public void Stay() {
            while (house.Score() < 17) {
                house.Add(deck.Draw());
            }

            if (house.Score() <= 21) {
                if (house.Score() > player.Score() || player.Score() > 21) {
                    // House Wins
                    onGameFinished("house");
                }
                else {
                    // Player Wins
                    onGameFinished("player");
                }
            }
            else {
                // Player Wins
                onGameFinished("player");
            }
        }

        /// <summary>
        /// Event to be fired when game ends
        /// </summary>
        /// <param name="winner"></param>
        protected virtual void onGameFinished(String winner) {
            if (GameFinished != null)
                GameFinished(this, new GameFinishedArgs() { Winner = winner });
        }

    }

    public class GameFinishedArgs : EventArgs {
        public string Winner { get; set; }
    }
}
