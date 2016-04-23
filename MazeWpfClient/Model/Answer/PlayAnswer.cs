namespace MazeClient.Model.Answer
{
    /// <summary>
    /// a Play Server Answer
    /// </summary>
    /// <seealso cref="MazeClient.Model.Answer.IServerAnswer" />
    class PlayAnswer : IServerAnswer
    {
        /// <summary>
        /// The game name
        /// </summary>
        private string gameName;
        /// <summary>
        /// The move that the other player has made
        /// </summary>
        private string move;

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
        /// Gets or sets the move.
        /// </summary>
        /// <value>
        /// The move.
        /// </value>
        public string Move
        {
            get { return this.move; }
            set { this.move = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("in that game: {0}, the other player"
               +" has made the [{1}] move", this.gameName, this.move);
        }
    }
}
