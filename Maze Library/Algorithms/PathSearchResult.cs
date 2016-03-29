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