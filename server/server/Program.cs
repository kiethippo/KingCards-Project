using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using server;
using System.Numerics;
using System.Linq.Expressions;
using System.Reflection.Emit;



namespace Server2
{
    class Program
    {
        
        private static Socket? serverSocket;//tao 1 socket o server
        private static Socket? client;
        private static Thread? clientThread;
        private static List<Players> connectedPlayers = new List<Players>();
        private const int MaxPlayers = 4;
        private static int currentturn = 1;
        private static int skip= 0;

        static void Main(string[] args)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
            serverSocket.Bind(serverEP);
            serverSocket.Listen(4);
            Console.WriteLine("Waiting....");

            while (true)
            {
                if(connectedPlayers.Count <= MaxPlayers)
                {
                    client = serverSocket.Accept();
                    Console.WriteLine(">> Connection from " + client.RemoteEndPoint);
                    clientThread = new Thread(() => readingClientSocket(client));
                    clientThread.Start();
                }
                else
                {
                    Console.WriteLine("Room is Full");
                    Socket tempSocket = serverSocket.Accept();
                    tempSocket.Close();
                    break;
                }    
               
            }
        }

        public static void readingClientSocket(Socket client)
        {
            Players p = new Players();
            p.playerSocket = client;
            connectedPlayers.Add(p);

            byte[] buffer = new byte[10240]; 

            while (p.playerSocket.Connected)
            {
                if (p.playerSocket.Available > 0)
                {
                    string msg = "";

                    while (p.playerSocket.Available > 0)
                    {
                        int bRead = p.playerSocket.Receive(buffer);
                        msg += Encoding.UTF8.GetString(buffer, 0, bRead);
                    }

                    Console.WriteLine(p.playerSocket.RemoteEndPoint + ": " + msg);
                    AnalyzingMessage(msg, p);
                }
            }
        }

