using System.Collections.Generic;

/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// the State of the Door between two cells
    /// </summary>
    public enum DoorState
    {
        /// <summary>
        /// Closed or Opened
        /// </summary>
        Closed, Opened
    }

    /// <summary>
    /// Maze Interface to be given to the users of this DLL
    /// </summary>
    public interface IMaze
    {
        /// <summary>
        /// Gets the start Position of this Maze
        /// </summary>
        /// <returns>the start Position</returns>
        MazePosition GetStartPosition();

        /// <summary>
        /// Gets the End Position of this Maze
        /// </summary>
        /// <returns>the End Position</returns>
        MazePosition GetFinishPosition();

        /// <summary>
        /// Gets all the available Positions that can be made from
        /// the given position
        /// </summary>
        /// <param name="position">the position to move from</param>
        /// <returns>all the available moves</returns>
        List<MazePosition> GetAvailablePositionsFrom(MazePosition position);

        /// <summary>
        /// Changes the start position.
        /// </summary>
        void ChangeStartPosition();

        /// <summary>
        /// Changes the end position.
        /// </summary>
        void ChangeEndPosition();

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="solver">The solver.</param>
        void SolveMaze(MazeSolverFactory solver);

        /// <summary>
        /// Solutions to string.
        /// </summary>
        /// <returns>the maze string with the solution on it</returns>
        string SolutionToString();
    }
}
