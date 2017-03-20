using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public class ConnectionResponse : Message {
        public int ClientID { get; private set; }
        public int[] ConnectedClients { get; private set; }

        public ConnectionResponse(int id, int[] connectedClients) : base(MessageType.ConnectionResponse) {
            this.ClientID = id;
            this.ConnectedClients = connectedClients;
        }

        public override void Encode(BinaryWriter bw) {
            // ID do comando, Status, Sizeof(Name), Nome
            bw.Write(Convert.ToInt32(MsgType));
            bw.Write(ClientID);
            
            bw.Write(ConnectedClients.Length);
            for (int i = 0; i < ConnectedClients.Length; i++) {
                bw.Write(ConnectedClients[i]);
            }
        }
    }
}
