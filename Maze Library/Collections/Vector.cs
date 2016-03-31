using System;

namespace Maze_Library.Collections
{
    internal class Vector<T>
    {
        private const int STARTING_SIZE = 4;
        private T[] vector;
        private int lastIndex;

        public Vector()
        {
            this.vector = new T[STARTING_SIZE];
            this.lastIndex = 0;
        }

        public void Add(T item)
        {
            this[this.lastIndex++] = item;
        }

        public void AddTo(T item, int index)
        {
            if (index >= this.lastIndex)
            {
                this[index] = item;
            }
            else if (index >= 0)
            {
                for (int i = this.lastIndex; i > index; i--)
                {
                    this[i] = this[i - 1];
                }
                this[index] = item;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        public void Remove()
        {
            this.lastIndex--;
            this.ShrinkIfNeeded();
        }

        public void RemoveFrom(int index)
        {
            this.lastIndex--;
            for (int i = index; i < this.lastIndex; i++)
            {
                this[i] = this[i + 1];
            }
            this.ShrinkIfNeeded();
        }

        private void ShrinkIfNeeded()
        {
            if (this.vector.Length >= STARTING_SIZE
                            && this.lastIndex < this.vector.Length / 3)
            {
                T[] resized = new T[this.vector.Length / 2];
                for (int j = 0; j <= this.lastIndex; j++)
                {
                    resized[j] = this.vector[j];
                }
                this.vector = resized;
            }
        }

        public bool Contains(T s)
        {
            for (int i = 0; i < this.lastIndex; i++)
            {
                if (this.vector[i] != null && s.Equals(this.vector[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public void Swap(int firstIndex, int secondIndex)
        {
            T temp = this[firstIndex];
            this[firstIndex] = this[secondIndex];
            this[secondIndex] = temp;
        }

        public T this[int i]
        {
            get
            {
                if (i >= 0 && i < this.vector.Length)
                {
                    return this.vector[i];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (i >= 0 && i < this.vector.Length)
                {
                    this.vector[i] = value;
                    if (i > this.lastIndex)
                    {
                        this.lastIndex = i;
                    }
                }
                else if (i >= 0 && i < this.vector.Length * 2)
                {
                    T[] resized = new T[this.vector.Length * 2];
                    for (int j = 0; j <= this.lastIndex; j++)
                    {
                        resized[j] = this.vector[j];
                    }
                    this.vector = resized;
                    this.vector[i] = value;
                    this.lastIndex = i;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
