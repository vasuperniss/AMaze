using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.View
{
    public class MessageEventArgs: EventArgs
    {
        private string message;
        private IClientView client;
        public string Msg
        {
            get { return message; }
        }

        public IClientView Client
        {
            get { return client; }
        }

        public MessageEventArgs(string str, IClientView cl)
        {
            message = str;
            client = cl;
        }
    }
}
