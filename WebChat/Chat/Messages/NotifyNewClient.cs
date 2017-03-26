using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public class NotifyNewClient : Message {
        public int NewClientID { get; private set; }
        public ClientStatus NewClientStatus { get; private set; }
        public string ClientName { get; private set; }

        public NotifyNewClient(int newClientID, ClientStatus newClientStatus, string clientName) : base(MessageType.ConnectionRequest) {
            this.NewClientID = newClientID;
            this.NewClientStatus = newClientStatus;
            this.ClientName = clientName;
        }

        public override void Encode(BinaryWriter bw) {
            // ID do comando, Status, Sizeof(Name), Nome
            bw.Write(Convert.ToInt32(MsgType));
            bw.Write(NewClientID);
            bw.Write(Convert.ToInt32(NewClientStatus));

            byte[] nameData = Encoding.UTF8.GetBytes(ClientName);
            bw.Write(nameData.Length);
            bw.Write(nameData);
        }
    }
}
