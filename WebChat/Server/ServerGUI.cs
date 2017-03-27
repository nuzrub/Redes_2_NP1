using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat;

namespace Server {
    public partial class ServerGUI : Form {
        private ServerHandler handler;


        public ServerGUI(ServerHandler handler) {
            InitializeComponent();

            this.handler = handler;

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void UpdateUI() {
            for (int i = 0; i < clientList.Items.Count; i++) {
                clientList.Items[i].SubItems[2].Text = ClientStatus.Disconnected.ToString();
            }
            for (int i = 0; i < handler.ServerClientHandlers.Count; i++) {
                ServerClientHandler clientHandler = handler.ServerClientHandlers[i];
                ClientData client = clientHandler.RemoteClientData;

                if (client == null) {
                    continue;
                }
                
                if (client.ID - 1 == clientList.Items.Count) {
                    clientList.Items.Add(new ListViewItem(new string[] {
                        client.ID.ToString(),
                        client.Name,
                        client.Status.ToString(),
                        clientHandler.EndPoint().Address.ToString(),
                        clientHandler.EndPoint().Port.ToString()
                    }));
                } else {
                    clientList.Items[client.ID - 1].SubItems[2].Text = client.Status.ToString();
                }
            }
        }

        private void ServerGUI_FormClosing(object sender, FormClosingEventArgs e) {
            handler.Quit();
        }

        private void kickButton_Click(object sender, EventArgs e) {
            foreach (ListViewItem item in clientList.Items) {
                if (item.Checked) {
                    int clientId = item.Index + 1;
                    if (item.SubItems[2].Text != ClientStatus.Disconnected.ToString()) {
                        handler.Kick(clientId);
                    }
                }
            }
        }

        private void updateTimer_Tick(object sender, EventArgs e) {
            UpdateUI();
        }
    }
}
