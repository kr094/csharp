using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncChat;

namespace AsyncChatGUI
{
    class ServerGUI
    {
        ChatServer server;

        private bool connected = false;

        public ServerGUI()
        {
            this.server = new ChatServer();
        }

        public bool isConnected()
        {
            return this.connected;
        }

        public void startServer()
        {
            Console.WriteLine("Trying to start server!");
            server.Client_Conn += new ClientConnHandler(this.Client_Conn_Event);
            server.Client_Disc += new ClientDiscHandler(this.Client_Disc_Event);
            server.Message_Sent += new MessageSentHandler(this.Message_Sent_Event);
            server.Message_Received += new MessageReceivedHandler(this.Message_Received_Event);
            this.server.connect();
            this.connected = true;
        }

        public void sendMessage(string message)
        {
            server.sendMessage(new MessageArgs("Server", message));
        }

        public void disconnect()
        {
            this.server.disconnect();
        }

        public void Client_Conn_Event(MessageArgs message)
        {
            printMessage(message);
        }

        public void Client_Disc_Event(MessageArgs message)
        {
            printMessage(message);
        }

        public void Message_Sent_Event(MessageArgs message)
        {
            printMessage(message);
        }

        public void Message_Received_Event(MessageArgs message)
        {
            if (message.getMessage().Equals("quit"))
            {
                this.disconnect();
                this.connected = false;
            }
            else
                printMessage(message);
        }

        private void printMessage(MessageArgs message)
        {
            Console.WriteLine(message.getName() + ": " + message.getMessage());
        }
    }
}
