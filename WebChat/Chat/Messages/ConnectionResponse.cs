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
        /// <summary>
        /// Public Key do Servidor
        /// </summary>
        public byte[] PublicKey { get; private set; }
        public byte[] Expoent { get; private set; }


        public ConnectionResponse(int id, byte[] pk, byte[] e) : base(MessageType.ConnectionResponse) {
            this.ClientID = id;
            this.PublicKey = pk;
            this.Expoent = e;
        }

        public override void Encode(BinaryWriter bw) {
            // ID do comando, Status, Sizeof(Name), Nome
            bw.Write(Convert.ToInt32(MsgType));
            bw.Write(ClientID);

            bw.Write(PublicKey.Length);
            bw.Write(PublicKey);
            bw.Write(Expoent.Length);
            bw.Write(Expoent);
        }
    }
}
