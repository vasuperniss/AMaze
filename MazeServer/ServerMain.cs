using MazeServer.View;
using MazeServer.Utilities;
using MazeServer.Presenter;
using MazeServer.Model;
using System;

namespace MazeServer
{
    class ServerMain
    {
        /// <summary>
        ///     Reads information from AppConfig file and initiates the View, Model and Presenter.
        ///     View starts listening.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // Reading App.config file
            if (!AppSettings.Settings.ReadAllSettings(AppSettings.settings))
            {
                Console.WriteLine("Error in app.config.");
                return;
            }

            // test
            ILobbyView lv = new Communicator(int.Parse(AppSettings.Settings["port"]));
            IModel m = new MasterModel();
            MazePresenter presenter = new MazePresenter(m, lv);
            lv.StartListening();
        }
    }
}
