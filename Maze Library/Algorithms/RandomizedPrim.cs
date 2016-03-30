using System;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class RandomizedPrim<T> : ITreeBrancher<T>
    {
        public TreeSearchResult<T> Branch(ISearchable<T> searchable)
        {
            TreeSearchResult<T> result = new TreeSearchResult<T>(searchable.GetInitialState());
            HashSet<State<T>> visited = new HashSet<State<T>>();
            List<State<T>> pending = new List<State<T>>();

            pending.Add(searchable.GetInitialState());
            while (pending.Count > 0)
            {
                State<T> currState = RandomRemoval(pending);
                if (!visited.Contains(currState))
                {
                    foreach (State<T> state in searchable.GetReachableStatesFrom(currState))
                    {
                        pending.Add(state);
                        result.Add(state, currState);
                    }
                    visited.Add(currState);
                }
            }

            return result;
        }

        private State<T> RandomRemoval(List<State<T>> list)
        {
            Random r = new Random();
            int random = r.Next(0, list.Count - 1);
            State<T> returnVal = list[random];
            list.RemoveAt(random);
            return returnVal;
        }
    }
}
