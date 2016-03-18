using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeServer
{
    class RequestHandler
    {
        private Dictionary<string, ICommandable> Options;

        public void HandleRequest(string Request)
        {
            ICommandable Value;

            // parse Request string

            // Execute request if it's in Options dictionary and of valid format
            if (Options.TryGetValue(Request, out Value) && Options[Request].Validate(Request))
            {
                Options[Request].Execute();
            }
            else
            {
                Console.WriteLine("RequestHandler Error: Unknown request");
            }
        }

        public void AddOption(string OptionName, ICommandable Command)
        {
            Options.Add(OptionName, Command);
        }
    }
}
