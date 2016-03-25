using MazeClient.Presenter;
using MazeClient.ServerSide;

namespace MazeClient.View
{
    class Server : IServerView
    {
        private ServerCommunicator server;
        private IPresenter presenter;

        public Server(string ip, int port)
        {
            this.server = new ServerCommunicator(ip, port);
        }

        public bool Connect()
        {
            return this.server.EstablishConnection();
        }

        public void AddPresenter(IPresenter presenter)
        {
            this.presenter = presenter;
            this.server.OnResponse += this.OnResponse;
        }

        private void OnResponse(string response)
        {
            this.presenter.HandleRespose(response);
        }

        public void SendRequest(string request)
        {
            this.server.SendRequest(request);
        }

        public void Close()
        {
            this.server.Close();
        }
    }
}
