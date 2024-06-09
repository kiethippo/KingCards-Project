using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client2

{
    public partial class Lobby : Form
    {
        public Lobby lobby;
        public List<Label> PlayerName = new List<Label>();
        public int connectedPlayer = 0;
        public Lobby()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            lobby = this;
            buttonStart.Visible = false;


            PlayerName.Add(label1);
            PlayerName.Add(label2);
            PlayerName.Add(label3);
            PlayerName.Add(label4);
        }
        public void Tempdisplay(string msg)
        {
            richTextBox1.Text += msg + '\n';
        } 

        public void DisplayConnectedPlayer(string name)
        {
            // Tạo một hàm để cập nhật label từ một luồng khác
           
                connectedPlayer++;
                switch (connectedPlayer)
                {
                    case 1:
                        label1.Text = name;
                        break;
                    case 2:
                        label2.Text = name;
                        break;
                    case 3:
                        label3.Text = name;
                        break;
                    case 4:
                        label4.Text = name;
                        break;
                    default:
                        break;
                
            };

            
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            ClientSocket.datatype = "START";
            ClientSocket.SendMessage("");
        }
        public void ShowStartButton()
        {
            buttonStart.Visible = true;
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}