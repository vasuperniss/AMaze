using System;
using System.Collections.Generic;

namespace Maze_Library.Collections
{
    public class PriorityQueue<T>
    {
        private Vector<T> pQueue;
        private IComparer<T> comparer;
        private int numItems;

        public PriorityQueue(IComparer<T> comparer)
        {
            this.pQueue = new Vector<T>();
            this.comparer = comparer;
            this.numItems = 0;
        }

        public bool isEmpty()
        {
            return this.numItems == 0;
        }

        public void Queue(T item)
        {
            pQueue[++this.numItems] = item;
            this.ShiftUp(this.numItems);
        }

        public T DeQueue()
        {
            if (this.isEmpty())
            {
                throw new IndexOutOfRangeException();
            }
            else if (this.numItems == 1)
            {
                T item = this.pQueue[this.numItems];
                this.pQueue.RemoveFrom(this.numItems--);
                return item;
            }
            else
            {
                T item = this.pQueue[1];
                this.pQueue[1] = this.pQueue[this.numItems];
                this.pQueue.RemoveFrom(this.numItems--);
                this.ShiftDown(1);
                return item;
            }
        }

        internal T Remove(T s)
        {
            for (int i = 1; i <= this.numItems; i++)
            {
                if (this.pQueue[i].Equals(s))
                {
                    T value = this.pQueue[i];
                    this.pQueue[i] = this.pQueue[this.numItems];
                    this.pQueue.RemoveFrom(this.numItems--);
                    this.ShiftDown(i);
                    return value;
                }
            }
            return default(T);
        }

        public bool Contains(T s)
        {
            return this.pQueue.Contains(s);
        }

        private void ShiftUp(int k)
        {
            if (k <= 1) { return; }
            if (this.comparer.Compare(this.pQueue[k],
                                this.pQueue[k/2]) == 1)
            {
                this.pQueue.Swap(k, k / 2);
                this.ShiftUp(k / 2);
            }
        }

        private void ShiftDown(int k)
        {
            if (k < this.numItems / 2)
            {
                if (this.comparer.Compare(this.pQueue[k], this.pQueue[k * 2]) == 1)
                {
                    this.pQueue.Swap(k, k * 2);
                    this.ShiftDown(k * 2);
                }
                else if (this.comparer.Compare(this.pQueue[k], this.pQueue[k * 2 + 1]) == 1)
                {
                    this.pQueue.Swap(k, k * 2 + 1);
                    this.ShiftDown(k * 2 + 1);
                }
            }
        }
    }
}
