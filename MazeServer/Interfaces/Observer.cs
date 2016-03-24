using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.MVP_Components.Interfaces
{
    interface Observer
    {
        void Update(Observable obi, object args);
    }
}
