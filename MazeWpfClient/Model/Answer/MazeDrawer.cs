using System.Text;

namespace MazeClient.Model.Answer
{
    /// <summary>
    /// a Maze Drawer Object
    /// </summary>
    class MazeDrawer
    {
        /// <summary>
        /// Gets the maze to string.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <param name="tabs">The tabs to add in each new line.</param>
        /// <param name="isVisual">if the maze should be more visual.</param>
        /// <param name="rows">The number of rows.</param>
        /// <param name="cols">The number of cols.</param>
        /// <returns>the Maze String</returns>
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
