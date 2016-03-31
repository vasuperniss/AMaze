using System;
using System.Collections.Generic;
using MazeServer.Model.Options;
using MazeServer.View;

namespace MazeServer.Model
{
    class RequestHandler
    {
        private Dictionary<string, Commandable> Options;

        public void HandleRequest(object from, MessageEventArgs request)
        {
            Commandable option;

            // get first keyword
            string key = request.Msg.Split(' ')[0];

            // Try to get option
            if (Options.TryGetValue(key, out option))
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
            Options.Add(optionName, command);
        }
    }
}
