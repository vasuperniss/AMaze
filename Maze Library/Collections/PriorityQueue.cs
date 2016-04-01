using System;
using System.Collections.Generic;

namespace Maze_Library.Collections
{
    public class PriorityQueue<T>
    {
        private List<T> pQueue;
        private IComparer<T> comparer;

        public PriorityQueue(IComparer<T> comparer)
        {
            this.pQueue = new List<T>();
            this.comparer = comparer;
        }

        public bool isEmpty()
        {
            return this.pQueue.Count == 0;
        }

        public void Queue(T item)
        {
            for (int i = 0; i < this.pQueue.Count; i++)
            {
                if (comparer.Compare(item, this.pQueue[i]) == -1)
                {
                    this.pQueue.Insert(i, item);
                    return;
                }
            }
            this.pQueue.Add(item);
        }

        public T DeQueue()
        {
            T item = this.pQueue[0];
            this.pQueue.RemoveAt(0);
            return item;
        }

        internal T Remove(T s)
        {
            T item = this.pQueue[this.pQueue.IndexOf(s)];
            this.pQueue.RemoveAt(this.pQueue.IndexOf(s));
            return item;
        }

        public bool Contains(T s)
        {
            return this.pQueue.Contains(s);
        }
    }
}
