using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Depth First Search algoritm class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Maze_Library.Algorithms.ISearcher{T}" />
    /// <seealso cref="Maze_Library.Algorithms.ITreeBrancher{T}" />
    class DFS<T> : ISearcher<T>, ITreeBrancher<T>
    {
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable to search.</param>
        /// <returns>
        /// the search Path resulted by the search
        /// </returns>
        public PathSearchResult<T> Search(ISearchable<T> searchable)
        {
            TreeSearchResult<T> tree = this.DFSSearch(searchable);
            State<T> goal = tree.Find(searchable.GetGoalState());
            if (goal != null)
            {
                return new PathSearchResult<T>(goal);
            }
            return null;
        }

        /// <summary>
        /// Branches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>
        /// a Search States Tree
        /// </returns>
        public TreeSearchResult<T> Branch(ISearchable<T> searchable)
        {
            return this.DFSSearch(searchable);
        }

        /// <summary>
        /// DFS search on searchable.
        /// </summary>
        /// <param name="searchable">The searchable to search.</param>
        /// <returns>the search States Tree</returns>
        private TreeSearchResult<T> DFSSearch(ISearchable<T> searchable)
        {
            TreeSearchResult<T> resultTree = new TreeSearchResult<T>(searchable.GetInitialState());
            // closed list - HashSet
            HashSet<State<T>> visited = new HashSet<State<T>>();
            // open list - Stack
            Stack<State<T>> pending = new Stack<State<T>>();

            pending.Push(searchable.GetInitialState());
            while(pending.Count > 0)
            {
                State<T> currState = pending.Pop();
                if (!visited.Contains(currState))
                {
                    foreach (State<T> state in searchable.GetReachableStatesFrom(currState))
                    {
                        if (!pending.Contains(state) && !visited.Contains(state))
                        {
                            // found a new State - add to stack
                            pending.Push(state);
                            // add to the result tree as currState's child
                            resultTree.Add(state, currState);
                        }
                    }
                }
                visited.Add(currState);
            }

            return resultTree;
        }
    }
}
