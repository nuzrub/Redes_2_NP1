using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat;

namespace Server {
    public partial class ServerGUI : Form {
        private Socket serverSocket;
        private ServerData serverData;


        public ServerGUI(Socket serverSocket) {
            InitializeComponent();

            this.serverSocket = serverSocket;
        }
    }
}
