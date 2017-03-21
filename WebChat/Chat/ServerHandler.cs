using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat {
    public class ServerHandler {
        private ClientList Clients { get; private set; }
        private Thread connectionListenerThread;
        private Socket connectionListener;
        private List<SocketHelper> ClientLinks;
        private object disconnectMutex;
        private bool disconnectRequested;

        public ServerHandler(Socket connectionListener) {
            this.Clients = new ClientList();
            this.ClientLinks = new List<SocketHelper>();
            this.connectionListener = connectionListener;

            StartListenerThread();
        }
        
        
        private void StartListenerThread() {
            var connectionListenerThread = new Thread(() => {
                while (true) {
                    lock (disconnectMutex) {
                        if (disconnectRequested) {
                            break;
                        }
                    }
                    Console.WriteLine("Waiting for a connection...");

                    Socket clientHandler = connectionListener.Accept();
                    Console.WriteLine("Cliente conectado");
                    
                }
            });
            connectionListenerThread.Start();
        }


        public static ServerHandler Connect(int porta) {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Loopback, porta);

            var connectionListener = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Stream,
                ProtocolType.Tcp);


            try {
                connectionListener.Bind(localEndPoint);
                connectionListener.Listen(10);

                return new ServerHandler(connectionListener);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

    }
}
