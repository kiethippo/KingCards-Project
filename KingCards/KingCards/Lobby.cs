using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KingCards
{
    public partial class Lobby : Form
    {
        public Lobby()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Server server = new Server();
            //server.ShowDialog();
            server.Show();
            //server = null;
            //this.Show();
            
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Client client = new Client();
            //client.ShowDialog();
            client.Show();
            //client = null;
            //this.Show();
        }
    }
}
