namespace MazeClient.Model.Answer
{
    /// <summary>
    /// a Solve Server Answer
    /// </summary>
    /// <seealso cref="MazeClient.Model.Answer.IServerAnswer" />
    class SolveAnswer : IServerAnswer
    {
        /// <summary>
        /// The name of the maze
        /// </summary>
        private string name;
        /// <summary>
        /// The sol of the maze
        /// </summary>
        private string sol;
        /// <summary>
        /// The start position of the maze
        /// </summary>
        private MazePosition start;
        /// <summary>
        /// The end position of the maze
        /// </summary>
        private MazePosition end;

        /// <summary>
        /// Gets or sets the name of the maze.
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
            get { return this.sol; }
            set { this.sol = value; }
        }

        /// <summary>
        /// Gets or sets the start Position the maze.
        /// </summary>
        /// <value>
        /// The start Position the maze.
        /// </value>
        public MazePosition Start
        {
            get { return this.start; }
            set { this.start = value; }
        }

        /// <summary>
        /// Gets or sets the end Position the maze.
        /// </summary>
        /// <value>
        /// The end Position the maze.
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
            string solDisplay = new MazeDrawer().getMazeToStr(this.sol,
                                                    13, true, rows, cols);

            return string.Format("Solution for Maze: {0}\n"
                                + "solution is: {1}\n"
                                + "start: {2}\nend: {3}",
                                this.name, solDisplay, this.start, this.end);
        }
    }
}
