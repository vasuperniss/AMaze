using System;

namespace MazeClient.Model.Server
{
    /// <summary>
    /// event args object for server Response event
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    class ResponseEventArgs : EventArgs
    {
        /// <summary>
        /// The response
        /// </summary>
        private string response;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseEventArgs"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        public ResponseEventArgs(string response)
        {
            this.response = response;
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string Response
        {
            get { return this.response; }
        }
    }
}
