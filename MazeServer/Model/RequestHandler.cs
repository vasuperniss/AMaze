using System.Collections.Generic;
using MazeServer.Model.Options;
using MazeServer.View;

namespace MazeServer.Model
{
    /// <summary>
    /// Handles requests from clients.
    /// </summary>
    class RequestHandler
    {
        /// <summary>
        /// The options dictionary.
        /// </summary>
        private Dictionary<string, Commandable> options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestHandler"/> class.
        /// </summary>
        public RequestHandler()
        {
            options = new Dictionary<string, Commandable>();
        }

        /// <summary>
        /// Attempts to fetch a request from the dictionary, and then executes it.
        /// </summary>
        /// <param name="from">Client that sent the message.</param>
        /// <param name="request">The <see cref="MessageEventArgs"/> instance containing the event data.</param>
        public void HandleRequest(object from, MessageEventArgs request)
        {
            Commandable option;

            // get first keyword
            string key = request.Msg.Split(' ')[0];

            // Try to get option
            if (options.TryGetValue(key, out option))
            {
                option.PerformAction(from, request);
            }
            else
            {
                //Invalid option.
            }
        }

        /// <summary>
        /// Adds an option.
        /// </summary>
        /// <param name="optionName">Name of the option.</param>
        /// <param name="command">Object that contains the option's execution code.</param>
        public void AddOption(string optionName, Commandable command)
        {
            options.Add(optionName, command);
        }
    }
}
