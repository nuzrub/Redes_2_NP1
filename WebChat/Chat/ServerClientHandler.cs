using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Chat.Messages;


namespace Chat {
    public class ServerClientHandler {
        public ClientData RemoteClientData { get; private set; }
        private Thread thread;
        private SocketHelper link;
        
        private ServerHandler parent;

        private object disconnectMutex;
        private bool disconnectRequested;

        public ServerClientHandler(ServerHandler parent, Socket rawLink) {
            this.parent = parent;
            this.link = new SocketHelper(rawLink);
            this.thread = new Thread(() => {
                ClientLinkTask();
            });

            this.disconnectMutex = new object();
            this.disconnectRequested = false;

            thread.Start();
        }

        public void NotifyDisconnection() {
            lock (disconnectMutex) {
                disconnectRequested = true;
            }
        }


        private void ClientLinkTask() {
            // receber o pedido de conexão
            // mandar a resposta
            // ficar olhando se tem algo pra mandar
            // se não tiver, fica olhando se tem algo pra receber
            bool waitingForConnectionRequest = true;
            while (true) {
                lock (disconnectMutex) {
                    if (disconnectRequested) {
                        break;
                    }
                }

                link.Update();
                while (true) {
                    Message msg = link.DequeueReceivedMessage();
                    if (msg == null) {
                        break;
                    } else {
                        if (waitingForConnectionRequest) {
                            Debug.Assert(msg.MsgType == MessageType.ConnectionRequest);

                            ConnectionRequest creq = (ConnectionRequest)msg;
                            int id = parent.RequestNewID();

                            RemoteClientData = new ClientData(creq.ClientName, id, creq.InitialStatus);

                            ConnectionResponse crep = new ConnectionResponse(id);
                            link.EnqueueForSending(crep);

                            waitingForConnectionRequest = false;
                        } else {
                            Console.WriteLine("Received message of type " + msg.MsgType + " from client " + RemoteClientData.ID);
                        }
                    }
                }
            }
        }
    }
}
