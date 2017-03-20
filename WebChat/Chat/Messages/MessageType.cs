using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Chat.Messages {
    public enum MessageType {
        ConnectionRequest, 
        ConnectionResponse, 
        SendMessage, 
        ChangeStatus, 
        Disconnect, 
        Kick
    }
}
