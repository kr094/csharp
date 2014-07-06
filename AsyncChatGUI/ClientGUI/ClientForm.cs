using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AsyncChat;

namespace AsyncChatGUI
{
    public partial class ClientForm : Form
    {
        ChatClient client;
        const string placeholder = "Type your message here...";
        const string messageSent = "Message Sent";

        public ClientForm()
        {
            InitializeComponent();
            client = new ChatClient();
            client.Client_Conn += new ClientConnHandler(this.Message_Received_Event);
            client.Client_Disc += new ClientDiscHandler(this.Message_Received_Event);
            client.Message_Received += new MessageReceivedHandler(this.Message_Received_Event);
            client.Message_Sent += new MessageSentHandler(this.Message_Sent_Event);
            tbMessage.Text = placeholder;
        }

        private delegate void ts_Message_Recieved_Event(ChatMessage message);

        private void Message_Received_Event(ChatMessage message)
        {
            if(this.rtbChat.InvokeRequired)
            {
                this.rtbChat.Invoke(new ts_Message_Recieved_Event(this.Message_Received_Event), message);
            }
            else
            {
                this.rtbChat.AppendText(message.getName() + ": " + message.getMessage() + Environment.NewLine);
            }
        }

        private void Message_Sent_Event(ChatMessage message)
        {
            this.rtbChat.AppendText(message.getName() + ": " + message.getMessage() + Environment.NewLine);
            tbMessage.Text = placeholder;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.tbMessage.Text))
                {
                    if (this.tbMessage.Text.Equals("quit"))
                    {
                        client.sendMessage(new ChatMessage("Client", "quit"));
                        this.client.disconnect();
                    }
                    else if (!this.tbMessage.Text.Equals(placeholder))
                    {
                        try
                        {
                            client.sendMessage(new ChatMessage("Client", this.tbMessage.Text));
                        }
                        catch (System.Net.Sockets.SocketException)
                        {
                            this.client.disconnect();
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Not connected to server!" + Environment.NewLine + "Use File->Connect to find a server.");
            }
        }

        private void tsbtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.client.connect();
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Could not find a server to connect to");
            }
        }

        private void tbMessage_Enter(object sender, EventArgs e)
        {
            if (tbMessage.Text.Equals(placeholder))
            {
                tbMessage.Text = String.Empty;
            }
        }

        private void tbMessage_Leave(object sender, EventArgs e)
        {
            if (tbMessage.Text.Equals(String.Empty))
            {
                tbMessage.Text = placeholder;
            }
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.client != null)
                this.client.disconnect();
        }

        private void tsbtnDisconnect_Click(object sender, EventArgs e)
        {
            if (this.client != null)
                this.client.disconnect();
        }

        private void tbMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                this.btnSend_Click(this.btnSend, new EventArgs());
                this.tbMessage.Text = String.Empty;
            }
        }
    }
}
