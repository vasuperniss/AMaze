using Maze_Library.Algorithms;

namespace Maze_Library.Maze.WallBreakers
{
    internal class DFSWallBreaker : BaseMazeWallBreaker
    {
        protected override TreeSearchResult<MazePosition> GetSearchTree(IReshapeAbleMaze reshapeAble)
        {
            ITreeBrancher<MazePosition> dfsAlg = new DFS<MazePosition>();
            return dfsAlg.Branch(new RandomSearchableMaze(reshapeAble));
        }
    }
}
