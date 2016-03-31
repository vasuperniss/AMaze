using MazeServer.View;
using MazeServer.Utilities;
using MazeServer.Presenter;
using MazeServer.Model;

namespace MazeServer
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            AppConfigSettingsFetcher f = new AppConfigSettingsFetcher();
            int port = f.GetPort();
            if (port == -1) return;

            ILobbyView cm = new Communicator(port);
            IModel m = new MasterModel();
            MazePresenter presenter = new MazePresenter(m, cm);
            cm.StartListening();
        }
    }
}
