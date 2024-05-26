using System;
using System.Collections.Generic;

public class GFG
{
    // A utility function to print an array
    static void print(int[] arr, int n)
    {
        for (int i = 0; i < n; i++)
        {
            Console.Write(arr[i] + " ");
        }
        Console.WriteLine();
    }

    // A utility function to get the digit at index d in an integer
    static int digit_at(int x, int d)
    {
        return (int)(x / Math.Pow(10, d - 1)) % 10;
    }

    // The main function to sort array using MSD Radix Sort recursively
    static int[] MSD_sort(int[] arr, int lo, int hi, int d)
    {
        // recursion break condition
        if (hi <= lo || d < 1)
        {
            return arr;
        }

        int[] count = new int[10 + 2];  // For counting 0-9 plus two additional spaces
        List<int> temp = new List<int>(new int[hi - lo + 1]);  // Use List to easily swap elements

        // Store occurrences of the most significant character from each integer in count
        for (int i = lo; i <= hi; i++)
        {
            int c = digit_at(arr[i], d);
            count[c + 2]++;
        }

        // Change count so that it contains actual positions of these digits in temp
        for (int r = 0; r < 10 + 1; r++)
            count[r + 1] += count[r];

        // Build the temp
        for (int i = lo; i <= hi; i++)
        {
            int c = digit_at(arr[i], d);
            temp[count[c + 1]++] = arr[i];
        }

        // Copy all integers of temp back to arr
        for (int i = lo; i <= hi; i++)
            arr[i] = temp[i - lo];

        // Recursively MSD_sort on each partially sorted integer set
        for (int r = 0; r < 10; r++)
            MSD_sort(arr, lo + count[r], lo + count[r + 1] - 1, d - 1);

        return arr;
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

    // Main function to call MSD_sort
    static int[] radixsort(int[] arr, int n)
    {
        // Find the maximum number to know number of digits
        int m = getMax(arr, n);

        // Get the length of the largest integer
        int d = (int)Math.Floor(Math.Log10(Math.Abs(m))) + 1;

        // Function call
        return MSD_sort(arr, 0, n - 1, d);
    }

    // Driver Code
    public static void Main(String[] args)
    {
        Random random = new Random();
        // Input array
        int lengthOfArray = 1000;
        int[] gigaArray = new int[lengthOfArray];
        for (int i = 0; i < lengthOfArray; i++)
        {
            gigaArray[i] = random.Next(100000);
        }
        // Size of the array
        int n = gigaArray.Length;

        Console.Write("Unsorted array : ");

        // Print the unsorted array
        print(gigaArray, n);

        // Function Call
        gigaArray = radixsort(gigaArray, n);

        Console.Write("Sorted array : ");

        // Print the sorted array
        print(gigaArray, n);
        Console.ReadLine();
    }
}

// This code is contributed by Rajput-Ji
