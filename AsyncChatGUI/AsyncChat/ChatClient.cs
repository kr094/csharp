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
        private string data;
        private byte[] bytes = new byte[1024];
        private TcpClient client;
        private NetworkStream stream;

        private Thread listener;
        private bool listening = true;

        public event MessageSentHandler Message_Sent;
        public event MessageReceivedHandler Message_Received;
        public event ClientConnHandler Client_Conn;
        public event ClientDiscHandler Client_Disc;
        
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

        public void sendMessage(ChatMessage message)
        {
            this.writeStream(message.getMessage());
            if(Message_Sent != null)
                Message_Sent(message);
        }

        public void writeStream(string message)
        {
            bytes = new byte[1024];
            data = null;
            bytes = Encoding.ASCII.GetBytes(message);
            stream.Write(bytes, 0, bytes.Length);
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
                        Message_Received(new ChatMessage("Server", this.data));
                }
            }
        }

        public void connect()
        {
            //create new client with static definitions
            this.client = new TcpClient(NetworkInfo.IpString, NetworkInfo.port);
            this.stream = client.GetStream();
            this.listening = true;
            if (this.listener.ThreadState == ThreadState.Unstarted)
                this.listener.Start();
            else if (this.listener.ThreadState == ThreadState.Stopped)
            {
                this.listener = new Thread(this.listen);
                this.listener.Start();
            }
            //Client_Conn(new ChatMessage("Server:", "Welcome to the server!"));
        }

        public void disconnect()
        {
            if (this.listening)
            {
                this.writeStream("quit");
                this.listening = false;
                if (this.stream != null)
                    this.stream.Close();
                if (this.client != null)
                    this.client.Close();
                if (this.Client_Disc != null)
                    Client_Disc(new ChatMessage("CLIENT", "CLOSED"));
            }
        }
    }
}
