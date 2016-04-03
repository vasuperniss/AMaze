using MazeServer.Utilities;
using MazeServer.View;
using System;

namespace MazeServer.Model.Options
{
    abstract class Commandable
    {
        protected IModel model;

        public abstract void Execute(object from, string[] commandParsed);

        public abstract bool Validate(string[] commandParsed);

        public void PerformAction(object from, MessageEventArgs request)
        {
            string[] commandParsed = request.Msg.Split(' ');

            if (Validate(commandParsed))
            {
                Execute(from, commandParsed);
            }
            else
            {
                //Console.WriteLine("Commandable Error: invalid option");
            }
        }
    }
}
