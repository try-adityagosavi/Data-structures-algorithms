using System;

public static class ArrayRangeFinder
{
	// Optimized approach to find range of an unsorted array
    // Uses paired comparisons to reduce total number of comparisons
    // Time Complexity: O(n) with fewer comparisons than naive approach
    // Space Complexity: O(1) - constant extra space
	public static int FindRangeOptimized(int[] array)
    {
        if (array == null || array.Length == 0)
        {
            Console.WriteLine("Array cannot be null or empty");
        }
        
        if (array.Length == 1)
        {
            return 0;
        }
        
        int minValue, maxValue;
        
        if (array[0] > array[1])
        {
            maxValue = array[0];
            minValue = array[1];
        }
        else
        {
            maxValue = array[1];
            minValue = array[0];
        }
        
        for (int i = 2; i < array.Length - 1; i += 2)
        {
            if (array[i] > array[i + 1])
            {
                if (array[i] > maxValue)
                {
                    maxValue = array[i];
                }
                
                if (array[i + 1] < minValue)
                {
                    minValue = array[i + 1];
                }
            }
            else
            {
                if (array[i + 1] > maxValue)
                {
                    maxValue = array[i + 1];
                }
                
                if (array[i] < minValue)
                {
                    minValue = array[i];
                }
            }
        }
        
        if (array.Length % 2 != 0)
        {
            int lastElement = array[array.Length - 1];
            
            if (lastElement > maxValue)
            {
                maxValue = lastElement;
            }
            else if (lastElement < minValue)
            {
                minValue = lastElement;
            }
        }
        
        return maxValue - minValue;
    }
	
    // Time Complexity: O(n) - single pass through the array
    // Space Complexity: O(1) - constant extra space
    public static int FindRangeUnsorted(int[] array)
    {
        if (array == null || array.Length == 0)
        {
            Console.WriteLine("Array cannot be null or empty");
        }
        
        int minValue = array[0];
        int maxValue = array[0];
        
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < minValue)
            {
                minValue = array[i];
            }
            
            if (array[i] > maxValue)
            {
                maxValue = array[i];
            }
        }
        
        return maxValue - minValue;
    }
    

    // Time Complexity: O(1) - direct access to first and last elements
    // Space Complexity: O(1) - constant extra space
    public static int FindRangeSorted(int[] sortedArray)
    {
        if (sortedArray == null || sortedArray.Length == 0)
        {
            Console.WriteLine("Array cannot be null or empty");
        }
        
        int minValue = sortedArray[0];
        int maxValue = sortedArray[sortedArray.Length - 1];
        
        return maxValue - minValue;
    }
}

// Class for sorting algorithms used in the program
public static class Sorter
{
    // quick sort
    // Time Complexity: O(n log n) average case
    // Space Complexity: O(log n)
    public static void QuickSort(int[] array)
    {
        if (array == null || array.Length <= 1)
            return;
            
        QuickSortRecursive(array, 0, array.Length - 1);
    }
    
    private static void QuickSortRecursive(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right);
            
            QuickSortRecursive(array, left, pivotIndex - 1);
            QuickSortRecursive(array, pivotIndex + 1, right);
        }
    }
    
    private static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = left - 1;
        
        for (int j = left; j < right; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }
        
        Swap(array, i + 1, right);
        return i + 1;
    }
    
    private static void Swap(int[] array, int i, int j)
    {
        (array[i], array[j]) = (array[j], array[i]);
    }
}

public class Program
{
    public static void Main()
    {
		int[] arr = { 5, 3, 8, 1, 9, 6 };
		
		Console.WriteLine($"[{string.Join(", ", arr)}]");
		
		int unsortedRange = ArrayRangeFinder.FindRangeUnsorted(arr);
        Console.WriteLine($"Range of unsorted array: {unsortedRange}");
		
		int optimizedResult = ArrayRangeFinder.FindRangeOptimized(arr);
        Console.WriteLine($"Optimized approach result: {optimizedResult}");
    }
}