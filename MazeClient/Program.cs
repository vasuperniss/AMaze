using System;
using System.Configuration;
using MazeClient.View;
using MazeClient.Presenter;
using MazeClient.Model.Server;

namespace MazeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // Reading App.config file
            string serverIp = ReadSetting("ip");
            string serverPort = ReadSetting("port");
            if (serverIp == null || serverPort == null)
            {
                Console.WriteLine("Error in app.config.");
                return;
            }

            // Views Creation
            IView io = new IO();
            IServer server = new Server(serverIp, int.Parse(serverPort));

            // Presenter Creation
            MazeGamePresenter mazeGame = new MazeGamePresenter(io, server);
            mazeGame.Run();
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? null;
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}
