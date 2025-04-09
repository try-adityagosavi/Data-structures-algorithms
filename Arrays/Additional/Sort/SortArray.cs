using System;

public class SortArray
{
    public static void Sort<T>(T[] array) where T : IComparable<T>
    {
        if (array == null || array.Length <= 1)
            return;
            
        QuickSort(array, 0, array.Length - 1);
    }
    
	#region QuickSort
	
    private static void QuickSort<T>(T[] arr, int left, int right) where T : IComparable<T>
    {
        if (left < right)
        {
            int pivotIndex = Partition(arr, left, right);
            
            QuickSort(arr, left, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, right);
        }
    }
    
    private static int Partition<T>(T[] arr, int left, int right) where T : IComparable<T>
    {
        T pivot = arr[right];
        
        int i = left - 1;
        
        for (int j = left; j < right; j++)
        {
            if (arr[j].CompareTo(pivot) <= 0)
            {
                i++;
				(arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        
		(arr[i+1], arr[right]) = (arr[right], arr[i+1]);
        
        return i + 1;
    }
    
	#endregion
    public static void Main()
    {
		#region QuickSort example
		
        int[] numbers = { 10, 7, 8, 9, 1, 5 };
        Console.WriteLine("Original array:");
        PrintArray(numbers);
        
        Sort(numbers);
        
        Console.WriteLine("\nSorted array:");
        PrintArray(numbers);
        
        string[] names = { "David", "Alice", "Charlie", "Bob", "Eva" };
        Console.WriteLine("\nOriginal string array:");
        PrintArray(names);
        
        Sort(names);
        
        Console.WriteLine("\nSorted string array:");
        PrintArray(names);
		
		#endregion
    }
    
    private static void PrintArray<T>(T[] array)
    {
        foreach (var item in array)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }
}