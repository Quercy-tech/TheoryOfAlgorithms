
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Laba7_Binary_Trees_
{
    public class TreeDrawer
    {
        private const int NodeRadius = 15;
        private const int VerticalSpacing = 50;
        private const int HorizontalSpacing = 35;
        private static PictureBox pictureBox;

        public static void DrawTree(RedBlackTreeNode<int> root, PictureBox pb)
        {
            pictureBox = pb;
            if (root == null)
                return;

            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.LightGray);
                DrawNode(graphics, root, pictureBox.Width / 2, 50, pictureBox.Width / 2, 2);
            }

            pictureBox.Image = bitmap;
        }

        private static void DrawNode(Graphics graphics, RedBlackTreeNode<int> node, int x, int y, int xOffset, int depth)
        {
            if (node == null)
            {
                DrawEmptyNode(graphics, x, y);
                return;
            }

            Brush brush = node.Color == NodeColor.Black ? Brushes.Black : Brushes.Red;
            Pen pen = Pens.Black;

            // Draw node
            graphics.FillEllipse(brush, x - NodeRadius, y - NodeRadius, 2 * NodeRadius, 2 * NodeRadius);
            graphics.DrawEllipse(pen, x - NodeRadius, y - NodeRadius, 2 * NodeRadius, 2 * NodeRadius);
            graphics.DrawString(node.Value.ToString(), SystemFonts.DefaultFont, Brushes.White, x - 8, y - 4);



            // Draw left child
            int xOffsetLeft = xOffset / 2;
            int xLeft = x - xOffsetLeft;
            int yLeft = y + VerticalSpacing;
            int depthLeft = depth + 1;
            graphics.DrawLine(pen, x, y + NodeRadius, xLeft, yLeft - NodeRadius);
            DrawNode(graphics, node.Left, xLeft, yLeft, xOffsetLeft, depthLeft);

            // Draw right child
            int xOffsetRight = xOffset / 2;
            int xRight = x + xOffsetRight;
            int yRight = y + VerticalSpacing;
            int depthRight = depth + 1;
            graphics.DrawLine(pen, x, y + NodeRadius, xRight, yRight - NodeRadius);
            DrawNode(graphics, node.Right, xRight, yRight, xOffsetRight, depthRight);
        }

        private static void DrawEmptyNode(Graphics graphics, int x, int y)
        {
            Brush brush = Brushes.Black;
            Pen pen = Pens.Black;

            // Draw node
            graphics.FillEllipse(brush, x - NodeRadius, y - NodeRadius, 2 * NodeRadius, 2 * NodeRadius);
            graphics.DrawEllipse(pen, x - NodeRadius, y - NodeRadius, 2 * NodeRadius, 2 * NodeRadius);
            graphics.DrawString("пусто", SystemFonts.DefaultFont, Brushes.White, x - 15, y - 8);
        }

        public static void DrawTreeWithHighlight(RedBlackTreeNode<int> root, int searchedValue, List<int> path)
        {
            if (root == null)
                return;

            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.LightGray);
                DrawNodeWithHighlight(graphics, root, pictureBox.Width / 2, 50, pictureBox.Width / 2, 1, searchedValue, path);
            }

            pictureBox.Image = bitmap;
        }

        private static void DrawNodeWithHighlight(Graphics graphics, RedBlackTreeNode<int> node, int x, int y, int xOffset, int depth, int searchedValue, List<int> path)
        {
            if (node == null)
            {
                // Draw a node with "null" label
                DrawEmptyNode(graphics, x, y);

                // Draw a line connecting the null node to its parent
                int parentY = y - VerticalSpacing;
                graphics.DrawLine(Pens.Black, x, parentY + NodeRadius, x, y - NodeRadius);

                return;
            }

            Brush brush = node.Color == NodeColor.Black ? Brushes.Black : Brushes.Red;
            Pen pen = Pens.Black;

            if (node.Value == searchedValue || path.Contains(node.Value))
                brush = Brushes.Orange;

            // Draw node
            graphics.FillEllipse(brush, x - NodeRadius, y - NodeRadius, 2 * NodeRadius, 2 * NodeRadius);
            graphics.DrawEllipse(pen, x - NodeRadius, y - NodeRadius, 2 * NodeRadius, 2 * NodeRadius);
            graphics.DrawString(node.Value.ToString(), SystemFonts.DefaultFont, Brushes.White, x - 15, y - 8);

            if (node.Left != null)
            {
                // Draw left child
                int xOffsetLeft = xOffset / 2;
                int xLeft = x - xOffsetLeft;
                int yLeft = y + VerticalSpacing;
                int depthLeft = depth + 1;

                graphics.DrawLine(pen, x, y + NodeRadius, xLeft, yLeft - NodeRadius);
                DrawNodeWithHighlight(graphics, node.Left, xLeft, yLeft, xOffsetLeft, depthLeft, searchedValue, path);
            }
            else
            {
                // Draw a node with "null" label for the left child
                int xLeft = x - xOffset / 2;
                int yLeft = y + VerticalSpacing;
                DrawEmptyNode(graphics, xLeft, yLeft);

                // Draw a line connecting the parent to the null child
                graphics.DrawLine(pen, x, y + NodeRadius, xLeft, yLeft);
            }

            if (node.Right != null)
            {
                // Draw right child
                int xOffsetRight = xOffset / 2;
                int xRight = x + xOffsetRight;
                int yRight = y + VerticalSpacing;
                int depthRight = depth + 1;

                graphics.DrawLine(pen, x, y + NodeRadius, xRight, yRight - NodeRadius);
                DrawNodeWithHighlight(graphics, node.Right, xRight, yRight, xOffsetRight, depthRight, searchedValue, path);
            }
            else
            {
                // Draw a node with "null" label for the right child
                int xRight = x + xOffset / 2;
                int yRight = y + VerticalSpacing;
                DrawEmptyNode(graphics, xRight, yRight);

                // Draw a line connecting the parent to the null child
                graphics.DrawLine(pen, x, y + NodeRadius, xRight, yRight);
            }
        }




    }
}






