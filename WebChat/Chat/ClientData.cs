﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Chat {
    public class ClientData {
        public string Name { get; private set; }
        public int ID { get; private set; }

        public IPAddress IP { get; private set; }

        public ClientStatus Status { get; set; }


        public ClientData(string name, int id, IPAddress ip, ClientStatus status) {
            this.Name = name;
            this.ID = id;
            this.IP = ip;
            this.Status = status;
        }

        public override string ToString() {
            return "[" + ID + ":" + Name + "@" + IP.ToString() + " (" + Status + ")]";
        }
    }
}
