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
            ICommandable option;

            // get first keyword
            string key = request.Split(' ')[0];

            // Execute request if it's in Options dictionary and of valid format
            if (Options.TryGetValue(key, out option) && option.Validate(request))
            {
                option.Execute();
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
