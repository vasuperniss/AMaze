using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MazeServer.View;

namespace MazeServer
{
    delegate void Update(object source, EventArgs e);

    class MazeServerMain
    {
        static void Main(string[] args)
        {
            int p = 0;
            string port = ConfigurationManager.AppSettings["port"];

            if (!Int32.TryParse(port, out p)) return;
            Communicator cm = new Communicator(p);
            cm.StartCommunication();
        }
    }
}
