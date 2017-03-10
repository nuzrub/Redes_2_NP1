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

namespace Client {
    public partial class ConnectionGUI : Form {
        public ConnectionGUI() {
            InitializeComponent();

            nameBox.Text = "Cliente";
            statusBox.SelectedIndex = 1;
            ipBox.Text = "127.0.0.1";
            portBox.Text = "7070";
        }

        private void Log(string message) {
            logBox.Items.Add(message);
        }
        private bool Connect() {
            #region Leitura e Validação
            string name = nameBox.Text;
            if (name == "") {
                Log("O nome não pode ser vazio!");
                return false;
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
                    return false;
            }

            IPAddress ip;
            if (!IPAddress.TryParse(ipBox.Text, out ip)) {
                Log("IP inválido!");
                return false;
            }

            int porta;
            if (!int.TryParse(portBox.Text, out porta)) {
                Log("Porta inválido!");
                return false;
            }
            if (porta > 65536) {
                Log("Porta inválido!");
                return false;
            }

            Log("Os valores de entrada foram lidos com sucesso.");
            #endregion

            return true;
        }
        private void connectButton_Click(object sender, EventArgs e) {
            nameBox.Enabled = false;
            statusBox.Enabled = false;
            ipBox.Enabled = false;
            portBox.Enabled = false;

            if (Connect()) {
                Log("Conexão estabelecida.");

                ClientGUI clientGUI = new ClientGUI();
                clientGUI.ShowDialog();

                Log("Conexão encerrada.");
                Log("");
            }

            nameBox.Enabled = true;
            statusBox.Enabled = true;
            ipBox.Enabled = true;
            portBox.Enabled = true;
        }
    }
}
