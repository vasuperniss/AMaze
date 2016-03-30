using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library.Maze
{
    internal class SearchableMaze : ISearchable<MazePosition>
    {
        private IMaze maze;

        public SearchableMaze(IMaze maze)
        {
            this.maze = maze;
        }

        public virtual State<MazePosition> GetGoalState()
        {
            return new State<MazePosition>(this.maze.GetFinishPosition(), null, 0);
        }

        public virtual State<MazePosition> GetInitialState()
        {
            return new State<MazePosition>(this.maze.GetStartPosition(), null, 0);
        }

        public int GetCost(State<MazePosition> from, State<MazePosition> to)
        {
            Random r = new Random();
            return r.Next(0, 10);
        }

        public virtual List<State<MazePosition>> GetReachableStatesFrom(State<MazePosition> state)
        {
            List<State<MazePosition>> states = new List<State<MazePosition>>();
            List<MazePosition> positions = this.maze.GetAvailablePositionsFrom(state.TState);
            foreach (MazePosition mp in positions)
            {
                Random r = new Random();
                states.Add(new State<MazePosition>(mp, state, state.Cost + r.Next(0, 10)));
            }
            return states;
        }
    }
}
