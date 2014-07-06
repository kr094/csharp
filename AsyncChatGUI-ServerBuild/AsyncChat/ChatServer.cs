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
    public class ChatServer : IChat
    {
        private string data;
        private byte[] bytes = new byte[1024];

        private TcpListener tcpListener;
        private TcpClient client;
        private NetworkStream stream;

        private Thread threadListener;
        private bool listening = true;

        public event MessageSentHandler Message_Sent;
        public event MessageReceivedHandler Message_Received;
        public event ClientConnHandler Client_Conn;
        public event ClientDiscHandler Client_Disc;
        
        public ChatServer()
        {
            this.tcpListener = new TcpListener(NetworkInfo.IpAddr, NetworkInfo.port);
            this.client = null;
            this.stream = null;
            this.listening = true;
            this.threadListener = new Thread(this.listen);
        }

        /// <summary>
        /// Gets the last string of data received by the TcpClient
        /// </summary>
        /// <returns>The message sent as a string</returns>
        public string getData()
        {
            return this.data;
        }

        public void sendMessage(MessageArgs message)
        {
            bytes = new byte[1024];
            data = null;
            bytes = Encoding.ASCII.GetBytes(message.getMessage());
            stream.Write(bytes, 0, bytes.Length);

            if(Message_Sent != null)
                Message_Sent(message);
        }

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
                        Message_Received(new MessageArgs("Client", this.data));
                }
            }
        }

        public void connect()
        {
                //start listening for a client
                this.tcpListener.Start();
                if (Client_Conn != null)
                    Client_Conn(new MessageArgs("Server", "Started on " + NetworkInfo.IpString + ":" + NetworkInfo.portString + Environment.NewLine + "Awaiting Client..."));
                //wait for them to connect
                this.client = this.tcpListener.AcceptTcpClient();
                if (Client_Conn != null)
                    Client_Conn(new MessageArgs("Client", "Joined Server"));
                //and get their stream
                this.stream = client.GetStream();
                this.listening = true;
                if (this.threadListener.ThreadState == ThreadState.Unstarted)
                    this.threadListener.Start();
                else if (this.threadListener.ThreadState == ThreadState.Stopped)
                {
                    this.threadListener = new Thread(this.listen);
                    this.threadListener.Start();
                }
        }

        public void disconnect()
        {
            this.listening = false;
            this.stream.Close();
            this.client.Close();
            this.tcpListener.Stop();
            Client_Disc(new MessageArgs("Server", "Closed"));
        }
    }
}
