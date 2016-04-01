using System;
using System.Collections.Generic;
using Maze_Library.Algorithms;

namespace Maze_Library.Collections
{
    internal class Tree<T>
    {
        TreeNode<T> root;

        public Tree(T root)
        {
            this.root = new TreeNode<T>(root);
        }

        public void Add(T child, T father)
        {
            TreeNode<T> fatherNode = this.root.GetNodeWith(father);
            if (fatherNode != null)
            {
                fatherNode.AddChild(child);
            }
        }

        public T GetRoot()
        {
            return this.root.Item;
        }

        public T Find(T value)
        {
            return this.root.GetNodeWith(value).Item;
        }

        public List<T> getAllChildrenOf(T father)
        {
            TreeNode<T> fatherNode = this.root.GetNodeWith(father);
            List<T> returnList = new List<T>();
            ICollection<TreeNode<T>> children = fatherNode.GetAllChildren();
            foreach (TreeNode<T> node in children)
            {
                returnList.Add(node.Item);
            }
            return returnList;
        }

        internal void RemoveLeaf(T item)
        {
            TreeNode<T> leaf = this.root.GetNodeWith(item);
            TreeNode<T> father = leaf.GetFather();
            father.RemoveChild(leaf);
        }
    }

    internal class TreeNode<T>
    {
        private T root;
        private TreeNode<T> father;
        private HashSet<TreeNode<T>> children;

        public TreeNode(T root)
        {
            this.father = null;
            this.root = root;
            this.children = new HashSet<TreeNode<T>>();
        }

        public void AddChild(T child)
        {
            TreeNode<T> newNode = new TreeNode<T>(child);
            newNode.father = this;
            this.children.Add(newNode);
        }

        public TreeNode<T> GetNodeWith(T value)
        {
            TreeNode<T> result = null;
            if (this.root.Equals(value))
            {
                return this;
            }
            foreach (TreeNode<T> child in this.children)
            {
                if (child.Item.Equals(value))
                {
                    return child;
                }
                if ((result = child.GetNodeWith(value)) != null)
                {
                    return result;
                }
            }
            return result;
        }

        public HashSet<TreeNode<T>> GetAllChildren()
        {
            return this.children;
        }

        internal TreeNode<T> GetFather()
        {
            return this.father;
        }

        internal void RemoveChild(TreeNode<T> leaf)
        {
            this.children.Remove(leaf);
        }

        public T Item
        {
            get { return this.root; }
        }
    }
}
