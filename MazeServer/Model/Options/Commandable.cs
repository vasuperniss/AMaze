using MazeServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer.Model.Options
{
    abstract class Commandable
    {
        protected IMazeModel Model;
        protected string[] CommandParsed;
        public event Update ModelChanged;

        public abstract string Execute();

        public abstract bool Validate();

        public void PerformAction(string command)
        {
            CommandParsed = command.Split(' ');
            if (Validate())
            {
                Console.WriteLine("Commandable Error: invalid option");
                return;
            }
            Execute();
            ModelChanged(this, EventArgs.Empty);
        }
    }
}
