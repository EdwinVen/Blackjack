//******************************************************
// File: Card.cs
//
// Purpose: Contains the class definition for Card.
// Card will hold a rank and suit value of the card.
// As well as several methods to read, write and 
// serialize the object.
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
using System.IO;
using System.Xml;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using CardPics;
using System.Windows.Media.Imaging;


namespace CardLibrary
{
    public enum RankType { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
    public enum SuitType { Hearts, Diamonds, Spades, Clubs };

	[DataContract]
    public class Card 
    {
		// variable containing the rank of the card
		[DataMember(Name = "Rank")]
        public RankType Rank { get; set; }
		// variable containing the suit of the card
		[DataMember(Name = "Suit")]
        public SuitType Suit { get; set; }
        // variable containing the picture of the card
        public BitmapImage Pic { get; set; }

              
		//******************************************************
		// Method: Card
		//
		// Purpose: Default Constructor
		//******************************************************
		public Card(): this(RankType.Ace, SuitType.Hearts){}

		//******************************************************
		// Method: Card
		//
		// Purpose: Parametized constructor
		//******************************************************
		public Card(RankType r, SuitType s): this(r, s, new BitmapImage()) {}

        //******************************************************
        // Method: Card
        //
        // Purpose: Parametized constructor
        //******************************************************
        public Card(RankType r, SuitType s, BitmapImage p) {
            Rank = r;
            Suit = s;
            Pic = p;
        }

		//******************************************************
		// Method: writeXml
		// 
		// Purpose: Serialize the object into a file as Xml
		//******************************************************
		public void writeXml(string fileName) {
			
			// Create an xml file that will have indentation
            XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			XmlWriter writer = XmlWriter.Create(fileName, settings);
				
			// Serialize the current object
			DataContractSerializer ser = new DataContractSerializer(typeof(Card));
			ser.WriteObject(writer, this);

			writer.Close();
        }

		//******************************************************
		// Method: writeJSON
		//
		// Purpose: Serialize the object into a file as JSON
		//******************************************************
		public void writeJSON(string filename) {
			// Create file and serializer
			FileStream filestream = File.Create(filename);
			DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Card));

			// Serialize the current object to the file
			ser.WriteObject(filestream, this);

			filestream.Close();
        }

		//******************************************************
		// Method: write
		//
		// Purpose: Write the object raw data into a file
		//******************************************************
		public void write(string filename) {
			StreamWriter writer = new StreamWriter(filename);

			try {
				writer.WriteLine(Rank);
				writer.WriteLine(Suit);
			}
			finally {
				writer.Close();
			}

		}

		//******************************************************
		// Method: read
		//
		// Purpose: read raw data from a file and load it on the object
		//******************************************************
		public void read(string filename) {
			StreamReader reader = new StreamReader(filename);

			try {
				Rank = (RankType)Enum.Parse(typeof(RankType), reader.ReadLine());
				Suit = (SuitType)Enum.Parse(typeof(SuitType), reader.ReadLine());
				
			}
			finally {
				reader.Close();
			}
		}

		//******************************************************
		// Method: ToString
		//
		// Purpose: returns a string representation of the object
		//******************************************************
		public override string ToString() {
			string result = $"Rank: {Rank}, Suit: {Suit}";

            return result;
		}


    }
}
