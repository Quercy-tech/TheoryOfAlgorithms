using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System;

namespace Laba5
{
    public partial class Form1 : Form
    {
        public static List<string> valuesList = new List<string>();
        // Метод квадратичного зондирования

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int newCapacity;
            try
            {
                newCapacity = int.Parse(textBox1.Text);
                List<string> valuesListnew = new List<string>();
                valuesListnew.Capacity = newCapacity;
                for (int i = 0; i < newCapacity; i++)
                {
                    valuesListnew.Add($"{i + 1}");
                }
                valuesList = valuesListnew;
            }
            catch
            {
                MessageBox.Show("Введіть коректну кількість елементів для створення таблиці.");
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int counter;
            try
            {
                MessageBox.Show($"Перед початком роботи оберіть метод вирішення колізій.");
               
                counter = int.Parse(textBox1.Text);
                HashTable ht = new HashTable(counter);
                string student = textBox2.Text + " | " + textBox3.Text;
                int index;
                if (comboBox1.SelectedIndex == 1)
                {
                    index = ht.LinearProbing(student);
                    if (index >= 0 && index < valuesList.Count)
                    {
                        valuesList[index] = student + $" | ID: {index}";
                    }
                    else
                    {
                        MessageBox.Show("Index is out of range.");
                    }
                }
                if (comboBox1.SelectedIndex == 0)
                {
                    index = ht.QuadraticProbing(student);
                    if (index >= 0 && index < valuesList.Count)
                    {
                        valuesList[index] = student + $" | ID: {index}";
                    }
                    else
                    {
                        MessageBox.Show("Index is out of range.");
                    }
                }

            }
            catch
            {
                MessageBox.Show("Сталася помилка!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.DataSource = null;
            listBox1.DataSource = valuesList;
        }
        public class HashTable
        {
            private int tableSize;
            private Dictionary<int, string> table;
            public HashTable(int size)
            {
                tableSize = size;
                table = new Dictionary<int, string>(size);
            }
            private int HashFunction(string key)
            {
                int hash = 0;
                foreach (char c in key)
                {
                    hash = (hash * 31 + c) % tableSize;
                }
                return hash;
            }
            public int LinearProbing(string key)
            {
                int index = HashFunction(key);
                while (table.ContainsKey(index) && table[index] != null)
                {
                    if (table[index] == key)
                    {
                        return index;
                    }
                    index = (index + 1) % tableSize;
                }
                return index;
            }
            public int QuadraticProbing(string key)
            {
                int index = HashFunction(key);
                int i = 1;
                while (table.ContainsKey(index) && table[index] != null)
                {
                    if (table[index] == key)
                    {
                        return index;
                    }
                    index = (index + (int)Math.Pow(i, 2)) % tableSize;
                    i++;
                }
                return index;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int index;
            try
            {
                index = int.Parse(textBox4.Text);
                valuesList[index] = textBox5.Text + ($" | ID: {index}");
                MessageBox.Show("Дані успішно змінені");
                textBox4.Clear();
                textBox5.Clear();
            }
            catch
            {
                MessageBox.Show("Введіть правильні дані!");
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
        private void button5_Click(object sender, EventArgs e)
        {
            int index;
            try
            {
                index = int.Parse(textBox6.Text);
                valuesList[index] = (index + 1).ToString();
            }
            catch
            {
                MessageBox.Show("Введіть правильні дані!");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1248, 857);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
    }
}
