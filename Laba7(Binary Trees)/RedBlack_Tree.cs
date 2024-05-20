using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba7_Binary_Trees_
{ 
        public enum ColorNode
        {
            Red,
            Black
        }
        public class RedBlackTreeNode
        {
            public int Value { get; set; }
            public RedBlackTreeNode Left { get; set; }
            public RedBlackTreeNode Right { get; set; }
            public RedBlackTreeNode Parent { get; set; }
            public ColorNode NodeColor { get; set; }

            public RedBlackTreeNode(int value)
            {
                Value = value;
                NodeColor = ColorNode.Red;
            }

            //public RedBlackTreeNode()
            //{

            //}

        }

        public class RedBlackTree
        {
            public RedBlackTreeNode Root { get; private set; }

            private RedBlackTreeNode GetGrandparent(RedBlackTreeNode node)
            {
                if (node?.Parent == null)
                    return null;
                return node.Parent.Parent;
            }

            private RedBlackTreeNode GetUncle(RedBlackTreeNode node)
            {
                var grandparent = GetGrandparent(node);
                if (grandparent == null)
                    return null;
                if (grandparent.Left == node.Parent)
                    return grandparent.Right;
                else
                    return grandparent.Left;
            }

            private void RotateLeft(RedBlackTreeNode node)
            {
                var newParent = node.Right;
                ReplaceNode(node, newParent);
                node.Right = newParent.Left;
                if (newParent.Left != null)
                    newParent.Left.Parent = node;
                newParent.Left = node;
                node.Parent = newParent;
            }

            private void RotateRight(RedBlackTreeNode node)
            {
                var newParent = node.Left;
                ReplaceNode(node, newParent);
                node.Left = newParent.Right;
                if (newParent.Right != null)
                    newParent.Right.Parent = node;
                newParent.Right = node;
                node.Parent = newParent;
            }

            private void ReplaceNode(RedBlackTreeNode oldNode, RedBlackTreeNode newNode)
            {
                if (oldNode.Parent == null)
                    Root = newNode;
                else
                {
                    if (oldNode == oldNode.Parent.Left)
                        oldNode.Parent.Left = newNode;
                    else
                        oldNode.Parent.Right = newNode;
                }
                if (newNode != null)
                    newNode.Parent = oldNode.Parent;
            }

            public void Insert(int value)
            {
                var node = new RedBlackTreeNode(value);
                if (Root == null)
                    Root = node;
                else
                {
                    RedBlackTreeNode temp = Root;
                    while (true)
                    {
                        if (node.Value < temp.Value)
                        {
                            if (temp.Left == null)
                            {
                                temp.Left = node;
                                break;
                            }
                            else
                                temp = temp.Left;
                        }
                        else if (node.Value > temp.Value)
                        {
                            if (temp.Right == null)
                            {
                                temp.Right = node;
                                break;
                            }
                            else
                            {
                                temp = temp.Right;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }
                    node.Parent = temp;
                }
                InsertCase1(node);
            }
            private void InsertCase1(RedBlackTreeNode node)
            {
                if (node.Parent == null)
                    node.NodeColor = ColorNode.Black;
                else
                    InsertCase2(node);
            }

            private void InsertCase2(RedBlackTreeNode node)
            {
                if (node.Parent.NodeColor == ColorNode.Black)
                    return;
                else
                    InsertCase3(node);
            }

            private void InsertCase3(RedBlackTreeNode node)
            {
                var uncle = GetUncle(node);
                if (uncle != null && uncle.NodeColor == ColorNode.Red)
                {
                    node.Parent.NodeColor = ColorNode.Black;
                    uncle.NodeColor = ColorNode.Black;
                    var grandparent = GetGrandparent(node);
                    grandparent.NodeColor = ColorNode.Red;
                    InsertCase1(grandparent);
                }
                else
                    InsertCase4(node);
            }

            private void InsertCase4(RedBlackTreeNode node)
            {
                var grandparent = GetGrandparent(node);
                if (node == node.Parent.Right && node.Parent == grandparent.Left)
                {
                    RotateLeft(node.Parent);
                    node = node.Left;
                }
                else if (node == node.Parent.Left && node.Parent == grandparent.Right)
                {
                    RotateRight(node.Parent);
                    node = node.Right;
                }
                InsertCase5(node);
            }

            private void InsertCase5(RedBlackTreeNode node)
            {
                var grandparent = GetGrandparent(node);
                node.Parent.NodeColor = ColorNode.Black;
                grandparent.NodeColor = ColorNode.Red;
                if (node == node.Parent.Left)
                    RotateRight(grandparent);
                else
                    RotateLeft(grandparent);
            }
            public RedBlackTreeNode Search(int value)
            {
                RedBlackTreeNode node = Root;
                while (node != null)
                {
                    if (value < node.Value)
                        node = node.Left;
                    else if (value > node.Value)
                        node = node.Right;
                    else
                        return node;
                }
                return null;
            }
            public RedBlackTreeNode Attack(int value)
            {
                RedBlackTreeNode node = null;
                for (int i = 0; i < 500; i++, value++)
                {
                    node = Search(value);
                    if (node != null && this.Root != node)
                        return node;
                }

                return node;
            }
            public void Delete(int value)
            {
                var node = FindNode(Root, value);
                if (node != null)
                {
                    DeleteNode(node);
                }
            }
            private RedBlackTreeNode FindNode(RedBlackTreeNode node, int value)
            {
                while (node != null)
                {
                    if (value < node.Value)
                        node = node.Left;
                    else if (value > node.Value)
                        node = node.Right;
                    else
                        return node;
                }
                return null;
            }
            private void DeleteNode(RedBlackTreeNode node)
            {
                if (node.Left != null && node.Right != null)
                {
                    var pred = MaximumNode(node.Left);
                    node.Value = pred.Value;
                    node = pred;
                }
                var child = (node.Right == null) ? node.Left : node.Right;
                if (NodeColor(node) == ColorNode.Black)
                {
                    node.NodeColor = NodeColor(child);
                    DeleteCase1(node);
                }
                ReplaceNode(node, child);
            }
            private RedBlackTreeNode MaximumNode(RedBlackTreeNode node)
            {
                while (node.Right != null)
                {
                    node = node.Right;
                }
                return node;
            }
            private ColorNode NodeColor(RedBlackTreeNode node)
            {
                return node == null ? ColorNode.Black : node.NodeColor;
            }
            private void DeleteCase1(RedBlackTreeNode node)
            {
                if (node.Parent != null)
                    DeleteCase2(node);
            }
            private void DeleteCase2(RedBlackTreeNode node)
            {
                var sibling = GetSibling(node);
                if (NodeColor(sibling) == ColorNode.Red)
                {
                    node.Parent.NodeColor = ColorNode.Red;
                    sibling.NodeColor = ColorNode.Black;
                    if (node == node.Parent.Left)
                        RotateLeft(node.Parent);
                    else
                        RotateRight(node.Parent);
                }
                DeleteCase3(node);
            }
            private RedBlackTreeNode GetSibling(RedBlackTreeNode node)
            {
                if (node == node.Parent.Left)
                    return node.Parent.Right;
                else
                    return node.Parent.Left;
            }
            private void DeleteCase3(RedBlackTreeNode node)
            {
                var sibling = GetSibling(node);
                if (NodeColor(node.Parent) == ColorNode.Black && NodeColor(sibling) == ColorNode.Black &&
                    NodeColor(sibling.Left) == ColorNode.Black && NodeColor(sibling.Right) == ColorNode.Black)
                {
                    sibling.NodeColor = ColorNode.Red;
                    DeleteCase1(node.Parent);
                }
                else
                    DeleteCase4(node);
            }
            private void DeleteCase4(RedBlackTreeNode node)
            {
                var sibling = GetSibling(node);
                if (NodeColor(node.Parent) == ColorNode.Red && NodeColor(sibling) == ColorNode.Black &&
                    NodeColor(sibling.Left) == ColorNode.Black && NodeColor(sibling.Right) == ColorNode.Black)
                {
                    sibling.NodeColor = ColorNode.Red;
                    node.Parent.NodeColor = ColorNode.Black;
                }
                else
                    DeleteCase5(node);
            }
            private void DeleteCase5(RedBlackTreeNode node)
            {
                var sibling = GetSibling(node);
                if (node == node.Parent.Left &&
                    NodeColor(sibling) == ColorNode.Black &&
                    NodeColor(sibling.Left) == ColorNode.Red &&
                    NodeColor(sibling.Right) == ColorNode.Black)
                {
                    sibling.NodeColor = ColorNode.Red;
                    sibling.Left.NodeColor = ColorNode.Black;
                    RotateRight(sibling);
                }
                else if (node == node.Parent.Right &&
                    NodeColor(sibling) == ColorNode.Black &&
                    NodeColor(sibling.Right) == ColorNode.Red &&
                    NodeColor(sibling.Left) == ColorNode.Black)
                {
                    sibling.NodeColor = ColorNode.Red;
                    sibling.Right.NodeColor = ColorNode.Black;
                    RotateLeft(sibling);
                }
                DeleteCase6(node);
            }
            private void DeleteCase6(RedBlackTreeNode node)
            {
                var sibling = GetSibling(node);
                sibling.NodeColor = NodeColor(node.Parent);
                node.Parent.NodeColor = ColorNode.Black;
                if (node == node.Parent.Left)
                {
                    sibling.Right.NodeColor = ColorNode.Black;
                    RotateLeft(node.Parent);
                }
                else
                {
                    sibling.Left.NodeColor = ColorNode.Black;
                    RotateRight(node.Parent);
                }
            }

        public void Search(int value, List<RedBlackTreeNode> way)
        {
            var node = Root;
            node = SearchNode(node, value, way);
            if (node == null)
                Insert(value); // Якщо не знайдено - додати
        }
        internal RedBlackTreeNode SearchNode(RedBlackTreeNode current, int value, List<RedBlackTreeNode> way)
        {
            var currentNode = current;
            while (currentNode != null)
            {
                way.Add(currentNode);
                if (currentNode.Value == value)
                {
                    return currentNode;
                }
                else if (currentNode.Value < value)
                {
                    currentNode = currentNode.Right;
                }
                else
                {
                    currentNode = currentNode.Left;
                }
            }
            return null;
        }


    }

    }
