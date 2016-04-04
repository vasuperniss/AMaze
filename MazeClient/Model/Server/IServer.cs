using System;

namespace MazeClient.Model.Server
{
    /// <summary>
    /// a basic event delegate
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
    public delegate void HandleEvent(object sender, EventArgs args);

    /// <summary>
    /// the Server interface
    /// </summary>
    interface IServer
    {
        /// <summary>
        /// Occurs when [on response received].
        /// </summary>
        event HandleEvent OnResponseReceived;

        /// <summary>
        /// Connects to the Server.
        /// </summary>
        /// <returns>true if successful</returns>
        bool Connect();

        /// <summary>
        /// Closes the connection with the Server.
        /// </summary>
        void Close();

        /// <summary>
        /// Sends the request to the server.
        /// </summary>
        /// <param name="request">The request.</param>
        void SendRequest(string request);
    }
}
