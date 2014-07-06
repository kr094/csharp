using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncChat
{
    /// <summary>
    /// Default network info (localhost)
    /// </summary>
    public static class NetworkInfo
    {
        public static string IpString = "127.0.0.1";
        public static IPAddress IpAddr = IPAddress.Parse(IpString);
        public static int port = 11000;
        public static string portString = port.ToString();
    }

    /// <summary>
    /// IChat - main chat interface
    /// </summary>
    public interface IChat
    {
        void connect();
        void disconnect();
        void sendMessage(ChatMessage message);
        void listen();
    }

    /*Client Events*/
    /// <summary>
    /// Client connected to server
    /// </summary>
    public delegate void ClientConnHandler(ChatMessage message);

    /// <summary>
    /// Client disconnected from server
    /// </summary>
    /// <param name="message"></param>
    public delegate void ClientDiscHandler(ChatMessage message);

    /*Message Events*/

    /// <summary>
    /// An event that fires from the GUI with a message, when it is sent.
    /// </summary>
    /// <param name="message"></param>
    public delegate void MessageSentHandler(ChatMessage message);

    /// <summary>
    /// An event that fires from the library when a message is on the network stream
    /// </summary>
    /// <param name="message"></param>
    public delegate void MessageReceivedHandler(ChatMessage message);
    
}
