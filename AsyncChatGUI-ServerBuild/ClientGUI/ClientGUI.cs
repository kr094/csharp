using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AsyncChatGUI
{
    public static class ClientGUI
    {
        /// <summary>
        /// This runs both the Server AND Client
        /// If args[] contains -server
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        static void Main(String[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].StartsWith("-server"))
                {
                    ServerGUI serverGUI = new ServerGUI();
                    while (true)
                    {
                        if (!serverGUI.isConnected())
                        {
                            serverGUI.startServer();
                            string message = String.Empty;
                            while (serverGUI.isConnected())
                            {
                                if (!String.IsNullOrEmpty(message = Console.ReadLine()))
                                {
                                    if (message.Equals("quit"))
                                    {
                                        serverGUI.disconnect();
                                    }
                                    else
                                        serverGUI.sendMessage(message);
                                }
                            }//end message loop
                            Thread.Sleep(2500);
                        }
                    }//end outer loop
                }
                else
                    Console.WriteLine("Arguments are -server");
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClientForm());
            }
        }
    }
}
