using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.View
{
    public delegate void OnConnection(object o, ConnectionEventArgs args);
    interface ILobbyView
    {
        event OnConnection OnConnect;
        void StartListening();
        void Stop();
    }
}
