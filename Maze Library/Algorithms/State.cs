namespace Maze_Library.Algorithms
{
    class State<T>
    {
        private T state;
        private int cost;
        private State<T> cameFrom;
        private int distance;

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

        public T TState
        {
            get { return this.state; }
        }

        public int Distance
        {
            get { return this.distance; }
        }

        public int Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }

        public State<T> CameFrom
        {
            get { return this.cameFrom; }
            set { this.cameFrom = value; }
        }

        public override bool Equals(object obj)
        {
            return this.state.Equals((obj as State<T>).state);
        }

        public override int GetHashCode()
        {
            return this.state.GetHashCode();
        }
    }
}
