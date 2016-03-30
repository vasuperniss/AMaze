using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library.Maze
{
    internal class RandomSearchableMaze : SearchableMaze
    {
        private const int SCRAMBLE_ROUNDS = 7;

        private IMaze maze;

        public RandomSearchableMaze(IMaze maze) : base(maze)
        {
            this.maze = maze;
        }

        public override List<State<MazePosition>> GetReachableStatesFrom(State<MazePosition> state)
        {
            List<State<MazePosition>> states = new List<State<MazePosition>>();
            List<MazePosition> positions = this.maze.GetAvailablePositionsFrom(state.TState);
            foreach (MazePosition mp in positions)
            {
                states.Add(new State<MazePosition>(mp, state, 0));
            }
            this.Scramble(states);
            return states;
        }

        private void Scramble(List<State<MazePosition>> states)
        {
            State<MazePosition> tempState;
            Random r = new Random();
            for (int i = 0; i < SCRAMBLE_ROUNDS; i++)
            {
                for (int j = 0; j < states.Count; j++)
                {
                    int random = r.Next(0, states.Count - 1);
                    tempState = states[j];
                    states[j] = states[random];
                    states[random] = states[j];
                }
            }
        }
    }
}
