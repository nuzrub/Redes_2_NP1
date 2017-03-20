using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Client {
    public partial class ClientGUI : Form {
        private Socket clientSocket;

        public ClientGUI(Socket clientSocket) {
            InitializeComponent();

            this.clientSocket = clientSocket;
        }
    }
}
