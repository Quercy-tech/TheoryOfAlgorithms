using System;

public class SmoothSort
{
    public static void Main(int n)
    {
        int[] lst = new int[n];
        for (int i = 0; i < n; i++)
        {
            lst[i] = i;
        }
        Shuffle(lst);
        Console.WriteLine("Shuffled list: " + string.Join(", ", lst));
        SmoothSortAlgorithm(lst);
        Console.WriteLine("Sorted list: " + string.Join(", ", lst));
    }

    public static void Shuffle(int[] lst)
    {
        Random random = new Random();
        for (int i = lst.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = lst[i];
            lst[i] = lst[j];
            lst[j] = temp;
        }
    }

    public static void SmoothSortAlgorithm(int[] lst)
    {
        int[] leoNums = LeonardoNumbers(lst.Length);
        int[] heap = new int[lst.Length];
        int heapSize = 0;

        // Create initial heap
        for (int i = 0; i < lst.Length; i++)
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
            RestoreHeap(lst, i, heap, heapSize, leoNums);
        }

        // Decompose the heap
        for (int i = lst.Length - 1; i >= 0; i--)
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
                RestoreHeap(lst, t_l, heap, heapSize, leoNums);
                heap[heapSize++] = k_r;
                RestoreHeap(lst, t_r, heap, heapSize, leoNums);
            }
        }
    }

    public static int[] LeonardoNumbers(int hi)
    {
        int a = 1, b = 1;
        int[] numbers = new int[hi];
        int count = 0;
        while (a <= hi)
        {
            numbers[count++] = a;
            int temp = a;
            a = b;
            b = temp + b + 1;
        }
        Array.Resize(ref numbers, count);
        return numbers;
    }

    public static void RestoreHeap(int[] lst, int i, int[] heap, int heapSize, int[] leoNums)
    {
        int current = heapSize - 1;
        int k = heap[current];

        while (current > 0)
        {
            int j = i - leoNums[k];
            if (lst[j] > lst[i] && (k < 2 || (lst[j] > lst[i - 1] && lst[j] > lst[i - 2])))
            {
                int temp = lst[i];
                lst[i] = lst[j];
                lst[j] = temp;
                i = j;
                current--;
                k = heap[current];
            }
            else
            {
                break;
            }
        }

        while (k >= 2)
        {
            var (t_r, k_r, t_l, k_l) = GetChildTrees(i, k, leoNums);
            if (lst[i] < lst[t_r] || lst[i] < lst[t_l])
            {
                if (lst[t_r] > lst[t_l])
                {
                    int temp = lst[i];
                    lst[i] = lst[t_r];
                    lst[t_r] = temp;
                    i = t_r;
                    k = k_r;
                }
                else
                {
                    int temp = lst[i];
                    lst[i] = lst[t_l];
                    lst[t_l] = temp;
                    i = t_l;
                    k = k_l;
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
        int t_r = i - 1;
        int k_r = k - 2;
        int t_l = t_r - leoNums[k_r];
        int k_l = k - 1;
        return (t_r, k_r, t_l, k_l);
    }
}
