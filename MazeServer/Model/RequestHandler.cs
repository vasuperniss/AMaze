using System;
using System.Collections.Generic;
using MazeServer.Model.Options;
using MazeServer.View;

namespace MazeServer.Model
{
    class RequestHandler
    {
        private Dictionary<string, Commandable> options;

        public RequestHandler()
        {
            options = new Dictionary<string, Commandable>();
        }
        public void HandleRequest(object from, MessageEventArgs request)
        {
            Commandable option;
            Console.WriteLine("RequestHandler recieved message");

            // get first keyword
            string key = request.Msg.Split(' ')[0];

            // Try to get option
            if (options.TryGetValue(key, out option))
            {
                option.PerformAction(from, request);
            }
            else
            {
                Console.WriteLine("RequestHandler Error: Unknown request");
            }
        }

        public void AddOption(string optionName, Commandable command)
        {
            options.Add(optionName, command);
        }
    }
}
