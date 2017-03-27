using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Chat;

namespace Server {
    public partial class ServerConnectionGUI: Form {
        public ServerConnectionGUI(string porta) {
            InitializeComponent();
            
            portBox.Text = porta;
        }

        private void Log(string message) {
            logBox.Items.Add(message);
        }

        private void connectButton_Click(object sender, EventArgs e) {
            portBox.Enabled = false;

            #region Leitura e Validação

            int porta;
            if (!int.TryParse(portBox.Text, out porta)) {
                Log("Porta inválido!");
                return;
            }
            if (porta > 65536) {
                Log("Porta inválido!");
                return;
            }

            Log("Os valores de entrada foram lidos com sucesso.");
            #endregion

            ServerHandler handler = ServerHandler.Connect(IPAddress.Loopback, porta);
            if (handler != null) {
                Log("Server inicializado na porta " + portBox.Text + ".");

                ServerGUI serverGUI = new ServerGUI(handler);
                serverGUI.ShowDialog();

                Log("Server encerrado.");
                Log("");
            }

            portBox.Enabled = true;
        }
    }
}
