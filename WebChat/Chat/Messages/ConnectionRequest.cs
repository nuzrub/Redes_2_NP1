using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public class ConnectionRequest : Message {
        public ClientStatus InitialStatus { get; private set; }
        public string ClientName { get; private set; }
        public byte[] PublicKey { get; private set; }
        public byte[] Expoent { get; private set; }

        public ConnectionRequest(ClientStatus initialStatus, string clientName, byte[] pk, byte[] e) : base(MessageType.ConnectionRequest) {
            this.InitialStatus = initialStatus;
            this.ClientName = clientName;
            this.PublicKey = pk;
            this.Expoent = e;
        }

        public override void Encode(BinaryWriter bw) {
            // ID do comando, Status, Sizeof(Name), Nome
            bw.Write(Convert.ToInt32(MsgType));
            bw.Write(Convert.ToInt32(InitialStatus));

            byte[] nameData = Encoding.UTF8.GetBytes(ClientName);
            bw.Write(nameData.Length);
            bw.Write(nameData);

            bw.Write(PublicKey.Length);
            bw.Write(PublicKey);
            bw.Write(Expoent.Length);
            bw.Write(Expoent);
        }
    }
}
