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
        /// <summary>
        /// Client class from the library
        /// </summary>
        ChatClient client;
        /// <summary>
        /// Function delegate to ensure thread saftey
        /// </summary>
        /// <param name="message"></param>
        private delegate void ts_Message_Recieved_Event(MessageArgs message);

        /// <summary>
        /// Setting up UI
        /// </summary>
        const string placeholder = "Type your message here...";
        const string messageSent = "Message Sent";
        private bool connected = false;

        /// <summary>
        /// public ctor
        /// </summary>
        public ClientForm()
        {
            InitializeComponent();
            client = new ChatClient();
            ///Hook up message delegates
            client.Client_Conn += new ClientConnHandler(this.Message_Received_Event);
            client.Client_Disc += new ClientDiscHandler(this.Message_Received_Event);
            client.Message_Received += new MessageReceivedHandler(this.Message_Received_Event);
            client.Message_Sent += new MessageSentHandler(this.Message_Sent_Event);
            tbMessage.Enabled = false;
            tbMessage.Text = placeholder;
        }

        /// <summary>
        /// Events
        /// Message and client events handled here
        /// </summary>
        /// <param name="message"></param>
        private void Message_Received_Event(MessageArgs message)
        {
            if (message.getMessage().Equals("quit"))
            {
                this.connected = false;
                this.client.disconnect();
            }
                ///Thread saftey dance
            else if(this.rtbChat.InvokeRequired)
            {
                ///Set the address of this function into the null delegate ts_
                ///Then run ts_ (Recieved_Event) on the thread that owns 
                ///The windows form handle of this.rtbChat (Invoke)
                this.rtbChat.Invoke(new ts_Message_Recieved_Event(this.Message_Received_Event), message);
            }
                ///When ts_ executes on the handle's native thread
                ///this else will run and the text will get outputted as normal
            else
            {
                this.rtbChat.AppendText(message.getName() + ": " + message.getMessage() + Environment.NewLine);
                this.rtbChat.ScrollToCaret();
                if (!this.connected)
                    this.tbMessage.Enabled = false;
            }
        }

        /// <summary>
        /// The primary function dealing with updating the client UI
        /// </summary>
        /// <param name="message"></param>
        private void Message_Sent_Event(MessageArgs message)
        {
            this.rtbChat.AppendText(message.getName() + ": " + message.getMessage() + Environment.NewLine);
            tbMessage.Text = placeholder;
        }

        #region "Forms"
        /// <summary>
        /// Windows form controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (this.connected)
            {
                try
                {
                    if (!String.IsNullOrEmpty(this.tbMessage.Text))
                    {
                        if (this.tbMessage.Text.Equals("quit"))
                        {
                            client.sendMessage(new MessageArgs("Client", "quit"));
                            this.client.disconnect();
                        }
                        else if (!this.tbMessage.Text.Equals(placeholder))
                        {
                            try
                            {
                                client.sendMessage(new MessageArgs("Client", this.tbMessage.Text));
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
        }

        private void tsbtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.client != null)
                {
                    this.client.connect();
                    this.connected = true;
                }
                this.tbMessage.Enabled = true;
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Could not find a server to connect to");
            }
        }

        private void tsbtnDisconnect_Click(object sender, EventArgs e)
        {
            this.connected = false;
            if (this.client != null)
            {
                this.client.disconnect();
            }
            this.tbMessage.Enabled = false;
        }

        private void tbMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                this.btnSend_Click(this.btnSend, new EventArgs());
                this.tbMessage.Text = String.Empty;
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
            try
            {
                this.connected = false;
                if (this.client != null)
                    this.client.disconnect();
            }
            catch (Exception)
            {
                MessageBox.Show("More than 1 client running? Change that, and then try again.");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