        public static void AnalyzingMessage(string msg, Players p)
        {
            string[] arrPayload = msg.Split(';');

            // Xử lý thông điệp từ client như thường
            if (arrPayload[0] == "CONNECT" && connectedPlayers.Count > MaxPlayers)
            {
                // Gửi thông báo cho client rằng phòng đã đầy
                byte[] buffer = Encoding.UTF8.GetBytes("ROOM_FULL");
                p.playerSocket.Send(buffer);
                return; // Thoát khỏi phương thức
            }
            switch (arrPayload[0])
            {
                case "CONNECT":
                    {
                        p.name = arrPayload[1];
                        foreach (var player in connectedPlayers)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes("LOBBYINFO;" + player.name);
                            p.playerSocket.Send(buffer);
                            Thread.Sleep(100);
                        }

                        foreach (var player in connectedPlayers)
                        {
                            if (player.playerSocket != p.playerSocket)
                            {
                                byte[] buffer = Encoding.UTF8.GetBytes("LOBBYINFO;" + p.name);
                                player.playerSocket.Send(buffer);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    break;
                case "DISCONNECT":
                    {
                        foreach (var player in connectedPlayers.ToList())
                        {
                            if (player.name == arrPayload[1])
                            {
                                player.playerSocket.Shutdown(SocketShutdown.Both);
                                player.playerSocket.Close();
                                connectedPlayers.Remove(player);
                            }
                        }
                    }
                    break;
                case "START":
                    {
                        RandomizePlayerTurn();
                        connectedPlayers.Sort((x, y) => x.turn.CompareTo(y.turn));
                        ShuffleArrays(ref Cards.card_id, ref Cards.card_values);
                        int connect = 1;
                       

                        foreach (var player in connectedPlayers)
                        {
                            // 13 initial cards for each player
                            // sắp xếp các con bài từ bé đến lớn
                            List <int> rankcards = new List <int>();
                            List<string> labai = InitialCardsDeal(connect, Cards.card_id);
                            foreach (string card in labai)
                            {
                                int t = Cards.GetRank(card);
                                rankcards.Add(t);
                            }
                                
                            
                            SortLists(rankcards, labai);
                            player.playerhand = labai; 
                            string result = string.Join(";", labai); 

                            string makemsg = "INIT;" + player.name + ";" + player.turn + ";" + player.numOfCards + ";" + result;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Console.WriteLine("Sendback: " + makemsg);
                            Thread.Sleep(100);

                            connect++;

                        }

                        foreach (var player in connectedPlayers)
                        {
                            foreach (var player_ in connectedPlayers)
                            {
                                if (player.name != player_.name)
                                {
                                    string makemsg = "OTHERINFO;" + player_.name + ";" + player_.turn + ";" + player_.numOfCards;
                                    byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                    player.playerSocket.Send(buffer);
                                    Console.WriteLine("Sendback: " + makemsg);
                                    Thread.Sleep(100);
                                }
                            }
                        }

                        foreach (var player in connectedPlayers)
                        {
                            string makemsg = "SETUP;" + player.name;
                            byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                            player.playerSocket.Send(buffer);
                            Console.WriteLine("Sendback: " + makemsg);
                            Thread.Sleep(100);
                        }

                        foreach (var player in connectedPlayers)
                        {
                            string makemsg_ = "TURN;" + connectedPlayers[currentturn - 1].name + ";" + skip;
                            byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                            player.playerSocket.Send(buffer_);
                            Console.WriteLine("Sendback: " + makemsg_);
                            Thread.Sleep(100);
                        }

                    }
                    break;

                case "DISCARD":
                    {
                        skip = 0;
                        
                        connectedPlayers[currentturn - 1].numOfCards = int.Parse(arrPayload[2]);

                        if (connectedPlayers[currentturn - 1].numOfCards == 0)
                        {
                            foreach (var player in connectedPlayers)
                            {
                                string makemsg = "END;" + arrPayload[1] + ";" + arrPayload[2] + ";" + arrPayload[3] + ";" + arrPayload[4] + ";" + arrPayload[5] + ";" + arrPayload[6] + ";" + arrPayload[7] + ";" + arrPayload[8] + ";" + arrPayload[9] + ";" + arrPayload[10] + ";" + arrPayload[11] + ";" + arrPayload[12] + ";" + arrPayload[13] + ";" + arrPayload[14] + ";" + arrPayload[15];
                                byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                player.playerSocket.Send(buffer);
                                Console.WriteLine("Sendback: " + makemsg);
                                Thread.Sleep(100);
                            }
                        }
                        else
                        {
                            foreach (var player in connectedPlayers)
                            {
                                if (player.turn != currentturn)
                                {
                                    string makemsg = "UPDATE;" + arrPayload[1] + ";" + arrPayload[2] + ";" + arrPayload[3] + ";" + arrPayload[4] + ";" + arrPayload[5] + ";" + arrPayload[6] + ";" + arrPayload[7] + ";" + arrPayload[8] + ";" + arrPayload[9] + ";" + arrPayload[10] + ";" + arrPayload[11] + ";" + arrPayload[12] + ";" + arrPayload[13] + ";" + arrPayload[14] + ";" + arrPayload[15];
                                    byte[] buffer = Encoding.UTF8.GetBytes(makemsg);
                                    player.playerSocket.Send(buffer);
                                    Console.WriteLine("Sendback: " + makemsg);
                                    Thread.Sleep(100);
                                }
                            }

                            currentturn++;
                            if (currentturn > connectedPlayers.Count)
                                currentturn = 1;

                            if (currentturn < 1)
                                currentturn = connectedPlayers.Count;
                            foreach (var player in connectedPlayers)
                            {
                                string makemsg_ = "TURN;" + connectedPlayers[currentturn - 1].name + ";" + skip;
                                byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                                player.playerSocket.Send(buffer_);
                                Console.WriteLine("Sendback: " + makemsg_);
                                Thread.Sleep(100);
                            }
                        }
                    }
                    break;
                case "SKIP":
                    {
                        skip++;


                        currentturn++;
                        if (currentturn > connectedPlayers.Count)
                            currentturn = 1;

                        if (currentturn < 1)
                            currentturn = connectedPlayers.Count;

                        foreach (var player in connectedPlayers)
                        {
                            //
                            string makemsg_ = "TURN;" + connectedPlayers[currentturn - 1].name + ";" + skip;
                            byte[] buffer_ = Encoding.UTF8.GetBytes(makemsg_);
                            player.playerSocket.Send(buffer_);
                            Console.WriteLine("Sendback: " + makemsg_);
                            Thread.Sleep(100);
                        }
                        
                    }
                    break;

            }
        }
        public static void RandomizePlayerTurn()
        {
            int[] turns = new int[connectedPlayers.Count];

            for (int i = 1; i <= connectedPlayers.Count; i++)
            {
                turns[i - 1] = i;
            }

            Random rand = new Random();
            foreach (var player in connectedPlayers)
            {
                int pick = rand.Next(turns.Length);
                player.turn = turns[pick];
                turns = turns.Where(val => val != turns[pick]).ToArray();
                player.numOfCards = 13;
            }
        }

        public static void PileSuffle() //sáo bài
        {
            Random rand = new Random();
            Cards.card_id = Cards.card_id.OrderBy(x => rand.Next()).ToArray();
        }


        public static List <string> InitialCardsDeal(int a, string[] b) // Đưa các lá bài thành 1 string
        {
            List<string> cards = new List<string>();
            string thirdteencards = ""; // 

            switch (a)
            {
                case 1:
                    for (int i = 0; i <= 12; i++)
                    {
                        thirdteencards = b[i];
                        cards.Add(thirdteencards);
                    }
                    break;

                case 2:
                    for (int i = 13; i <= 25; i++)
                    {
                        thirdteencards = b[i];
                        cards.Add(thirdteencards);
                    }
                    break;
                case 3:
                    for (int i = 26; i <= 38; i++)
                    {
                        thirdteencards = b[i];
                        cards.Add(thirdteencards);
                    }
                    break;
                case 4:
                    for (int i = 39; i <= 51; i++)
                    {
                        thirdteencards = b[i];
                        cards.Add(thirdteencards);
                    }
                    break;
            }

            return cards; 


        }
        static void ShuffleArrays(ref string[] a, ref int[] b) // Sáo bài (mảng string)
        {
            Random rnd = new Random();
            int n = a.Length;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                string tempA = a[k];
                a[k] = a[n];
                a[n] = tempA;

                int tempB = b[k];
                b[k] = b[n];
                b[n] = tempB;
            }
        }

        static void SortLists(List<int> nums, List<string> values) //sắp xếp lá bài từ bé đến lớn
        {
            // Sử dụng Sort của List để sắp xếp nums,
            // và áp dụng cùng thứ tự sắp xếp cho values
            for (int i = 0; i < nums.Count; i++)
            {
                for (int j = i + 1; j < nums.Count; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        // Hoán đổi vị trí giữa nums
                        int tempNum = nums[i];
                        nums[i] = nums[j];
                        nums[j] = tempNum;

                        // Hoán đổi vị trí giữa values
                        string tempValue = values[i];
                        values[i] = values[j];
                        values[j] = tempValue;
                    }
                }
            }
        }

    }
    
}
