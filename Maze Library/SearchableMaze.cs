using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library
{
    class SearchableMaze : ISearchable<MazePosition>
    {
        private IMaze maze;

        public SearchableMaze(IMaze maze)
        {
            this.maze = maze;
        }

        public State<MazePosition> GetGoalState()
        {
            return new State<MazePosition>(this.maze.getFinishPosition(), null, 0);
        }

        public State<MazePosition> GetInitialState()
        {
            return new State<MazePosition>(this.maze.getStartPosition(), null, 0);
        }

        public List<State<MazePosition>> GetReachableStatesFrom(State<MazePosition> state)
        {
            throw new NotImplementedException();
        }
    }
}
