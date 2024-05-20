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
        private RedBlackTree redBlackTree;
        AVL_Tree AVL_Tree = new AVL_Tree();
        List<AVLNode> AVLNodes = new List<AVLNode>();
        private Bitmap _RBbitmap;
        private Graphics _RBgraphics;
        public Form1()
        {
            InitializeComponent();
            redBlackTree = new RedBlackTree();

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

            _RBbitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _RBgraphics = Graphics.FromImage(_RBbitmap);

            foreach (var node in AVLNodes)
            {
                AVL_Tree.Insert(node.value);
            }
            DrawAVLTree();
            DrawTree();
            //stopwatch.Stop();
            //MessageBox.Show("Це зайняло " + stopwatch.ElapsedTicks + " ticks");
        }

        private void DrawTree()
        {
            _RBgraphics.Clear(Color.White);
            DrawNode(redBlackTree.Root, 300, 20, 150);
            pictureBox1.Image = _RBbitmap;
        }

        private void DrawNode(RedBlackTreeNode node, float x, float y, float dx)
        {
            if (node == null)
                return;

            Pen pen = node.NodeColor == ColorNode.Red ? Pens.Red : Pens.Black;

            _RBgraphics.DrawEllipse(pen, x, y, 30, 30);
            _RBgraphics.DrawString(node.Value.ToString(), new Font("Times New Roman", 10), Brushes.Black, x + 8, y + 8);

            if (node.Left != null)
            {
                _RBgraphics.DrawLine(Pens.Black, x + 15, y + 30, x - dx + 15, y + 50);
                DrawNode(node.Left, x - dx, y + 50, dx / 2);
            }

            if (node.Right != null)
            {
                _RBgraphics.DrawLine(Pens.Black, x + 15, y + 30, x + dx + 15, y + 50);
                DrawNode(node.Right, x + dx, y + 50, dx / 2);
            }
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
                AVLNodes.Add(new AVLNode(power));
                DrawTree();
                DrawAVLTree();
            }
            

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

                //string path = GetPath(RedBlackTreeNode(power));
                List<RedBlackTreeNode> RB_way = new List<RedBlackTreeNode>();
                List<AVLNode> AVL_way = new List<AVLNode>();
                

                AVL_Tree.Search(power, AVL_way);
                redBlackTree.Search(power, RB_way);


                bool Ishere_avl = AVLNodes.Any(node_to_search => node_to_search.value == power);

                if (!Ishere_avl) AVLNodes.Add(new AVLNode(power));

                foreach (var node_on_way in RB_way)
                    textBox2.Text += node_on_way.Value.ToString() + " -> ";
                foreach (var node_on_way in AVL_way)
                    textBox3.Text += node_on_way.value.ToString() + " -> ";
                DrawTree();
                DrawAVLTree();
            }
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
                DrawTree();
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
