using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat {
    public class ServerData {
        public ClientList Clients { get; private set; }

        public ServerData() {
            Clients = new ClientList();
        }

        public void Broadcast() {

        }
    }
}
