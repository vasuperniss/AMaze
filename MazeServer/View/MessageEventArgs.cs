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
        public string Msg
        {
            get { return message; }
        }

        public MessageEventArgs(string str)
        {
            message = str;
        }
    }
}
