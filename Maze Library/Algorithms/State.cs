namespace Maze_Library.Algorithms
{
    class State<T>
    {
        private T state;
        private int cost;

        public State(T state, int cost)
        {
            this.state = state;
            this.cost = cost;
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
    }
}
