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
        public delegate void TaskCompleted(object source, EventArgs e);
        public event TaskCompleted Task;

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
            NotifyCompletion();
        }

        protected virtual void NotifyCompletion()
        {
            if (Task != null)
            {
                Task(this, EventArgs.Empty);
            }
        }
    }
}
