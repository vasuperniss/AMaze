namespace MazeWpfClient.Model.Answer
{
    /// <summary>
    /// a Position in a Maze
    /// </summary>
    public class MazePosition
    {
        /// <summary>
        /// The row of the position
        /// </summary>
        private int row;
        /// <summary>
        /// The col of the position
        /// </summary>
        private int col;

        public MazePosition(MazePosition pos)
        {
            this.row = pos.row;
            this.col = pos.col;
        }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        /// <summary>
        /// Gets or sets the col.
        /// </summary>
        /// <value>
        /// The col.
        /// </value>
        public int Col
        {
            get { return this.col; }
            set { this.col = value; }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "(row: " + this.row + ", col: " + this.col + ")";
        }
    }
}
