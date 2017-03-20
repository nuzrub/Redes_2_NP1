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
        public ServerConnectionGUI() {
            InitializeComponent();
            
            portBox.Text = "7070";
        }

        private void Log(string message) {
            logBox.Items.Add(message);
        }
        private void Connect(int porta) {
            IPEndPoint localEndPoint = new IPEndPoint(
                new IPAddress(new byte[] { 127, 0, 0, 1 }), 
                porta);
            
            Socket serverSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream, 
                ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try {
                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(10);

                Log("Server inicializado na porta " + portBox.Text + ".");

                ServerGUI serverGUI = new ServerGUI(serverSocket);
                serverGUI.ShowDialog();

                Log("Server encerrado.");
                Log("");
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
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

            Connect(porta);


            portBox.Enabled = true;
        }
    }
}
