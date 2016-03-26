using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeServer.Interfaces;
using System.Threading;
using MazeServer.Model.Options;

namespace MazeServer.Model
{
    class RequestHandler: Observable
    {
        private Dictionary<string, Commandable> Options;

        public void HandleRequest(string request)
        {
            Commandable option;

            // get first keyword
            string key = request.Split(' ')[0];

            // Try to get option
            if (Options.TryGetValue(key, out option))
            {
                option.PerformAction(request);
            }
            else
            {
                Console.WriteLine("RequestHandler Error: Unknown request");
            }
        }

        public void AddOption(string optionName, Commandable command)
        {
            Options.Add(optionName, command);
        }
    }
}
