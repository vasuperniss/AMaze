namespace Maze_Library.Algorithms
{
    class State<T>
    {
        private T state;
        private int cost;
        private State<T> cameFrom;

        public State(T state, State<T> cameFrom, int cost)
        {
            this.state = state;
            this.cost = cost;
            this.cameFrom = cameFrom;
        }

        public int Cost
        {
            get
            {
                return this.cost;
            }
            set
            {
                this.cost = value;
            }
        }

        public State<T> CameFrom
        {
            get
            {
                return this.CameFrom;
            }
            set
            {
                this.CameFrom = value;
            }
        }

        public override bool Equals(object obj)
        {
            return this.state.Equals((obj as State<T>).state);
        }

        public override int GetHashCode()
        {
            return this.state.GetHashCode();
        }

        public T getState()
        {
            return this.state;
        }
    }
}
