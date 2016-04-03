using System;
using System.Collections.Generic;

/// <summary>
/// My Special Collections
/// </summary>
namespace Maze_Library.Collections
{
    /// <summary>
    /// RandomList - a List that is Random
    /// </summary>
    /// <typeparam name="T">the type of items in the list</typeparam>
    class RandomList<T>
    {
        /// <summary>
        /// the list
        /// </summary>
        private List<T> list;
        /// <summary>
        /// a Random object for Random operations
        /// </summary>
        private Random r;

        /// <summary>
        /// Initiallize a new Random List
        /// </summary>
        public RandomList()
        {
            this.list = new List<T>();
            r = new Random();
        }

        /// <summary>
        /// the number of items in the list
        /// </summary>
        public int Count
        {
            get { return this.list.Count; }
        }

        /// <summary>
        /// Randomly inserts the item into the list
        /// </summary>
        /// <param name="item">the item to be added</param>
        public void RandomInsert(T item)
        {
            int random = list.Count > 0 ? r.Next(list.Count) : 0;
            list.Insert(random, item);
        }

        /// <summary>
        /// Randomly Removes an item from the list
        /// </summary>
        /// <returns>the removed item</returns>
        public T RandomRemoval()
        {
            int random = list.Count > 0 ? r.Next(list.Count) : 0;
            T item = this.list[random];
            this.list.RemoveAt(random);
            return item;
        }

        /// <summary>
        /// Checks if the item is in the list
        /// </summary>
        /// <param name="item">the item to search for</param>
        /// <returns>true - if the item is in the list</returns>
        internal bool Contains(T item)
        {
            return this.list.Contains(item);
        }
    }
}
