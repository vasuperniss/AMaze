using MazeServer.View;
using System;

namespace MazeServer.Model.Options
{
    abstract class Commandable
    {
        protected IModel Model;
        protected string[] CommandParsed;

        public abstract string Execute();

        public abstract bool Validate();

        public void PerformAction(MessageEventArgs request)
        {
            CommandParsed = request.Msg.Split(' ');
            if (Validate())
            {
                Console.WriteLine("Commandable Error: invalid option");
                return;
            }
            string reply = Execute();
            Model.CompletedTask(new MessageEventArgs(reply, request.Client));
        }
    }
}
