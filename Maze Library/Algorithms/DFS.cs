using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class DFS<T> : ISearcher<T>, ITreeBrancher<T>
    {
        public SearchResult Search(ISearchable<T> searchable)
        {
            SearchResult result = new SearchResult();
            this.DFSSearch(searchable, result);
            return result;
        }

        public SearchTree Branch(ISearchable<T> searchable)
        {
            SearchTree result = new SearchTree();
            this.DFSSearch(searchable, result);
            return result;
        }

        private void DFSSearch(ISearchable<T> searchable, object result)
        {
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
                    }
                }
                visited.Add(currState);
            }
        }
    }
}
