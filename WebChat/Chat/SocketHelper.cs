using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Chat.Messages;

namespace Chat {
    /// <summary>
    /// Classe que encapsula o socket e realiza o envio/recebimento de mensagens da aplicação
    /// Possui funcionalidade de fila de envio e recebimento
    /// 
    /// RSA: o rsa é o provedor do algoritmo. A privatekey é a chave q vai ser usada pra descriptografar
    /// e a publickey a que vai ser usada para criptografar.
    /// </summary>
    public class SocketHelper {
        private Socket socket;
        private NetworkStream socketStream;
        private MemoryStream memoryStream;
        private BinaryWriter writer;
        private BinaryReader reader;
        private RSACryptoServiceProvider rsa;
        private RSAParameters privateKey;
        private RSAParameters publicKey;

        private Queue<Message> outboundMessages;
        private Queue<Message> inboundMessages;

        


        public SocketHelper(Socket socket) {
            this.socket = socket;
            this.socketStream = new NetworkStream(socket);
            this.memoryStream = new MemoryStream(1024);
            this.writer = new BinaryWriter(memoryStream);
            this.reader = new BinaryReader(memoryStream);
            
            this.outboundMessages = new Queue<Message>();
            this.inboundMessages = new Queue<Message>();
        }

        public IPEndPoint EndPoint() {
            return (IPEndPoint)socket.LocalEndPoint;
        }
        /// <summary>
        /// Set a private key que vai ser utilizada pra decriptografar e seta a
        /// public key que vai ser utilizada para criptografar.
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        public void SetKeys(RSAParameters privateKey, RSAParameters publicKey) {
            this.rsa = new RSACryptoServiceProvider();
            this.privateKey = privateKey;
            this.publicKey = publicKey;
        }

        public void Update() {
            Message msg;
            lock(inboundMessages) {
                while (socketStream.DataAvailable) {
                    msg = ReadMessage();
                    inboundMessages.Enqueue(msg);
                    Console.WriteLine("Lendo: " + msg.MsgType);
                }
            }
            lock (outboundMessages) {
                while (outboundMessages.Count > 0) {
                    msg = outboundMessages.Dequeue();
                    WriteMessage(msg);
                    Console.WriteLine("Escrevendo: " + msg.MsgType);
                }
            }
        }

        public void EnqueueForSending(Message msg) {
            lock (outboundMessages) {
                outboundMessages.Enqueue(msg);
            }
        }
        public Message DequeueReceivedMessage() {
            lock (inboundMessages) {
                if (inboundMessages.Count > 0) {
                    return inboundMessages.Dequeue();
                } else {
                    return null;
                }
            }
        }
        public void RequeueReceivedMessage(Message msg) {
            lock (inboundMessages) {
                inboundMessages.Enqueue(msg);
            }
        }


        private void WriteMessage(Message message) {
            message.Encode(writer);
            byte[] serializedData = memoryStream.ToArray();
            byte[] encryptedData = null;
            memoryStream.SetLength(0); // reseta o buffer.

            if (rsa != null) {
                rsa.ImportParameters(publicKey);
                encryptedData = rsa.Encrypt(serializedData, true);
            } else {
                encryptedData = serializedData;
            }
            // escreve o tamanho da mensagem (não criptografado)
            socketStream.WriteByte((byte)encryptedData.Length);
            // escreve a mensagem cifrada (criptografada)
            socketStream.Write(encryptedData, 0, encryptedData.Length);
            socketStream.Flush();
        }
        private Message ReadMessage() {
            // lê o tamanho da mensagem (não criptografado)
            int messageLength = socketStream.ReadByte();
            // cria o buffer para a mensagem e lê a mensagem toda.
            byte[] temporaryBuffer = new byte[messageLength];
            int readLength = 0;
            int remaining;
            do {
                remaining = messageLength - readLength;

                readLength += socketStream.Read(temporaryBuffer, readLength, remaining);
            } while (readLength < messageLength);

            byte[] encryptedData = new byte[messageLength];
            if (rsa != null) {
                rsa.ImportParameters(privateKey);
                encryptedData = rsa.Decrypt(temporaryBuffer, true);
            } else {
                Array.Copy(temporaryBuffer, encryptedData, messageLength);
            }
            
            memoryStream.SetLength(0); // reseta o buffer.
            memoryStream.Write(encryptedData, 0, encryptedData.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);

            ClientStatus status;
            int id, newClientID, from_, to_, size, who;
            string msg, name;
            byte[] modulus, expoent;

            MessageType type = (MessageType)reader.ReadInt32();
            Message output = null;
            switch (type) {
                case MessageType.ConnectionRequest:
                    status = (ClientStatus)reader.ReadInt32();
                    size = reader.ReadInt32();
                    name = Encoding.UTF8.GetString(reader.ReadBytes(size));
                    size = reader.ReadInt32();
                    modulus = reader.ReadBytes(size);
                    size = reader.ReadInt32();
                    expoent = reader.ReadBytes(size);
                    output = new ConnectionRequest(status, name, modulus, expoent);
                    break;
                case MessageType.ConnectionResponse:
                    id = reader.ReadInt32();
                    size = reader.ReadInt32();
                    modulus = reader.ReadBytes(size);
                    size = reader.ReadInt32();
                    expoent = reader.ReadBytes(size);
                    output = new ConnectionResponse(id, modulus, expoent);
                    break;
                case MessageType.NotifyNewClient:
                    newClientID = reader.ReadInt32();
                    status = (ClientStatus)reader.ReadInt32();
                    size = reader.ReadInt32();
                    name = Encoding.UTF8.GetString(reader.ReadBytes(size));
                    output = new NotifyNewClient(newClientID, status, name);
                    break;
                case MessageType.SendMessage:
                    from_ = reader.ReadInt32();
                    to_ = reader.ReadInt32();
                    size = reader.ReadInt32();
                    msg = Encoding.UTF8.GetString(reader.ReadBytes(size));
                    output = new SendMessage(from_, to_, msg);
                    break;
                case MessageType.ChangeStatus:
                    who = reader.ReadInt32();
                    status = (ClientStatus)reader.ReadInt32();
                    output = new ChangeStatus(who, status);
                    break;
                case MessageType.Disconnect:
                    who = reader.ReadInt32();
                    output = new Disconnect(who);
                    break;
                case MessageType.Kick:
                    who = reader.ReadInt32();
                    output = new Kick(who);
                    break;
            }
            memoryStream.SetLength(0); // reseta o buffer.
            if (output != null) {
                return output;
            } else {
                throw new ArgumentException("TIpo da mensagem não conhecido.");
            }
        }

        public void Dispose() {
            //writer.Dispose();
            //reader.Dispose();
            //socketStream.Dispose();
            //socket.Shutdown(SocketShutdown.Both);
            //socket.Dispose();
        }
    }
}
