namespace MazeClient.Model.Server
{
    class Server : IServer
    {
        private SocketCommunicator server;
        private volatile bool isClosed;

        public event HandleEvent OnResponseReceived;

        public Server(string ip, int port)
        {
            this.server = new SocketCommunicator(ip, port);
        }

        public bool Connect()
        {
            this.server.OnResponse += OnResponseFromServer;
            this.isClosed = false;
            return this.server.EstablishConnection();
        }

        private void OnResponseFromServer(string response)
        {
            if (this.OnResponseReceived != null && !this.isClosed)
            {
                this.OnResponseReceived(this, new ResponseEventArgs(response));
            }
        }

        public void SendRequest(string request)
        {
            this.server.SendRequest(request);
        }

        public void Close()
        {
            this.isClosed = true;
            this.server.Close();
        }
    }
}
