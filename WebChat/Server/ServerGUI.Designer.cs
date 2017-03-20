namespace Server {
    partial class ServerGUI {
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.servidorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desconectarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.clientList = new System.Windows.Forms.ListView();
            this.nomeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ipHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.portaHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.salaHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.kickButton = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.servidorToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(561, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // servidorToolStripMenuItem
            // 
            this.servidorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desconectarToolStripMenuItem});
            this.servidorToolStripMenuItem.Name = "servidorToolStripMenuItem";
            this.servidorToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.servidorToolStripMenuItem.Text = "Servidor";
            // 
            // desconectarToolStripMenuItem
            // 
            this.desconectarToolStripMenuItem.Name = "desconectarToolStripMenuItem";
            this.desconectarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.desconectarToolStripMenuItem.Text = "Encerrar";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 294);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(561, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // clientList
            // 
            this.clientList.CheckBoxes = true;
            this.clientList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nomeHeader,
            this.ipHeader,
            this.portaHeader,
            this.salaHeader});
            this.clientList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientList.FullRowSelect = true;
            this.clientList.GridLines = true;
            this.clientList.Location = new System.Drawing.Point(0, 24);
            this.clientList.Name = "clientList";
            this.clientList.Size = new System.Drawing.Size(561, 270);
            this.clientList.TabIndex = 3;
            this.clientList.UseCompatibleStateImageBehavior = false;
            this.clientList.View = System.Windows.Forms.View.Details;
            // 
            // nomeHeader
            // 
            this.nomeHeader.Text = "Nome";
            this.nomeHeader.Width = 158;
            // 
            // ipHeader
            // 
            this.ipHeader.Text = "IP";
            this.ipHeader.Width = 97;
            // 
            // portaHeader
            // 
            this.portaHeader.Text = "Porta";
            // 
            // salaHeader
            // 
            this.salaHeader.Text = "Sala";
            this.salaHeader.Width = 235;
            // 
            // kickButton
            // 
            this.kickButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kickButton.Location = new System.Drawing.Point(0, 271);
            this.kickButton.Name = "kickButton";
            this.kickButton.Size = new System.Drawing.Size(561, 23);
            this.kickButton.TabIndex = 4;
            this.kickButton.Text = "Kickar Selecionados";
            this.kickButton.UseVisualStyleBackColor = true;
            // 
            // ServerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 316);
            this.Controls.Add(this.kickButton);
            this.Controls.Add(this.clientList);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ServerGUI";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerGUI_FormClosing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem servidorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desconectarToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ListView clientList;
        private System.Windows.Forms.ColumnHeader nomeHeader;
        private System.Windows.Forms.ColumnHeader ipHeader;
        private System.Windows.Forms.ColumnHeader portaHeader;
        private System.Windows.Forms.ColumnHeader salaHeader;
        private System.Windows.Forms.Button kickButton;
    }
}

