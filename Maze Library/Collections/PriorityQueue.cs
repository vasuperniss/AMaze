using System.Collections.Generic;

/// <summary>
/// My Special Collections
/// </summary>
namespace Maze_Library.Collections
{
    /// <summary>
    /// Represents a Genenric PriorityQueue
    /// </summary>
    /// <typeparam name="T">the type of items in the queue</typeparam>
    public class PriorityQueue<T>
    {
        /// <summary>
        /// a list for the queue
        /// </summary>
        private List<T> pQueue;
        /// <summary>
        /// the comparer to use
        /// </summary>
        private IComparer<T> comparer;

        /// <summary>
        /// Initiallize the Priority queue
        /// </summary>
        /// <param name="comparer">the Comparer to use</param>
        public PriorityQueue(IComparer<T> comparer)
        {
            this.pQueue = new List<T>();
            this.comparer = comparer;
        }

        /// <summary>
        /// Checks if the Queue is currently empty
        /// </summary>
        /// <returns>true - empty, else - false</returns>
        public bool isEmpty()
        {
            return this.pQueue.Count == 0;
        }

        /// <summary>
        /// Adds item to the queue
        /// </summary>
        /// <param name="item">the item to add</param>
        public void Queue(T item)
        {
            for (int i = 0; i < this.pQueue.Count; i++)
            {
                if (comparer.Compare(item, this.pQueue[i]) == -1)
                {
                    // found the sorted position for item in the queue
                    this.pQueue.Insert(i, item);
                    return;
                }
            }
            this.pQueue.Add(item);
        }

        /// <summary>
        /// Dequeues the top item
        /// </summary>
        /// <returns>the item at the top of the queue</returns>
        public T DeQueue()
        {
            T item = this.pQueue[0];
            this.pQueue.RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Removes s from the queue
        /// </summary>
        /// <param name="s">the item to remove</param>
        /// <returns>the removed item</returns>
        internal T Remove(T s)
        {
            T item = this.pQueue[this.pQueue.IndexOf(s)];
            this.pQueue.RemoveAt(this.pQueue.IndexOf(s));
            return item;
        }

        /// <summary>
        /// Checks if the queue contains s in it
        /// </summary>
        /// <param name="s">the item to search</param>
        /// <returns>true is s is in the queue</returns>
        public bool Contains(T s)
        {
            return this.pQueue.Contains(s);
        }
    }
}
