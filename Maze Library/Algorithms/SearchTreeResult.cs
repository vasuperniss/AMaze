using System.Collections.Generic;

namespace Maze_Library.Algorithms
{
    class SearchTreeResult<T>
    {
        TreeNode<T> root;

        public SearchTreeResult(T root)
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
                if (child.GetValue().Equals(child))
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

        public T GetValue()
        {
            return this.root;
        }
    }
}
