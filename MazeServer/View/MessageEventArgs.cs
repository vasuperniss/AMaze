using System;

namespace MazeServer.View
{
    /// <summary>
    /// Arguments for when a client sends a message.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MessageEventArgs: EventArgs
    {
        /// <summary>
        /// The message.
        /// </summary>
        private string message;

        /// <summary>
        /// property for message member.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Msg
        {
            get { return message; }
        }

        /// <summary>
        /// Constructor. Receives a message.
        /// </summary>
        /// <param name="str">The message string.</param>
        public MessageEventArgs(string str)
        {
            message = str;
        }
    }
}
