namespace Maze_Library.Maze.WallBreakers
{
    public class WallBreakerFactory
    {
        public enum BreakingType
        {
            DFS = 1, Random = 2
        }

        private BreakingType type;

        public WallBreakerFactory(BreakingType type)
        {
            this.type = type;
        }

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
