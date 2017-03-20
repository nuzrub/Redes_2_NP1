using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public class SendMessage : Message {
        public int From_ { get; private set; }
        public int To_ { get; private set; }
        public string Msg { get; private set; }

        public SendMessage(int from, int to, string msg) : base(MessageType.SendMessage) {
            this.From_ = from;
            this.To_ = to;
            this.Msg = msg;
        }

        public override void Encode(BinaryWriter bw) {
            // ID do comando, Status, Sizeof(Name), Nome
            bw.Write(Convert.ToInt32(MsgType));
            bw.Write(Convert.ToInt32(From_));
            bw.Write(Convert.ToInt32(To_));

            byte[] msgData = Encoding.UTF8.GetBytes(Msg);
            bw.Write(msgData.Length);
            bw.Write(msgData);
        }
    }
}
