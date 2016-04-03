namespace Maze_Library.Algorithms
{
    /// <summary>
    /// State object
    /// </summary>
    /// <typeparam name="T">the type of the inner state</typeparam>
    class State<T>
    {
        /// <summary>
        /// The inner state
        /// </summary>
        private T state;
        /// <summary>
        /// The cost of the current state
        /// </summary>
        private int cost;
        /// <summary>
        /// The State from which this State was reached
        /// </summary>
        private State<T> cameFrom;
        /// <summary>
        /// The distance from the initial State
        /// </summary>
        private int distance;

        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <param name="cameFrom">The came from State.</param>
        /// <param name="cost">The cost.</param>
        public State(T state, State<T> cameFrom, int cost)
        {
            this.state = state;
            this.cost = cost;
            this.cameFrom = cameFrom;
            if (cameFrom != null)
                this.distance = cameFrom.distance + 1;
            else
                this.distance = 0;
        }

        /// <summary>
        /// Gets the inner state of the State.
        /// </summary>
        /// <value>
        /// The inner state.
        /// </value>
        public T TState
        {
            get { return this.state; }
        }

        /// <summary>
        /// Gets the distancef from the initial State.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public int Distance
        {
            get { return this.distance; }
        }

        /// <summary>
        /// Gets or sets the cost of the State.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public int Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }

        /// <summary>
        /// Gets or sets the came from State.
        /// </summary>
        /// <value>
        /// The came from State.
        /// </value>
        public State<T> CameFrom
        {
            get { return this.cameFrom; }
            set { this.cameFrom = value; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.state.Equals((obj as State<T>).state);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.state.GetHashCode();
        }
    }
}
