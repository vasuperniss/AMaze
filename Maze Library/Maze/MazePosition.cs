/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// represents a Position in a Rows/Cols Maze
    /// </summary>
    public class MazePosition
    {
        /// <summary>
        /// The row and colomn of the MazePosition
        /// </summary>
        private int row, colomn;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazePosition"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="colomn">The colomn.</param>
        public MazePosition(int row, int colomn)
        {
            this.row = row;
            this.colomn = colomn;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            string str = row + "|" + colomn;
            return str.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.row == (obj as MazePosition).row
                && this.colomn == (obj as MazePosition).colomn;
        }

        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public int Row
        {
            get { return this.row; }
        }

        /// <summary>
        /// Gets the colomn.
        /// </summary>
        /// <value>
        /// The colomn.
        /// </value>
        public int Colomn
        {
            get { return this.colomn; }
        }

        /// <summary>
        /// Positions the between the MazePositions pos1 and pos2.
        /// </summary>
        /// <param name="pos1">The pos1 the first MazePosition.</param>
        /// <param name="pos2">The pos2 the second MazePosition.</param>
        /// <returns>the MazePosition between</returns>
        internal static MazePosition PositionBetween(MazePosition pos1
                                                        , MazePosition pos2)
        {
            int row = pos1.row;
            int col = pos1.colomn;

            if (pos2.row > pos1.row) { row++; }
            else if (pos2.row < pos1.row) { row--; }

            if (pos2.colomn > pos1.colomn) { col++; }
            else if (pos2.colomn < pos1.colomn) { col--; }

            return new MazePosition(row, col);
        }
    }
}