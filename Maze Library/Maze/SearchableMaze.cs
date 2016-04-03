using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

/// <summary>
/// Maze namespace inside Maze_Library
/// </summary>
namespace Maze_Library.Maze
{
    /// <summary>
    /// Represents a Maze that can be searched using a Searcher
    /// </summary>
    internal class SearchableMaze : ISearchable<MazePosition>
    {
        /// <summary>
        /// the maze to be searched
        /// </summary>
        private IMaze maze;

        /// <summary>
        /// Initiallizes a Searchable Maze
        /// </summary>
        /// <param name="maze">the searched maze</param>
        public SearchableMaze(IMaze maze)
        {
            this.maze = maze;
        }

        /// <summary>
        /// Gets the goal state.
        /// </summary>
        /// <returns>
        /// the goal state
        /// </returns>
        public virtual State<MazePosition> GetGoalState()
        {
            return new State<MazePosition>(this.maze.GetFinishPosition(), null, 0);
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>
        /// the initial state
        /// </returns>
        public virtual State<MazePosition> GetInitialState()
        {
            return new State<MazePosition>(this.maze.GetStartPosition(), null, 0);
        }

        /// <summary>
        /// Gets the cost to move from [from] to [to] States.
        /// </summary>
        /// <param name="from">From state.</param>
        /// <param name="to">To state.</param>
        /// <returns>
        /// the cost of moving
        /// </returns>
        public int GetCost(State<MazePosition> from, State<MazePosition> to)
        {
            Random r = new Random();
            return r.Next(0, 10);
        }

        /// <summary>
        /// Gets the reachable states from the given State.
        /// </summary>
        /// <param name="state">The state to move from.</param>
        /// <returns>
        /// all reachable States
        /// </returns>
        public virtual List<State<MazePosition>> GetReachableStatesFrom(State<MazePosition> state)
        {
            Random r = new Random();
            List<State<MazePosition>> states = new List<State<MazePosition>>();
            List<MazePosition> positions = this.maze.GetAvailablePositionsFrom(state.TState);
            foreach (MazePosition mp in positions)
            {
                // adds the state with a random cost
                states.Add(new State<MazePosition>(mp, state, state.Cost + r.Next(0, 10)));
            }
            return states;
        }
    }
}
