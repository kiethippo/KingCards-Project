using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client2
{
    public partial class ConnectMenu : Form
    {
        public static Lobby lobby;
        public ConnectMenu()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), 9000);
            ClientSocket.datatype = "CONNECT";
            ClientSocket.Connect(serverEP);
            lobby = new Lobby();
            ClientSocket.SendMessage(textBoxName.Text);

            ThisPlayer.name = textBoxName.Text;

            lobby.FormClosed += new FormClosedEventHandler(lobby_FormClosed);
            this.Hide();
            lobby.Show();
        }
        void lobby_FormClosed(object sender, EventArgs e)
        {
            ClientSocket.datatype = "DISCONNECT";
            ClientSocket.SendMessage(ThisPlayer.name);
            ClientSocket.clientSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            ClientSocket.clientSocket.Close();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse(textBoxIP.Text), 9000);
            ClientSocket.datatype = "CONNECT";
            ClientSocket.Connect(serverEP);
            lobby = new Lobby();
            ClientSocket.SendMessage(textBoxName.Text);

            ThisPlayer.name = textBoxName.Text;

            lobby.FormClosed += new FormClosedEventHandler(lobby_FormClosed);
            lobby.ShowStartButton();
            this.Hide();
            lobby.Show();
        }
    }
    }

