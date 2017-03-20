using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public class Disconnect : Message {
        public int Who { get; private set; }

        public Disconnect(int who) : base(MessageType.Disconnect) {
            this.Who = who;
        }

        public override void Encode(BinaryWriter bw) {
            // ID do comando, Status, Sizeof(Name), Nome
            bw.Write(Convert.ToInt32(MsgType));
            bw.Write(Who);
        }
    }
}
