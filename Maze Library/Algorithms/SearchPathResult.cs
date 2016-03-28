using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    public class SearchPathResult<T>
    {
        private List<T> path;
        
        public SearchPathResult()
        {
            this.path = new List<T>();
        }

        public void AddNode(T node)
        {
            this.path.Add(node);
        }

        public int GetPathLenght()
        {
            return this.path.Count;
        }

        public T this[int i]
        {
            get { return i < this.path.Count ? this.path[i] : default(T); }
        }
    }
}