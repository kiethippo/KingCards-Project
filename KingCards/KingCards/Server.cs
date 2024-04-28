using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace KingCards
{
    public partial class Server : Form
    {
        private UdpClient udpServer;
        private IPEndPoint anyIP;
        public Server()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string portStr = textBox1.Text.Trim();
            if (!int.TryParse(portStr, out int port))
            {
                MessageBox.Show("Port must be a number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //ConnectClient(port);
            if (port >= 1000 && port <= 9999)
                {

                /*
                 //khởi tạo server và chạy ở cổng port ta nhập
                 udpServer = new UdpClient(port);
                 anyIP = new IPEndPoint(IPAddress.Any, 0);

                 MessageBox.Show("Phòng đã được tạo", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


             }
             catch(Exception ex)
             {
                 MessageBox.Show("Đã xảy ra lỗi khi tạo phòng: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             } */


                ConnectClient(port);




        }
        else if (port > 9999)
        {
            MessageBox.Show("Vui lòng nhập 4 chữ số cho phòng: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else if (port < 1000)
        {
            MessageBox.Show("Vui lòng nhập 4 chữ số cho phòng: ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        } 


}



private void Server_FormClosing(object sender, FormClosingEventArgs e)
{
    // Đóng UdpClient trước khi đóng form
    if (udpServer != null)
    {
        udpServer.Close();
    }
}

private void textBox1_TextChanged(object sender, EventArgs e)
{

}

Socket server;
IPEndPoint IP;
List<Socket> clientList;

public void ConnectClient(int port) //Tạo server
{
    /* clientList = new List<Socket>();
     //IP : địa chỉ của Server
     IP = new IPEndPoint(IPAddress.Any, port);
     server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

     server.Bind(IP);
     MessageBox.Show("Phòng đã được tạo", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


     /*Thread Listen = new Thread(() =>
     {
         server.Listen(10);
         Socket client = server.Accept();
         clientList.Add(client);
     });*/

                try
                {
                // Khởi tạo server và chạy ở cổng 8080
                udpServer = new UdpClient(port);
                anyIP = new IPEndPoint(IPAddress.Any, 0);

                MessageBox.Show("Server started successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Bắt đầu nhận tin nhắn từ client
                //udpServer.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while starting the server: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
