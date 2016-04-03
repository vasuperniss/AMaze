using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    /// <summary>
    /// Comarator for comparing State objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.Generic.IComparer{Maze_Library.Algorithms.State{T}}" />
    class StateComparer<T> : IComparer<State<T>>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.Value Meaning Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
        /// </returns>
        public int Compare(State<T> x, State<T> y)
        {
            if (x.Cost > y.Cost)
            {
                return 1;
            }
            else if (x.Cost == y.Cost)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
