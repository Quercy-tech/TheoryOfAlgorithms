using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Laba8Console
{
    internal class AVL_Tree
    {
        public AVLNode root;


        // Додавання вузла
        public void Insert(int value)
        {
            root = Insert(root, value);
        }

        private AVLNode Insert(AVLNode node, int value)
        {
            if (node == null)
            {
                return new AVLNode(value);
            }

            if (value < node.value)
            {
                node.left = Insert(node.left, value);
            }
            else
            {
                node.right = Insert(node.right, value);
            }


            // Реалізація балансування АВЛ-дерева
            node.height = Math.Max(Height(node.left), Height(node.right)) + 1;

            int balance = GetBalance(node);

            if (balance > 1) // Якщо різниця висот > 1 (ліве піддерево довше)  
            {
                if (value < node.left.value) // Якщо потужність доданого вузла > потужності лівого сина  
                {
                    return RightRotate(node); // Правий оберт
                }

                else // Якщо потужність доданого вузла <= потужності лівого сина  
                {
                    node.left = LeftRotate(node.left);
                    return RightRotate(node);
                }
            }
            else if (balance < -1) // Якщо різниця висот > 1 (праве піддерево довше)  
            {
                if (value > node.right.value)  // Якщо потужність доданого вузла > потужності правого сина
                {
                    return LeftRotate(node); // Лівий оберт
                }

                else// Якщо потужність доданого вузла <= потужності правого сина  
                {
                    node.right = RightRotate(node.right);
                    return LeftRotate(node);
                }
            }

            return node;
        }


        // Видалення вузла
        public void Remove(int value)
        {
            root = RemoveNode(root, value);
        }

        private AVLNode RemoveNode(AVLNode node, int value)
        {

            if (node == null)
            {
                return node;
            }

            if (value < node.value)
            {
                node.left = RemoveNode(node.left, value);
            }

            else if (value > node.value)
            {
                node.right = RemoveNode(node.right, value);
            }

            else // Якщо потужність видаленого вузла = потужності поточного вузла 
            {
                if (node.left == null || node.right == null)
                {
                    AVLNode temp = null;
                    if (temp == node.left)
                        temp = node.right;  // Якщо у нас лише правий син - повертаемо його
                    else
                        temp = node.left;  // Якщо у нас лише лівий син - повертаемо його

                    if (temp == null)    // Якщо у нас синів нема - видааляємо вузол
                    {
                        temp = node;
                        node = null;
                    }
                    else
                        node = temp;
                }
                else // Якщо у нас два сини - шукаємо мінімальний та повертаемо його
                {
                    AVLNode temp = FindMinValue(node.right);
                    node.value = temp.value;
                    node.right = RemoveNode(node.right, temp.value);
                }
            }

            if (node == null)
            {
                return node;
            }


            // Реалізація балансування АВЛ-дерева
            node.height = 1 + Math.Max(Height(node.left), Height(node.right));

            int balance = GetBalance(node);

            if (balance > 1 && GetBalance(node.left) >= 0)
            {
                return RightRotate(node);
            }

            if (balance > 1 && GetBalance(node.left) < 0)
            {
                node.left = LeftRotate(node.left);
                return RightRotate(node);
            }
            if (balance < -1 && GetBalance(node.right) <= 0)
            {
                return LeftRotate(node);
            }

            if (balance < -1 && GetBalance(node.right) > 0)
            {
                node.right = RightRotate(node.right);
                return LeftRotate(node);
            }
            return node;
        }

        // Пошук мінімального серед листків для вставки замість корення
        private AVLNode FindMinValue(AVLNode node)
        {
            AVLNode current = node;

            while (current.left != null)
                current = current.left;

            return current;
        }



        //Пошук вузла
        public void Search(int value, List<AVLNode> way)
        {
            var node = root;
            node = SearchNode(node, value, way);
            if (node == null)
                Insert(value); // Якщо не знайдено - додати
        }
        internal AVLNode SearchNode(AVLNode current, int value, List<AVLNode> way)
        {
            var currentNode = current;
            while (currentNode != null)
            {
                way.Add(currentNode);
                if (currentNode.value == value)
                {
                    return currentNode;
                }
                else if (currentNode.value < value)
                {
                    currentNode = currentNode.right;
                }
                else
                {
                    currentNode = currentNode.left;
                }
            }
            return null;
        }



        // Функції для збалансування

        // Лівий оберт
        private AVLNode LeftRotate(AVLNode node)
        {
            AVLNode right = node.right;
            AVLNode leftSubtree = right.left;

            right.left = node;
            node.right = leftSubtree;

            node.height = Math.Max(Height(node.left), Height(node.right)) + 1;
            right.height = Math.Max(Height(right.left), Height(right.right)) + 1;

            return right;
        }

        //Правий оберт
        private AVLNode RightRotate(AVLNode node)
        {
            AVLNode left = node.left;
            AVLNode rightSubtree = left.right;

            left.right = node;
            node.left = rightSubtree;

            node.height = Math.Max(Height(node.left), Height(node.right)) + 1;
            left.height = Math.Max(Height(left.left), Height(left.right)) + 1;

            return left;
        }


        // Висота піддерева
        private int Height(AVLNode node)
        {
            if (node == null)
                return 0;

            return node.height;
        }

        // Баланс дерева (різниця висот) - одна из властивостей
        private int GetBalance(AVLNode node)
        {
            if (node == null)
                return 0;

            return Height(node.left) - Height(node.right);
        }


    }
}

