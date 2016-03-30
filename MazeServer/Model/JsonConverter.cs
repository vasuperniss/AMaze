using Maze_Library;

namespace MazeServer.Model
{
    class JsonConverter
    {
        public static string MazeToJson(IMaze maze)
        {
            string res = "";
            return "\"Maze\":\""+res+"\"";
        }

        public static string NameToJson(string name)
        {
            return "\"Name\":\""+name+"\"";
        }

        public static string PointToJson(string type, IMazePosition start)
        {
            string row = "0";
            string col = "0";
            return "\""+type+"\":{\"Row\":"+row+",\"Col\":"+col+"}";
        }
    }
}
