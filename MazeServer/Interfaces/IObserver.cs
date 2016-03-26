using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Model;

namespace MazeServer.Interfaces
{
    interface IObserver
    {
        void Update(Observable observable, object p);
    }
}
