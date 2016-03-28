using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.View
{
    public delegate void OnMessageReceived(object o, EventArgs args);
    public interface IClientView
    {
        event OnMessageReceived MessageReceived;
    }
}
