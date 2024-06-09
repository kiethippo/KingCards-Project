using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Diagnostics.Eventing.Reader;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace Client2
{
    class ClientSocket
    {
        public static Socket clientSocket;
        public static Thread recvThread;
        public static string datatype = "";

        public static void Connect(IPEndPoint serverEP)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverEP);
            recvThread = new Thread(() => readingReturnData());
            recvThread.Start();
        }

        public static void SendMessage(string data)
        {
            string msgstr = datatype + ";" + data;
            byte[] msg = Encoding.UTF8.GetBytes(msgstr);
            clientSocket.Send(msg);
        }

        public static void readingReturnData()
        {
            byte[] buffer = new byte[1024];

            while (clientSocket.Connected)
            {
                if (clientSocket.Available > 0)
                {
                    string msg = "";

                    while (clientSocket.Available > 0)
                    {
                        int bRead = clientSocket.Receive(buffer);
                        msg += Encoding.UTF8.GetString(buffer, 0, bRead);
                    }

                    AnalyzingReturnMessage(msg);
                    ConnectMenu.lobby.Tempdisplay(msg);
                }
            }
        }
        public static GameTable gametable;
        public static List<OtherPlayers> otherplayers;
        public static void AnalyzingReturnMessage(string msg)
        {
            string[] arrPayload = msg.Split(';');

            switch (arrPayload[0])
            {
                case "LOBBYINFO":
                    {
                        ConnectMenu.lobby.DisplayConnectedPlayer(arrPayload[1]);
                    }
                    break;
                case "INIT":
                    {
                        ThisPlayer.turn = int.Parse(arrPayload[2]);
                        ThisPlayer.numOfCards = int.Parse(arrPayload[3]);
                        for (int i = 4; i <= 16; i++)
                        {
                            ThisPlayer.cards.Add(arrPayload[i]);
                        }

                        gametable = new GameTable();
                        otherplayers = new List<OtherPlayers>();
                        ConnectMenu.lobby.Invoke((MethodInvoker)delegate ()
                        {
                            gametable.DisplayCardsTemp(); 
                            gametable.InitCardFetch();
                            gametable.DisplayFaceUp();
                            gametable.Show();
                        });


                    }
                    break;
                case "OTHERINFO":
                    {
                        OtherPlayers otherplayer = new OtherPlayers();
                        otherplayer.name = arrPayload[1];
                        otherplayer.turn = arrPayload[2];
                        otherplayer.numofCards = arrPayload[3];
                        otherplayers.Add(otherplayer);
                    }
                    break;
                case "SETUP":
                    {
                        gametable.InitDisplay();
                    }
                    break;
                case "UPDATE":
                    {
                        gametable.UpdateNumOfCards(arrPayload[1], arrPayload[2]);
                        if (arrPayload.Length > 3)
                        {
                            gametable.faceUpCard = arrPayload[3];
                            gametable.faceUpCard2 = arrPayload[4];
                            gametable.faceUpCard3 = arrPayload[5];
                            gametable.faceUpCard4 = arrPayload[6];
                            gametable.faceUpCard5 = arrPayload[7];
                            gametable.faceUpCard6 = arrPayload[8];
                            gametable.faceUpCard7 = arrPayload[9];
                            gametable.faceUpCard8 = arrPayload[10];
                            gametable.faceUpCard9 = arrPayload[11];
                            gametable.faceUpCard10 = arrPayload[12];
                            gametable.faceUpCard11 = arrPayload[13];
                            gametable.faceUpCard12 = arrPayload[14];
                            gametable.DisplayFaceUp();
                        }
                    }
                    break;
                case "TURN":
                    {
                        if (int.Parse(arrPayload[2]) == 3)
                        {
                            gametable.faceUpCard = "";
                            gametable.faceUpCard2 = "";
                            gametable.faceUpCard3 = "";
                            gametable.faceUpCard4 = "";
                            gametable.faceUpCard5 = "";
                            gametable.faceUpCard6 = "";
                            gametable.faceUpCard7 = "";
                            gametable.faceUpCard8 = "";
                            gametable.faceUpCard9 = "";
                            gametable.faceUpCard10 = "";
                            gametable.faceUpCard11 = "";
                            gametable.faceUpCard12 = "";
                            gametable.DisplayFaceUp();
                        }
                        if (arrPayload[1] == ThisPlayer.name)
                            CheckForPotentialCards();


                        gametable.UndoHighlightTurn();
                        gametable.HighlightTurn(arrPayload[1]);

                    }
                    break;
                case "END":
                    {
                        if (ThisPlayer.name == arrPayload[1])
                        {
                            ThisPlayer.numOfCards = int.Parse(arrPayload[2]);
                        }
                        else
                        {
                            foreach (var player in otherplayers)
                            {
                                if (player.name == arrPayload[1])
                                {
                                    player.numofCards = arrPayload[2];
                                }
                            }
                        }

                        gametable.UpdateNumOfCards(arrPayload[1], arrPayload[2]);
                        if (arrPayload.Length > 3)
                        {
                            gametable.faceUpCard = arrPayload[3];
                            gametable.DisplayFaceUp();
                        }
                        Form1 form1 = new Form1();
                        form1.ShowDialog();

                    }
                    break;
                default:
                    break;

            }
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
        static void SortLists(List<int> nums, List<string> values)
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

            int index = Array.IndexOf(Cards.card_id, cardId);
            int value = Cards.card_values[index];
            return value * 4 + suitValue;
        }
        public static int GetRank2(string CardId)
        {
            int index = Array.IndexOf(Cards.card_id, CardId);
            int value = Cards.card_values[index];
            return value;
        }
        public static bool IsPair(string card1, string card2)
        {
            // Lấy giá trị số của lá bài (bỏ ký tự chất)
            string value1 = card1.Substring(0, card1.Length - 1);
            string value2 = card2.Substring(0, card2.Length - 1);

            if (value1 == value2) return true;
            else return false;
        }
        public static bool Check3(string card1, string card2, string card3)
        {
            // Lấy giá trị số của lá bài (bỏ ký tự chất)
            string value1 = card1.Substring(0, card1.Length - 1);
            string value2 = card2.Substring(0, card2.Length - 1);
            string value3 = card3.Substring(0, card3.Length - 1);

            if (value1 == value2 && value2 == value3) return true;
            else return false;
        }
        public static bool Check4(string card1, string card2, string card3, string card4)
        {
            // Lấy giá trị số của lá bài (bỏ ký tự chất)
            string value1 = card1.Substring(0, card1.Length - 1);
            string value2 = card2.Substring(0, card2.Length - 1);
            string value3 = card3.Substring(0, card3.Length - 1);
            string value4 = card4.Substring(0, card4.Length - 1);

            if (value1 == value2 && value2 == value3 && value3 == value4) return true;
            else return false;
        }
        public static List<int> rankcards = new List<int>();
        public static bool checksanh3(List<Button> cards)
        {


            return false;
        }


        public static List<Button> buttons = new List<Button>();
        public static void CheckForPotentialCards()
        {
            gametable.EnableSkipBtn();
            List<string> buttons = new List<string>();
            foreach (var row in gametable.CardBtns) // duyệt hàng các lá bài // 
            {
                if (row.Count >= 1)
                {
                    if (gametable.faceUpCard2 != "" && GetRank2(gametable.faceUpCard) == GetRank2(gametable.faceUpCard2) && gametable.faceUpCard3 == "")
                    {
                        // luật đôi
                        if (row.Count >= 2)
                        {
                            for (int i = 0; i < row.Count; i++)
                            {
                                if (i == 0)
                                {
                                    if (IsPair(row[i].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard2))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i > 0 && i < (row.Count - 1))
                                {
                                    if (IsPair(row[i].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard2))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (IsPair(row[i].id, row[i - 1].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard2))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == row.Count - 1)
                                {
                                    if (IsPair(row[i].id, row[i - 1].id) == true)
                                    {
                                        row[i - 1].btn.Enabled = true;
                                        row[i].btn.Enabled = true;
                                        gametable.EnableDiscardBtn();
                                    }
                                }
                            }
                        }

                    }
                    //luat danh 3 
                    else if (gametable.faceUpCard3 != "" && GetRank2(gametable.faceUpCard) == GetRank2(gametable.faceUpCard3) && GetRank2(gametable.faceUpCard2) == GetRank2(gametable.faceUpCard3) && gametable.faceUpCard4 == "")
                    {
                        if (row.Count >= 3)
                        {
                            for (int i = 0; i < row.Count; i++)
                            {
                                if (i == 0)
                                {
                                    if (Check3(row[i].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard3))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == 1)
                                {
                                    if (Check3(row[i].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard3))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check3(row[i].id, row[i - 1].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard3))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i > 1 && i < (row.Count - 1))
                                {
                                    if (Check3(row[i].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard3))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check3(row[i].id, row[i - 1].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard3))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    if (Check3(row[i].id, row[i - 1].id, row[i - 2].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard3))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }

                                }
                                else if (i == (row.Count - 2))
                                {
                                    if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == row.Count - 1)
                                {
                                    if (Check3(row[i].id, row[i - 1].id, row[i - 2].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard3))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i - 1].btn.Enabled = true;
                                            row[i - 2].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                            }
                        }

                    }
                    //luat danh 4 (tu quy)
                    else if (gametable.faceUpCard4 != "" && GetRank2(gametable.faceUpCard) == GetRank2(gametable.faceUpCard4) && GetRank2(gametable.faceUpCard2) == GetRank2(gametable.faceUpCard4) && GetRank2(gametable.faceUpCard3) == GetRank2(gametable.faceUpCard4))
                    {
                        if (row.Count >= 4)
                        {
                            for (int i = 0; i < row.Count; i++)
                            {
                                if (i == 0)
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == 1)
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == 2)
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i > 2 && i < (row.Count - 3))
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }


                                }
                                else if (i == (row.Count - 3))
                                {
                                    if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == row.Count - 2)
                                {
                                    if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == (row.Count - 1))
                                {
                                    if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }

                                }
                            }
                        }
                    }
                    //sanh
                    // sanh 3
                    else if (gametable.faceUpCard3 != "" && GetRank2(gametable.faceUpCard3) != GetRank2(gametable.faceUpCard2) && GetRank2(gametable.faceUpCard) != GetRank2(gametable.faceUpCard3) && gametable.faceUpCard4 == "")
                    {
                        if (row.Count >= 3)
                        {
                            for (int i = 0; i < row.Count - 2; i++)
                            {
                                for (int j = i + 1; j < row.Count - 1; j++)
                                {
                                    for (int k = j + 1; k < row.Count; k++)
                                    {
                                        // Kiểm tra các bộ ba lá bài liên tiếp
                                        if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[j].btn.Enabled = true;
                                            row[k].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();


                                        }
                                    }
                                }
                            }
                        }


                    }
                    //sanh 4
                    else if (gametable.faceUpCard4 != "" && GetRank2(gametable.faceUpCard) != GetRank2(gametable.faceUpCard4) && GetRank2(gametable.faceUpCard2) != GetRank2(gametable.faceUpCard4) && GetRank2(gametable.faceUpCard3) != GetRank2(gametable.faceUpCard4) && gametable.faceUpCard5 == "")
                    {
                        if (row.Count >= 4)
                        {
                            for (int i = 0; i < row.Count - 3; i++)
                            {
                                for (int j = i + 1; j < row.Count - 2; j++)
                                {
                                    for (int k = j + 1; k < row.Count - 1; k++)
                                    {
                                        for (int l = k + 1; l < row.Count; l++)
                                        {
                                            if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id))
                                            {

                                                row[i].btn.Enabled = true;
                                                row[j].btn.Enabled = true;
                                                row[k].btn.Enabled = true;
                                                row[l].btn.Enabled = true;
                                                gametable.EnableDiscardBtn();

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //sanh 5
                    else if (gametable.faceUpCard5 != "" && gametable.faceUpCard6 == "")
                     {
                         if (row.Count >= 5)
                         {
                             for (int i = 0; i < row.Count - 4; i++)
                             {
                                 for (int j = i + 1; j < row.Count - 3; j++)
                                 {
                                     for (int k = j + 1; k < row.Count - 2; k++)
                                     {
                                         for (int l = k + 1; l < row.Count - 1; l++)
                                         {
                                             for (int m = l + 1; m < row.Count; m++)
                                             {
                                                 if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id) && GetRank2(row[l].id) + 1 == GetRank2(row[m].id))
                                                 {
                                                     row[i].btn.Enabled = true;
                                                     row[j].btn.Enabled = true;
                                                     row[k].btn.Enabled = true;
                                                     row[l].btn.Enabled = true;
                                                     row[m].btn.Enabled = true;
                                                     gametable.EnableDiscardBtn();

                                                 }
                                             }
                                         }
                                     }
                                 }
                             }
                         }
                     }
                    //sanh 6
                    else if (gametable.faceUpCard6 != "" && gametable.faceUpCard7 == "")
                    {
                        if (row.Count >= 6)
                        {
                            for (int i = 0; i < row.Count - 5; i++)
                            {
                                for (int j = i + 1; j < row.Count - 4; j++)
                                {
                                    for (int k = j + 1; k < row.Count - 3; k++)
                                    {
                                        for (int l = k + 1; l < row.Count - 2; l++)
                                        {
                                            for (int m = l + 1; m < row.Count - 1; m++)
                                            {
                                                for (int n = m + 1; n < row.Count; n++)
                                                {
                                                    if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id) && GetRank2(row[l].id) + 1 == GetRank2(row[m].id) && GetRank2(row[m].id) + 1 == GetRank2(row[n].id))
                                                    {
                                                        row[i].btn.Enabled = true;
                                                        row[j].btn.Enabled = true;
                                                        row[k].btn.Enabled = true;
                                                        row[l].btn.Enabled = true;
                                                        row[m].btn.Enabled = true;
                                                        row[n].btn.Enabled = true;
                                                        gametable.EnableDiscardBtn();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //sanh 7
                    else if(gametable.faceUpCard7 != "" && gametable.faceUpCard8 == "")
                    {
                        if(row.Count >=7)
                        {
                            for (int i = 0; i < row.Count - 6; i++)
                            {
                                for (int j = i + 1; j < row.Count - 5; j++)
                                {
                                    for (int k = j + 1; k < row.Count - 4; k++)
                                    {
                                        for (int l = k + 1; l < row.Count - 3; l++)
                                        {
                                            for (int m = l + 1; m < row.Count - 2; m++)
                                            {
                                                for (int n = m + 1; n < row.Count - 1; n++)
                                                {
                                                    for (int o = n + 1; o < row.Count; o++)
                                                    {
                                                        if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id) && GetRank2(row[l].id) + 1 == GetRank2(row[m].id) && GetRank2(row[m].id) + 1 == GetRank2(row[n].id) && GetRank2(row[n].id) + 1 == GetRank2(row[o].id))
                                                        {
                                                            row[i].btn.Enabled = true;
                                                            row[j].btn.Enabled = true;
                                                            row[k].btn.Enabled = true;
                                                            row[l].btn.Enabled = true;
                                                            row[m].btn.Enabled = true;
                                                            row[n].btn.Enabled = true;
                                                            row[o].btn.Enabled = true;
                                                            gametable.EnableDiscardBtn();
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }    
                    }   
                    //sanh 8
                    else if(gametable.faceUpCard8 != "" && gametable.faceUpCard9 == "")
                    {
                        if(row.Count >= 8)
                        {
                            for (int i = 0; i < row.Count - 7; i++)
                            {
                                for (int j = i + 1; j < row.Count - 6; j++)
                                {
                                    for (int k = j + 1; k < row.Count - 5; k++)
                                    {
                                        for (int l = k + 1; l < row.Count - 4; l++)
                                        {
                                            for (int m = l + 1; m < row.Count - 3; m++)
                                            {
                                                for (int n = m + 1; n < row.Count - 2; n++)
                                                {
                                                    for (int o = n + 1; o < row.Count - 1; o++)
                                                    {
                                                        for (int p = o + 1; p < row.Count; p++)
                                                        {
                                                            if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id) && GetRank2(row[l].id) + 1 == GetRank2(row[m].id) && GetRank2(row[m].id) + 1 == GetRank2(row[n].id) && GetRank2(row[n].id) + 1 == GetRank2(row[o].id) && GetRank2(row[o].id) + 1 == GetRank2(row[p].id))
                                                            {
                                                                row[i].btn.Enabled = true;
                                                                row[j].btn.Enabled = true;
                                                                row[k].btn.Enabled = true;
                                                                row[l].btn.Enabled = true;
                                                                row[m].btn.Enabled = true;
                                                                row[n].btn.Enabled = true;
                                                                row[o].btn.Enabled = true;
                                                                row[p].btn.Enabled = true;
                                                                gametable.EnableDiscardBtn();
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }    
                    } 
                    // sanh 9
                    else if(gametable.faceUpCard9 != "" && gametable.faceUpCard10 == "")
                    {
                        if(row.Count >= 9)
                        {
                            for (int i = 0; i < row.Count - 8; i++)
                            {
                                for (int j = i + 1; j < row.Count - 7; j++)
                                {
                                    for (int k = j + 1; k < row.Count - 6; k++)
                                    {
                                        for (int l = k + 1; l < row.Count - 5; l++)
                                        {
                                            for (int m = l + 1; m < row.Count - 4; m++)
                                            {
                                                for (int n = m + 1; n < row.Count - 3; n++)
                                                {
                                                    for (int o = n + 1; o < row.Count - 2; o++)
                                                    {
                                                        for (int p = o + 1; p < row.Count - 1; p++)
                                                        {
                                                            for (int q = p + 1; q < row.Count; q++)
                                                            {
                                                                if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id) && GetRank2(row[l].id) + 1 == GetRank2(row[m].id) && GetRank2(row[m].id) + 1 == GetRank2(row[n].id) && GetRank2(row[n].id) + 1 == GetRank2(row[o].id) && GetRank2(row[o].id) + 1 == GetRank2(row[p].id) && GetRank2(row[p].id) + 1 == GetRank2(row[q].id))
                                                                {
                                                                    row[i].btn.Enabled = true;
                                                                    row[j].btn.Enabled = true;
                                                                    row[k].btn.Enabled = true;
                                                                    row[l].btn.Enabled = true;
                                                                    row[m].btn.Enabled = true;
                                                                    row[n].btn.Enabled = true;
                                                                    row[o].btn.Enabled = true;
                                                                    row[p].btn.Enabled = true;
                                                                    row[q].btn.Enabled = true;
                                                                    gametable.EnableDiscardBtn();
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //sanh 10
                    else if(gametable.faceUpCard10 != "" && gametable.faceUpCard11 == "")
                    {
                        if(row.Count >= 10)
                        {
                            for (int i = 0; i < row.Count - 9; i++)
                            {
                                for (int j = i + 1; j < row.Count - 8; j++)
                                {
                                    for (int k = j + 1; k < row.Count - 7; k++)
                                    {
                                        for (int l = k + 1; l < row.Count - 6; l++)
                                        {
                                            for (int m = l + 1; m < row.Count - 5; m++)
                                            {
                                                for (int n = m + 1; n < row.Count - 4; n++)
                                                {
                                                    for (int o = n + 1; o < row.Count - 3; o++)
                                                    {
                                                        for (int p = o + 1; p < row.Count - 2; p++)
                                                        {
                                                            for (int q = p + 1; q < row.Count - 1; q++)
                                                            {
                                                                for (int r = q + 1; r < row.Count; r++)
                                                                {
                                                                    if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id) && GetRank2(row[l].id) + 1 == GetRank2(row[m].id) && GetRank2(row[m].id) + 1 == GetRank2(row[n].id) && GetRank2(row[n].id) + 1 == GetRank2(row[o].id) && GetRank2(row[o].id) + 1 == GetRank2(row[p].id) && GetRank2(row[p].id) + 1 == GetRank2(row[q].id) && GetRank2(row[q].id) + 1 == GetRank2(row[r].id))
                                                                    {
                                                                        row[i].btn.Enabled = true;
                                                                        row[j].btn.Enabled = true;
                                                                        row[k].btn.Enabled = true;
                                                                        row[l].btn.Enabled = true;
                                                                        row[m].btn.Enabled = true;
                                                                        row[n].btn.Enabled = true;
                                                                        row[o].btn.Enabled = true;
                                                                        row[p].btn.Enabled = true;
                                                                        row[q].btn.Enabled = true;
                                                                        row[r].btn.Enabled = true;  
                                                                        gametable.EnableDiscardBtn();

                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }    
                    }  
                    // sanh 11
                    else if(gametable.faceUpCard11 != "" && gametable.faceUpCard12 == "")
                    {
                        if(row.Count >= 11)
                        {
                            for (int i = 0; i < row.Count - 10; i++)
                            {
                                for (int j = i + 1; j < row.Count - 9; j++)
                                {
                                    for (int k = j + 1; k < row.Count - 8; k++)
                                    {
                                        for (int l = k + 1; l < row.Count - 7; l++)
                                        {
                                            for (int m = l + 1; m < row.Count - 6; m++)
                                            {
                                                for (int n = m + 1; n < row.Count - 5; n++)
                                                {
                                                    for (int o = n + 1; o < row.Count - 4; o++)
                                                    {
                                                        for (int p = o + 1; p < row.Count - 3; p++)
                                                        {
                                                            for (int q = p + 1; q < row.Count - 2; q++)
                                                            {
                                                                for (int r = q + 1; r < row.Count - 1; r++)
                                                                {
                                                                    for (int s = r + 1; s < row.Count; s++)
                                                                    {
                                                                        if (GetRank2(row[i].id) + 1 == GetRank2(row[j].id) && GetRank2(row[j].id) + 1 == GetRank2(row[k].id) && GetRank2(row[k].id) + 1 == GetRank2(row[l].id) && GetRank2(row[l].id) + 1 == GetRank2(row[m].id) && GetRank2(row[m].id) + 1 == GetRank2(row[n].id) && GetRank2(row[n].id) + 1 == GetRank2(row[o].id) && GetRank2(row[o].id) + 1 == GetRank2(row[p].id) && GetRank2(row[p].id) + 1 == GetRank2(row[q].id) && GetRank2(row[q].id) + 1 == GetRank2(row[r].id) && GetRank2(row[r].id) + 1 == GetRank2(row[s].id))
                                                                        {
                                                                            row[i].btn.Enabled = true;
                                                                            row[j].btn.Enabled = true;
                                                                            row[k].btn.Enabled = true;
                                                                            row[l].btn.Enabled = true;
                                                                            row[m].btn.Enabled = true;
                                                                            row[n].btn.Enabled = true;
                                                                            row[o].btn.Enabled = true;
                                                                            row[p].btn.Enabled = true;
                                                                            row[q].btn.Enabled = true;
                                                                            row[r].btn.Enabled = true;
                                                                            row[s].btn.Enabled = true;
                                                                            gametable.EnableDiscardBtn();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }    
                    }    

                    //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
                    // chat heo va doi heo bang tu quy
                   else if (gametable.faceUpCard != "" && GetRank2(gametable.faceUpCard) == 15)
                    {
                        if (row.Count >= 4)
                        {
                            for (int i = 0; i < row.Count; i++)
                            {
                                if (i == 0)
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == 1)
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == 2)
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i > 2 && i < (row.Count - 3))
                                {
                                    if (Check4(row[i].id, row[i + 1].id, row[i + 2].id, row[i + 3].id) == true)
                                    {
                                        if (GetRank(row[i + 3].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            row[i + 1].btn.Enabled = true;
                                            row[i + 2].btn.Enabled = true;
                                            row[i + 3].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }


                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }


                                }
                                else if (i == (row.Count - 3))
                                {
                                    if (Check4(row[i].id, row[i - 1].id, row[i + 1].id, row[i + 2].id) == true)
                                    {
                                        if (GetRank(row[i + 2].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == row.Count - 2)
                                {
                                    if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i + 1].id) == true)
                                    {
                                        if (GetRank(row[i + 1].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                    else if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }
                                }
                                else if (i == (row.Count - 1))
                                {
                                    if (Check4(row[i].id, row[i - 1].id, row[i - 2].id, row[i - 3].id) == true)
                                    {
                                        if (GetRank(row[i].id) > GetRank(gametable.faceUpCard4))
                                        {
                                            row[i].btn.Enabled = true;
                                            gametable.EnableDiscardBtn();
                                        }
                                    }

                                }
                            }


                        }
                        foreach (var bt in row)
                        {
                            //string checknum = new String(gametable.faceUpCard.Where(Char.IsDigit).ToArray()); // là bài hiện thị
                            //string getnum = new String(bt.id.Where(Char.IsDigit).ToArray()); // lá bài trên tayEventLogSession




                            // luật cóc
                            if (gametable.faceUpCard == "")
                            {

                                bt.btn.Enabled = true;
                                gametable.EnableDiscardBtn();
                            }
                            else
                            {
                                if (GetRank(gametable.faceUpCard) < GetRank(bt.id) && gametable.faceUpCard2 == "")
                                {
                                    //bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
                                    bt.btn.Enabled = true;
                                    gametable.EnableDiscardBtn();
                                    continue;
                                }



                                //continue;





                            }
                        }
                    }
                    else
                    {
                        foreach (var bt in row)
                        {
                            //string checknum = new String(gametable.faceUpCard.Where(Char.IsDigit).ToArray()); // là bài hiện thị
                            //string getnum = new String(bt.id.Where(Char.IsDigit).ToArray()); // lá bài trên tayEventLogSession




                            // luật cóc
                            if (gametable.faceUpCard == "")
                            {

                                bt.btn.Enabled = true;
                                gametable.EnableDiscardBtn();
                            }
                            else
                            {
                                if (GetRank(gametable.faceUpCard) < GetRank(bt.id) && gametable.faceUpCard2 == "")
                                {
                                    //bt.btn.FlatAppearance.BorderColor = Color.Chartreuse;
                                    bt.btn.Enabled = true;
                                    gametable.EnableDiscardBtn();
                                    continue;
                                }



                                //continue;





                            }
                        }
                    
                    }

                    //------------------------------------------------------//
                    /*foreach (var bt in row)
                    {
                        bt.btn.Enabled = true;
                        gametable.EnableDiscardBtn();
                    }*/


                }

            }
        }
    }
}
 
