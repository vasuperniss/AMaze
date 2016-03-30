using Maze_Library.Collections;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class BFS<T> : ISearcher<T>
    {
        public PathSearchResult<T> Search(ISearchable<T> searchee)
        {
            HashSet<State<T>> visited = new HashSet<State<T>>();
            PriorityQueue<State<T>> pending = new PriorityQueue<State<T>>(new StateComparer<T>());
            pending.Queue(searchee.GetInitialState());

            while (!pending.isEmpty())
            {
                State<T> state = pending.DeQueue();
                visited.Add(state);
                if (state.Equals(searchee.GetGoalState()))
                {
                    return new PathSearchResult<T>(state);
                }
                List<State<T>> nextStates = searchee.GetReachableStatesFrom(state);
                foreach (State<T> s in nextStates)
                {
                    if (!visited.Contains(s))
                    {
                        if (!pending.Contains(s))
                        {
                            State<T> pendingState = pending.Remove(s);
                            if (state.Cost + searchee.GetCost(state, pendingState) < s.Cost)
                            {
                                s.Cost = state.Cost;
                                pendingState = s;
                            }
                            pending.Queue(pendingState);
                        }
                        else
                        {
                            pending.Queue(s);
                        }
                    }
                }
            }
            // didn't find
            return null;
        }
    }
}
