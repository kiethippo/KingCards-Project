using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KingCards
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private UdpClient udpClient;
        private IPEndPoint serverEndPoint;

        private void button1_Click(object sender, EventArgs e)
        {
            string portStr = textBox1.Text.Trim();


            if (string.IsNullOrEmpty(portStr))
            {
                MessageBox.Show("Please enter Port!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!int.TryParse(portStr, out int port))
            {
                MessageBox.Show("Port must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConnectServer(port);
        }

        Socket client;
        IPEndPoint IP;


        public void ConnectServer(int port)
        {

            string serverIp = "127.0.0.1";

            udpClient = new UdpClient();
            serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIp), port);

            try
            {
                udpClient.Connect(serverEndPoint);
                MessageBox.Show("Connected to server successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SocketException)
            {
                MessageBox.Show("Server is not listening!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Wrong port number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
