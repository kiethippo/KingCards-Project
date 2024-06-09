using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Players
    {
        public List<Cards> cards;
        public  string name { get; set; }
        public int numOfCards { get; set; }
        public int turn { get; set; }
        public bool isHost { get; set; }
        public  Socket playerSocket { get; set; }

        public  List<string> playerhand {  get; set; } // các quân bài mà người chơi cầm trên tay

        public  List<string> danhSach = new List<string>();
    }
    
}
