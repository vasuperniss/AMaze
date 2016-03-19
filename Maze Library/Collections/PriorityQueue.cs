using System.Collections.Generic;

namespace Maze_Library.Collections
{
    class PriorityQueue<T>
    {
        private Vector<T> pQueue;
        private IComparer<T> comparer;

        public PriorityQueue(IComparer<T> comparer)
        {
            this.pQueue = new Vector<T>();
            this.comparer = comparer;
        }

        public void Queue(T item)
        {

        }

        public T DeQueue()
        {
            return default(T);
        }
    }
}
