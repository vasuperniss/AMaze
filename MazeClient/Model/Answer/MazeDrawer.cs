using System.Text;

namespace MazeClient.Model.Answer
{
    class MazeDrawer
    {
        public string getMazeToStr(string maze, int tabs, bool isVisual,
                                                    int rows, int cols)
        {
            StringBuilder result = new StringBuilder();
            maze = maze.Replace("\n", "");
            if (maze.Length != (cols * 2 - 1) * (rows * 2 - 1))
            {
                result.Append(maze + "\n");
            }
            else
            {
                string tabStr = "";
                for (int i = 0; i <tabs; i++) { tabStr += " "; }
                for (int i = 0; i < rows * 2 - 1; i++)
                {
                    if (i > 0) { result.Append("\n" + tabStr); }
                    if (isVisual) { result.Append("|"); }
                    result.Append(maze.Substring(i * (cols * 2 - 1),
                                                    cols * 2 - 1));
                    if (isVisual) { result.Append("|"); }
                }
                if (isVisual)
                {
                    result.Replace('1', '█').Replace('0', ' ')
                                            .Replace('2', 'o');
                }
            }

            return result.ToString();
        }
    }
}
