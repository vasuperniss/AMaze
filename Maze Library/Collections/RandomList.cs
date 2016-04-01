using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library.Collections
{
    class RandomList<T>
    {
        private List<T> list;
        private Random r;

        public RandomList()
        {
            this.list = new List<T>();
            r = new Random();
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public void RandomInsert(T item)
        {
            int random = list.Count > 0 ? r.Next(list.Count) : 0;
            list.Insert(random, item);
        }

        public T RandomRemoval()
        {
            int random = list.Count > 0 ? r.Next(list.Count) : 0;
            T item = this.list[random];
            this.list.RemoveAt(random);
            return item;
        }

        internal bool Contains(T item)
        {
            return this.list.Contains(item);
        }
    }
}
