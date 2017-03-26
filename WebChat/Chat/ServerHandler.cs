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

        public int RequestNewID() {
            int output;
            lock (globalIdMutex) {
                output = nextGlobalID;
                nextGlobalID++;
            }
            return output;
        }

        public void BroadcastNewClient(ServerClientHandler newClientHandler) {
            ClientData nc = newClientHandler.RemoteClientData;
            NotifyNewClient nnc = new NotifyNewClient(nc.ID, nc.Status, nc.Name);

            // Avisar os outros do cara novo
            foreach (var handler in serverClientHandlers) {
                if (handler != newClientHandler) {
                    handler.ForwardMessage(nnc);
                }
            }
            // Avisar o cara novo dos outros
            foreach (var handler in serverClientHandlers) {
                if (handler != newClientHandler) {
                    nc = handler.RemoteClientData;
                    nnc = new NotifyNewClient(nc.ID, nc.Status, nc.Name);
                    newClientHandler.ForwardMessage(nnc);
                }
            }
        }
        public void BroadcastSendMessage(SendMessage sm) {
            foreach (var handler in serverClientHandlers) {
                if (handler.RemoteClientData.ID != sm.From_) {
                    handler.ForwardMessage(sm);
                }
            }
        }
        public void BroadcastChangeStatus(ChangeStatus cs) {
            foreach (var handler in serverClientHandlers) {
                if (handler.RemoteClientData.ID != cs.Who) {
                    handler.ForwardMessage(cs);
                }
            }
        }
        public void HandleDisconnectRequest(Disconnect d, ServerClientHandler handler) {
            Kick k = new Kick(d.Who);
            handler.ForwardMessage(k);
            BroadcastKick(k);
        }
        public void BroadcastKick(Kick k) {
            ServerClientHandler targetHandler = null;
            foreach (var handler in serverClientHandlers) {
                if (handler.RemoteClientData.ID == k.Who) {
                    targetHandler = handler;
                } else {
                    handler.ForwardMessage(k);
                }
            }
            Debug.Assert(targetHandler != null);
            serverClientHandlers.Remove(targetHandler);
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

                // dá a vez
                Thread.Yield();
            }

            foreach (var sch in serverClientHandlers) {
                sch.NotifyDisconnection();
            }
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
