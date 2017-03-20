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
        private Socket serverSocket;
        private ServerData serverData;
        private bool closeRequested;


        public ServerGUI(Socket serverSocket) {
            InitializeComponent();

            this.serverSocket = serverSocket;
            Thread t = new Thread(() => {
                while(!closeRequested) {
                    Console.WriteLine("Waiting for a connection...");

                    Socket clientHandler = serverSocket.Accept();
                    Console.WriteLine("Cliente conectado");

                    // An incoming connection needs to be processed.  
                    string data = "";
                    while (true) {
                        byte[] bytes = new byte[1024];
                        int bytesRec = clientHandler.Receive(bytes);
                        data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf('\0') > -1) {
                            break;
                        }
                    }

                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    clientHandler.Send(msg);
                    clientHandler.Shutdown(SocketShutdown.Both);
                    clientHandler.Close();
                }
            });
        }

        private void ServerGUI_FormClosing(object sender, FormClosingEventArgs e) {
            closeRequested = true;
        }
    }
}
