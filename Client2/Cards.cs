using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client2
{
    internal class Cards
    {

        //public Cards() { }
        //public static string faceupcard = "";
        public static string[] card_id =
    {
        "2H", "3H", "4H", "5H", "6H", "7H", "8H", "9H", "10H", "JH", "QH", "KH", "AH", // Hearts
        "2D", "3D", "4D", "5D", "6D", "7D", "8D", "9D", "10D", "JD", "QD", "KD", "AD", // Diamonds
        "2S", "3S", "4S", "5S", "6S", "7S", "8S", "9S", "10S", "JS", "QS", "KS", "AS", // Spades
        "2C", "3C", "4C", "5C", "6C", "7C", "8C", "9C", "10C", "JC", "QC", "KC", "AC"  // Clubs
    };
        public static int[] card_values =
    {
        15, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, // Hearts
        15, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, // Diamonds
        15, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, // Spades
        15, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14  // Clubs
    };

    }

        
}
