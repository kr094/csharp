using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncChat
{
    /// <summary>
    /// Message object sent internally by this chat software
    /// Extends eventargs
    /// </summary>
    public class MessageArgs : EventArgs
    {
        /// <summary>
        /// Internal message properties 
        /// </summary>
        private string name;
        private string message;

        /// <summary>
        /// Public ctor
        /// </summary>
        /// <param name="name">Client or Server</param>
        /// <param name="message">The message sent</param>
        public MessageArgs(string name, string message)
        {
            this.name = name;
            this.message = message;
        }

        /// <summary>
        /// Public accessors
        /// </summary>
        /// <returns></returns>
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
