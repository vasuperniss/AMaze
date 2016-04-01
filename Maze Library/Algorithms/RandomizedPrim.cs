﻿using Maze_Library.Collections;
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
                                pending.RandomInsert(state);
                                result.Add(state, currState);
                            }
                            else if (this.RandomBool())
                            {
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
            int random = r.Next(2);
            if (random == 0)
            {
                return true;
            }
            return false;
        }
    }
}
