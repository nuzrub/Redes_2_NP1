using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace Chat {
    public class ClientHandler : ClientData {
        private Socket serverLink;

        private ClientHandler(string name, int id, ClientStatus status, Socket serverLink) : base(name, id, status) {
            this.serverLink = serverLink;

        }

        public void SendMessage(int destinationID, string message) {

        }
        public void ChangeStatus(ClientStatus newStatus) {

        }
        public void Disconnect() {
            // Falar com o server primeiro.
            serverLink.Shutdown(SocketShutdown.Both);
            serverLink.Close();
        }


        public static ClientHandler Connect(string name, ClientStatus status, IPAddress ip, int porta) {
            try {
                IPEndPoint remoteEP = new IPEndPoint(ip, porta);

                Socket serverLink = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);

                try {
                    serverLink.Connect(remoteEP);

                    // Conectar com o server de fato pra pegar o ID.

                    return new ClientHandler(name, 0, status, serverLink);

                    
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

            return null;
        }
    }
}
