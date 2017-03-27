using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Chat;
using Chat.Messages;

namespace Client {
    public partial class ConnectionGUI : Form {
        public ConnectionGUI(string name, int status, string ip, string port) {
            InitializeComponent();

            nameBox.Text = name;
            statusBox.SelectedIndex = status;
            ipBox.Text = "127.0.0.1";
            portBox.Text = "7070";
        }

        private void Log(string message) {
            logBox.Items.Add(message);
        }


        private void connectButton_Click(object sender, EventArgs e) {
            nameBox.Enabled = false;
            statusBox.Enabled = false;
            ipBox.Enabled = false;
            portBox.Enabled = false;

            #region Leitura e Validação
            string name = nameBox.Text;
            if (name == "") {
                Log("O nome não pode ser vazio!");
                return;
            }


            ClientStatus status;
            switch (statusBox.SelectedIndex) {
                case 0:
                    status = ClientStatus.Online; break;
                case 1:
                    status = ClientStatus.Busy; break;
                case 2:
                    status = ClientStatus.Away; break;
                default:
                    Log("Status inválido!");
                    return;
            }

            IPAddress ip;
            if (!IPAddress.TryParse(ipBox.Text, out ip)) {
                Log("IP inválido!");
                return;
            }

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

            ClientHandler handler = ClientHandler.Connect(name, status, ip, porta);
            if (handler != null) {
                Log("Conexão TCP estabelecida.");

                ClientGUI clientGUI = new ClientGUI(handler);
                clientGUI.ShowDialog();
                
                Log("Conexão encerrada.");
                Log("");
            } else {
                Log("A conexão não foi estabelecida.");
                Log("");
            }
           

            nameBox.Enabled = true;
            statusBox.Enabled = true;
            ipBox.Enabled = true;
            portBox.Enabled = true;
        }
    }
}
