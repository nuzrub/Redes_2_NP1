using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public class ChangeStatus : Message {
        public int Who { get; private set; }
        public ClientStatus NewStatus { get; private set; }

        public ChangeStatus(int who, ClientStatus newStatus) : base(MessageType.ChangeStatus) {
            this.Who = who;
            this.NewStatus = newStatus;
        }

        public override void Encode(BinaryWriter bw) {
            // ID do comando, Status, Sizeof(Name), Nome
            bw.Write(Convert.ToInt32(MsgType));
            bw.Write(Who);
            bw.Write(Convert.ToInt32(NewStatus));
        }
    }
}
