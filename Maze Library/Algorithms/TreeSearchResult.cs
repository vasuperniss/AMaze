using Maze_Library.Collections;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    internal class TreeSearchResult<T>
    {
        Tree<State<T>> root;

        public TreeSearchResult(State<T> root)
        {
            this.root = new Tree<State<T>>(root);
        }

        public void Add(State<T> child, State<T> father)
        {
            this.root.Add(child, father);
        }

        public State<T> GetRoot()
        {
            return this.root.GetRoot();
        }

        public List<State<T>> getAllChildrenOf(State<T> father)
        {
            return this.root.getAllChildrenOf(father);
        }
    }
}
