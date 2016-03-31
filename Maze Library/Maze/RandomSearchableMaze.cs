using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library.Maze
{
    internal class RandomSearchableMaze : SearchableMaze
    {
        private const int SCRAMBLE_ROUNDS = 7;

        private IMaze maze;
        private Random r;
        private HashSet<MazePosition> removedOnce;

        public RandomSearchableMaze(IMaze maze) : base(maze)
        {
            this.maze = maze;
            this.r = new Random();
            this.removedOnce = new HashSet<MazePosition>();
        }

        public override List<State<MazePosition>> GetReachableStatesFrom(State<MazePosition> state)
        {
            List<State<MazePosition>> states = base.GetReachableStatesFrom(state);
            this.Scramble(states);
            this.RandomlyRemoveAState(states);
            this.RandomlyRemoveAState(states);
            return states;
        }

        private void RandomlyRemoveAState(List<State<MazePosition>> states)
        {
            if (states.Count > 1 && this.RandomBool())
            {
                for (int i = 0; i < states.Count; i++)
                {
                    if (!this.removedOnce.Contains(states[i].TState))
                    {
                        this.removedOnce.Add(states[i].TState);
                        states.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void Scramble(List<State<MazePosition>> states)
        {
            State<MazePosition> tempState;
            for (int i = 0; i < SCRAMBLE_ROUNDS; i++)
            {
                for (int j = 0; j < states.Count; j++)
                {
                    int random = r.Next(states.Count);
                    tempState = states[j];
                    states[j] = states[random];
                    states[random] = tempState;
                }
            }
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
