using System;
using MazeClient.View;
using MazeClient.Presenter;
using MazeClient.Model.Server;

namespace MazeClient
{
    class Program
    {
        static string[] settings = new string[] { "ip", "port",
                                              "rows", "cols" };

        static void Main(string[] args)
        {
            // Reading App.config file
            if (!AppSettings.Settings.ReadAllSettings(settings))
            {
                Console.WriteLine("Error in app.config.");
                return;
            }

            AppSettings.Settings.AddSetting("isCoolVersion", "false");

            // Views Creation
            IView io = new IO();
            IServer server = new Server(AppSettings.Settings["ip"],
                                    int.Parse(AppSettings.Settings["port"]));

            // Presenter Creation
            MazeGamePresenter mazeGame = new MazeGamePresenter(io, server);
            mazeGame.Run();
        }
    }
}
