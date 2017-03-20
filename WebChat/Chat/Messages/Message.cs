using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public abstract class Message {
        public MessageType MsgType { get; private set; }


        public Message(MessageType type) {
            this.MsgType = type;
        }

        public abstract void Encode(BinaryWriter bw);

        public override string ToString() {
            return MsgType.ToString();
        }
    }
}
