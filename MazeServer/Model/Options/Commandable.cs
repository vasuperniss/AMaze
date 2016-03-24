using MazeServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model.Options
{
    abstract class Commandable : Observable
    {
        public abstract void Execute();

        public abstract bool Validate(string command);

        public void PerformAction()
        {
            Execute();
            NotifyObservers();
        }
    }
}
