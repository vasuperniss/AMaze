using System;
using System.Configuration;
using MazeClient.ServerSide;
using MazeClient.View;

namespace MazeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverIp = ReadSetting("ip");
            string serverPort = ReadSetting("port");
            if (serverIp == null || serverPort == null)
            {
                Console.WriteLine("error in app.config");
                return;
            }
            Server server = new Server(serverIp, int.Parse(serverPort));
            if (!server.Connect())
            {
                Console.WriteLine("failed to Establish a connection with the server");
                return;
            }

            Console.ReadLine();
        }

        static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
                
            }
            catch (ConfigurationErrorsException)
            {
                return null;
            }
        }
    }
}
