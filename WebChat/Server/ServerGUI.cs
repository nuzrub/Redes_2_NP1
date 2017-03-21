using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat;

namespace Server {
    public partial class ServerGUI : Form {
        private ServerHandler handler;
        private bool closeRequested;


        public ServerGUI(ServerHandler handler) {
            InitializeComponent();
        }

        private void ServerGUI_FormClosing(object sender, FormClosingEventArgs e) {
            closeRequested = true;
        }
    }
}
