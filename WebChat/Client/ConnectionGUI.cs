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
        private void Connect(string name, ClientStatus status, IPAddress ip, int porta) {
            try {
                IPEndPoint remoteEP = new IPEndPoint(ip, porta);
 
                Socket clientSocket = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream, 
                    ProtocolType.Tcp);
  
                try {
                    clientSocket.Connect(remoteEP);
                    
                    Log("Conexão TCP estabelecida.");

                    using (NetworkStream ns = new NetworkStream(clientSocket)) {
                        BinaryWriter bw = new BinaryWriter(ns);
                        BinaryReader br = new BinaryReader(ns);

                        ConnectionRequest cr = new ConnectionRequest(status, name);
                        cr.Encode(bw);


                    }

                    ClientGUI clientGUI = new ClientGUI(clientSocket);
                    clientGUI.ShowDialog();

                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();

                    Log("Conexão encerrada.");
                    Log("");
                } catch (ArgumentNullException ane) {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                } catch (SocketException se) {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                } catch (Exception e) {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
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

            Connect(name, status, ip, porta);


            nameBox.Enabled = true;
            statusBox.Enabled = true;
            ipBox.Enabled = true;
            portBox.Enabled = true;
        }
    }
}
