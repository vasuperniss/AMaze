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
            return new State<MazePosition>(this.maze.getFinishPosition());
        }

        public State<MazePosition> GetInitialState()
        {
            return new State<MazePosition>(this.maze.getStartPosition());
        }

        public List<State<MazePosition>> GetReachableStatesFrom(State<MazePosition> state)
        {
            throw new NotImplementedException();
        }
    }
}
