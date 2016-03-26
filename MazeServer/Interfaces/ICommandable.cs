using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Interfaces
{
    interface ICommandable
    {
        void Execute();

        bool Validate(string command);
    }
}
