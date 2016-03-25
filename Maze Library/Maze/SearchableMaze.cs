using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library
{
    class SearchableMaze : ISearchable<IMazePosition>
    {
        private IMaze maze;

        public SearchableMaze(IMaze maze)
        {
            this.maze = maze;
        }

        public State<IMazePosition> GetGoalState()
        {
            return new State<IMazePosition>(this.maze.getFinishPosition(), null, 0);
        }

        public State<IMazePosition> GetInitialState()
        {
            return new State<IMazePosition>(this.maze.getStartPosition(), null, 0);
        }

        public List<State<IMazePosition>> GetReachableStatesFrom(State<IMazePosition> state)
        {
            List<State<IMazePosition>> states = new List<State<IMazePosition>>();
            List<IMazePosition> positions = this.maze.getAvailablePositionsFrom(state.getState());
            foreach (IMazePosition mp in positions)
            {
                states.Add(new State<IMazePosition>(mp, state, 0));
            }
            return states;
        }
    }
}
