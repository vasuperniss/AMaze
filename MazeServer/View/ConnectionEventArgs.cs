using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.View
{
    public class ConnectionEventArgs : EventArgs
    {
        private IClientView view;
        public IClientView CView
        {
            get { return view; }
        }

        public ConnectionEventArgs(IClientView cview)
        {
            view = cview;
        }
    }
}
