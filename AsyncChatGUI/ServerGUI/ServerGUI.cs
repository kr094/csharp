using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AsynChat;
using AsyncChatGUI;

namespace AsyncChatGUI
{
    class ServerGUI
    {
        private string data = null;

        [STAThread]
        public static void Main(string[] args)
        {
            //catch all for program exceptions
            try
            {
                //create driver of this type
                ServerGUI driver = new ServerGUI();
                if (args.Length == 1)
                {
                    switch (args[0])
                    {
                        case "s":
                        case "-server":
                            driver.serverMode();
                            break;
                        case "c":
                        default:
                            Application.EnableVisualStyles();
                            Application.Run(new );
                            break;
                    }
                }
                else
                {
                    driver.clientMode();
                }

                //after initial selection + eventual termination, drop user into Selection Mode 
                driver.switchMode();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void switchMode()
        {
            bool prompt = true;

            do
            {
                Console.WriteLine(Environment.NewLine + "Begin SyncChat In Which Mode? (client/server/quit)");
                switch (Console.ReadLine().ToLower())
                {
                    case "c":
                    case "client":
                        clientMode();
                        break;
                    case "s":
                    case "server":
                        serverMode();
                        break;
                    case "quit":
                        prompt = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Command");
                        break;
                }
            }
            while (prompt);
        }

        public void clientMode()
        {
            //catch for client socket exceptions
            try
            {
                ChatClient client = new ChatClient();
                Console.WriteLine("SyncChat Client started on " + NetworkInfo.IpString + ":" + NetworkInfo.portString);
                this.fancyOutput();

                //start looping forever, or until they quit
                while (true)
                {
                    //if theres a key to be read
                    if (Console.KeyAvailable)
                    {
                        //read it, but dont display it (true)
                        //then check if that was I
                        if (Console.ReadKey(true).Key == ConsoleKey.I)
                        {
                            Console.Write(">> ");
                            this.data = Console.ReadLine();
                            //begin send data
                            try
                            {
                                client.sendMessage(this.data);
                                if (this.data.ToLower().Equals("quit"))
                                {
                                    client.close();
                                    Console.WriteLine("Client Closed!");
                                    break;
                                }
                            }
                            catch (System.IO.IOException)
                            {
                                client.close();
                                Console.WriteLine("Lost Connection To Server - Closing");
                                break;
                            }
                        }
                    }
                    //returns true if there is a new message to read
                    else if (client.messageAvailable())
                    {
                        //simply fetching a private string, nothing complicated
                        this.data = client.getData();
                        //handle quitting
                        if (this.data.ToLower().Equals("quit"))
                        {
                            client.close();
                            Console.WriteLine("Client Closed!");
                            break;
                        }
                        else
                            Console.WriteLine("Server: {0}", this.data);
                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Could not connect to a server!");
            }
        }

        public void serverMode()
        {
            ChatServer server = null;
            try
            {
                server = new ChatServer();
                Console.WriteLine("SyncChat Server started on " + NetworkInfo.IpString + ":" + NetworkInfo.portString);
                Console.WriteLine("Awaiting Connection...");
                if (server.startServer())
                    Console.WriteLine("Client Connected!");
                this.fancyOutput();

                server.sendMessage("Welcome to the server");

                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.I)
                        {
                            Console.Write(">> ");
                            this.data = Console.ReadLine();
                            server.sendMessage(this.data);
                            if (this.data.ToLower().Equals("quit"))
                            {
                                server.close();
                                Console.WriteLine("Server Closed!");
                                break;
                            }
                        }
                    }
                    else if (server.messageAvailable())
                    {
                        this.data = server.getData();
                        if (this.data.ToLower().Equals("quit"))
                        {
                            server.close();
                            Console.WriteLine("Server Closed!");
                            break;
                        }
                        else
                            Console.WriteLine("Client: {0}", this.data);
                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("A Server is already running!");
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine("All Clients have left! - Exiting");
                server.close();
            }
        }

        private void fancyOutput()
        {
            Console.WriteLine();
            Console.WriteLine("Messages: ");
            for (int i = 0; i < 50; i++)
                Console.Write("-");
            Console.WriteLine();
        }
    }
}
