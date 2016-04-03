using Maze_Library.Algorithms;

/// <summary>
/// Maze Wall Breakers
/// </summary>
namespace Maze_Library.Maze.WallBreakers
{
    /// <summary>
    /// Breaks Walls using the Random Prim algoritm
    /// </summary>
    internal class RandomWallBreaker : BaseMazeWallBreaker
    {
        protected override TreeSearchResult<MazePosition> GetSearchTree(IReshapeAbleMaze reshapeAble)
        {
            ITreeBrancher<MazePosition> dfsAlg = new RandomizedPrim<MazePosition>();
            return dfsAlg.Branch(new RandomSearchableMaze(reshapeAble));
        }
    }
}
