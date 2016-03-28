using Maze_Library.Algorithms;
using System;

namespace Maze_Library.Maze
{
    class DFSWallBreaker : IMazeWallBreaker
    {
        public void BreakWalls(IReshapeAbleMaze reshapeAble)
        {
            ITreeBrancher<IMazePosition> dfsAlg = new DFS<IMazePosition>();
            SearchTreeResult tree = dfsAlg.Branch(new RandomSearchableMaze(reshapeAble));
        }
    }
}
