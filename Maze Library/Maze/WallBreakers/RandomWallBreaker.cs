using Maze_Library.Algorithms;

namespace Maze_Library.Maze.WallBreakers
{
    internal class RandomWallBreaker : BaseMazeWallBreaker
    {
        protected override SearchTreeResult<MazePosition> GetSearchTree(IReshapeAbleMaze reshapeAble)
        {
            ITreeBrancher<MazePosition> dfsAlg = new RandomizedPrim<MazePosition>();
            return dfsAlg.Branch(new RandomSearchableMaze(reshapeAble));
        }
    }
}
