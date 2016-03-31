using Maze_Library.Algorithms;
using Maze_Library.Collections;
using System.Collections.Generic;

namespace Maze_Library.Maze.WallBreakers
{
    internal abstract class BaseMazeWallBreaker : IMazeWallBreaker
    {
        public void BreakWalls(IReshapeAbleMaze reshapeAble)
        {
            TreeSearchResult<MazePosition> tree = this.GetSearchTree(reshapeAble);
            reshapeAble.CloseAllDoors();
            Stack<State<MazePosition>> positions = new Stack<State<MazePosition>>();
            positions.Push(tree.GetRoot());
            while (positions.Count > 0)
            {
                State<MazePosition> pos = positions.Pop();
                ICollection<State<MazePosition>> children
                                                = tree.getAllChildrenOf(pos);
                foreach (State<MazePosition> child in children)
                {
                    reshapeAble.ChangeDoorStateBetween(pos.TState, child.TState,
                                                        DoorState.Opened);
                    positions.Push(child);
                }
            }
        }

        protected abstract TreeSearchResult<MazePosition> GetSearchTree(
                                                IReshapeAbleMaze reshapeAble);
    }
}
