namespace MazeWpfClient.Model.Answer
{
    /// <summary>
    /// a Generate Server Answer
    /// </summary>
    /// <seealso cref="MazeClient.Model.Answer.IServerAnswer" />
    public class GenerateAnswer : IServerAnswer
    {
        /// <summary>
        /// The name of the Maze
        /// </summary>
        private string name;
        /// <summary>
        /// The maze
        /// </summary>
        private string maze;
        /// <summary>
        /// The start position of the Maze
        /// </summary>
        private MazePosition start;
        /// <summary>
        /// The end position of the Maze
        /// </summary>
        private MazePosition end;

        /// <summary>
        /// Gets or sets the name of the Maze.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public string Maze
        {
            get { return this.maze; }
            set { this.maze = value; }
        }

        /// <summary>
        /// Gets or sets the start.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public MazePosition Start
        {
            get { return this.start; }
            set { this.start = value; }
        }

        /// <summary>
        /// Gets or sets the end.
        /// </summary>
        /// <value>
        /// The end.
        /// </value>
        public MazePosition End
        {
            get { return this.end; }
            set { this.end = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            int rows = 0, cols = 0;
            int.TryParse(AppSettings.Settings["rows"], out rows);
            int.TryParse(AppSettings.Settings["cols"], out cols);
            string mazeDisplayStr = new MazeDrawer().getMazeToStr(this.maze,
                                                    6, true, rows, cols);

            return string.Format("Maze - name: {0}\nmaze: {1}\n"
                                + "start: {2}\nend: {3}",
                            this.name, mazeDisplayStr, this.start, this.end);
        }
    }
}
