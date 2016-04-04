using System;
using MazeClient.View;
using MazeClient.Presenter;
using MazeClient.Model.Server;

namespace MazeClient
{
    /// <summary>
    /// class holding the Main Function
    /// </summary>
    class Program
    {
        /// <summary>
        /// The settings to read from the App.config file
        /// </summary>
        static string[] settings = new string[] { "ip", "port",
                                              "rows", "cols" };

        /// <summary>
        /// The Main function
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // Reading App.config file
            if (!AppSettings.Settings.ReadAllSettings(settings))
            {
                Console.WriteLine("Error in app.config.");
                return;
            }

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
