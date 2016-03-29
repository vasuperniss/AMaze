using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class DFS<T> : ISearcher<T>, ITreeBrancher<T>
    {
        public PathSearchResult<State<T>> Search(ISearchable<T> searchable)
        {
            PathSearchResult<State<T>> result = new PathSearchResult<State<T>>();
            this.DFSSearch(searchable);
            return result;
        }

        public TreeSearchResult<T> Branch(ISearchable<T> searchable)
        {
            return this.DFSSearch(searchable);
        }

        private TreeSearchResult<T> DFSSearch(ISearchable<T> searchable)
        {
            TreeSearchResult<T> resultTree = new TreeSearchResult<T>(searchable.GetInitialState());
            HashSet<State<T>> visited = new HashSet<State<T>>();
            Stack<State<T>> pending = new Stack<State<T>>();

            pending.Push(searchable.GetInitialState());

            while(pending.Count > 0)
            {
                State<T> currState = pending.Pop();
                if (!visited.Contains(currState))
                {
                    foreach (State<T> state in searchable.GetReachableStatesFrom(currState))
                    {
                        pending.Push(state);
                        resultTree.Add(state, currState);
                    }
                }
                visited.Add(currState);
            }

            return resultTree;
        }
    }
}
