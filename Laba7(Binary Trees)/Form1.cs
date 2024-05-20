using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Laba7_Binary_Trees_
{
    
    public partial class Form1 : Form
    {
        private RedBlackTree<int> redBlackTree;
        AVL_Tree AVL_Tree = new AVL_Tree();
        List<AVLNode> AVLNodes = new List<AVLNode>();

        public Form1()
        {
            InitializeComponent();
            redBlackTree = new RedBlackTree<int>();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

           

            

            AVLNodes.Add(new AVLNode(110));
            AVLNodes.Add(new AVLNode(120));
            AVLNodes.Add(new AVLNode(200));
            AVLNodes.Add(new AVLNode(230));
            AVLNodes.Add(new AVLNode(235));
            AVLNodes.Add(new AVLNode(130));
            AVLNodes.Add(new AVLNode(150));
            AVLNodes.Add(new AVLNode(140));

            redBlackTree.Insert(110);
            redBlackTree.Insert(120);
            redBlackTree.Insert(200);
            redBlackTree.Insert(230);
            redBlackTree.Insert(235);
            redBlackTree.Insert(130);
            redBlackTree.Insert(150);
            redBlackTree.Insert(140);

            TreeDrawer.DrawTree(redBlackTree.Root, pictureBox1);
            foreach (var node in AVLNodes)
            {
                AVL_Tree.Insert(node.value);
            }

            //stopwatch.Stop();
            //MessageBox.Show("Це зайняло " + stopwatch.ElapsedTicks + " ticks");
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            //text = "";

            if (!int.TryParse(textBox1.Text, out int power) || power <= 0)
            {
                MessageBox.Show("Enter a valid input");
            }
            else 
            {
                AVL_Tree.Insert(power);

                redBlackTree.Insert(power);
                RefreshTreeView();
                AVLNodes.Add(new AVLNode(power));

                DrawAVLTree();
            }
            

        }

        private void RefreshTreeView()
        {
            TreeDrawer.DrawTree(redBlackTree.Root, pictureBox1);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //text = "";

            if (!int.TryParse(textBox1.Text, out int power) || power <= 0)
            {
                MessageBox.Show("Enter a valid input");
            }
            else
            {

                textBox2.Text = "";
                textBox3.Text = "";
                RedBlackTreeNode<int> result = redBlackTree.Search(power);
                if (result != null)
                {
                    List<int> path = GetPathToNode(redBlackTree.Root, power);
                    TreeDrawer.DrawTreeWithHighlight(redBlackTree.Root, power, path);
                }
                else
                {
                    MessageBox.Show("Елемент не знайдено!.");
                }

                List<AVLNode> AVL_way = new List<AVLNode>();

                AVL_Tree.Search(power, AVL_way);


                bool Ishere_avl = AVLNodes.Any(node_to_search => node_to_search.value == power);

                if (!Ishere_avl) AVLNodes.Add(new AVLNode(power));


                foreach (var node_on_way in AVL_way)
                    textBox3.Text += node_on_way.value.ToString() + " -> ";

                DrawAVLTree();
            }
        }

        private List<int> GetPathToNode(RedBlackTreeNode<int> node, int value)
        {
            List<int> path = new List<int>();

            RedBlackTreeNode<int> current = node;
            while (current != null)
            {
                path.Insert(0, current.Value); // Insert at the beginning to maintain the correct order
                int compareResult = value.CompareTo(current.Value);
                if (compareResult == 0)
                    break; // Stop if the value matches
                else if (compareResult < 0)
                    current = current.Left; // Move to the left child
                else
                    current = current.Right; // Move to the right child
            }

            return path;
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int power) || power <= 0)
            {
                MessageBox.Show("Enter a valid input");
            }
            else
            {
                redBlackTree.Delete(power);
                AVL_Tree.Remove(power);
                RefreshTreeView();
                //MessageBox.Show(text);

                foreach (var node_to_del in AVLNodes)
                {
                    if (node_to_del.value == power) AVLNodes.Remove(new AVLNode(power));
                }
            }
            DrawAVLTree();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawAVLTree();
        }


        // Draw red-black tree 
        
        // Draw AVL tree 
        private void DrawAVLTree()
        {

            Bitmap bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            Graphics g = Graphics.FromImage(bmp);


            DrawAVLNode(AVL_Tree.root, g, pictureBox2.Width / 2, 50, pictureBox2.Width / 4);


            pictureBox2.Image = bmp;
        }

        // Draw AVL tree node
        private void DrawAVLNode(AVLNode node, Graphics g, int x, int y, int xOffset)
        {
            if (node == null)
                return;

            g.DrawEllipse(Pens.Black, x - 20, y - 20, 40, 40);
            g.DrawString(node.value.ToString(), new Font("Arial", 9), Brushes.Black, x - 10, y - 10);

            DrawAVLNode(node.left, g, x - xOffset, y + 50, (xOffset / 2) + 3);
            DrawAVLNode(node.right, g, x + xOffset, y + 50, (xOffset / 2) + 3);

            if (node.left != null)
                g.DrawLine(Pens.Black, x, y, x - xOffset, y + 40);
            if (node.right != null)
                g.DrawLine(Pens.Black, x, y, x + xOffset, y + 40);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
