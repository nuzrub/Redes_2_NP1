using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using Chat;

namespace Client {
    public partial class ClientGUI : Form {
        private ClientHandler handler;
        private Dictionary<int, int> idIndexMapping;
        private int currentChatID {
            get { return idIndexMapping[contactsListbox.SelectedIndex]; }
        }
        private string currentChatText {
            get {
                if (currentChatID == 0) {
                    return handler.GlobalChat.ToString();
                } else {
                    return handler.PrivateChats[currentChatID].ToString();
                }
            }
        }

        public ClientGUI(ClientHandler handler) {
            InitializeComponent();

            this.handler = handler;
            this.idIndexMapping = new Dictionary<int, int>();
            idIndexMapping.Add(0, 0);

            contactsListbox.Items.Add("Global Chat");
            contactsListbox.SelectedIndex = 0;

            UpdateUI();
        }


        private void UpdateUI() {
            handler.Update();
            statusLabel.Text = handler.Name + " (" + handler.Status + ")";

            if (handler.RecentlyConnectedClients.Count > 0) {
                foreach (var client in handler.RecentlyConnectedClients) {
                    idIndexMapping.Add(contactsListbox.Items.Count, client.ID);
                    contactsListbox.Items.Add(client.Name);
                }
                handler.RecentlyConnectedClients.Clear();
            }

            foreach (var key in idIndexMapping.Keys) {
                if (key == 0) {
                    continue;
                }

                var client = handler.OtherClients[idIndexMapping[key]];
                contactsListbox.Items[key] = client.Name + "(" + client.Status + ")";
            }

            UpdateChatBox();
            Application.DoEvents();
        }

        private void UpdateChatBox() {
            chatBox.Text = currentChatText;
        }

        private void sendButton_Click(object sender, EventArgs e) {
            string message = typeMessageBox.Text;

            if (message != "") {
                handler.SendMessage(currentChatID, message);
            }
        }

        private void desconectarToolStripMenuItem_Click(object sender, EventArgs e) {
            handler.Disconnect();
        }

        private void contactsListbox_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateChatBox();
        }

        private void ChangeStatus(ClientStatus newStatus) {
            handler.ChangeStatus(newStatus);
            statusLabel.Text = handler.Name + " (" + handler.Status + ")";
        }

        private void disponívelToolStripMenuItem_Click(object sender, EventArgs e) {
            ChangeStatus(ClientStatus.Online);
        }
        private void ocupadoToolStripMenuItem_Click(object sender, EventArgs e) {
            ChangeStatus(ClientStatus.Busy);
        }
        private void ausenteToolStripMenuItem_Click(object sender, EventArgs e) {
            ChangeStatus(ClientStatus.Away);
        }

        private void updateTimer_Tick(object sender, EventArgs e) {
            UpdateUI();
        }
    }
}
