using Maze_Library.Maze;

namespace MazeServer.Utilities
{
    class JsonConverter
    {
        public static string MazeToJson(string maze)
        {
            return "\"Maze\":\""+maze+"\"";
        }

        public static string NameToJson(string name)
        {
            return "\"Name\":\""+name+"\"";
        }

        public static string PointToJson(string type, MazePosition start)
        {
            string row = "0";
            string col = "0";
            return "\""+type+"\":{\"Row\":"+row+",\"Col\":"+col+"}";
        }
    }
}
