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
    public class SocketHelper {
        private Socket socket;
        private NetworkStream socketStream;
        private BinaryWriter writer;
        private BinaryReader reader;
        

        public SocketHelper(Socket socket) {
            this.socket = socket;
            this.socketStream = new NetworkStream(socket);
            this.writer = new BinaryWriter(socketStream);
            this.reader = new BinaryReader(socketStream);
        }

        public void WriteMessage(Message message) {
            message.Encode(writer);
            socketStream.Flush();
        }
        public Message ReadMessage() {
            int[] clientsIds;
            ClientStatus status;
            int id, from_, to_, size, who;
            string msg, name;

            MessageType type = (MessageType)reader.ReadInt32();
            switch (type) {
                case MessageType.ConnectionRequest:
                    status = (ClientStatus)reader.ReadInt32();
                    size = reader.ReadInt32();
                    name = Encoding.UTF8.GetString(reader.ReadBytes(size));
                    return new ConnectionRequest(status, name)
                case MessageType.ConnectionResponse:
                    id = reader.ReadInt32();
                    size = reader.ReadInt32();
                    clientsIds = new int[size];
                    for (int i = 0; i < size; i++) {
                        clientsIds[i] = reader.ReadInt32();
                    }
                    return new ConnectionResponse(id, clientsIds);
                case MessageType.SendMessage:
                    from_ = reader.ReadInt32();
                    to_ = reader.ReadInt32();
                    size = reader.ReadInt32();
                    msg = Encoding.UTF8.GetString(reader.ReadBytes(size));
                    return new SendMessage(from_, to_, msg);
                case MessageType.ChangeStatus:
                    who = reader.ReadInt32();
                    status = (ClientStatus)reader.ReadInt32();
                    return new ChangeStatus(who, status);
                case MessageType.Disconnect:
                    who = reader.ReadInt32();
                    return new Disconnect(who);
                case MessageType.Kick:
                    who = reader.ReadInt32();
                    return new Kick(who);
                default:
                    throw new ArgumentException("TIpo da mensagem não conhecido.");
            }
        }

        public void Dispose() {
            writer.Dispose();
            reader.Dispose();
            socketStream.Dispose();
            socket.Shutdown(SocketShutdown.Both);
            socket.Dispose();
        }
    }
}
