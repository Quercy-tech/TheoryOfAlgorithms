using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba9
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        static int lengthOfArray = 10;
        int[] array = new int[lengthOfArray];
        public Form1()
        {
            InitializeComponent();
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            for (int i = 0; i < lengthOfArray; i += 1)
            {
                array[i] = random.Next(100);
            }
            Output(richTextBox1, array);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            for (int i = 0; i < lengthOfArray; i += 1)
            {
                if (i < 5)
                {
                    array[i] = random.Next(i, 30);
                    for (int j = 0; j < i; j += 1)
                    {
                        if (array[i] == array[j]) array[i] = random.Next(i, 30);
                    }
                }
                else array[i] = random.Next(100);
            }
            Output(richTextBox1, array);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            array[0] = random.Next(100);
            for (int i = 1; i < lengthOfArray; i += 1)
            {
                array[i] = random.Next(100);

                if (array[i] % 2 == 0) array[i] = array[i - 1];
            }

            Output(richTextBox1, array);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            BubbleSort(richTextBox1, array);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            ShellSort(richTextBox1, array);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            CountingSort(richTextBox1, array);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        static void Output(RichTextBox textBox, int[] array)
        {
            textBox.Text += ("\r\nМасив:  ");
            for (int i = 0; i < array.Length; i++)
            {
                textBox.Text += Convert.ToString(array[i]) + " ";
            }
        }

        static void Swap(ref int a, ref int b)
        {
            var t = a;
            a = b;
            b = t;
        }

        static int FindMax(ref int[] array)
        {
            int max = array[0];
            for (int i = 0; i < array.Length; i += 1)
            {
                if (array[i] > max) max = array[i];
            }
            return max;
        }

        static int FindMin(ref int[] array)
        {
            int min = array[0];
            for (int i = 0; i < array.Length; i += 1)
            {
                if (array[i] < min) min = array[i];
            }
            return min;
        }

        static void BubbleSort(RichTextBox textBox, int[] array)
        {
            for (int i = 0; i < array.Length ; i++)
            {
                bool sorted = true;
                for (int j = 0; j < array.Length - 1; j++)
                {
                    if (array[j] > array[j+1])
                    {
                        Swap(ref array[j],ref array[j+1]);
                        sorted = false;

                    }
                }

                textBox.Text += "\r\nІтерація " + (i + 1);
                Output(textBox, array);
                if (sorted) break;
            }

        }

        static void ShellSort(RichTextBox textBox, int[] array)
        {
            int counter = 1;
            var gap = array.Length / 2;
            while (gap >= 1)
            {
                for (var i = gap; i < array.Length; i++)
                {
                    var j = i;
                    while ((j >= gap) && (array[j - gap] > array[j]))
                    {
                        Swap(ref array[j], ref array[j - gap]);
                        j = j - gap;

                        textBox.Text += "\r\nІтерація " + (counter);
                        Output(textBox, array);
                        counter += 1;
                    }
                    
                }

                gap /= 2;
            }
        }

        static void CountingSort(RichTextBox textBox, int[] array)
        {
            int max = FindMax(ref array);
            int min = FindMin(ref array);
            int counter = 1;
            int[] countArray = new int[max - min + 1];
            int[] sortedArray = new int[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                sortedArray[i] = array[i];
            }

            for (int i = 0; i < array.Length; i += 1)
            {
                countArray[array[i] - min] += 1;
            }

            countArray[0] -= 1;
            for (int i = 1; i < countArray.Length; i++)
            {
                countArray[i] = countArray[i] + countArray[i - 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                sortedArray[countArray[array[i] - min]--] = array[i];
                textBox.Text += "\r\nІтерація " + (counter);
                Output(textBox, sortedArray);
                counter += 1;
            }
        }


        }
}
