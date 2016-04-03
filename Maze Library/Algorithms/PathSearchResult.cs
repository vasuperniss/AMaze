using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    /// <summary>
    /// a Path result of a State's search
    /// </summary>
    /// <typeparam name="T">the type of items in State</typeparam>
    internal class PathSearchResult<T>
    {
        /// <summary>
        /// The path of the search from initial to goal
        /// </summary>
        private List<State<T>> path;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSearchResult{T}"/> class.
        /// </summary>
        public PathSearchResult()
        {
            this.path = new List<State<T>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSearchResult{T}"/> class.
        /// </summary>
        /// <param name="backTrace">The back trace State.</param>
        public PathSearchResult(State<T> backTrace)
        {
            this.path = new List<State<T>>();
            this.path.Insert(0, backTrace);
            while (backTrace.CameFrom != null)
            {
                this.path.Insert(0, backTrace.CameFrom);
                backTrace = backTrace.CameFrom;
            }
        }

        /// <summary>
        /// Adds the node.
        /// </summary>
        /// <param name="node">The node to add.</param>
        public void AddNode(State<T> node)
        {
            this.path.Add(node);
        }

        /// <summary>
        /// Gets the path lenght.
        /// </summary>
        /// <returns>the lenght of the path</returns>
        public int GetPathLenght()
        {
            return this.path.Count;
        }

        /// <summary>
        /// Gets the <see cref="State{T}"/> with the specified i.
        /// </summary>
        /// <value>
        /// The <see cref="State{T}"/>.
        /// </value>
        /// <param name="i">The i.</param>
        /// <returns>the State in the [i] position</returns>
        public State<T> this[int i]
        {
            get { return i < this.path.Count ? this.path[i] : null; }
        }
    }
}