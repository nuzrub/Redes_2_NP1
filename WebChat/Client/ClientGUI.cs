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
        private List<string> chats;

        private int currentChatID {
            get { return contactsListbox.SelectedIndex; }
        }
        private string currentChat {
            get { return chats[currentChatID]; }
            set { chats[currentChatID] = value; }
        }
        private string generalChat {
            get { return chats[0]; }
            set { chats[0] = value; }
        }

        public ClientGUI(ClientHandler handler) {
            InitializeComponent();

            this.handler = handler;
            statusLabel.Text = handler.Name + " (" + handler.Status + ")";
        }

        
        public void ReceiveMessage(int from_, int to_, string message) {

            if (to_ == 0) {
                generalChat += message;
            } else if (to_ == handler.ID) {
                chats[from_] += message;
            } else {
                throw new ArgumentException("Essa mensagem não deveria ter vindo pra esse cliente.");
            }
            
            UpdateChatBox();
        }
        private void UpdateChatBox() {
            chatBox.Text = currentChat;
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
    }
}
