using System;
using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class RandomizedPrim<T> : ITreeBrancher<T>
    {
        private Random r = new Random();

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
                        if (!pending.Contains(state) && !visited.Contains(state))
                        {
                            RandomInsert(pending, state);
                            result.Add(state, currState);
                        }
                        else if (pending.Contains(state) && RandomBool())
                        {
                            pending.Remove(state);
                            RandomInsert(pending, state);
                        }
                    }
                    visited.Add(currState);
                }
            }

            return result;
        }

        private State<T> RandomRemoval(List<State<T>> list)
        {
            int random = r.Next(list.Count - 1);
            State<T> returnVal = list[random];
            list.RemoveAt(random);
            return returnVal;
        }

        private void RandomInsert(List<State<T>> list, State<T> item)
        {
            int random = r.Next(list.Count > 0 ? list.Count - 1 : 0);
            random = random > 0 ? random : 0;
            list.Insert(random, item);
        }

        private bool RandomBool()
        {
            int random = r.Next(3);
            if (random == 0)
            {
                return true;
            }
            return false;
        }
    }
}
