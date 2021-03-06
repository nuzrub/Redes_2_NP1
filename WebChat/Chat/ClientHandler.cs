﻿using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using Chat.Messages;


namespace Chat {
    public class ClientHandler : ClientData {
        public Dictionary<int, ClientData> OtherClients { get; private set; }
        public Dictionary<int, StringBuilder> PrivateChats { get; private set; }
        public List<ClientData> RecentlyConnectedClients { get; private set; }
        public StringBuilder GlobalChat { get; private set; }

        private SocketHelper serverLink;
        private bool disconnected;


        private ClientHandler(string name, int id, ClientStatus status, SocketHelper serverLink) : base(name, id, status) {
            this.serverLink = serverLink;
            this.OtherClients = new Dictionary<int, ClientData>();
            this.PrivateChats = new Dictionary<int, StringBuilder>();
            this.RecentlyConnectedClients = new List<ClientData>();
            this.GlobalChat = new StringBuilder();
            this.disconnected = false;

            GlobalChat.AppendLine("[Sistema:] Você entrou no chat global.");
        }

        public void SendMessage(int destinationID, string message) {
            if (!disconnected) {
                if (destinationID == 0 || OtherClients[destinationID].Status != ClientStatus.Disconnected) {
                    SendMessage sm = new SendMessage(ID, destinationID, message);
                    serverLink.EnqueueForSending(sm);

                    ReceiveMessage(sm);
                }
            }
        }
        private void ReceiveMessage(SendMessage sm) {
            if (!disconnected) {
                // Mensagem que o cliente enviou
                if (sm.From_ == this.ID) {
                    if (sm.To_ == 0) {
                        GlobalChat.AppendLine("[" + this.Name + ":] " + sm.Msg);
                    } else {
                        PrivateChats[sm.To_].AppendLine("[" + this.Name + ":] " + sm.Msg);
                    }
                } else {
                    ClientData sender = OtherClients[sm.From_];
                    if (sm.To_ == 0) {
                        GlobalChat.AppendLine("[" + sender.Name + ":] " + sm.Msg);
                    } else {
                        // Todas as mensagens que não são globais devem ser pra esse cliente
                        Debug.Assert(sm.To_ == this.ID);
                        PrivateChats[sm.From_].AppendLine("[" + sender.Name + ":] " + sm.Msg);
                    }
                }
            }
        }
        public void ChangeStatus(ClientStatus newStatus) {
            // Para disconectar, usa-se o Disconnect e Kick, não changeStatus;
            Debug.Assert(newStatus != ClientStatus.Disconnected);

            if (!disconnected) {
                ChangeStatus cs = new ChangeStatus(ID, newStatus);
                serverLink.EnqueueForSending(cs);

                this.Status = newStatus;
                foreach (var key in PrivateChats.Keys) {
                    PrivateChats[key].AppendLine("[Sistema:] Você mudou seu estado para: " + newStatus);
                }
                GlobalChat.AppendLine("[Sistema:] Você mudou seu estado para: " + newStatus);
            }
        }
        public void AlertDisconnection() {
            if (!disconnected) {
                // Falar com o server primeiro.
                Disconnect d = new Disconnect(ID);
                serverLink.EnqueueForSending(d);

                Disconnect();
            }
        }
        public void Disconnect() {
            foreach (var key in PrivateChats.Keys) {
                PrivateChats[key].AppendLine("[Sistema:] Você se desconectou do sistema");
            }
            GlobalChat.AppendLine("[Sistema:] Você se desconectou do sistema");

            this.Status = ClientStatus.Disconnected;
            disconnected = true;
        }
        public void TurnOff() {
            // o sistema tem de garantir que a aplicação vai disconectar antes de desligar
            Debug.Assert(disconnected);
            serverLink.Dispose();
        }

