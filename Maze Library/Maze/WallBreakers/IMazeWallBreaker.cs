/// <summary>
/// Maze Wall Breakers
/// </summary>
namespace Maze_Library.Maze.WallBreakers
{
    /// <summary>
    /// Maze Wall Breaker Interface
    /// </summary>
    internal interface IMazeWallBreaker
    {
        /// <summary>
        /// breaks the walls of the given reshapeable maze
        /// </summary>
        /// <param name="reshapeAble">the maze to reshape</param>
        void BreakWalls(IReshapeAbleMaze reshapeAble);
    }
}