namespace Server {
    partial class ServerConnectionGUI {
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
            this.logBox = new System.Windows.Forms.ListBox();
            this.logLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.portBox = new System.Windows.Forms.TextBox();
            this.servidorPortaLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logBox);
            this.groupBox1.Controls.Add(this.logLabel);
            this.groupBox1.Controls.Add(this.connectButton);
            this.groupBox1.Controls.Add(this.portBox);
            this.groupBox1.Controls.Add(this.servidorPortaLabel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 325);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações de Conexão";
            // 
            // logBox
            // 
            this.logBox.FormattingEnabled = true;
            this.logBox.Location = new System.Drawing.Point(15, 106);
            this.logBox.Name = "logBox";
            this.logBox.Size = new System.Drawing.Size(257, 212);
            this.logBox.TabIndex = 10;
            // 
            // logLabel
            // 
            this.logLabel.AutoSize = true;
            this.logLabel.Location = new System.Drawing.Point(15, 86);
            this.logLabel.Name = "logLabel";
            this.logLabel.Size = new System.Drawing.Size(28, 13);
            this.logLabel.TabIndex = 9;
            this.logLabel.Text = "Log:";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(12, 51);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(257, 32);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Conectar";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(107, 25);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(165, 20);
            this.portBox.TabIndex = 7;
            // 
            // servidorPortaLabel
            // 
            this.servidorPortaLabel.AutoSize = true;
            this.servidorPortaLabel.Location = new System.Drawing.Point(15, 28);
            this.servidorPortaLabel.Name = "servidorPortaLabel";
            this.servidorPortaLabel.Size = new System.Drawing.Size(89, 13);
            this.servidorPortaLabel.TabIndex = 3;
            this.servidorPortaLabel.Text = "Porta do Servidor";
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
        private System.Windows.Forms.Label servidorPortaLabel;
    }
}