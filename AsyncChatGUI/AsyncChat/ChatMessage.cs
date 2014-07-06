using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncChat
{
    public class ChatMessage
    {
        private string name;
        private string message;

        public ChatMessage(string name, string message)
        {
            this.name = name;
            this.message = message;
        }

        public string getName()
        {
            return this.name;
        }

        public string getMessage()
        {
            return this.message;
        }

    }
}
