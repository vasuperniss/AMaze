using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    interface ISearchable<T>
    {
        State<T> GetInitialState();

        State<T> GetGoalState();

        int GetCost(State<T> from, State<T> to);

        List<State<T>> GetReachableStatesFrom(State<T> state);
    }
}
