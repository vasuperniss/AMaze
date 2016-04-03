using Maze_Library.Collections;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Tree Result of a Search algoritm
    /// </summary>
    /// <typeparam name="T">the type of States to use</typeparam>
    internal class TreeSearchResult<T>
    {
        /// <summary>
        /// The root State (initial State)
        /// </summary>
        Tree<State<T>> root;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeSearchResult{T}"/> class.
        /// </summary>
        /// <param name="root">The root State of the Tree.</param>
        public TreeSearchResult(State<T> root)
        {
            this.root = new Tree<State<T>>(root);
        }

        /// <summary>
        /// Finds the specified state.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        internal State<T> Find(State<T> state)
        {
            return this.root.Find(state);
        }

        /// <summary>
        /// Adds the specified child under [father] State in the tree.
        /// </summary>
        /// <param name="child">The child State.</param>
        /// <param name="father">The father State.</param>
        public void Add(State<T> child, State<T> father)
        {
            this.root.Add(child, father);
        }

        /// <summary>
        /// Gets the root State.
        /// </summary>
        /// <returns>the root State</returns>
        public State<T> GetRoot()
        {
            return this.root.GetRoot();
        }

        /// <summary>
        /// Gets all children of the given State.
        /// </summary>
        /// <param name="father">The father State.</param>
        /// <returns>[father]'s children</returns>
        public List<State<T>> getAllChildrenOf(State<T> father)
        {
            return this.root.getAllChildrenOf(father);
        }

        /// <summary>
        /// Removes the leaf.
        /// </summary>
        /// <param name="state">The state leaf to remove.</param>
        internal void RemoveLeaf(State<T> state)
        {
            this.root.RemoveLeaf(state);
        }
    }
}
