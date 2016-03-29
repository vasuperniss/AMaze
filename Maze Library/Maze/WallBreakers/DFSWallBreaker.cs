using Maze_Library.Algorithms;

namespace Maze_Library.Maze.WallBreakers
{
    public class DFSWallBreaker : IMazeWallBreaker
    {
        public void BreakWalls(IReshapeAbleMaze reshapeAble)
        {
            ITreeBrancher<MazePosition> dfsAlg = new DFS<MazePosition>();
            SearchTreeResult<MazePosition> tree = dfsAlg.Branch(new RandomSearchableMaze(reshapeAble));
        }
    }
}
