using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace server
{
    class Cards
   {
        public Cards() { }
        public static string faceupcard = "";
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

        public static int GetRank(string cardId)
        {
            char suitChar = cardId[cardId.Length - 1]; // Lấy ký tự cuối cùng của cardId
            int suitValue;



            switch (suitChar)
            {
                case 'S':
                    suitValue = 0;
                    break;
                case 'C':
                    suitValue = 1;
                    break;
                case 'D':
                    suitValue = 2;
                    break;
                case 'H':
                    suitValue = 3;
                    break;
                default:
                    throw new ArgumentException("Invalid cardId: " + cardId);
            }

            int index = Array.IndexOf(card_id, cardId);
            int value = card_values[index];
            return value * 4 + suitValue;
        }
   }



}
    class DiscardPile
    {
        public static List<string> disPile = new List<string>();
    }

