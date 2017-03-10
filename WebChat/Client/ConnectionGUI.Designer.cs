namespace Client {
    partial class ConnectionGUI {
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.serverIPLabel = new System.Windows.Forms.Label();
            this.servidorPortaLabel = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.statusBox = new System.Windows.Forms.ComboBox();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.logLabel = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logBox);
            this.groupBox1.Controls.Add(this.logLabel);
            this.groupBox1.Controls.Add(this.connectButton);
            this.groupBox1.Controls.Add(this.portBox);
            this.groupBox1.Controls.Add(this.ipBox);
            this.groupBox1.Controls.Add(this.statusBox);
            this.groupBox1.Controls.Add(this.nameBox);
            this.groupBox1.Controls.Add(this.servidorPortaLabel);
            this.groupBox1.Controls.Add(this.serverIPLabel);
            this.groupBox1.Controls.Add(this.statusLabel);
            this.groupBox1.Controls.Add(this.nameLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 325);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações de Conexão";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 28);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Nome:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 54);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 13);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Status:";
            // 
            // serverIPLabel
            // 
            this.serverIPLabel.AutoSize = true;
            this.serverIPLabel.Location = new System.Drawing.Point(12, 81);
            this.serverIPLabel.Name = "serverIPLabel";
            this.serverIPLabel.Size = new System.Drawing.Size(77, 13);
            this.serverIPLabel.TabIndex = 2;
            this.serverIPLabel.Text = "IP do Servidor:";
            // 
            // servidorPortaLabel
            // 
            this.servidorPortaLabel.AutoSize = true;
            this.servidorPortaLabel.Location = new System.Drawing.Point(12, 107);
            this.servidorPortaLabel.Name = "servidorPortaLabel";
            this.servidorPortaLabel.Size = new System.Drawing.Size(89, 13);
            this.servidorPortaLabel.TabIndex = 3;
            this.servidorPortaLabel.Text = "Porta do Servidor";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(107, 25);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(165, 20);
            this.nameBox.TabIndex = 4;
            // 
            // statusBox
            // 
            this.statusBox.FormattingEnabled = true;
            this.statusBox.Items.AddRange(new object[] {
            "Online",
            "Ocupado",
            "Ausente"});
            this.statusBox.Location = new System.Drawing.Point(107, 51);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(165, 21);
            this.statusBox.TabIndex = 5;
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(107, 78);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(165, 20);
            this.ipBox.TabIndex = 6;
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(107, 104);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(165, 20);
            this.portBox.TabIndex = 7;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(15, 154);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(257, 32);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Conectar";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(15, 193);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(28, 13);
            this.logLabel.TabIndex = 9;
            this.logLabel.Text = "Log:";
            // 
            // logBox
            // 
            this.logBox.FormattingEnabled = true;
            this.logBox.Location = new System.Drawing.Point(15, 210);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(257, 108);
            this.logBox.TabIndex = 10;
            // 
            // ConnectionGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 325);
            this.Controls.Add(this.groupBox1);
            this.Name = "ConnectionGUI";
            this.Text = "Conectar";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox logBox;
        private System.Windows.Forms.Label logLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.ComboBox statusBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label servidorPortaLabel;
        private System.Windows.Forms.Label serverIPLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label nameLabel;
    }
}