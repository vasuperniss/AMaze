using System.Configuration;

namespace MazeServer.Utilities
{
    class AppConfigSettingsFetcher
    {
        public int GetHeight()
        {
            int h;
            string height = ConfigurationManager.AppSettings["height"];
            if (!int.TryParse(height, out h)) return -1;
            return h;
        }

        public int GetWidth()
        {
            int w;
            string width = ConfigurationManager.AppSettings["width"];
            if (!int.TryParse(width, out w)) return -1;
            return w;
        }

        public int GetPort()
        {
            int p;
            string port = ConfigurationManager.AppSettings["port"];
            if (!int.TryParse(port, out p)) return -1;
            return p;
        }
    }
}
