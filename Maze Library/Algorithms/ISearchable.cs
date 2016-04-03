using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Searchable interface
    /// </summary>
    /// <typeparam name="T">the type of the States</typeparam>
    interface ISearchable<T>
    {
        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>the initial state</returns>
        State<T> GetInitialState();

        /// <summary>
        /// Gets the goal state.
        /// </summary>
        /// <returns>the goal state</returns>
        State<T> GetGoalState();

        /// <summary>
        /// Gets the cost to move from [from] to [to] States.
        /// </summary>
        /// <param name="from">From state.</param>
        /// <param name="to">To state.</param>
        /// <returns>the cost of moving</returns>
        int GetCost(State<T> from, State<T> to);

        /// <summary>
        /// Gets the reachable states from the given State.
        /// </summary>
        /// <param name="state">The state to move from.</param>
        /// <returns>all reachable States</returns>
        List<State<T>> GetReachableStatesFrom(State<T> state);
    }
}
