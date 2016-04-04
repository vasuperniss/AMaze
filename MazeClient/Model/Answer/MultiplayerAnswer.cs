namespace MazeClient.Model.Answer
{
    /// <summary>
    /// a MultiPlayer Game start Server Answer
    /// </summary>
    /// <seealso cref="MazeClient.Model.Answer.IServerAnswer" />
    class MultiplayerAnswer : IServerAnswer
    {
        /// <summary>
        /// The game's name
        /// </summary>
        private string gameName;
        /// <summary>
        /// The maze name
        /// </summary>
        private string mazeName;
        /// <summary>
        /// My maze
        /// </summary>
        private GenerateAnswer myMaze;
        /// <summary>
        /// The other player's maze
        /// </summary>
        private GenerateAnswer othersMaze;

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return this.gameName; }
            set { this.gameName = value; }
        }

        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>
        /// The name of the maze.
        /// </value>
        public string MazeName
        {
            get { return this.mazeName; }
            set { this.mazeName = value; }
        }

        /// <summary>
        /// Gets or sets your Maze.
        /// </summary>
        /// <value>
        /// You.
        /// </value>
        public GenerateAnswer You
        {
            get { return this.myMaze; }
            set { this.myMaze = value; }
        }

        /// <summary>
        /// Gets or sets the other's Maze.
        /// </summary>
        /// <value>
        /// The other.
        /// </value>
        public GenerateAnswer Other
        {
            get { return this.othersMaze; }
            set { this.othersMaze = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Multiplayer Game, game-name: {0}, maze name: {1}\n"
                                +"Your maze: {2}\nOther player's maze: {3}",this.gameName,
                                this.mazeName, this.myMaze, this.othersMaze);
        }
    }
}
