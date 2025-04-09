using System;
using System.Collections.Generic;

#region brute force
//brute-force (TC - O(nlogn), SC - O(1))
//sort the array - Kth Largest = n - k
//                 Kth Smallest = k - 1 

#endregion

#region Custom Heap
//using custom Heap (TC - O(nlogk), SC - O(k))
class KthElementFinder
{
    public static T FindKthLargest<T>(T[] arr, int k) where T : IComparable<T>
    {
        if (arr == null || arr.Length == 0)
            throw new ArgumentException("Array cannot be null or empty");
            
        if (k <= 0 || k > arr.Length)
            throw new ArgumentException($"k must be between 1 and {arr.Length}");
            
        Heap<T> minHeap = new Heap<T>(HeapType.MinHeap); // using our custom MinMaxHeap implementation
        
        foreach (T item in arr)
        {
            if (minHeap.Count < k)
            {
                minHeap.Add(item);
            }
            else if (item.CompareTo(minHeap.Peek()) > 0)
            {
                minHeap.Extract();
                minHeap.Add(item);
            }
        }
        
        return minHeap.Peek();
    }
    
    public static T FindKthSmallest<T>(T[] arr, int k) where T : IComparable<T>
    {
        if (arr == null || arr.Length == 0)
            throw new ArgumentException("Array cannot be null or empty");
            
        if (k <= 0 || k > arr.Length)
            throw new ArgumentException($"k must be between 1 and {arr.Length}");

        Heap<T> maxHeap = new Heap<T>(HeapType.MaxHeap);
        
        foreach (T item in arr)
        {
            if (maxHeap.Count < k)
            {
                maxHeap.Add(item);
            }
            else if (item.CompareTo(maxHeap.Peek()) < 0)
            {
                maxHeap.Extract();
                maxHeap.Add(item);
            }
        }
        
        return maxHeap.Peek();
    }
}

#endregion

#region QuickSelect
//using QuickSelect (TC - O(n) , SC - O(1))
class QuickSelectSolution
{
    public static T FindKthLargest<T>(T[] arr, int k) where T : IComparable<T>
    {
        if (arr == null || arr.Length == 0)
            Console.WriteLine("Array cannot be null or empty");

        if (k <= 0 || k > arr.Length)
            Console.WriteLine($"k must be between 1 and {arr.Length}");

        return QuickSelect(arr, 0, arr.Length - 1, arr.Length - k);
    }

    public static T FindKthSmallest<T>(T[] arr, int k) where T : IComparable<T>
    {
        if (arr == null || arr.Length == 0)
            Console.WriteLine("Array cannot be null or empty");

        if (k <= 0 || k > arr.Length)
            Console.WriteLine($"k must be between 1 and {arr.Length}");

        return QuickSelect(arr, 0, arr.Length - 1, k - 1);
    }

    private static T QuickSelect<T>(T[] arr, int left, int right, int k) where T : IComparable<T>
    {
        if (left == right)
            return arr[left];
        int pivotIndex = Partition(arr, left, right);
        if (k == pivotIndex)
            return arr[k];
        else if (k < pivotIndex)
            return QuickSelect(arr, left, pivotIndex - 1, k);
        else
            return QuickSelect(arr, pivotIndex + 1, right, k);
    }

    private static int Partition<T>(T[] arr, int left, int right) where T : IComparable<T>
    {
        T pivot = arr[right];
        int i = left;

        for (int j = left; j < right; j++)
        {
            if (arr[j].CompareTo(pivot) <= 0)
            {
                (arr[i], arr[j]) = (arr[j], arr[i]);
                i++;
            }
        }

        (arr[i], arr[right]) = (arr[right], arr[i]);

        return i;
    }
}

#endregion

public class Program
{
	public static void Main()
	{
        
		int[] arr = { 7, 10, 4, 3, 20, 15 };
        int k = 4;
		
		Console.WriteLine($"Array: [{string.Join(", ", arr)}]");
        
        //using custom Heap
        int kthLargest = KthElementFinder.FindKthLargest(arr, k);
        Console.WriteLine($"{k}th largest element is: {kthLargest}");

        int kthSmallest = KthElementFinder.FindKthSmallest(arr, k);
        Console.WriteLine($"{k}th smallest element is: {kthSmallest}");

        //using QuickSelect
        int[] arrCopy1 = (int[])arr.Clone();
        int[] arrCopy2 = (int[])arr.Clone();

        int kthLargest = QuickSelectSolution.FindKthLargest(arrCopy1, k);
        Console.WriteLine($"{k}th largest element is: {kthLargest}");

        int kthSmallest = QuickSelectSolution.FindKthSmallest(arrCopy2, k);
        Console.WriteLine($"{k}th smallest element is: {kthSmallest}");
	}
}