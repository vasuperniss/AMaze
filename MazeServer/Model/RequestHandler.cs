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

        public void HandleRequest(string request)
        {
            ICommandable Value;

            // parse Request string

            // Execute request if it's in Options dictionary and of valid format
            if (Options.TryGetValue(request, out Value) && Options[request].Validate(request))
            {
                Options[request].Execute();
            }
            else
            {
                Console.WriteLine("RequestHandler Error: Unknown request");
            }
        }

        public void AddOption(string optionName, ICommandable command)
        {
            Options.Add(optionName, command);
        }
    }
}
