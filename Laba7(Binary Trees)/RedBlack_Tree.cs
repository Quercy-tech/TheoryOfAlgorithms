using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Laba7_Binary_Trees_
{
    internal class RedBlackTree
    {
        public RBNode root;

        //Додавання вузла
        public void Insert(int value)
        {
            root = InsertNode(root, value);
            root.IsRed = false; // корінь завжди чорний
        }

        private RBNode InsertNode(RBNode current, int value)
        {
            if (current == null)
            {
                return new RBNode(value, true);
            }

            if (value < current.Value)
            {
                current.LeftChild = InsertNode(current.LeftChild, value);
            }
            else if (value > current.Value)
            {
                current.RightChild = InsertNode(current.RightChild, value);
            }

            // Реалізація балансування червоно-чорного дерева
            bool IsRotate = false;


            if (IsRed(current.RightChild) && !IsRed(current.LeftChild))
            {
                current = RotateLeft(current);
                IsRotate = true;

            }
            if (IsRed(current.LeftChild) && IsRed(current.LeftChild.LeftChild))
            {
                current = RotateRight(current);
                IsRotate = true;

            }
            if (IsRed(current.LeftChild) && IsRed(current.RightChild))
            {
                FlipColors(current);
            }

            // Реалізація балансування червоно-чорного дерева, аналогічна до балансування збалансованого
            current.height = Math.Max(Height(current.LeftChild), Height(current.RightChild)) + 1;

            int balance = GetBalance(current);


            if (!IsRotate)
            {
                if (balance > 1) // Якщо різниця висот > 1 (ліве піддерево довше)
                {
                    if (value < current.LeftChild.Value) // Якщо потужність доданого вузла > потужності лівого сина  
                    {
                        return RotateRight(current);  // Правий оберт
                    }
                    else // Якщо потужність доданого вузла <= потужності лівого сина  
                    {
                        current.LeftChild = RotateLeft(current.LeftChild);
                        return RotateRight(current);
                    }
                }
                else if (balance < -1) // Якщо різниця висот > 1 (праве піддерево довше)  
                {
                    if (value > current.RightChild.Value) // Якщо потужність доданого вузла > потужності правого сина
                    {
                        return RotateLeft(current);// Лівий оберт
                    }

                    else      // Якщо потужність доданого вузла <= потужності правого сина  
                    {
                        current.RightChild = RotateRight(current.RightChild);
                        return RotateLeft(current);
                    }
                }
            }
            return current;
        }



        // Видалення вузла

        public void Delete(int value)
        {
            root = DeleteNode(root, value);
        }

        private RBNode DeleteNode(RBNode current, int value)
        {
            if (current == null)
            {
                return null;
            }

            if (value < current.Value)
            {
                current.LeftChild = DeleteNode(current.LeftChild, value);
            }
            else if (value > current.Value)
            {
                current.RightChild = DeleteNode(current.RightChild, value);
            }
            else // Якщо потужність видаленого вузла = потужності поточного вузла 
            {
                if (current.LeftChild == null) // Якщо у нас лише правий син - повертаемо його
                {
                    return current.RightChild;
                }
                if (current.RightChild == null) // Якщо у нас лише лівий син - повертаемо його
                {
                    return current.LeftChild;
                }

                // Якщо у нас два сини - шукаємо мінімальний та повертаемо його
                RBNode temp = current;
                current = FindMin(temp.RightChild);
                current.RightChild = DeleteMin(temp.RightChild);
                current.LeftChild = temp.LeftChild;
            }

            // Реалізація балансування червоно-чорного дерева
            if (IsRed(current.RightChild) && !IsRed(current.LeftChild))
            {
                current = RotateLeft(current);
            }
            if (IsRed(current.LeftChild) && IsRed(current.LeftChild.LeftChild))
            {
                current = RotateRight(current);
            }
            if (IsRed(current.LeftChild) && IsRed(current.RightChild))
            {
                FlipColors(current);
            }

            // Реалізація балансування червоно-чорного дерева, аналогічна до балансування збалансованого
            current.height = 1 + Math.Max(Height(current.LeftChild), Height(current.RightChild));

            int balance = GetBalance(current);


            if (balance > 1 && GetBalance(current.LeftChild) >= 0)
                return RotateRight(current);

            if (balance > 1 && GetBalance(current.LeftChild) < 0)
            {
                current.LeftChild = RotateLeft(current.LeftChild);
                return RotateRight(current);
            }
            if (balance < -1 && GetBalance(current.RightChild) <= 0)
                return RotateLeft(current);

            if (balance < -1 && GetBalance(current.RightChild) > 0)
            {
                current.RightChild = RotateRight(current.RightChild);
                return RotateLeft(current);
            }

            return current;
        }

        // Пошук мінімального серед листків для вставки замість корення
        private RBNode FindMin(RBNode current)
        {
            if (current.LeftChild == null)
            {
                return current;
            }
            return FindMin(current.LeftChild);
        }

        // Видалення мінімального серед листків
        private RBNode DeleteMin(RBNode current)
        {
            if (current.LeftChild == null)
            {
                return current.RightChild;
            }
            current.LeftChild = DeleteMin(current.LeftChild);
            return current;
        }




        // Пошук у дереві

        public void Search(int value, List<RBNode> way)
        {
            var node = root;
            node = SearchNode(node, value, way);

            if (node == null) // Якщо не знайдено - додати
            {
                Insert(value);
            }

        }
        internal RBNode SearchNode(RBNode current, int value, List<RBNode> way)
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
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    currentNode = currentNode.LeftChild;
                }
            }
            return null;
        }

        // Функції для балансування червоно-чорного дерева

        // Лівий оберт
        private RBNode RotateLeft(RBNode node)
        {
            RBNode right = node.RightChild;
            RBNode left = right.LeftChild;

            node.RightChild = left;
            right.LeftChild = node;

            right.IsRed = node.IsRed;
            node.IsRed = true;

            node.height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;
            right.height = Math.Max(Height(right.LeftChild), Height(right.RightChild)) + 1;

            return right;
        }

        //Правий оберт
        private RBNode RotateRight(RBNode node)
        {
            RBNode left = node.LeftChild;
            RBNode right = left.RightChild;

            node.LeftChild = right;
            left.RightChild = node;

            left.IsRed = node.IsRed;
            node.IsRed = true;

            node.height = Math.Max(Height(node.LeftChild), Height(node.RightChild)) + 1;
            left.height = Math.Max(Height(left.LeftChild), Height(left.RightChild)) + 1;

            return left;
        }

        // Зміна кольору
        private void FlipColors(RBNode node)
        {
            node.IsRed = true;
            node.LeftChild.IsRed = false;
            node.RightChild.IsRed = false;
        }

        // Чи є красним?
        private bool IsRed(RBNode node)
        {
            if (node == null) return false;
            return node.IsRed;
        }

        // Висота піддерева
        private int Height(RBNode node)
        {
            if (node == null)
                return 0;

            return node.height;
        }

        // Баланс дерева (різниця висот) - одна из властивостей
        private int GetBalance(RBNode node)
        {
            if (node == null)
                return 0;

            return Height(node.LeftChild) - Height(node.RightChild);
        }
    }
}
