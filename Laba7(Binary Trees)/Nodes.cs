using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba7_Binary_Trees_
{

    // Вузли червоно-чорного дерева
    public enum NodeColor
    {
        Red,
        Black
    }
    public class RedBlackTreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public RedBlackTreeNode<T> Left { get; set; }
        public RedBlackTreeNode<T> Right { get; set; }
        public RedBlackTreeNode<T> Parent { get; set; } // Додано поле Parent
        public NodeColor Color { get; set; }
        public int Height { get; set; }

        public RedBlackTreeNode(T value)
        {
            Value = value;
            Left = null;
            Right = null;
            Height = 1;
            Color = NodeColor.Red;
        }

        public void UpdateHeight()
        {
            int leftHeight = (Left != null) ? Left.Height : 0;
            int rightHeight = (Right != null) ? Right.Height : 0;
            Height = 1 + Math.Max(leftHeight, rightHeight);
        }

        public int BalanceFactor()
        {
            int leftHeight = (Left != null) ? Left.Height : 0;
            int rightHeight = (Right != null) ? Right.Height : 0;
            return leftHeight - rightHeight;
        }

        public RedBlackTreeNode<T> RotateLeft()
        {
            RedBlackTreeNode<T> newRoot = Right;
            Right = newRoot.Left;
            newRoot.Left = this;
            UpdateHeight();
            newRoot.UpdateHeight();
            return newRoot;
        }

        public RedBlackTreeNode<T> RotateRight()
        {
            RedBlackTreeNode<T> newRoot = Left;
            Left = newRoot.Right;
            newRoot.Right = this;
            UpdateHeight();
            newRoot.UpdateHeight();
            return newRoot;
        }
    }

    // Вузли АВЛ-дерева
    internal class AVLNode
    {
        public int value; // Потужність
        public int height;  // Висота піддерева
        public AVLNode left;
        public AVLNode right;

        public AVLNode(int value)
        {
            this.value = value;
            height = 1;
        }
    }
}

