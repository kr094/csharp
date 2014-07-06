using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncChat;

namespace ServerGUI
{
    class ServerGUI
    {
        ChatServer server;

        //public static void Main()
        //{
        //    ServerGUI serverGUI = new ServerGUI();
        //    serverGUI.startServer();
        //}

        public void startServer()
        {
            this.server = new ChatServer();
            server.Client_Conn += new ClientConnHandler(this.Client_Conn_Event);
            server.Client_Disc += new ClientDiscHandler(this.Client_Disc_Event);
            server.Message_Sent += new MessageSentHandler(this.Message_Sent_Event);
            server.Message_Received += new MessageReceivedHandler(this.Message_Received_Event);
            this.server.connect();
        }

        public void Client_Conn_Event(ChatMessage message)
        {
            printMessage(message);
        }

        public void Client_Disc_Event(ChatMessage message)
        {
            printMessage(message);
        }

        public void Message_Sent_Event(ChatMessage message)
        {
            printMessage(message);
        }

        public void Message_Received_Event(ChatMessage message)
        {
            printMessage(message);
        }

        private void printMessage(ChatMessage message)
        {
            Console.WriteLine(message.getName() + " " + message.getMessage());
        }
    }
}
