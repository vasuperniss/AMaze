using Maze_Library.Collections;
using System;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Randomized Prim algoritm class
    /// </summary>
    /// <typeparam name="T">the type of States to use</typeparam>
    /// <seealso cref="Maze_Library.Algorithms.ITreeBrancher{T}" />
    class RandomizedPrim<T> : ITreeBrancher<T>
    {
        /// <summary>
        /// Random object
        /// </summary>
        private Random r = new Random();

        /// <summary>
        /// Branches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>
        /// a Search States Tree
        /// </returns>
        public TreeSearchResult<T> Branch(ISearchable<T> searchable)
        {
            TreeSearchResult<T> result = new TreeSearchResult<T>(searchable.GetInitialState());
            // closed list - HashSet
            HashSet<State<T>> visited = new HashSet<State<T>>();
            // opened list - RandomList
            RandomList<State<T>> pending = new RandomList<State<T>>();

            pending.RandomInsert(searchable.GetInitialState());
            while (pending.Count > 0)
            {
                State<T> currState = pending.RandomRemoval();
                if (!visited.Contains(currState))
                {
                    foreach (State<T> state in searchable.GetReachableStatesFrom(currState))
                    {
                        if (!visited.Contains(state))
                        {
                            if (!pending.Contains(state))
                            {
                                // found a brand new State
                                pending.RandomInsert(state);
                                result.Add(state, currState);
                            }
                            else if (this.RandomBool())
                            {
                                // change the father of state in the tree
                                result.RemoveLeaf(state);
                                result.Add(state, currState);
                            }
                        }
                    }
                    visited.Add(currState);
                }
            }

            return result;
        }

        /// <summary>
        /// Randomizes a bool value.
        /// </summary>
        /// <returns>a random boolean value</returns>
        private bool RandomBool()
        {
            int random = r.Next(2);
            if (random == 0)
            {
                return true;
            }
            return false;
        }
    }
}
