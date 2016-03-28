using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class DFS<T> : ISearcher<T>, ITreeBrancher<T>
    {
        public SearchPathResult<T> Search(ISearchable<T> searchable)
        {
            SearchPathResult<T> result = new SearchPathResult<T>();
            this.DFSSearch(searchable);
            return result;
        }

        public SearchTreeResult<T> Branch(ISearchable<T> searchable)
        {
            return this.DFSSearch(searchable);
        }

        private SearchTreeResult<T> DFSSearch(ISearchable<T> searchable)
        {
            SearchTreeResult<T> resultTree = new SearchTreeResult<T>(searchable.GetInitialState().getState());
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
                        resultTree.Add(state.getState(), currState.getState());
                    }
                }
                visited.Add(currState);
            }

            return resultTree;
        }
    }
}
