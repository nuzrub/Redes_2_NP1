using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using Chat.Messages;


namespace Chat {
    public class ServerHandler {
        public List<ServerClientHandler> ServerClientHandlers { get; private set; }
        private Thread connectionListenerThread;
        private TcpListener connectionListener;
        private RSACryptoServiceProvider rsa;
        
        private object disconnectMutex;
        private bool disconnectRequested;

        private object globalIdMutex;
        private int nextGlobalID;

        public ServerHandler(TcpListener connectionListener) {
            this.connectionListener = connectionListener;
            this.ServerClientHandlers = new List<ServerClientHandler>();
            
            this.disconnectMutex = new object();
            this.disconnectRequested = false;
            this.globalIdMutex = new object();
            this.nextGlobalID = 1;

            this.rsa = new RSACryptoServiceProvider(203 * 8);

            connectionListenerThread = new Thread(ConnectionListenerTask);
            connectionListenerThread.Start();
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
            foreach (var handler in ServerClientHandlers) {
                if (handler != newClientHandler) {
                    handler.ForwardMessage(nnc);
                }
            }
            // Avisar o cara novo dos outros
            foreach (var handler in ServerClientHandlers) {
                if (handler != newClientHandler) {
                    nc = handler.RemoteClientData;
                    nnc = new NotifyNewClient(nc.ID, nc.Status, nc.Name);
                    newClientHandler.ForwardMessage(nnc);
                }
            }
        }
        public void BroadcastSendMessage(SendMessage sm) {
            if (sm.To_ == 0) {
                foreach (var handler in ServerClientHandlers) {
                    if (handler.RemoteClientData.ID != sm.From_) {
                        handler.ForwardMessage(sm);
                    }
                }
            } else {
                foreach (var handler in ServerClientHandlers) {
                    if (handler.RemoteClientData.ID != sm.From_) {
                        if (handler.RemoteClientData.ID == sm.To_) {
                            handler.ForwardMessage(sm);
                            break;
                        }
                    }
                }
            }
        }
        public void BroadcastChangeStatus(ChangeStatus cs) {
            foreach (var handler in ServerClientHandlers) {
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
            foreach (var handler in ServerClientHandlers) {
                if (handler.RemoteClientData.ID == k.Who) {
                    targetHandler = handler;
                } else {
                    handler.ForwardMessage(k);
                }
            }
            Debug.Assert(targetHandler != null);
            ServerClientHandlers.Remove(targetHandler);
        }
        public void Kick(int ID) {
            Kick k = new Kick(ID);

            ServerClientHandler targetHandler = null;
            foreach (var handler in ServerClientHandlers) {
                if (handler.RemoteClientData.ID == ID) {
                    targetHandler = handler;
                } else {
                    handler.ForwardMessage(k);
                }
            }
            Debug.Assert(targetHandler != null);
            targetHandler.ForwardMessage(k);

            ServerClientHandlers.Remove(targetHandler);
        }

        public void Quit() {
            foreach (var handler in ServerClientHandlers) {
                Kick k = new Kick(handler.RemoteClientData.ID);
                handler.ForwardMessage(k);
            }
            lock (disconnectMutex) {
                disconnectRequested = true;
            }
            // ter certeza q todos os quits foram enviados.
            Thread.Sleep(200);

            foreach (var handler in ServerClientHandlers) {
                handler.NotifyDisconnection();
            }
            ServerClientHandlers.Clear();
        }

        

        private void ConnectionListenerTask() {
            connectionListener.Start();

            Console.WriteLine("Esperando conexões...");
            while (true) {
                lock (disconnectMutex) {
                    if (disconnectRequested) {
                        break;
                    }
                }
                if (connectionListener.Pending()) {
                    Socket clientLink = connectionListener.AcceptSocket();

                    ServerClientHandlers.Add(new ServerClientHandler(this, rsa, clientLink));
                    Console.WriteLine("Cliente conectado.");
                    Console.WriteLine("Esperando conexões...");
                }
                // dá a vez
                Thread.Yield();
            }

            connectionListener.Stop();
            Console.WriteLine("Conexão fechada\n\n\n");
        }

        public static ServerHandler Connect(IPAddress ip, int porta) {
            var connectionListener = new TcpListener(ip, porta);
            
            try {
                return new ServerHandler(connectionListener);
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }
            return null;
        }
    }
}
