using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library.Maze
{
    class RandomSearchableMaze : SearchableMaze
    {
        IMaze maze;

        public RandomSearchableMaze(IMaze maze) : base(maze)
        {
            this.maze = maze;
        }

        public override List<State<IMazePosition>> GetReachableStatesFrom(State<IMazePosition> state)
        {
            List<State<IMazePosition>> states = new List<State<IMazePosition>>();
            List<IMazePosition> positions = this.maze.getAvailablePositionsFrom(state.getState());
            foreach (IMazePosition mp in positions)
            {
                states.Add(new State<IMazePosition>(mp, state, 0));
            }
            this.Scramble(states);
            return states;
        }

        private void Scramble(List<State<IMazePosition>> states)
        {
            State<IMazePosition> tempState;
            Random r = new Random();
            for (int i = 0; i < states.Count; i++)
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
