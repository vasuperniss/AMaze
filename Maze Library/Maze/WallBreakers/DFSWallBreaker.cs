using Maze_Library.Algorithms;

/// <summary>
/// Maze Wall Breakers
/// </summary>
namespace Maze_Library.Maze.WallBreakers
{
    /// <summary>
    /// Breaks Walls using the DFS algoritm
    /// </summary>
    internal class DFSWallBreaker : BaseMazeWallBreaker
    {
        protected override TreeSearchResult<MazePosition> GetSearchTree(IReshapeAbleMaze reshapeAble)
        {
            ITreeBrancher<MazePosition> dfsAlg = new DFS<MazePosition>();
            return dfsAlg.Branch(new RandomSearchableMaze(reshapeAble));
        }
    }
}
