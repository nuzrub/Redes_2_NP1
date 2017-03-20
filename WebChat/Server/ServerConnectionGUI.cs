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
        private bool Connect() {
            #region Leitura e Validação
            
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

            byte[] bytes = new Byte[1024];
            string data;
            
            IPEndPoint localEndPoint = new IPEndPoint(
                new IPAddress(new byte[] { 127, 0, 0, 1 }), 
                porta);
            
            Socket listener = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream, 
                ProtocolType.Tcp);

            // Bind the socket to the local endpoint and   
            // listen for incoming connections.  
            try {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true) {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (true) {
                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1) {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

            return true;
        }
        private void connectButton_Click(object sender, EventArgs e) {
            portBox.Enabled = false;

            if (Connect()) {
                Log("Server inicializado na porta " + portBox.Text + ".");

                ServerGUI serverGUI = new ServerGUI();
                serverGUI.ShowDialog();

                Log("Server encerrado.");
                Log("");
            }
            
            portBox.Enabled = true;
        }
    }
}
