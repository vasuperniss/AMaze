using System.Collections.Generic;

/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// Base abstact class for general mazes
    /// </summary>
    /// <seealso cref="Maze_Library.Maze.IMaze" />
    internal abstract class BaseMaze : IMaze
    {
        /// <summary>
        /// The start position
        /// </summary>
        protected MazePosition startPosition;
        /// <summary>
        /// The end position
        /// </summary>
        protected MazePosition endPosition;

        /// <summary>
        /// Gets all the available Positions that can be made from
        /// the given position
        /// </summary>
        /// <param name="position">the position to move from</param>
        /// <returns>
        /// all the available moves
        /// </returns>
        public abstract List<MazePosition> GetAvailablePositionsFrom(MazePosition position);


        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="solver">The solver.</param>
        public abstract void SolveMaze(MazeSolverFactory solver);

        /// <summary>
        /// Solutions to string.
        /// </summary>
        /// <returns>
        /// the maze string with the solution on it
        /// </returns>
        public abstract string SolutionToString();

        /// <summary>
        /// Changes the start position.
        /// </summary>
        public abstract void ChangeStartPosition();

        /// <summary>
        /// Changes the end position.
        /// </summary>
        public abstract void ChangeEndPosition();

        /// <summary>
        /// Gets the End Position of this Maze
        /// </summary>
        /// <returns>
        /// the End Position
        /// </returns>
        public MazePosition GetFinishPosition()
        {
            return this.endPosition;
        }

        /// <summary>
        /// Gets the start Position of this Maze
        /// </summary>
        /// <returns>
        /// the start Position
        /// </returns>
        public MazePosition GetStartPosition()
        {
            return this.startPosition;
        }
    }
}
