using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.View
{
    interface IMazeView
    {
        event Update ViewChanged;
        string GetMessage();
        void SendReply(string reply);
    }
}
