using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Presenter
{
    class ServerReplyEventArgs : EventArgs
    {
        private Socket sock;
        private string message;

        public Socket Socket
        {
            get { return sock; }
        }

        public string Message
        {
            get { return message; }
        }

        ServerReplyEventArgs(Socket sock, string message)
        {
            this.sock = sock;
            this.message = message;
        }
    }
}
