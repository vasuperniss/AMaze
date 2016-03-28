using Maze_Library.Collections;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class BFS<T> : ISearcher<T>
    {
        public SearchPathResult<T> Search(ISearchable<T> searchee)
        {
            SearchPathResult<T> result = new SearchPathResult<T>();

            HashSet<State<T>> visited = new HashSet<State<T>>();
            PriorityQueue<State<T>> pending = new PriorityQueue<State<T>>(new StateComparer<T>());
            pending.Queue(searchee.GetInitialState());

            while (!pending.isEmpty())
            {
                State<T> state = pending.DeQueue();
                visited.Add(state);
                if (state == searchee.GetGoalState())
                {
                    // end code
                }
                List<State<T>> nextStates = searchee.GetReachableStatesFrom(state);
                foreach (State<T> s in nextStates)
                {
                    if (!visited.Contains(s))
                    {
                        if (!pending.Contains(s))
                        {
                            if (s.Cost > state.Cost)
                            {
                                pending.Remove(s);
                                s.Cost = state.Cost;
                                pending.Queue(s);
                            }
                        }
                        else
                        {
                            s.Cost += state.Cost;
                            pending.Queue(s);
                        }
                    }
                }
            }

            return result;
        }
    }
}
