using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba9
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stopwatch stopwatch = new Stopwatch();

        public bool isThreeWay = false;
        static int lengthOfArray = 50;
        int[] array = new int[lengthOfArray];
        public Form1()
        {
            InitializeComponent();
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            isThreeWay = false;

            for (int i = 0; i < lengthOfArray; i += 1)
            {
                array[i] = random.Next(100);
            }
            Output(richTextBox1, array);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            isThreeWay = false;

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
            isThreeWay = true;
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
            BubbleSort(richTextBox1, array, stopwatch);
            richTextBox1.Text += "\r\nЧас виконання: " + stopwatch.ElapsedMilliseconds + "ms";
            stopwatch.Reset();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (isThreeWay) ThreeWayQuicksort(richTextBox1, array, 0, array.Length - 1, stopwatch, isThreeWay);
            else Quicksort(richTextBox1, array, 0, array.Length - 1, stopwatch, isThreeWay);
            richTextBox1.Text += "\r\nЧас виконання: " + stopwatch.ElapsedTicks + " тактів";
            stopwatch.Reset();
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

        static void BubbleSort(RichTextBox textBox, int[] array, Stopwatch stopwatch)
        {
            stopwatch.Start();
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

                textBox.Text += "\r\n Iteration №" + (i + 1);
                Output(textBox, array);
                stopwatch.Stop();
                if (sorted) break;
            }

        }

        public void Quicksort(RichTextBox textBox, int[] array, int start, int end, Stopwatch stopwatch, bool threeWay)
        {
            if (start < end)
            {
                int pivotIndex = Partition(textBox, array, start, end);

                    stopwatch.Stop();
                    textBox.Text += "\r\n Split in: " + start + " - " + end;
                    Output(textBox, array);
                    stopwatch.Start();

                Quicksort(textBox, array, start, pivotIndex - 1, stopwatch, threeWay);
                Quicksort(textBox, array, pivotIndex + 1, end, stopwatch, threeWay);
            }


        }

        public int Partition(RichTextBox textBox, int[] arr, int start, int end)
        {
            int pivot = arr[end];
            int i = start - 1;
            for (int j = start; j < end; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }
            Swap(arr, i + 1, end);
            return i + 1;
        }

        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }


        public void ThreeWayQuicksort(RichTextBox textBox, int[] array, int start, int end, Stopwatch stopwatch, bool threeWay)
        {
            if (start < end)
            {
                int pivotIndex = ThreeWayPartition(textBox, array, start, end);

                stopwatch.Stop();
                textBox.Text += "\r\n Split in: " + start + " - " + end;
                Output(textBox, array);
                stopwatch.Start();

                ThreeWayQuicksort(textBox, array, start, pivotIndex - 1, stopwatch, threeWay);
                ThreeWayQuicksort(textBox, array, pivotIndex + 1, end, stopwatch, threeWay);
            }


        }

        public int ThreeWayPartition(RichTextBox textBox, int[] arr, int start, int end)
        {
            int pivot = arr[end];
            int i = start - 1;
            int j = start;
            int k = end;

            while (j <= k)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                    j++;
                }
                else if (arr[j] > pivot)
                {
                    Swap(arr, j, k);
                    k--;
                }
                else
                {
                    j++; // Move j only when a duplicate is encountered
                }
            }
            return i + 1;
        }

    }
}
