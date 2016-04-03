using Maze_Library.Maze.Matrix;
using Maze_Library.Maze.WallBreakers;

/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// Factory for generating Mazes
    /// </summary>
    public class MazeFactory
    {
        /// <summary>
        /// The number of colomns
        /// </summary>
        private int colomns;
        /// <summary>
        /// The number of rows
        /// </summary>
        private int rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeFactory"/> class.
        /// </summary>
        /// <param name="rows">The number of rows.</param>
        /// <param name="colomns">The number of colomns.</param>
        public MazeFactory(int rows, int colomns)
        {
            this.rows = rows;
            this.colomns = colomns;
        }

        /// <summary>
        /// Gets the maze created with the given wall Breaker factory.
        /// </summary>
        /// <param name="wBreakerFactory">The wall breaker factory.</param>
        /// <returns>the newly created maze</returns>
        public IMaze GetMaze(WallBreakerFactory wBreakerFactory)
        {
            return new MatrixMaze(this.rows, this.colomns, wBreakerFactory);
        }

        /// <summary>
        /// Copies the maze.
        /// </summary>
        /// <param name="maze">The maze to copy.</param>
        /// <returns>the copied Maze</returns>
        public IMaze CopyMaze(IMaze maze)
        {
            if (maze is MatrixMaze)
            {
                return new MatrixMaze(maze as MatrixMaze);
            }
            return null;
        }
    }
}
