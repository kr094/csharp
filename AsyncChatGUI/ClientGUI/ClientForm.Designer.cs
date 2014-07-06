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
            this.picGame = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picGame)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbChat
            // 
            this.rtbChat.Location = new System.Drawing.Point(12, 200);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(471, 134);
            this.rtbChat.TabIndex = 0;
            this.rtbChat.Text = "";
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(12, 340);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(471, 20);
            this.tbMessage.TabIndex = 1;
            this.tbMessage.Enter += new System.EventHandler(this.tbMessage_Enter);
            this.tbMessage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMessage_KeyPress);
            this.tbMessage.Leave += new System.EventHandler(this.tbMessage_Leave);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(13, 366);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(470, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // picGame
            // 
            this.picGame.Image = global::AsyncChatGUI.Properties.Resources.rust;
            this.picGame.Location = new System.Drawing.Point(13, 27);
            this.picGame.Name = "picGame";
            this.picGame.Size = new System.Drawing.Size(470, 167);
            this.picGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picGame.TabIndex = 3;
            this.picGame.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(495, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnConnect,
            this.tsbtnDisconnect});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // tsbtnConnect
            // 
            this.tsbtnConnect.Name = "tsbtnConnect";
            this.tsbtnConnect.Size = new System.Drawing.Size(133, 22);
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
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 397);
            this.Controls.Add(this.picGame);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.rtbChat);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ClientForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picGame)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.PictureBox picGame;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsbtnConnect;
        private System.Windows.Forms.ToolStripMenuItem tsbtnDisconnect;
    }
}

