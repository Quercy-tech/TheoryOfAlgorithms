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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
           
            int index = hashtable.Insert("Stas", 99);
            students.Add("Stas");
            listBox1.Items.Add($"Учень Stas з індексом {index} та оцінкою 99 \n");
            int index2 = hashtable.Insert("Andrew", 98);
            students.Add("Andrew");
            listBox1.Items.Add($"Учень Andrew з індексом {index2} та оцінкою 98 \n");
            int index3 = hashtable.Insert("Oleksii", 95);
            students.Add("Oleksii");
            listBox1.Items.Add($"Учень Oleksii з індексом {index3} та оцінкою 95 \n");
            int index4 = hashtable.Insert("Maks", 37);
            students.Add("Maks");
            listBox1.Items.Add($"Учень Maks з індексом {index4} та оцінкою 37 \n");
            int index5 = hashtable.Insert("Kvartz", 2);
            students.Add("Kvartz");
            listBox1.Items.Add($"Учень Kvartz з індексом {index5} та оцінкою 2 \n");
            int index6 = hashtable.Insert("Katrine", 83);
            students.Add("Katrine");
            listBox1.Items.Add($"Учень Katrine з індексом {index6} та оцінкою 83 \n");
            int index7 = hashtable.Insert("Pavlo", 74);
            students.Add("Pavlo");
            listBox1.Items.Add($"Учень Pavlo з індексом {index7} та оцінкою 74 \n");
            int index8 = hashtable.Insert("Denis", 92);
            students.Add("Denis");
            listBox1.Items.Add($"Учень Denis з індексом {index8} та оцінкою 92 \n");


            int index9 = hashtable2.Insert("Stas", 99);
            students2.Add("Stas");
            listBox2.Items.Add($"Учень Stas з індексом {index} та оцінкою 99 \n");
            int index10 = hashtable2.Insert("Andrew", 98);
            students2.Add("Andrew");
            listBox2.Items.Add($"Учень Andrew з індексом {index2} та оцінкою 98 \n");
            int index11 = hashtable2.Insert("Oleksii", 95);
            students2.Add("Oleksii");
            listBox2.Items.Add($"Учень Oleksii з індексом {index3} та оцінкою 95 \n");
            int index12 = hashtable2.Insert("Maks", 37);
            students2.Add("Maks");
            listBox2.Items.Add($"Учень Maks з індексом {index4} та оцінкою 37 \n");
            int index13 = hashtable2.Insert("Kvartz", 2);
            students2.Add("Kvartz");
            listBox2.Items.Add($"Учень Kvartz з індексом {index5} та оцінкою 2 \n");
            int index14 = hashtable2.Insert("Katrine", 83);
            students2.Add("Katrine");
            listBox2.Items.Add($"Учень Katrine з індексом {index6} та оцінкою 83 \n");
            int index15 = hashtable2.Insert("Pavlo", 74);
            students2.Add("Pavlo");
            listBox2.Items.Add($"Учень Pavlo з індексом {index7} та оцінкою 74 \n");
            int index16 = hashtable2.Insert("Denis", 92);
            students2.Add("Denis");
            listBox2.Items.Add($"Учень Denis з індексом {index8} та оцінкою 92 \n");

            stopwatch.Stop();
            //MessageBox.Show("Це зайняло " + stopwatch.ElapsedTicks + "ticks");
        }



        public HashTableFirst hashtable = new HashTableFirst();
        public HashTableSecond hashtable2 = new HashTableSecond();
        public static int sizeTable = 13;
        public List<string> students = new List<string>();
        public List<string> students2 = new List<string>();

        public static int isReady = 0;
        public static int k = 0;

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
            public int positionInTable = 0;

            public HashTableFirst()
            {
                table = new List<Node>[capacity];
            }

            public int Hash(string key, int k)
            {
                // Simple hash function using sum of character codes
                int hash = 0;
                foreach (char c in key)
                {
                    hash += (int)c;
                }
                
                return (hash + k) % capacity;
            }

            public int Insert(string key, object value)
            {
                int k = 0;
                while (k < capacity)
                {
                    int index = Hash(key, k);

                    if (table[index] == null)
                    {
                        table[index] = new List<Node>();
                        table[index].Add(new Node(key, value));
                        return index; // Return the index where insertion occurred
                    }

                    k += 1;
                }

                // If table is full after trying all indices
                MessageBox.Show("The table is full :(");
                return -1; // Or handle table full condition appropriately
            }

            public object Get(string key)
            {
                int k = 0;
                while (k < capacity)
                {
                    int index = Hash(key, k);

                    if (table[index] != null && table[index][0].Key == key)
                    {
                        return table[index][0].Value;
                    }

                    k += 1;
                }

                return null; // Key not found
            }


            public object Remove(string key)
            {
                int k = 0;
                while (k < capacity)
                {
                    int index = Hash(key, k);

                    if (table[index] != null && table[index][0].Key == key)
                    {
                        object value = table[index][0].Value;
                        table[index] = null; // Clear the list at this index
                        return value;
                    }

                    k += 1;
                }

                return null; // Key not found
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            object mark = textBox2.Text;
            if (int.TryParse(textBox2.Text, out int markInt) && markInt < 100 && markInt > 0)
            {
                int index = hashtable.Insert(name, markInt);
                students.Add(name);
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
            try
            {
                if (int.TryParse(textBox3.Text, out int markInt) && markInt <= sizeTable && markInt > 0)
                {
                    listBox1.Items.RemoveAt(markInt - 1);
                    name = students[markInt-1];
                    hashtable.Remove(name);
                    students.RemoveAt(markInt-1);
                }
                else
                {
                    MessageBox.Show("Введіть число в проміжку від 0 до " + sizeTable);
                }
            } catch (Exception)
            {
                MessageBox.Show("There is no such student! ");
            }
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
            public int Hash(string key, int k)
            {
                int hash = 0;
                foreach (char c in key)
                {
                    hash += (int)c;
                }

                return (hash + k*k) % capacity;
            }

            public int Insert(string key, object value)
            {
                int k = 0;
                while (k < capacity)
                {
                    int index = Hash(key, k);

                    if (table[index] == null)
                    {
                        table[index] = new List<Node>();
                        table[index].Add(new Node(key, value));
                        return index; // Return the index where insertion occurred
                    }

                    k += 1;

                    if (table[index] != null)
                        continue;
                }

                // If table is full after trying all indices
                MessageBox.Show("The table is full :(");
                return -1; // Or handle table full condition appropriately
            }

            public object Get(string key)
            {
                int k = 0;
                while (k < capacity)
                {
                    int index = Hash(key, k);

                    if (table[index] != null && table[index][0].Key == key)
                    {
                        return table[index][0].Value;
                    }

                    k += 1;
                }

                return null; // Key not found
            }


            public object Remove(string key)
            {
                int k = 0;
                while (k < capacity)
                {
                    int index = Hash(key, k);

                    if (table[index] != null && table[index][0].Key == key)
                    {
                        object value = table[index][0].Value;
                        table[index] = null; // Clear the list at this index
                        return value;
                    }

                    k += 1;
                }

                return null; // Key not found
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
                students2.Add(name);
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
            try
            {
                if (int.TryParse(textBox6.Text, out int markInt) && markInt <= sizeTable && markInt > 0)
                {
                    listBox2.Items.RemoveAt(markInt - 1);
                    name = students2[markInt - 1];
                    hashtable2.Remove(name);
                    students2.RemoveAt(markInt - 1);
                }
                else
                {
                    MessageBox.Show("Введіть число в проміжку від 0 до " + sizeTable);
                }
            } catch (Exception)
            {
                MessageBox.Show("There is no such student! ");
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
