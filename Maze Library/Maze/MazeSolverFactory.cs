using Maze_Library.Algorithms;
using System.Collections.Generic;

/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// the Approch to use in solving the maze
    /// </summary>
    public enum WayToSolve
    {
        /// <summary>
        /// DFS or BFS algoritms
        /// </summary>
        DFS = 0, BFS = 1
    }

    /// <summary>
    /// a Factory for Maze solvers
    /// </summary>
    public class MazeSolverFactory
    {
        /// <summary>
        /// The way to solve mazes
        /// </summary>
        private WayToSolve way;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSolverFactory"/> class.
        /// </summary>
        /// <param name="wayToSolve">The way to solve.</param>
        public MazeSolverFactory(WayToSolve wayToSolve)
        {
            this.way = wayToSolve;
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="maze">The maze.</param>
        /// <returns>a list of mazePositions that represent the path
        /// that completed the given Maze
        /// </returns>
        internal List<MazePosition> SolveMaze(IMaze maze)
        {
            List<MazePosition> solution = new List<MazePosition>();
            PathSearchResult<MazePosition> result;
            switch (this.way)
            {
                case WayToSolve.BFS:
                    result = new BFS<MazePosition>().Search(new SearchableMaze(maze));
                    break;
                case WayToSolve.DFS:
                    result = new DFS<MazePosition>().Search(new SearchableMaze(maze));
                    break;
                default:
                    return null;
            }
            // from States List to MazePositions List
            for (int i = 0; i < result.GetPathLenght(); i++)
            {
                solution.Add(result[i].TState);
            }
            return solution;
        }
    }
}
