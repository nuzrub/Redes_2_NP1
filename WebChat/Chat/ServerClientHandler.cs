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
        public void ForwardMessage(Message m) {
            link.EnqueueForSending(m);
        }
       


        private void ClientLinkTask() {
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
                            link.Update();

                            waitingForConnectionRequest = false;
                            parent.BroadcastNewClient(this);
                        } else {
                            Console.WriteLine(RemoteClientData.Name + " handler received message of type " + msg.MsgType + " from client " + RemoteClientData.ID);
                            switch (msg.MsgType) {
                                case MessageType.SendMessage:
                                    SendMessage sm = (SendMessage)msg;
                                    parent.BroadcastSendMessage(sm);
                                    break;
                                case MessageType.ChangeStatus:
                                    ChangeStatus cs = (ChangeStatus)msg;
                                    parent.BroadcastChangeStatus(cs);
                                    break;
                                case MessageType.Disconnect:
                                    Disconnect d = (Disconnect)msg;
                                    parent.HandleDisconnectRequest(d, this);
                                    break;
                                case MessageType.Kick:
                                    Kick k = (Kick)msg;
                                    parent.BroadcastKick(k);
                                    break;
                                default:
                                    throw new ArgumentException("O server não deveria estar recebendo esse tipo de mensagem: " + msg.MsgType);
                            }
                        }
                    }
                    // dá a vez
                    Thread.Yield();
                }
            }
        }
    }
}
