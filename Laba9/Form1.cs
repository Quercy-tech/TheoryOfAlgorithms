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
        static int lengthOfArray = 1000000;
        int[] gigaArray = new int[lengthOfArray];
        public Form1()
        {
            InitializeComponent();
 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            isThreeWay = false;

            for (int i = 0; i < lengthOfArray; i += 1)
            {
                gigaArray[i] = random.Next(1000);
            }
            textBox1.Text = "Початковий масив:" + Environment.NewLine + string.Join(", ", gigaArray);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            isThreeWay = false;

            for (int i = 0; i < lengthOfArray; i += 1)
            {
                if (i < 5)
                {
                    gigaArray[i] = random.Next(i, 300);
                    for (int j = 0; j < i; j += 1)
                    {
                        if (gigaArray[i] == gigaArray[j]) gigaArray[i] = random.Next(i, 300);
                    }
                }
                else gigaArray[i] = random.Next(1000);
            }
            textBox1.Text = "Початковий масив:" + Environment.NewLine + string.Join(", ", gigaArray);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            isThreeWay = true;
            gigaArray[0] = random.Next(1000);
            for (int i = 1; i < lengthOfArray; i += 1)
            {
                gigaArray[i] = random.Next(1000);

                if (gigaArray[i] % 2 == 0) gigaArray[i] = gigaArray[i - 1];
            }

            textBox1.Text = "Початковий масив:" + Environment.NewLine + string.Join(", ", gigaArray);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (isThreeWay) ThreeWayQuicksort(textBox1, gigaArray, 0, gigaArray.Length - 1, stopwatch, isThreeWay);
            else Quicksort(textBox1, gigaArray, 0, gigaArray.Length - 1, stopwatch, isThreeWay);
            textBox1.AppendText(Environment.NewLine + Environment.NewLine + "Впорядкований масив:" + Environment.NewLine + string.Join(", ", gigaArray));
            textBox1.AppendText(Environment.NewLine + "Час виконання: " + (stopwatch.ElapsedMilliseconds * 0.001) + "с");
            stopwatch.Reset();
        }


        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            HeapSort(textBox1, gigaArray, stopwatch);
            textBox1.AppendText(Environment.NewLine + Environment.NewLine + "Впорядкований масив:" + Environment.NewLine + string.Join(", ", gigaArray));
            textBox1.AppendText(Environment.NewLine + "Час виконання: " + (stopwatch.ElapsedMilliseconds * 0.001) + "с");
            stopwatch.Reset();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            SmoothSort(gigaArray, stopwatch);
            textBox1.AppendText(Environment.NewLine + Environment.NewLine + "Впорядкований масив:" + Environment.NewLine + string.Join(", ", gigaArray));
            textBox1.AppendText(Environment.NewLine + "Час виконання: " + (stopwatch.ElapsedMilliseconds * 0.001) + "с");
            stopwatch.Reset();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        static void Swap(ref int a, ref int b)
        {
            var t = a;
            a = b;
            b = t;
        }


        public void Quicksort(TextBox textBox, int[] array, int start, int end, Stopwatch stopwatch, bool threeWay)
        {
            if (start < end)
            {
                int pivotIndex = Partition(textBox, array, start, end);

                    
                    
                    stopwatch.Start();

                Quicksort(textBox, array, start, pivotIndex - 1, stopwatch, threeWay);
                Quicksort(textBox, array, pivotIndex + 1, end, stopwatch, threeWay);
                stopwatch.Stop();
            }

            
        }

        public int Partition(TextBox textBox, int[] arr, int start, int end)
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


        public void ThreeWayQuicksort(TextBox textBox, int[] array, int start, int end, Stopwatch stopwatch, bool threeWay)
        {
            if (start < end)
            {
                int pivotIndex = ThreeWayPartition(textBox, array, start, end);

               
                stopwatch.Start();

                ThreeWayQuicksort(textBox, array, start, pivotIndex - 1, stopwatch, threeWay);
                ThreeWayQuicksort(textBox, array, pivotIndex + 1, end, stopwatch, threeWay);
            }

            stopwatch.Stop();
        }

        public int ThreeWayPartition(TextBox textBox, int[] arr, int start, int end)
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

      

        public static void HeapSort(TextBox textBox, int[] array, Stopwatch stopwatch)
        {
            stopwatch.Start();
            int n = array.Length;

            // Build max heap
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            // One by one extract an element from heap
            for (int i = n - 1; i >= 0; i--)
            {
                // Move current root to end
                Swap(ref array[0], ref array[i]);

                // Call max heapify on the reduced heap
                Heapify(array, i, 0);
            }
            stopwatch.Stop();
        }

        private static void Heapify(int[] array, int n, int i)
        {
            int indexLargest = i; // Initialize largest as root
            int indexLeft = 2 * i + 1; // left = 2*i + 1
            int indexRight = 2 * i + 2; // right = 2*i + 2

            // If left child is larger than root
            if (indexLeft < n && array[indexLeft] > array[indexLargest])
                indexLargest = indexLeft;

            // If right child is larger than largest so far
            if (indexRight < n && array[indexRight] > array[indexLargest])
                indexLargest = indexRight;

            // If largest is not root
            if (indexLargest != i)
            {
                Swap(ref array[i], ref array[indexLargest]);

                // Recursively heapify the affected sub-tree
                Heapify(array, n, indexLargest);
            }
        }

        public static void SmoothSort(int[] array, Stopwatch stopwatch)
        {
            stopwatch.Start();
            int[] leoNums = LeonardoNumbers(array.Length);
            int[] heap = new int[array.Length];
            int heapSize = 0;

            // Create initial heap
            for (int i = 0; i < array.Length; i++)
            {
                if (heapSize >= 2 && heap[heapSize - 2] == heap[heapSize - 1] + 1)
                {
                    heapSize--;
                    heap[heapSize - 1]++;
                }
                else
                {
                    if (heapSize >= 1 && heap[heapSize - 1] == 1)
                    {
                        heap[heapSize++] = 0;
                    }
                    else
                    {
                        heap[heapSize++] = 1;
                    }
                }
                LeoHeapify(array, i, heap, heapSize, leoNums);
                stopwatch.Stop();
            }

            // Decompose the heap
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (heap[heapSize - 1] < 2)
                {
                    heapSize--;
                }
                else
                {
                    int k = heap[--heapSize];
                    var (t_r, k_r, t_l, k_l) = GetChildTrees(i, k, leoNums);
                    heap[heapSize++] = k_l;
                    LeoHeapify(array, t_l, heap, heapSize, leoNums);
                    heap[heapSize++] = k_r;
                    LeoHeapify(array, t_r, heap, heapSize, leoNums);
                }
            }
        }

        public static int[] LeonardoNumbers(int arrLength)
        {
            int a = 1, b = 1;
            int[] numbers = new int[arrLength];
            int count = 0;
            while (a <= arrLength)
            {
                numbers[count++] = a;
                int temp = a;
                a = b;
                b = temp + b + 1;
            }
            Array.Resize(ref numbers, count);
            return numbers;
        }

        public static void LeoHeapify(int[] array, int i, int[] heap, int heapSize, int[] leoNums)
        {
            int current = heapSize - 1;
            int k = heap[current];

            while (current > 0)
            {
                int j = i - leoNums[k];
                if (array[j] > array[i] && (k < 2 || (array[j] > array[i - 1] && array[j] > array[i - 2])))
                {
                    Swap(ref array[i],ref array[j]);
                    i = j;
                    current -= 1;
                    k = heap[current];
                }
                else
                {
                    break;
                }
            }

            while (k >= 2)
            {
                var (rightTreeIndex, rightTreeSize, leftTreeIndex, leftTreeSize) = GetChildTrees(i, k, leoNums);
                if (array[i] < array[rightTreeIndex] || array[i] < array[leftTreeIndex])
                {
                    if (array[rightTreeIndex] > array[leftTreeIndex])
                    {
                        Swap(ref array[i], ref array[rightTreeIndex]);
                        i = rightTreeIndex;
                        k = rightTreeSize;
                    }
                    else
                    {
                        Swap(ref array[i], ref array[leftTreeIndex]);
                        i = leftTreeIndex;
                        k = leftTreeSize;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        public static (int, int, int, int) GetChildTrees(int i, int k, int[] leoNums)
        {
            int rightTreeIndex = i - 1;
            int rightTreeSize = k - 2;
            int leftTreeIndex = rightTreeIndex - leoNums[rightTreeSize];
            int leftTreeSize = k - 1;
            return (rightTreeIndex, rightTreeSize, leftTreeIndex, leftTreeSize);
        }


    }
}
