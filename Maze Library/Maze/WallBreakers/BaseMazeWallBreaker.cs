using Maze_Library.Algorithms;
using System.Collections.Generic;

/// <summary>
/// Maze Wall Breakers
/// </summary>
namespace Maze_Library.Maze.WallBreakers
{
    /// <summary>
    /// an Abstract MazeWallBreaker that uses Tree Generating algoritms
    /// to Break the walls of the reshapeable maze given to it
    /// </summary>
    internal abstract class BaseMazeWallBreaker : IMazeWallBreaker
    {
        public void BreakWalls(IReshapeAbleMaze reshapeAble)
        {
            TreeSearchResult<MazePosition> tree = this.GetSearchTree(reshapeAble);
            // closes all the doors of the Maze, to open them based on the tree
            reshapeAble.CloseAllDoors();
            Stack<State<MazePosition>> positions = new Stack<State<MazePosition>>();
            positions.Push(tree.GetRoot());
            // farthest will eventioally be the End Position of the maze
            State<MazePosition> farthest = tree.GetRoot();
            while (positions.Count > 0)
            {
                State<MazePosition> pos = positions.Pop();
                ICollection<State<MazePosition>> children
                                                = tree.getAllChildrenOf(pos);
                foreach (State<MazePosition> child in children)
                {
                    // open the door between pos and it's child
                    reshapeAble.ChangeDoorStateBetween(pos.TState, child.TState,
                                                        DoorState.Opened);
                    positions.Push(child);
                    if (child.Distance > farthest.Distance)
                    {
                        // found a farer state
                        if (reshapeAble.ChangeEndPosition(child.TState))
                        {
                            farthest = child;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a Search Tree Result of a search algoritm on the given maze
        /// </summary>
        /// <param name="reshapeAble">the reshapeable maze</param>
        /// <returns>the Search Tree Result</returns>
        protected abstract TreeSearchResult<MazePosition> GetSearchTree(
                                                IReshapeAbleMaze reshapeAble);
    }
}
