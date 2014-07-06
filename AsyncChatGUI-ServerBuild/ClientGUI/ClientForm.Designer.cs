namespace AsyncChatGUI
{
    public partial class ClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.msNetwork = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.picGame = new System.Windows.Forms.PictureBox();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGame)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbChat
            // 
            this.rtbChat.Location = new System.Drawing.Point(13, 310);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(514, 134);
            this.rtbChat.TabIndex = 0;
            this.rtbChat.Text = "";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(13, 450);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(514, 20);
            this.tbMessage.TabIndex = 1;
            this.tbMessage.Enter += new System.EventHandler(this.tbMessage_Enter);
            this.tbMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMessage_KeyPress);
            this.tbMessage.Leave += new System.EventHandler(this.tbMessage_Leave);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(14, 476);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(513, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // msNetwork
            // 
            this.msNetwork.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.fileToolStripMenuItem});
            this.msNetwork.Location = new System.Drawing.Point(0, 0);
            this.msNetwork.Name = "msNetwork";
            this.msNetwork.Size = new System.Drawing.Size(539, 24);
            this.msNetwork.TabIndex = 4;
            this.msNetwork.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnConnect,
            this.tsbtnDisconnect});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.fileToolStripMenuItem.Text = "Network";
            // 
            // tsbtnConnect
            // 
            this.tsbtnConnect.Name = "tsbtnConnect";
            this.tsbtnConnect.Size = new System.Drawing.Size(152, 22);
            this.tsbtnConnect.Text = "Connect";
            this.tsbtnConnect.Click += new System.EventHandler(this.tsbtnConnect_Click);
            // 
            // tsbtnDisconnect
            // 
            this.tsbtnDisconnect.Name = "tsbtnDisconnect";
            this.tsbtnDisconnect.Size = new System.Drawing.Size(152, 22);
            this.tsbtnDisconnect.Text = "Disconnect";
            this.tsbtnDisconnect.Click += new System.EventHandler(this.tsbtnDisconnect_Click);
            // 
            // picGame
            // 
            this.picGame.Image = global::AsyncChatGUI.Properties.Resources.rust_fire;
            this.picGame.Location = new System.Drawing.Point(13, 27);
            this.picGame.Name = "picGame";
            this.picGame.Size = new System.Drawing.Size(514, 277);
            this.picGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGame.TabIndex = 3;
            this.picGame.TabStop = false;
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 509);
            this.Controls.Add(this.picGame);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.rtbChat);
            this.Controls.Add(this.msNetwork);
            this.MainMenuStrip = this.msNetwork;
            this.Name = "ClientForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AsyncChat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.msNetwork.ResumeLayout(false);
            this.msNetwork.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.PictureBox picGame;
        private System.Windows.Forms.MenuStrip msNetwork;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbtnConnect;
        private System.Windows.Forms.ToolStripMenuItem tsbtnDisconnect;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

