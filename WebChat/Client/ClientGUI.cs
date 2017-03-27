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
        private Dictionary<int, int> indexIDMapping;
        private Dictionary<int, int> IDIndexMapping;
        private int currentChatID {
            get { return indexIDMapping[contactsListbox.SelectedIndex]; }
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
            this.indexIDMapping = new Dictionary<int, int>();
            this.IDIndexMapping = new Dictionary<int, int>();
            indexIDMapping.Add(0, 0);
            IDIndexMapping.Add(0, 0);

            contactsListbox.Items.Add("Global Chat");
            contactsListbox.SelectedIndex = 0;

            UpdateUI();
        }


        private void UpdateUI() {
            handler.Update();
            statusLabel.Text = handler.Name + " (" + handler.Status + ")";

            if (handler.RecentlyConnectedClients.Count > 0) {
                foreach (var client in handler.RecentlyConnectedClients) {
                    indexIDMapping.Add(contactsListbox.Items.Count, client.ID);
                    IDIndexMapping.Add(client.ID, contactsListbox.Items.Count);
                    contactsListbox.Items.Add(client.Name);
                }
                handler.RecentlyConnectedClients.Clear();
            }

            foreach (var key in indexIDMapping.Keys) {
                if (key == 0) {
                    continue;
                }

                var client = handler.OtherClients[indexIDMapping[key]];
                contactsListbox.Items[key] = client.Name + " (" + client.Status + ")";
            }

            UpdateChatBox();
            Application.DoEvents();
        }
        private void Disconnect() {
            handler.AlertDisconnection();
        }

        private void UpdateChatBox() {
            chatBox.Text = currentChatText;

            chatBox.SelectionStart = chatBox.Text.Length;
            chatBox.ScrollToCaret();
        }

        private void sendButton_Click(object sender, EventArgs e) {
            string message = typeMessageBox.Text;

            if (message != "") {
                message = message.Replace('\n', ' ');
                message = message.Replace('\r', ' ');
                handler.SendMessage(currentChatID, message);
                typeMessageBox.Text = "";

                chatBox.SelectionStart = chatBox.Text.Length;
                chatBox.ScrollToCaret();
            }
        }

        private void desconectarToolStripMenuItem_Click(object sender, EventArgs e) {
            Disconnect();
        }

        private void contactsListbox_SelectedIndexChanged(object sender, EventArgs e) {
            if (contactsListbox.SelectedIndex != -1) {
                UpdateChatBox();
            }
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

        private void ClientGUI_FormClosing(object sender, FormClosingEventArgs e) {
            Disconnect();
            handler.TurnOff();
        }

        private void typeMessageBox_KeyPress(object sender, KeyPressEventArgs e) {
            // 13 é a tecla enter
            if (e.KeyChar == (char)13) {
                sendButton_Click(null, null);
            }
        }
    }
}
