/// <summary>
/// Maze Wall Breakers
/// </summary>
namespace Maze_Library.Maze.WallBreakers
{
    /// <summary>
    /// Represents a Factory for generating Maze Wall Breakers
    /// </summary>
    public class WallBreakerFactory
    {
        /// <summary>
        /// the Algoritm to use for the wall breaking
        /// </summary>
        public enum BreakingType
        {
            /// <summary>
            /// DFS or Random
            /// </summary>
            DFS = 1, Random = 0
        }

        /// <summary>
        /// 
        /// </summary>
        private BreakingType type;
        
        /// <summary>
        /// Initiallizes a WallBreakerFactory with the given BreakingType
        /// </summary>
        /// <param name="type">the Algoritm type to be used</param>
        public WallBreakerFactory(BreakingType type)
        {
            this.type = type;
        }

        /// <summary>
        /// Creates a new Wall Breaker
        /// </summary>
        /// <returns>the wall Breaker</returns>
        internal IMazeWallBreaker GetWallBreaker()
        {
            switch (this.type)
            {
                case BreakingType.DFS:
                    return new DFSWallBreaker();
                case BreakingType.Random:
                    return new RandomWallBreaker();
                default:
                    return null;
            }
        }
    }
}
