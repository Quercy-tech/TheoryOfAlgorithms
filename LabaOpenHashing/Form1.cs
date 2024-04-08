using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace LabaOpenHashing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public HashTableFirst hashtable = new HashTableFirst();
        public HashTableSecond hashtable2 = new HashTableSecond();
        public static int sizeTable = 10;
        public double totalValue = 0;

        public class Node
        {
            public string Key { get; set; }
            public object Value { get; set; }
            public Node Next { get; set; }

            public Node(string key, object value)
            {
                Key = key;
                Value = value;
            }
        }

        public class HashTableFirst
        {
            public List<Node>[] table;
            public int capacity = sizeTable;

            public HashTableFirst()
            {
                table = new List<Node>[capacity];
            }

            public int Hash(string key)
            {
                // Simple hash function using sum of character codes
                int hash = 0;
                foreach (char c in key)
                {
                    hash += (int)c;
                }
                return hash % capacity;
            }
                                                         
            public int Insert(string key, object value)
            {
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                int index = Hash(key);
                if (table[index] == null)
                {
                    table[index] = new List<Node>();
                }
                table[index].Add(new Node(key, value));
                //stopwatch.Stop();
                //MessageBox.Show("Пройшло " + stopwatch.ElapsedMilliseconds + "ms");
                return index;
            }

            public object Get(string key)
            {
                int index = Hash(key);
                if (table[index] == null)
                {
                    return null;
                }
                foreach (Node node in table[index])
                {
                    if (node.Key == key)
                    {
                        return node.Value;
                    }
                }
                return null;
            }

            public object Remove(string key)
            {
                int index = Hash(key);
                if (table[index] == null)
                {
                    return null;
                }
                for (int i = 0; i < table[index].Count; i++)
                {
                    if (table[index][i].Key == key)
                    {
                        object value = table[index][i].Value;
                        table[index].RemoveAt(i);
                        return value;
                    }
                }
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            object mark = textBox2.Text;
            if (int.TryParse(textBox2.Text, out int markInt) && markInt < 100 && markInt > 0)
            {
                int index = hashtable.Insert(name, markInt);
                listBox1.Items.Add($"Учень {name} з індексом {index} та оцінкою {mark} \n");
            }
            else
            {
                MessageBox.Show("Введіть число в проміжку від 0 до 100!");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name = textBox3.Text;

            if (int.TryParse(textBox3.Text, out int markInt) && markInt <= sizeTable && markInt > 0)
            {
                listBox1.Items.RemoveAt(markInt - 1);
            }
            else
            {
                MessageBox.Show("Введіть число в проміжку від 0 до " + sizeTable);
            }
            //try
            //{
            //    hashtable.Remove(name);
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Where is no such student as " + name);
            //}
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string name = textBox4.Text;
                object value = hashtable.Get(name);
            if (value != null)
            {
                MessageBox.Show("Mr " + name + " has " + value);
            }
            else
            {
                MessageBox.Show("Where is no such student as " + name);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public class HashTableSecond
        {
            public List<Node>[] table;
            public int capacity = sizeTable;

            public HashTableSecond()
            {
                table = new List<Node>[capacity];
            }
            private int Hash(string key)
            {
                const double C = 0.564;
                const double M = 3.44;
                // Simple hash function using sum of character codes
                double hash = 0;
                foreach (char c in key)
                {
                    hash += (double)c;
                }
                return  (int)Math.Abs(M * ((C * hash) % 1));
            }

            public int Insert(string key, object value)
            {
                int index = Hash(key);
                if (table[index] == null)
                {
                    table[index] = new List<Node>();
                }
                table[index].Add(new Node(key, value));
                return index;
            }

            public object Get(string key)
            {
                int index = Hash(key);
                if (table[index] == null)
                {
                    return null;
                }
                foreach (Node node in table[index])
                {
                    if (node.Key == key)
                    {
                        return node.Value;
                    }
                }
                return null;
            }

            public object Remove(string key)
            {
                int index = Hash(key);
                if (table[index] == null)
                {
                    return null;
                }
                for (int i = 0; i < table[index].Count; i++)
                {
                    if (table[index][i].Key == key)
                    {
                        object value = table[index][i].Value;
                        table[index].RemoveAt(i);
                        return value;
                    }
                }
                return null;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string name = textBox8.Text;
            object mark = textBox7.Text;
            if (int.TryParse(textBox7.Text, out int markInt) && markInt < 100 && markInt > 0)
            {
                int index = hashtable2.Insert(name, markInt);
                listBox2.Items.Add($"Учень {name} з індексом {index} та оцінкою {mark} \n");
            } else
            {
                MessageBox.Show("Введіть число в проміжку від 0 до 100!");
            }
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string name = textBox6.Text;
            if (int.TryParse(textBox3.Text, out int markInt) && markInt <= sizeTable && markInt > 0)
            {
                listBox2.Items.RemoveAt(markInt - 1);
            }
            else
            {
                MessageBox.Show("Введіть число в проміжку від 0 до " + sizeTable);
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string name = textBox5.Text;
            object value = hashtable2.Get(name);
            if (value != null)
            {
                MessageBox.Show("У учня " + name + " оцінка: " + value);
            }
            else
            {
                MessageBox.Show("Where is no such student as " + name);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

    }


}
