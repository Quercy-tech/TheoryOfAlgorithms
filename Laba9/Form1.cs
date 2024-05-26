using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            gigaArray = MSD_Radixsort(gigaArray, gigaArray.Length, stopwatch);
            textBox1.AppendText(Environment.NewLine + Environment.NewLine + "Впорядкований масив:" + Environment.NewLine + string.Join(", ", gigaArray));
            textBox1.AppendText(Environment.NewLine + "Час виконання: " + (stopwatch.ElapsedMilliseconds * 0.001) + "с");
            stopwatch.Reset();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            gigaArray = BucketSort(gigaArray, stopwatch);
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

        // Main function to call MSD_sort
        static int[] MSD_Radixsort(int[] arr, int n, Stopwatch stopwatch)
        {
            stopwatch.Start();
            // Find the maximum number to know number of digits
            int m = getMax(arr, n);

            // Get the length of the largest integer
            int d = (int)Math.Floor(Math.Log10(m)) + 1;

            // Function call
            stopwatch.Stop();
            return MSD_sort(arr, 0, n - 1, d, stopwatch);
        }


        // A utility function to get the digit at index d in an integer
        static int digit_at(int x, int d)
        {
            return (int)(x / Math.Pow(10, d - 1)) % 10;
        }

        // The main function to sort array using MSD Radix Sort recursively
        static int[] MSD_sort(int[] array, int start, int end, int d, Stopwatch stopwatch)
        {
            stopwatch.Start();
            // recursion break condition
            if (end <= start || d < 1)
            {
                return array;
            }

            int[] count = new int[10 + 2];  // For counting 0-9 plus two additional spaces
            List<int> temp = new List<int>(new int[end - start + 1]);  // Use List to easily swap elements

            // Store occurrences of the most significant character from each integer in count
            for (int i = start; i <= end; i++)
            {
                int c = digit_at(array[i], d);
                count[c + 2]++;
            }

            // Change count so that it contains actual positions of these digits in temp
            for (int r = 0; r < 10 + 1; r++)
                count[r + 1] += count[r];

            // Build the temp
            for (int i = start; i <= end; i++)
            {
                int c = digit_at(array[i], d);
                temp[count[c + 1]++] = array[i];
            }

            // Copy all integers of temp back to arr
            for (int i = start; i <= end; i++)
                array[i] = temp[i - start];

            // Recursively MSD_sort on each partially sorted integer set
            for (int r = 0; r < 10; r++)
                MSD_sort(array, start + count[r], start + count[r + 1] - 1, d - 1, stopwatch);
            stopwatch.Stop();
            return array;
        }

        // Function to find the largest integer
        static int getMax(int[] arr, int n)
        {
            int mx = arr[0];
            for (int i = 1; i < n; i++)
                if (arr[i] > mx)
                    mx = arr[i];
            return mx;
        }


        // Bucket sort

        public static int[] BucketSort(int[] array, Stopwatch stopwatch)
        {
            stopwatch.Start();
            // Find the minimum and maximum values in the array
            int minValue = array[0];
            int maxValue = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < minValue)
                    minValue = array[i];
                else if (array[i] > maxValue)
                    maxValue = array[i];
            }

            // Calculate the range and number of buckets
            int range = maxValue - minValue + 1;
            int bucketCount = 10;

            // Create empty buckets
            List<int>[] buckets = new List<int>[bucketCount];
            for (int i = 0; i < bucketCount; i++)
                buckets[i] = new List<int>();

            // Assign elements to the appropriate bucket
            for (int i = 0; i < array.Length; i++)
            {
                int bucketIndex = Convert.ToInt32((array[i] - minValue) / (range / bucketCount * 2));
                buckets[bucketIndex].Add(array[i]);
            }

            // Sort each bucket using insertion sort
            for (int i = 0; i < bucketCount; i++)
                buckets[i].Sort();

            // Concatenate the sorted buckets into a single sorted array
            int index = 0;
            for (int i = 0; i < bucketCount; i++)
            {
                for (int j = 0; j < buckets[i].Count; j++)
                {
                    array[index] = buckets[i][j];
                    index += 1;
                }
            }
            stopwatch.Stop();
            return array;
        }
    }
}
