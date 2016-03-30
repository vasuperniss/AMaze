using System.Collections.Generic;

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
    }

    internal class TreeNode<T>
    {
        T root;
        HashSet<TreeNode<T>> children;

        public TreeNode(T root)
        {
            this.root = root;
            this.children = new HashSet<TreeNode<T>>();
        }

        public void AddChild(T child)
        {
            this.children.Add(new TreeNode<T>(child));
        }

        public TreeNode<T> GetNodeWith(T value)
        {
            TreeNode<T> result = null;
            foreach (TreeNode<T> child in this.children)
            {
                if (child.Item.Equals(child))
                {
                    return child;
                }
                if ((result = GetNodeWith(value)) != null)
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

        public T Item
        {
            get { return this.root; }
        }
    }
}
