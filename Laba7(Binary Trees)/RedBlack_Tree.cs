using System;

namespace Laba7_Binary_Trees_
{
    public class RedBlackTree<T> where T : IComparable<T>
    {
        public RedBlackTreeNode<T> Root { get; private set; }

        // Метод для вставки елемента в червоно-чорне дерево
        public void Insert(T value)
        {
            Root = InsertRecursive(Root, value);
            Root.Color = NodeColor.Black; // Корінь завжди чорний
        }

        private RedBlackTreeNode<T> InsertRecursive(RedBlackTreeNode<T> node, T value)
        {
            if (node == null)
                return new RedBlackTreeNode<T>(value);

            if (value.CompareTo(node.Value) < 0)
                node.Left = InsertRecursive(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = InsertRecursive(node.Right, value);

            // Перевірка на порушення властивостей червоно-чорного дерева
            if (IsRed(node.Right) && !IsRed(node.Left))
                node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left?.Left))
                node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);

            return node;
        }

        // Пошук елемента в червоно-чорному дереві
        public RedBlackTreeNode<T> Search(T value)
        {
            RedBlackTreeNode<T> current = Root;
            while (current != null)
            {
                if (value.CompareTo(current.Value) == 0)
                    return current;
                else if (value.CompareTo(current.Value) < 0)
                    current = current.Left;
                else
                    current = current.Right;

                // Перевірка, чи поточний вузол є листком
                if (current != null && current.Left == null && current.Right == null && !current.Value.Equals(value))
                    return null; // Якщо поточний вузол - листок, і не знайдено шукане значення, повертаємо null
            }
            return null; // Елемент не знайдено
        }


        // Видалення елемента з червоно-чорного дерева
        public void Delete(T value)
        {
            Root = DeleteRecursive(Root, value);
            if (Root != null) // Перевірка на наявність кореня
                Root.Color = NodeColor.Black; // Корінь завжди чорний
        }

        // Рекурсивний метод для видалення елемента
        private RedBlackTreeNode<T> DeleteRecursive(RedBlackTreeNode<T> node, T value)
        {
            if (node == null)
                return null;

            if (value.CompareTo(node.Value) < 0)
                node.Left = DeleteRecursive(node.Left, value);
            else if (value.CompareTo(node.Value) > 0)
                node.Right = DeleteRecursive(node.Right, value);
            else
            {
                if (node.Left == null)
                    return node.Right;
                else if (node.Right == null)
                    return node.Left;

                // Вузол має два нащадки, знаходимо найменший в правому піддереві
                RedBlackTreeNode<T> minNode = MinimumValueNode(node.Right);
                node.Value = minNode.Value;
                node.Right = DeleteRecursive(node.Right, minNode.Value);

                // Перевірка на порушення властивостей червоно-чорного дерева
                if (IsRed(node.Right) && !IsRed(node.Left))
                    node = RotateLeft(node);
                if (IsRed(node.Left) && IsRed(node.Left?.Left))
                    node = RotateRight(node);
                if (IsRed(node.Left) && IsRed(node.Right))
                    FlipColors(node);
            }

            // Перебалансування після видалення
            if (IsRed(node.Right) && !IsRed(node.Left))
                node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left?.Left))
                node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);

            // Оновлення висоти
            node.UpdateHeight();

            return node;
        }

        // Допоміжний метод для пошуку найменшого вузла в дереві
        private RedBlackTreeNode<T> MinimumValueNode(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        // Допоміжний метод для перевірки, чи є вузол червоним
        private bool IsRed(RedBlackTreeNode<T> node)
        {
            if (node == null)
                return false;
            return node.Color == NodeColor.Red;
        }

        // Метод для зміни кольорів вершини та її дітей
        private void FlipColors(RedBlackTreeNode<T> node)
        {
            node.Color = NodeColor.Red;
            node.Left.Color = NodeColor.Black;
            node.Right.Color = NodeColor.Black;
        }

        private RedBlackTreeNode<T> RotateLeft(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> newRoot = node.Right;
            node.Right = newRoot.Left;
            newRoot.Left = node;
            newRoot.Color = node.Color; // Зберігаємо колір вершини node
            node.Color = NodeColor.Red; // Змінюємо колір вершини node на червоний
            return newRoot;
        }

        private RedBlackTreeNode<T> RotateRight(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> newRoot = node.Left;
            node.Left = newRoot.Right;
            newRoot.Right = node;
            newRoot.Color = node.Color; // Зберігаємо колір вершини node
            node.Color = NodeColor.Red; // Змінюємо колір вершини node на червоний
            return newRoot;
        }
    }
}
