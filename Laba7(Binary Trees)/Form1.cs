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
        RedBlackTree RB_Tree = new RedBlackTree();
        AVL_Tree AVL_Tree = new AVL_Tree();
        List<RBNode> rbNodes = new List<RBNode>();
        List<AVLNode> AVLNodes = new List<AVLNode>();

        public Form1()
        {
            InitializeComponent();

           
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

           

            

            AVLNodes.Add(new AVLNode(10));
            AVLNodes.Add(new AVLNode(6));
            AVLNodes.Add(new AVLNode(13));
            AVLNodes.Add(new AVLNode(3));
            AVLNodes.Add(new AVLNode(8));
            AVLNodes.Add(new AVLNode(11));
            AVLNodes.Add(new AVLNode(15));
            AVLNodes.Add(new AVLNode(1));
            AVLNodes.Add(new AVLNode(5));
            AVLNodes.Add(new AVLNode(7));
            AVLNodes.Add(new AVLNode(9));
            AVLNodes.Add(new AVLNode(12));
            AVLNodes.Add(new AVLNode(14));
            AVLNodes.Add(new AVLNode(16));
            AVLNodes.Add(new AVLNode(17));

            rbNodes.Add(new RBNode(10, false));
            rbNodes.Add(new RBNode(6, true));
            rbNodes.Add(new RBNode(13, false));
            rbNodes.Add(new RBNode(3, false));
            rbNodes.Add(new RBNode(8, false));
            rbNodes.Add(new RBNode(11, false));
            rbNodes.Add(new RBNode(15, true));
            rbNodes.Add(new RBNode(1, true));
            rbNodes.Add(new RBNode(5, true));
            rbNodes.Add(new RBNode(7, true));
            rbNodes.Add(new RBNode(9, true));
            rbNodes.Add(new RBNode(12, true));
            rbNodes.Add(new RBNode(14, true));
            rbNodes.Add(new RBNode(16, true));
            rbNodes.Add(new RBNode(17, false));

            foreach (var node in rbNodes)
            {
                RB_Tree.Insert(node.Value);
            }

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
                RB_Tree.Insert(power);
                AVL_Tree.Insert(power);

                rbNodes.Add(new RBNode(power, true));
                AVLNodes.Add(new AVLNode(power));

                DrawAVLTree();
                DrawRBTree();
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

                List<RBNode> RB_way = new List<RBNode>();
                List<AVLNode> AVL_way = new List<AVLNode>();

                RB_Tree.Search(power, RB_way);
                AVL_Tree.Search(power, AVL_way);


                bool Ishere_rb = rbNodes.Any(node_to_search => node_to_search.Value == power);
                bool Ishere_avl = AVLNodes.Any(node_to_search => node_to_search.value == power);

                if (!Ishere_rb) rbNodes.Add(new RBNode(power, true));
                if (!Ishere_avl) AVLNodes.Add(new AVLNode(power));

                foreach (var node_on_way in RB_way)
                    textBox2.Text += node_on_way.Value.ToString() + " -> ";

                foreach (var node_on_way in AVL_way)
                    textBox3.Text += node_on_way.value.ToString() + " -> ";

                DrawRBTree();
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
                RB_Tree.Delete(power);
                AVL_Tree.Remove(power);
                //MessageBox.Show(text);

                foreach (var node_to_del in rbNodes)
                {
                    if (node_to_del.Value == power) rbNodes.Remove(new RBNode(power, true));
                }

                foreach (var node_to_del in AVLNodes)
                {
                    if (node_to_del.value == power) AVLNodes.Remove(new AVLNode(power));
                }
            }

            DrawRBTree();
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
            DrawRBTree();
            DrawAVLTree();
        }


        // Draw red-black tree 
        private void DrawRBTree()
        {

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox2.Height);
            Graphics g = Graphics.FromImage(bmp);

            DrawRBNode(RB_Tree.root, g, pictureBox1.Width / 2, 50, (pictureBox1.Width / 4));

            pictureBox1.Image = bmp;
        }

        // Draw red-black tree node
        private void DrawRBNode(RBNode node, Graphics g, int x, int y, int xOffset)
        {
            if (node == null)
                return;

            // red or black?
            if (node.IsRed)
            {
                g.DrawEllipse(Pens.Red, x - 20, y - 20, 40, 40);
                g.DrawString(node.Value.ToString(), new Font("Arial", 10), Brushes.Red, x - 10, y - 10);
            }
            else
            {
                g.DrawEllipse(Pens.Black, x - 20, y - 20, 40, 40);
                g.DrawString(node.Value.ToString(), new Font("Arial", 9), Brushes.Black, x - 10, y - 10);
            }


            // Draw node children
            DrawRBNode(node.LeftChild, g, x - xOffset, y + 60, (xOffset / 2) + 3);
            DrawRBNode(node.RightChild, g, x + xOffset, y + 60, (xOffset / 2) + 3);

            // Is not null?
            if (node.LeftChild != null)
                g.DrawLine(Pens.Black, x, y, x - xOffset, y + 40);
            if (node.RightChild != null)
                g.DrawLine(Pens.Black, x, y, x + xOffset, y + 40);
        }


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
