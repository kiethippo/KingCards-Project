using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Client2
{
    public partial class GameTable : Form
    {
        public string faceUpCard = "";
        public string faceUpCard2 = "";
        public string faceUpCard3 = "";
        public string faceUpCard4 = "";
        public string faceUpCard5 = "";
        public string faceUpCard6 = "";
        public string faceUpCard7 = "";
        public string faceUpCard8 = "";
        public string faceUpCard9 = "";
        public string faceUpCard10 = "";
        public string faceUpCard11 = "";
        public string faceUpCard12 = "";
        public string selectedCardId = "";
        List<string> selectCardId2 = new List<string>() { "", "" , "", "", "", "", "", "", "", "", "", "", ""};
        public List<string> sanh = new List<String>();
        //public string selectedCardIds= string.Join(",", Select);
        List<string> discards = new List<string>();
        public List<List<CardButton>> CardBtns;
        public List<Label> lbnames;
        public List<TextBox> tbnums;
        public int row = 0;



        public class CardButton
        {
            public int X { get; set; }
            public int Y { get; set; }
            public string id { get; set; }
            public Button btn = new Button();

        }
        public GameTable()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            buttonDiscard.Enabled = false;
            button2.Enabled = false; // nút skip 
            //panelColors.Visible = false;

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard1.Enabled = false;
            btnDiscardPileCard1.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard1.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard1.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard1.BackgroundImageLayout = ImageLayout.Stretch;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard2.Enabled = false;
            btnDiscardPileCard2.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard2.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard2.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard2.BackgroundImageLayout = ImageLayout.Stretch;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard3.Enabled = false;
            btnDiscardPileCard3.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard3.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard3.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard3.BackgroundImageLayout = ImageLayout.Stretch;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard4.Enabled = false;
            btnDiscardPileCard4.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard4.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard4.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard4.BackgroundImageLayout = ImageLayout.Stretch;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard5.Enabled = false;
            btnDiscardPileCard5.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard5.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard5.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard5.BackgroundImageLayout = ImageLayout.Stretch;
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard6.Enabled = false;
            btnDiscardPileCard6.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard6.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard6.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard6.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard6.Enabled = false;
            btnDiscardPileCard6.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard6.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard6.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard6.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard7.Enabled = false;
            btnDiscardPileCard7.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard7.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard7.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard7.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard8.Enabled = false;
            btnDiscardPileCard8.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard8.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard8.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard8.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard9.Enabled = false;
            btnDiscardPileCard9.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard9.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard9.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard9.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard10.Enabled = false;
            btnDiscardPileCard10.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard10.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard10.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard10.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard11.Enabled = false;
            btnDiscardPileCard11.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard11.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard11.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard11.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            btnDiscardPileCard12.Enabled = false;
            btnDiscardPileCard12.FlatStyle = FlatStyle.Flat;
            btnDiscardPileCard12.FlatAppearance.BorderSize = 2;
            btnDiscardPileCard12.FlatAppearance.BorderColor = Color.Black;
            btnDiscardPileCard12.BackgroundImageLayout = ImageLayout.Stretch;
            //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//
            

                //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++//


                CardBtns = new List<List<CardButton>>();
            lbnames = new List<Label>();
            tbnums = new List<TextBox>();
        }
        public void EnableDiscardBtn()
        {
            buttonDiscard.Enabled = true;
        }
        public void EnableSkipBtn()
        {
            button2.Enabled = true;
        }
        public void InitCardFetch()
        {

            CardBtns.Add(new List<CardButton>());
            int X = 124;
            int Y = 455;
            int i = 0;
            foreach (var cd in ThisPlayer.cards)
            {
                CardButton cardbtn = new CardButton();
                cardbtn.id = cd;
                cardbtn.btn.Tag = cd;
                cardbtn.btn.FlatStyle = FlatStyle.Flat;
                cardbtn.btn.FlatAppearance.BorderSize = 2;
                cardbtn.btn.BackgroundImageLayout = ImageLayout.Stretch;
                cardbtn.btn.Size = new Size(80, 120);
                cardbtn.btn.Location = new Point(X + i * 84, Y);
                cardbtn.X = X + i * 84;
                cardbtn.Y = Y;
                cardbtn.btn.Click += new EventHandler(cardBtn_Click);
                FetchImg(cardbtn.btn, cd);
                CardBtns[row].Add(cardbtn);
                Controls.Add(cardbtn.btn);
                i++;
            }

            CardsIdle();
        }
        public void FetchImg(Button btn, string cardid)
        {
            switch (cardid)
            {
                //Heart
                case "2H":
                    btn.BackgroundImage = Properties.Resources._2H;
                    break;
                case "3H":
                    btn.BackgroundImage = Properties.Resources._3H;
                    break;
                case "4H":
                    btn.BackgroundImage = Properties.Resources._4H;
                    break;
                case "5H":
                    btn.BackgroundImage = Properties.Resources._5H;
                    break;
                case "6H":
                    btn.BackgroundImage = Properties.Resources._6H;
                    break;
                case "7H":
                    btn.BackgroundImage = Properties.Resources._7H;
                    break;
                case "8H":
                    btn.BackgroundImage = Properties.Resources._8H;
                    break;
                case "9H":
                    btn.BackgroundImage = Properties.Resources._9H;
                    break;
                case "10H":
                    btn.BackgroundImage = Properties.Resources._10H;
                    break;
                case "JH":
                    btn.BackgroundImage = Properties.Resources._JH;
                    break;
                case "QH":
                    btn.BackgroundImage = Properties.Resources._QH;
                    break;
                case "KH":
                    btn.BackgroundImage = Properties.Resources._KH;
                    break;
                case "AH":
                    btn.BackgroundImage = Properties.Resources._AH;
                    break;
                //DIAMOND
                case "2D":
                    btn.BackgroundImage = Properties.Resources._2D;
                    break;
                case "3D":
                    btn.BackgroundImage = Properties.Resources._3D;
                    break;
                case "4D":
                    btn.BackgroundImage = Properties.Resources._4D;
                    break;
                case "5D":
                    btn.BackgroundImage = Properties.Resources._5D;
                    break;
                case "6D":
                    btn.BackgroundImage = Properties.Resources._6D;
                    break;
                case "7D":
                    btn.BackgroundImage = Properties.Resources._7D;
                    break;
                case "8D":
                    btn.BackgroundImage = Properties.Resources._8D;
                    break;
                case "9D":
                    btn.BackgroundImage = Properties.Resources._9D;
                    break;
                case "10D":
                    btn.BackgroundImage = Properties.Resources._10D;
                    break;
                case "JD":
                    btn.BackgroundImage = Properties.Resources._JD;
                    break;
                case "QD":
                    btn.BackgroundImage = Properties.Resources._QD;
                    break;
                case "KD":
                    btn.BackgroundImage = Properties.Resources._KD;
                    break;
                case "AD":
                    btn.BackgroundImage = Properties.Resources._AD;
                    break;
                //CLUB
                case "2C":
                    btn.BackgroundImage = Properties.Resources._2C;
                    break;
                case "3C":
                    btn.BackgroundImage = Properties.Resources._3C;
                    break;
                case "4C":
                    btn.BackgroundImage = Properties.Resources._4C;
                    break;
                case "5C":
                    btn.BackgroundImage = Properties.Resources._5C;
                    break;
                case "6C":
                    btn.BackgroundImage = Properties.Resources._6C;
                    break;
                case "7C":
                    btn.BackgroundImage = Properties.Resources._7C;
                    break;
                case "8C":
                    btn.BackgroundImage = Properties.Resources._8C;
                    break;
                case "9C":
                    btn.BackgroundImage = Properties.Resources._9C;
                    break;
                case "10C":
                    btn.BackgroundImage = Properties.Resources._10C;
                    break;
                case "JC":
                    btn.BackgroundImage = Properties.Resources._JC;
                    break;
                case "QC":
                    btn.BackgroundImage = Properties.Resources._QC;
                    break;
                case "KC":
                    btn.BackgroundImage = Properties.Resources._KC;
                    break;
                case "AC":
                    btn.BackgroundImage = Properties.Resources._AC;
                    break;
                // Spacing
                case "2S":
                    btn.BackgroundImage = Properties.Resources._2S;
                    break;
                case "3S":
                    btn.BackgroundImage = Properties.Resources._3S;
                    break;
                case "4S":
                    btn.BackgroundImage = Properties.Resources._4S;
                    break;
                case "5S":
                    btn.BackgroundImage = Properties.Resources._5S;
                    break;
                case "6S":
                    btn.BackgroundImage = Properties.Resources._6S;
                    break;
                case "7S":
                    btn.BackgroundImage = Properties.Resources._7S;
                    break;
                case "8S":
                    btn.BackgroundImage = Properties.Resources._8S;
                    break;
                case "9S":
                    btn.BackgroundImage = Properties.Resources._9S;
                    break;
                case "10S":
                    btn.BackgroundImage = Properties.Resources._10S;
                    break;
                case "JS":
                    btn.BackgroundImage = Properties.Resources._JS;
                    break;
                case "QS":
                    btn.BackgroundImage = Properties.Resources._QS;
                    break;
                case "KS":
                    btn.BackgroundImage = Properties.Resources._KS;
                    break;
                case "AS":
                    btn.BackgroundImage = Properties.Resources._AS;
                    break;
                case "":
                    btn.BackgroundImage = null;
                        break;





            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            foreach (string cardss in discardedCards)
            {
                int t = ClientSocket.GetRank(cardss);
                rankcards.Add(t);

            }
            if(discardedCards.Count > 1) 
            {
                SortLists(rankcards, discardedCards);
                SortLists(rankcards, discards);
            }

            
            int n = discardedCards.Count;
            ThisPlayer.numOfCards -= n;
            ClientSocket.datatype = "DISCARD";
            string cards = string.Join(";",discardedCards);
            string cardssss = string.Join(";", selectCardId2);


            string makemsg = ThisPlayer.name + ";" + ThisPlayer.numOfCards + ";" + cards + ";" + cardssss;
            ClientSocket.SendMessage(makemsg);


            buttonDiscard.Enabled = false;
            button2.Enabled = false;


            // display the discarded card.
            if (discardedCards.Count == 1)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = "";
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 2)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 3)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
                sanh.Clear();
            }
            else if (discardedCards.Count == 4)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 5)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 6)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                faceUpCard6 = discardedCards[5];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 7)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                faceUpCard6 = discardedCards[5];
                faceUpCard7 = discardedCards[6];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 8)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                faceUpCard6 = discardedCards[5];
                faceUpCard7 = discardedCards[6];
                faceUpCard8 = discardedCards[7];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 9)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                faceUpCard6 = discardedCards[5];
                faceUpCard7 = discardedCards[6];
                faceUpCard8 = discardedCards[7];
                faceUpCard9 = discardedCards[8];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }


            else if (discardedCards.Count == 10)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                faceUpCard6 = discardedCards[5];
                faceUpCard7 = discardedCards[6];
                faceUpCard8 = discardedCards[7];
                faceUpCard9 = discardedCards[8];
                faceUpCard10 = discardedCards[9];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 11)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                faceUpCard6 = discardedCards[5];
                faceUpCard7 = discardedCards[6];
                faceUpCard8 = discardedCards[7];
                faceUpCard9 = discardedCards[8];
                faceUpCard10 = discardedCards[9];
                faceUpCard11 = discardedCards[10];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            else if (discardedCards.Count == 12)
            {
                faceUpCard = discardedCards[0];
                faceUpCard2 = discardedCards[1];
                faceUpCard3 = discardedCards[2];
                faceUpCard4 = discardedCards[3];
                faceUpCard5 = discardedCards[4];
                faceUpCard6 = discardedCards[5];
                faceUpCard7 = discardedCards[6];
                faceUpCard8 = discardedCards[7];
                faceUpCard9 = discardedCards[8];
                faceUpCard10 = discardedCards[9];
                faceUpCard11 = discardedCards[10];
                faceUpCard12 = discardedCards[11];
                DisplayFaceUp();
                discardedCards.Clear();
                rankcards.Clear();
            }
            foreach (string cardsss in discards)
            {
                foreach (var cd in CardBtns[currentdisplayrow])
                {


                    if (cd.btn.Tag.ToString() == cardsss)

                    {
                        cd.btn.Visible = false;
                    }
                }
            
            }
            discards.Clear();

            /*foreach (var tb in tbnums)
             {
                 if (tb.Tag.ToString() == ThisPlayer.name)
                 {
                     tb.Text = ThisPlayer.numOfCards.ToString();
                     break;
                 }
             }*/

            CardsIdle();
        }
        private int currentdisplayrow = 0;
        private void button2_Click(object sender, EventArgs e) // nút skip
        {
            FetchImg(btnDiscardPileCard1, "");
            FetchImg(btnDiscardPileCard2, "");
            FetchImg(btnDiscardPileCard3, "");
            FetchImg(btnDiscardPileCard4, "");
            FetchImg(btnDiscardPileCard5, "");
            FetchImg(btnDiscardPileCard6, "");
            FetchImg(btnDiscardPileCard7, "");
            FetchImg(btnDiscardPileCard8, "");
            FetchImg(btnDiscardPileCard9, "");
            FetchImg(btnDiscardPileCard10, "");
            FetchImg(btnDiscardPileCard11, "");
            FetchImg(btnDiscardPileCard12, "");

            ClientSocket.datatype = "SKIP";
            string makemsg = ThisPlayer.name + ";" + ThisPlayer.numOfCards;

            ClientSocket.SendMessage(makemsg);
            buttonDiscard.Enabled = false;
            button2.Enabled = false;


        }
        //---------------------------------------
        public void DisplayCardsTemp()
        {
            foreach (var card in ThisPlayer.cards)
            {
                textBox1.Text += card + " ";
            }
        }
        // giao diện v
        public void DisplayFaceUp()
        {
            int a = discardedCards.Count;// discardedCards
            switch (a)
            {
                case 0:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    FetchImg(btnDiscardPileCard7, faceUpCard7);
                    FetchImg(btnDiscardPileCard8, faceUpCard8);
                    FetchImg(btnDiscardPileCard9, faceUpCard9);
                    FetchImg(btnDiscardPileCard10, faceUpCard10);
                    FetchImg(btnDiscardPileCard11, faceUpCard11);
                    FetchImg(btnDiscardPileCard12, faceUpCard12);
                    break;
                case 1:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    break;
                case 2:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    break;
                case 3:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    string value1 = faceUpCard.Substring(0, faceUpCard.Length - 1);
                    string value2 = faceUpCard2.Substring(0, faceUpCard2.Length - 1);
                    string value3 = faceUpCard3.Substring(0, faceUpCard3.Length - 1);
                    if (value1 == value2 && value1 == value3)
                    {
                        sanh.Clear();
                    }    
                    else
                    {

                        sanh = new List<string>() { value1, value2, value3 };
                    }    
                    break;
                case 4:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    break;
                case 5:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    break;
                case 6:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    break;
                case 7:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    FetchImg(btnDiscardPileCard7, faceUpCard7);
                    break;
                case 8:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    FetchImg(btnDiscardPileCard7, faceUpCard7);
                    FetchImg(btnDiscardPileCard8, faceUpCard8);
                    break;
                case 9:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    FetchImg(btnDiscardPileCard7, faceUpCard7);
                    FetchImg(btnDiscardPileCard8, faceUpCard8);
                    FetchImg(btnDiscardPileCard9, faceUpCard9);
                    break;
                case 10:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    FetchImg(btnDiscardPileCard7, faceUpCard7);
                    FetchImg(btnDiscardPileCard8, faceUpCard8);
                    FetchImg(btnDiscardPileCard9, faceUpCard9);
                    FetchImg(btnDiscardPileCard10, faceUpCard10);
                    break;
                case 11:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    FetchImg(btnDiscardPileCard7, faceUpCard7);
                    FetchImg(btnDiscardPileCard8, faceUpCard8);
                    FetchImg(btnDiscardPileCard9, faceUpCard9);
                    FetchImg(btnDiscardPileCard10, faceUpCard10);
                    FetchImg(btnDiscardPileCard11, faceUpCard11);
                    break;
                case 12:
                    FetchImg(btnDiscardPileCard1, faceUpCard);
                    FetchImg(btnDiscardPileCard2, faceUpCard2);
                    FetchImg(btnDiscardPileCard3, faceUpCard3);
                    FetchImg(btnDiscardPileCard4, faceUpCard4);
                    FetchImg(btnDiscardPileCard5, faceUpCard5);
                    FetchImg(btnDiscardPileCard6, faceUpCard6);
                    FetchImg(btnDiscardPileCard7, faceUpCard7);
                    FetchImg(btnDiscardPileCard8, faceUpCard8);
                    FetchImg(btnDiscardPileCard9, faceUpCard9);
                    FetchImg(btnDiscardPileCard10, faceUpCard10);
                    FetchImg(btnDiscardPileCard11, faceUpCard11);
                    FetchImg(btnDiscardPileCard12, faceUpCard12);
                    break;     

            }

        }

        public void InitDisplay()
        {
            ClientSocket.otherplayers.Sort((x, y) => x.turn.CompareTo(y.turn)); 

            labelName.Text = ThisPlayer.name;
            lbnames.Add(labelName);

            switch (ClientSocket.otherplayers.Count)
            {
                case 1:
                    {
                        labelNameU.Text = ClientSocket.otherplayers[0].name;
                        lbnames.Add(labelNameU);
                    }
                    break;
                case 2:
                    {
                        if (ThisPlayer.turn == 2)
                        {
                            labelNameL.Text = ClientSocket.otherplayers[1].name;
                            labelNameR.Text = ClientSocket.otherplayers[0].name;
                        }
                        else
                        {
                            labelNameL.Text = ClientSocket.otherplayers[0].name;
                            labelNameR.Text = ClientSocket.otherplayers[1].name;
                        }
                        lbnames.Add(labelNameL);
                        lbnames.Add(labelNameR);
                    }
                    break;
                case 3:
                    {
                        if (ThisPlayer.turn == 4)
                        {
                            labelNameL.Text = ClientSocket.otherplayers[0].name;
                            labelNameU.Text = ClientSocket.otherplayers[1].name;
                            labelNameR.Text = ClientSocket.otherplayers[2].name;
                        }
                        else if(ThisPlayer.turn == 1)
                        {
                            labelNameL.Text = ClientSocket.otherplayers[0].name;
                            labelNameU.Text = ClientSocket.otherplayers[1].name;
                            labelNameR.Text = ClientSocket.otherplayers[2].name;
                        }
                        else if (ThisPlayer.turn == 2)
                        {
                            labelNameL.Text = ClientSocket.otherplayers[1].name;
                            labelNameU.Text = ClientSocket.otherplayers[2].name;
                            labelNameR.Text = ClientSocket.otherplayers[0].name;
                        }
                        else
                        {
                            labelNameL.Text = ClientSocket.otherplayers[2].name;
                            labelNameU.Text = ClientSocket.otherplayers[0].name;
                            labelNameR.Text = ClientSocket.otherplayers[1].name;
                        }
                        lbnames.Add(labelNameL);
                        lbnames.Add(labelNameU);
                        lbnames.Add(labelNameR);
                    }
                    break;
            }
        }


        //---------------------------------------
        //METHODSECTIONS


        public string tempturnname = "";
        public void UndoHighlightTurn()
        {
            foreach (var n in lbnames)
            {
                if (n.Text == tempturnname)
                {
                    n.Font = new Font(n.Font, FontStyle.Regular);
                    n.ForeColor = Color.Black;
                    break;
                }
            }
        }
        public void UpdateNumOfCards(string name, string n)
        {
            foreach (var tb in tbnums)
            {
                if (tb.Tag.ToString() == name)
                {
                    tb.Text = n;
                }
            }
        }

        public void HighlightTurn(string name)
        {
            tempturnname = name;
            foreach (var n in lbnames)
            {
                if (n.Text == name)
                {
                    n.Font = new Font(n.Font, FontStyle.Bold);
                    n.ForeColor = Color.Red;
                    break;
                }
            }
        }

        //public List<Button> selectedCards = new List<Button>();

        //public List<string> selected = new List<string>();
        Dictionary<Button, bool> cardSelection = new Dictionary<Button, bool>();

        public List<string> discardedCards = new List<string>();
        List<int> rankcards = new List<int>();
        void cardBtn_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            selectedCardId = clickedButton.Tag.ToString();
            // Kiểm tra xem nút đã được chọn trước đó chưa
            if (cardSelection.ContainsKey(clickedButton))
            {
                // Nếu đã được chọn trước đó, đảo ngược trạng thái và cập nhật màu viền tương ứng
                cardSelection[clickedButton] = !cardSelection[clickedButton];
                if (cardSelection[clickedButton])
                {
                    clickedButton.FlatAppearance.BorderColor = Color.Chartreuse; // Đổi màu viền để đánh dấu đã chọn
                    discardedCards.Add(selectedCardId);
                    discards.Add(selectedCardId);
                }
                else
                {
                    clickedButton.FlatAppearance.BorderColor = Color.Black; // Đổi màu viền trở lại mặc định
                    discardedCards.Remove(selectedCardId);
                    discards.Remove(selectedCardId);
                }
            }
            else
            {
                // Nếu chưa được chọn trước đó, thêm nút vào Dictionary và đặt trạng thái là true (được chọn)
                cardSelection.Add(clickedButton, true);
                clickedButton.FlatAppearance.BorderColor = Color.Chartreuse; // Đổi màu viền để đánh dấu đã chọn
                discardedCards.Add(selectedCardId);
                discards.Add(selectedCardId);
            }


            //discardedCards.Add(selectedCardId);
            /*Button btn = (Button)sender; // card đã click

            if(selectedCards.Contains(btn))
            {
                selectedCards.Remove(btn);
                //btn.FlatAppearance.BorderColor = Color.Black;
            }   
            else
            {
                selectedCards.Add(btn);
                selectedCardId = btn.Tag.ToString();
                selected.Add(selectedCardId);
            }*/

            // selectedCardId = btn.Tag.ToString();
        }
        // void này được sử dụng để vô hiệu hóa những lá bài ko thể sử dụng trong trường hợp đó
        public void CardsIdle()
        {
            foreach (var row in CardBtns)
            {
                foreach (var cdbtn in row)
                {
                    cdbtn.btn.FlatAppearance.BorderColor = Color.Black;
                    cdbtn.btn.Enabled = false;
                }
            }
        }

        string GetCardName(Button cardButton)
        {
            return cardButton.Text;
        }

        private void progressBar3_Click(object sender, EventArgs e)
        {

        }

        private void btnDiscardPileCard1_Click(object sender, EventArgs e)
        {

        }
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

        private void GameTable_Load(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
           
        }
    }


}
