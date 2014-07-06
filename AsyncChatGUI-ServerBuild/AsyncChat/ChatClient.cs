using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncChat;

namespace AsyncChat
{
    public class ChatClient : IChat
    {
        /// <summary>
        /// Messaging objects
        /// </summary>
        private string data;
        private byte[] bytes = new byte[1024];

        /// <summary>
        /// Network objects
        /// </summary>
        private TcpClient client;
        private NetworkStream stream;

        /// <summary>
        /// Thread listening objects
        /// </summary>
        private Thread listener;
        private bool listening = true;

        /// <summary>
        /// Event delegate definitions
        /// The client hooks into this
        /// The server would have similar objects and also hook into them
        /// </summary>
        public event MessageSentHandler Message_Sent;
        public event MessageReceivedHandler Message_Received;
        public event ClientConnHandler Client_Conn;
        public event ClientDiscHandler Client_Disc;
        
        /// <summary>
        /// Public Ctor
        /// </summary>
        public ChatClient()
        {
            this.client = new TcpClient();
            this.stream = null;
            this.listening = false;
            this.listener = new Thread(this.listen);
        }

        /// <summary>
        /// Gets the last string of data received by the TcpClient
        /// </summary>
        /// <returns>The message sent as a string</returns>
        public string getData()
        {
            return this.data;
        }

        /// <summary>
        /// The method called by public implementations like the chat app
        /// In order to send a message over TCP
        /// </summary>
        /// <param name="message"></param>
        public void sendMessage(MessageArgs message)
        {
            this.writeStream(message.getMessage());
            if(Message_Sent != null)
                Message_Sent(message);
        }

        /// <summary>
        /// The helper method for writing a message to the standard TCP Stream
        /// </summary>
        /// <param name="message"></param>
        public void writeStream(string message)
        {
            try
            {
                bytes = new byte[1024];
                data = null;
                bytes = Encoding.ASCII.GetBytes(message);
                stream.Write(bytes, 0, bytes.Length);
            }
            catch (System.ObjectDisposedException)
            {

            }
            catch (Exception)
            {
                this.disconnect();
                if (this.Client_Disc != null)
                    this.Client_Disc(new MessageArgs("Server", "Error: Closed!"));
            }
        }

        /// <summary>
        /// The method that is run by another thread
        /// </summary>
        public void listen()
        {
            while (this.listening)
            {
                if (this.stream.DataAvailable)
                {
                    this.bytes = new byte[1024];
                    this.data = null;
                    int bytesRec = stream.Read(bytes, 0, 1024);
                    this.data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    if(Message_Received != null)
                        Message_Received(new MessageArgs("Server", this.data));
                }
            }
        }

        /// <summary>
        /// Method called to attempt to connect to server
        /// Throws SocketException
        /// </summary>
        public void connect()
        {
            if (client != null)
            {
                if (!client.Connected)
                {
                    //create new client with static definitions
                    this.client = new TcpClient(NetworkInfo.IpString, NetworkInfo.port);
                    if (Client_Conn != null)
                        Client_Conn(new MessageArgs("Client", "Connected"));
                    this.stream = client.GetStream();
                    this.listening = true;
                    if (this.listener.ThreadState == ThreadState.Unstarted)
                        this.listener.Start();
                    else if (this.listener.ThreadState == ThreadState.Stopped)
                    {
                        this.listener = new Thread(this.listen);
                        this.listener.Start();
                    }
                }
            }
        }

        /// <summary>
        /// The method called when 'quit' is sent over the network, or disconnect button pressed.
        /// </summary>
        public void disconnect()
        {
            if (this.listening)
            {
                try
                {
                    ///Kill the server with quit and safely try to shut down
                    this.writeStream("quit");
                    this.listening = false;
                    if (this.stream != null)
                        this.stream.Close();
                    if (this.client != null)
                        this.client.Close();
                    if (this.Client_Disc != null)
                        Client_Disc(new MessageArgs("Client", "Disconnected"));
                }
                catch (System.IO.IOException)
                {
                    if (this.Client_Disc != null)
                        Client_Disc(new MessageArgs("Server", "Shut down"));
                }
            }
        }
    }
}