        public void Update() {
            // Atualiza pra enviar/receber mensagens
            serverLink.Update();
            // Se tiver desconectado, desconsidera qualquer mensagem que possa
            // estar no link.
            if (disconnected) {
                return;
            }

            
            Message msg;
            while (true) {
                msg = serverLink.DequeueReceivedMessage();
                if (msg == null) {
                    break;
                } else {
                    Console.WriteLine(this.Name + " recebeu mensagem do tipo: " + msg.MsgType);
                    switch (msg.MsgType) {
                        case MessageType.NotifyNewClient:
                            NotifyNewClient nnc = (NotifyNewClient)msg;
                            ClientData newClient = new ClientData(nnc.ClientName, nnc.NewClientID, nnc.NewClientStatus);
                            OtherClients.Add(newClient.ID, newClient);
                            PrivateChats.Add(newClient.ID, new StringBuilder("[Sistema:] Essa é uma conversa privada entre você e " + nnc.ClientName + ".\n"));
                            GlobalChat.AppendLine("[Sistema:] " + nnc.ClientName + " se conectou no chat global.");
                            RecentlyConnectedClients.Add(newClient);
                            break;
                        case MessageType.SendMessage:
                            SendMessage sm = (SendMessage)msg;
                            ReceiveMessage(sm);
                            break;
                        case MessageType.ChangeStatus:
                            ChangeStatus cs = (ChangeStatus)msg;
                            OtherClients[cs.Who].Status = cs.NewStatus;
                            PrivateChats[cs.Who].AppendLine("[Sistema:] " + OtherClients[cs.Who].Name + " troucou seu estado para: " + cs.NewStatus + ".");
                            GlobalChat.AppendLine("[Sistema:] " + OtherClients[cs.Who].Name + " troucou seu estado para: " + cs.NewStatus + ".");
                            break;
                        case MessageType.Kick:
                            Kick k = (Kick)msg;
                            if (k.Who == this.ID) {
                                Disconnect();
                            } else {
                                OtherClients[k.Who].Status = ClientStatus.Disconnected;
                                PrivateChats[k.Who].AppendLine("[Sistema:] " + OtherClients[k.Who].Name + " se desconectou.");
                                GlobalChat.AppendLine("[Sistema:] " + OtherClients[k.Who].Name + " se desconectou.");
                            }
                            break;
                        default:
                            throw new ArgumentException("O cliente não deveria estar recebendo esse tipo de mensagem: " + msg.MsgType);
                    }
                }
            }
        }


        public static ClientHandler Connect(string name, ClientStatus status, IPAddress ip, int porta) {
            try {
                IPEndPoint remoteEP = new IPEndPoint(ip, porta);

                Socket serverRawLink = new Socket(
                    AddressFamily.InterNetwork,
                    SocketType.Stream,
                    ProtocolType.Tcp);

                try {
                    serverRawLink.Connect(remoteEP);
                    SocketHelper serverLink = new SocketHelper(serverRawLink);
                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(203 * 8);
                    
                    
                    RSAParameters clientKeys = rsa.ExportParameters(true); // true = exportar public E private key

                    // manda o modulus e expoent, q é a public key.
                    ConnectionRequest creq = new ConnectionRequest(status, name, clientKeys.Modulus, clientKeys.Exponent);
                    serverLink.EnqueueForSending(creq);
                    serverLink.Update();
                    Console.WriteLine("Connection Request sent by Client");

                    // Liga o RSA no lado cliente já pra receber encriptado, mas sem chaves válidas pra envio.
                    //serverLink.SetKeys(clientKeys, new RSAParameters());
                    while (true) {
                        serverLink.Update();
                        Message msg = serverLink.DequeueReceivedMessage();
                        if (msg != null) {
                            if (msg.MsgType != MessageType.ConnectionResponse) {
                                serverLink.RequeueReceivedMessage(msg);
                                Thread.Yield();
                            } else {
                                ConnectionResponse crep = (ConnectionResponse)msg;
                                Console.WriteLine("Connection Response Received at Client");

                                // recebe a public key (modulus + expoent) do server
                                RSAParameters serverKeys = new RSAParameters();
                                serverKeys.Modulus = crep.PublicKey;
                                serverKeys.Exponent = crep.Expoent;

                                // liga o RSA no link, tanto pra recebimento quanto envio.
                                serverLink.SetKeys(clientKeys, serverKeys);
                                return new ClientHandler(name, crep.ClientID, status, serverLink);
                            }
                        }
                    }
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
