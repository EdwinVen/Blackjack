//******************************************************
// File: CardUser.cs
//
// Purpose: Contains the class definition for CardUser.
// CardUser will be using CardLibrary.Card with the purpose
// of testing its correctness
//
// Written By: Edwins Ventura
//
// Compiler: Visual Studio 2015
//
//******************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardLibrary;

namespace Card_User {
    class CardUser {
        static void Main(string[] args) {
            Card card_1 = new Card();

			// Testing the default constructor
			Console.WriteLine("Should show the default: Ace and Hearts");
            Console.WriteLine(card_1.Rank + " and " + card_1.Suit);

			// Testing the parametized constructor
            Card card_2 = new Card(RankType.King, SuitType.Clubs);
			Console.WriteLine("\nShould show King and Clubs");
			Console.WriteLine(card_2.Rank + " and " + card_2.Suit);

			// Testing the writeXml and writeJSON
			// Check the files
            card_2.writeXml("testXml.xml");
			card_2.writeJSON("textJSON.json");

			// Writing to a file
            Card card_3 = new Card(RankType.Five, SuitType.Hearts);
            card_3.write("inputAndOutput.txt");

			// Reading from the same file as above
            Card card_4 = new Card();
            card_4.read("inputAndOutput.txt");
			Console.WriteLine("\nShould print Five and Hearts");
            Console.WriteLine(card_4.Rank + " and " + card_4.Suit);


            #region Test public Deck.GetCardValueForImage(Card)
            // Deck.GetCardValueForImage(Card) needs to be public

            //Deck d = new Deck();

            //card_4.Rank = RankType.Jack;
            //card_4.Suit = SuitType.Diamonds;
            //Tuple<string, string> t = d.GetCardValueForImage(card_4);


            //Console.WriteLine($"\n\nRank: {t.Item1}  Suit: {t.Item2}");
            #endregion

            Console.ReadLine();
        }
    }
}
