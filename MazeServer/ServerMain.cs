using MazeServer.View;
using MazeServer.Utilities;
using MazeServer.Presenter;
using MazeServer.Model;
using System;

namespace MazeServer
{
    class ServerMain
    {
        static void Main(string[] args)
        {
            // Reading App.config file
            if (!AppSettings.Settings.ReadAllSettings(AppSettings.settings))
            {
                Console.WriteLine("Error in app.config.");
                return;
            }

            ILobbyView cm = new Communicator(int.Parse(AppSettings.Settings["port"]));
            IModel m = new MasterModel();
            MazePresenter presenter = new MazePresenter(m, cm);
            cm.StartListening();
        }
    }
}
