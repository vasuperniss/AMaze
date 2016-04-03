using System.Collections.Generic;

/// <summary>
/// My Special Collections
/// </summary>
namespace Maze_Library.Collections
{
    /// <summary>
    /// Represents a Tree stucture
    /// </summary>
    /// <typeparam name="T">the type of items in the tree</typeparam>
    internal class Tree<T>
    {
        /// <summary>
        /// the root of the tree
        /// </summary>
        TreeNode<T> root;

        /// <summary>
        /// initiallize a new Tree from the given root
        /// </summary>
        /// <param name="root">the item in the root of the tree</param>
        public Tree(T root)
        {
            this.root = new TreeNode<T>(root);
        }

        /// <summary>
        /// adds child under the father node in the tree
        /// </summary>
        /// <param name="child">the item to add</param>
        /// <param name="father">the father of the added item</param>
        public void Add(T child, T father)
        {
            TreeNode<T> fatherNode = this.root.GetNodeWith(father);
            if (fatherNode != null)
            {
                fatherNode.AddChild(child);
            }
        }

        /// <summary>
        /// returns the root of the tree
        /// </summary>
        /// <returns>the root of the tree</returns>
        public T GetRoot()
        {
            return this.root.Item;
        }

        /// <summary>
        /// finds value in the tree
        /// </summary>
        /// <param name="value">the item to search</param>
        /// <returns>the found item</returns>
        public T Find(T value)
        {
            return this.root.GetNodeWith(value).Item;
        }

        /// <summary>
        /// returns all the children of the given father
        /// </summary>
        /// <param name="father">the father node</param>
        /// <returns>the children of the father node</returns>
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

        /// <summary>
        /// removes the given item from the tree
        /// </summary>
        /// <param name="item">the item to remove</param>
        internal void RemoveLeaf(T item)
        {
            TreeNode<T> leaf = this.root.GetNodeWith(item);
            TreeNode<T> father = leaf.GetFather();
            father.RemoveChild(leaf);
        }
    }

    /// <summary>
    /// Represents a node in the tree
    /// </summary>
    /// <typeparam name="T">the type of items in the tree</typeparam>
    internal class TreeNode<T>
    {
        /// <summary>
        /// the item in the treeNode
        /// </summary>
        private T root;
        /// <summary>
        /// the node of this node's father
        /// </summary>
        private TreeNode<T> father;
        /// <summary>
        /// the children nodes of this node
        /// </summary>
        private HashSet<TreeNode<T>> children;

        /// <summary>
        /// Initializes a new TreeNode
        /// </summary>
        /// <param name="root">the item inside the TreeNode</param>
        public TreeNode(T root)
        {
            this.father = null;
            this.root = root;
            this.children = new HashSet<TreeNode<T>>();
        }

        /// <summary>
        /// Adds child to this node's children
        /// </summary>
        /// <param name="child">the child to add</param>
        public void AddChild(T child)
        {
            TreeNode<T> newNode = new TreeNode<T>(child);
            newNode.father = this;
            this.children.Add(newNode);
        }

        /// <summary>
        /// Gets the TreeNode that holds value as it's rott
        /// </summary>
        /// <param name="value">the item to search for</param>
        /// <returns>the TreeNode with value</returns>
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

        /// <summary>
        /// Gets all the Children of this TreeNode
        /// </summary>
        /// <returns>the TreeNode's children</returns>
        public HashSet<TreeNode<T>> GetAllChildren()
        {
            return this.children;
        }

        /// <summary>
        /// Gets the father TreeNode of this TreeNode
        /// </summary>
        /// <returns>the TreeNode's father</returns>
        internal TreeNode<T> GetFather()
        {
            return this.father;
        }

        /// <summary>
        /// removes [leaf] from this TreeNode's children
        /// </summary>
        /// <param name="leaf">the child to remove</param>
        internal void RemoveChild(TreeNode<T> leaf)
        {
            this.children.Remove(leaf);
        }

        /// <summary>
        /// the inner item inside the TreeNode
        /// </summary>
        public T Item
        {
            get { return this.root; }
        }
    }
}
