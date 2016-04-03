using Maze_Library.Collections;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Best First Search searching algoritm class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Maze_Library.Algorithms.ISearcher{T}" />
    class BFS<T> : ISearcher<T>
    {
        /// <summary>
        /// Searches the specified searchee.
        /// </summary>
        /// <param name="searchee">The searchee.</param>
        /// <returns>the Search States Path</returns>
        public PathSearchResult<T> Search(ISearchable<T> searchee)
        {
            // the closed List - HashSet
            HashSet<State<T>> visited = new HashSet<State<T>>();
            // the open List - PriorityQueue
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
                        if (pending.Contains(s))
                        {
                            // check if found a better path for s
                            State<T> pendingState = pending.Remove(s);
                            if (s.Cost < pendingState.Cost)
                            {
                                // found a cheaper path
                                pending.Queue(s);
                            }
                            else { pending.Queue(pendingState); }
                        }
                        else
                        {
                            // new State found, add to queue
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
