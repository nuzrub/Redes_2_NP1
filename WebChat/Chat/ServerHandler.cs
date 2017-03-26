using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Chat.Messages;


namespace Chat {
    public class ServerHandler {
        private Thread connectionListenerThread;
        private Socket connectionListener;
        private List<ServerClientHandler> serverClientHandlers;
        
        private object disconnectMutex;
        private bool disconnectRequested;

        private object globalIdMutex;
        private int nextGlobalID;

        public ServerHandler(Socket connectionListener) {
            this.connectionListener = connectionListener;
            this.serverClientHandlers = new List<ServerClientHandler>();
            
            this.disconnectMutex = new object();
            this.disconnectRequested = false;
            this.globalIdMutex = new object();
            this.nextGlobalID = 1;

            StartListenerThread();
        }
        
        
        private void StartListenerThread() {
            connectionListenerThread = new Thread(ConnectionListenerTask);
            connectionListenerThread.Start();
        }

        private void ConnectionListenerTask() {
            while (true) {
                lock (disconnectMutex) {
                    if (disconnectRequested) {
                        break;
                    }
                }
                Console.WriteLine("Waiting for a connection...");

                Socket clientLink = connectionListener.Accept();
                serverClientHandlers.Add(new ServerClientHandler(this, clientLink));
                Console.WriteLine("Cliente conectado");
            }

            foreach (var sch in serverClientHandlers) {
                sch.NotifyDisconnection();
            }
        }
        

        public int RequestNewID() {
            int output;
            lock (globalIdMutex) {
                output = nextGlobalID;
                nextGlobalID++;
            }
            return output;
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
