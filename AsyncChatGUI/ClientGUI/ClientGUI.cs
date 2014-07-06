using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncChatGUI
{
    public static class ClientGUI
    {
        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (args.Length > 1)
            {
                switch (args[1])
                {
                    case "-s":
                    case "-server":
                        {

                        }
                        break;
                    default:
                        Console.WriteLine("Supply no arguments or -s[erver] to run in server mode!");
                        break;
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ClientForm());
            }
        }
        
        //[STAThread]
        //static void Main(String[] args)
        //{
        //    AsyncChatGUI.ServerGUI server = new AsyncChatGUI.ServerGUI();
        //    server.switchMode();
        //}
    }
}
