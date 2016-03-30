﻿using Maze_Library.Algorithms;
using Maze_Library.Collections;
using System.Collections.Generic;

namespace Maze_Library.Maze.WallBreakers
{
    internal abstract class BaseMazeWallBreaker : IMazeWallBreaker
    {
        public void BreakWalls(IReshapeAbleMaze reshapeAble)
        {
            Tree<MazePosition> tree = this.SearchTreeIntoTree(this.GetSearchTree(reshapeAble));
            reshapeAble.CloseAllDoors();
            Stack<MazePosition> positions = new Stack<MazePosition>();
            positions.Push(tree.GetRoot());
            while (positions.Count > 0)
            {
                MazePosition pos = positions.Pop();
                ICollection<MazePosition> children
                                                = tree.getAllChildrenOf(pos);
                foreach (MazePosition child in children)
                {
                    reshapeAble.ChangeDoorStateBetween(pos, child,
                                                        DoorState.Opened);
                    positions.Push(child);
                }
            }
        }

        protected abstract TreeSearchResult<MazePosition> GetSearchTree(
                                                IReshapeAbleMaze reshapeAble);
        
        private Tree<MazePosition> SearchTreeIntoTree(TreeSearchResult<MazePosition> statesTree)
        {
            Tree<MazePosition> tree = new Tree<MazePosition>(statesTree.GetRoot().TState);
            // fill in tree
            return tree;
        }
    }
}
