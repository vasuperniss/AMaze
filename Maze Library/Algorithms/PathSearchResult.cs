using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    internal class PathSearchResult<T>
    {
        private List<State<T>> path;
        
        public PathSearchResult()
        {
            this.path = new List<State<T>>();
        }

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

        public void AddNode(State<T> node)
        {
            this.path.Add(node);
        }

        public int GetPathLenght()
        {
            return this.path.Count;
        }

        public State<T> this[int i]
        {
            get { return i < this.path.Count ? this.path[i] : null; }
        }
    }
}