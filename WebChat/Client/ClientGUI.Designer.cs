namespace Client {
    partial class ClientGUI {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientGUI));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desconectarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ausenteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ocupadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disponívelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.sidePanel = new System.Windows.Forms.GroupBox();
            this.contactsListbox = new System.Windows.Forms.ListBox();
            this.mainPanel = new System.Windows.Forms.GroupBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.typeMessageBox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.limiteLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.sidePanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clienteToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desconectarToolStripMenuItem});
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.clienteToolStripMenuItem.Text = "Cliente";
            // 
            // desconectarToolStripMenuItem
            // 
            this.desconectarToolStripMenuItem.Name = "desconectarToolStripMenuItem";
            this.desconectarToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.desconectarToolStripMenuItem.Text = "Desconectar";
            this.desconectarToolStripMenuItem.Click += new System.EventHandler(this.desconectarToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.statusLabel,
            this.limiteLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 374);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 26);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ausenteToolStripMenuItem,
            this.ocupadoToolStripMenuItem,
            this.disponívelToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButton1.Text = "changeStatusButton";
            // 
            // ausenteToolStripMenuItem
            // 
            this.ausenteToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.ausenteToolStripMenuItem.Name = "ausenteToolStripMenuItem";
            this.ausenteToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.ausenteToolStripMenuItem.Text = "Ausente";
            this.ausenteToolStripMenuItem.Click += new System.EventHandler(this.ausenteToolStripMenuItem_Click);
            // 
            // ocupadoToolStripMenuItem
            // 
            this.ocupadoToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ocupadoToolStripMenuItem.Name = "ocupadoToolStripMenuItem";
            this.ocupadoToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.ocupadoToolStripMenuItem.Text = "Ocupado";
            this.ocupadoToolStripMenuItem.Click += new System.EventHandler(this.ocupadoToolStripMenuItem_Click);
            // 
            // disponívelToolStripMenuItem
            // 
            this.disponívelToolStripMenuItem.BackColor = System.Drawing.Color.Lime;
            this.disponívelToolStripMenuItem.Name = "disponívelToolStripMenuItem";
            this.disponívelToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.disponívelToolStripMenuItem.Text = "Disponível";
            this.disponívelToolStripMenuItem.Click += new System.EventHandler(this.disponívelToolStripMenuItem_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(66, 21);
            this.statusLabel.Text = "statusLabel";
            // 
            // sidePanel
            // 
            this.sidePanel.Controls.Add(this.contactsListbox);
            this.sidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanel.Location = new System.Drawing.Point(0, 24);
            this.sidePanel.Name = "sidePanel";
            this.sidePanel.Size = new System.Drawing.Size(200, 350);
            this.sidePanel.TabIndex = 2;
            this.sidePanel.TabStop = false;
            this.sidePanel.Text = "Contatos";
            // 
            // contactsListbox
            // 
            this.contactsListbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactsListbox.FormattingEnabled = true;
            this.contactsListbox.Location = new System.Drawing.Point(3, 16);
            this.contactsListbox.Name = "contactsListbox";
            this.contactsListbox.Size = new System.Drawing.Size(194, 331);
            this.contactsListbox.TabIndex = 0;
            this.contactsListbox.SelectedIndexChanged += new System.EventHandler(this.contactsListbox_SelectedIndexChanged);
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.splitContainer);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(200, 24);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(600, 350);
            this.mainPanel.TabIndex = 3;
            this.mainPanel.TabStop = false;
            this.mainPanel.Text = "Mensagens";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 16);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.chatBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.typeMessageBox);
            this.splitContainer.Panel2.Controls.Add(this.sendButton);
            this.splitContainer.Size = new System.Drawing.Size(594, 331);
            this.splitContainer.SplitterDistance = 273;
            this.splitContainer.TabIndex = 0;
            // 
            // chatBox
            // 
            this.chatBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatBox.Location = new System.Drawing.Point(0, 0);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(594, 273);
            this.chatBox.TabIndex = 0;
            this.chatBox.Text = "";
            // 
            // typeMessageBox
            // 
            this.typeMessageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeMessageBox.Location = new System.Drawing.Point(0, 0);
            this.typeMessageBox.Name = "typeMessageBox";
            this.typeMessageBox.Size = new System.Drawing.Size(459, 54);
            this.typeMessageBox.TabIndex = 1;
            this.typeMessageBox.Text = "";
            this.typeMessageBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.typeMessageBox_KeyPress);
            // 
            // sendButton
            // 
            this.sendButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.sendButton.Location = new System.Drawing.Point(459, 0);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(135, 54);
            this.sendButton.TabIndex = 0;
            this.sendButton.Text = "Enviar";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // limiteLabel
            // 
            this.limiteLabel.Name = "limiteLabel";
            this.limiteLabel.Size = new System.Drawing.Size(65, 21);
            this.limiteLabel.Text = "limiteLabel";
            // 
            // ClientGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.sidePanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ClientGUI";
            this.Text = "Cliente";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientGUI_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.sidePanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desconectarToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.GroupBox sidePanel;
        private System.Windows.Forms.ListBox contactsListbox;
        private System.Windows.Forms.GroupBox mainPanel;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.RichTextBox typeMessageBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem ausenteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ocupadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disponívelToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripStatusLabel limiteLabel;
    }
}

