using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Laba7_Binary_Trees_
{
    //public abstract
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            // var bst = new BinarySearchTree();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bst.Insert(10);
            bst.Insert(6);
            bst.Insert(11);
            bst.Insert(3);
            bst.Insert(7);
            bst.Insert(9);
            bst.Insert(8);
            bst.Insert(1);
            bst.Insert(5);
            bst.Insert(14);
            bst.Insert(13);
            bst.Insert(12);
            bst.Insert(16);
            bst.Insert(15);
            bst.Insert(17);

            stopwatch.Stop();
            MessageBox.Show("Це зайняло " + stopwatch.ElapsedTicks + " ticks");
        }

        public BinarySearchTree bst = new BinarySearchTree();
         public static string text = "";

        public class Node
        {
            public int Power { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(int power)
            {
                Power = power;
                Left = null;
                Right = null;
            }
        }

        public class BinarySearchTree
        {
            private Node root;

            public void Insert(int power)
            {
                root = Insert(root, power);
            }

            private Node Insert(Node node, int power)
            {
                if (node == null)
                {
                    text += $"\n Creating a device with {power} power";
                    return Insert(new Node(power), power);
                }


                if (power < node.Power)
                {
                    text += $"{node.Power}, ";
                    node.Left = Insert(node.Left, power);
                }
                else if (power > node.Power)
                {
                    text += $"{node.Power}, ";
                    node.Right = Insert(node.Right, power);
                }

                return node;
            }

            public Node Find(int power)
            {
                return Find(root, power);
            }

            private Node Find(Node node, int power)
            {
                if (node == null)
                {
                    text += $"No device found, creating a new device with power {power}  \n";
                    return Insert(node, power);

                }

                if (node.Power == power)
                {
                    text += $"\n Device with power {power} found";
                    return node;
                }



                if (power < node.Power)
                {
                    text += $"{node.Power}, ";
                    return Find(node.Left, power);
                }
                else
                {
                    text += $"{node.Power}, ";
                    return Find(node.Right, power);
                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = "";

            if (!int.TryParse(textBox1.Text, out int power) || power <= 0)
            {
                MessageBox.Show("Enter a valid input");
            }
            else
            {
                bst.Insert(power);
                MessageBox.Show(text);
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            text = "";

            if (!int.TryParse(textBox1.Text, out int power) || power <= 0)
            {
                MessageBox.Show("Enter a valid input");
            }
            else
            {
                bst.Find(power);
                MessageBox.Show(text);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
