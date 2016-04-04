namespace MazeClient.Model.Server
{
    /// <summary>
    /// the Server View
    /// </summary>
    /// <seealso cref="MazeClient.Model.Server.IServer" />
    class Server : IServer
    {
        /// <summary>
        /// The Socket Communicator to talk to the server
        /// </summary>
        private SocketCommunicator server;
        /// <summary>
        /// The is closed boolean
        /// </summary>
        private volatile bool isClosed;
        /// <summary>
        /// Occurs when [on response received].
        /// </summary>
        public event HandleEvent OnResponseReceived;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="ip">The ip of the server.</param>
        /// <param name="port">The port of the server.</param>
        public Server(string ip, int port)
        {
            this.server = new SocketCommunicator(ip, port);
        }

        /// <summary>
        /// Connects to the Server.
        /// </summary>
        /// <returns>
        /// true if successful
        /// </returns>
        public bool Connect()
        {
            this.server.OnResponse += OnResponseFromServer;
            this.isClosed = false;
            return this.server.EstablishConnection();
        }

        /// <summary>
        /// Called when [response from server].
        /// </summary>
        /// <param name="response">The response.</param>
        private void OnResponseFromServer(string response)
        {
            if (this.OnResponseReceived != null && !this.isClosed)
            {
                this.OnResponseReceived(this, new ResponseEventArgs(response));
            }
        }

        /// <summary>
        /// Sends the request to the server.
        /// </summary>
        /// <param name="request">The request.</param>
        public void SendRequest(string request)
        {
            this.server.SendRequest(request);
        }

        /// <summary>
        /// Closes the connection with the Server.
        /// </summary>
        public void Close()
        {
            this.isClosed = true;
            this.server.Close();
        }
    }
}
