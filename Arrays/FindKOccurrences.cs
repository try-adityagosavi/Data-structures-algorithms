using System;
using System.Collections.Generic;

#region Linear Search 
// TC - O(n), SC - O(1)
public class FindKOccurrences
{
	 public static int CountOccurrencesLinear<T>(T[] collection, T targetElement) where T : IEquatable<T>
    {
        if (collection == null || collection.Length == 0)
        {
            return 0;
        }
        
        if (collection.Length == 1)
        {
            return collection[0].Equals(targetElement) ? 1 : 0;
        }
        
        int occurrenceCount = 0;
        
        foreach (T element in collection)
        {
            if (element.Equals(targetElement))
                occurrenceCount++;
        }
        
        return occurrenceCount;
    }
}

#endregion
	
#region Binary Search	
// Applicable only array is sorted
// If we want to return count of k, we can just return lastIndex - firstIndex + 1
// TC - O(logn + k), SC - O(k)
public static class FindKOccurrencesBS<T> where T : IComparable<T>
{
	public static List<int> FindAllOccurrences(T[] sortedCollection, T targetElement)
    {
        List<int> occurrenceIndices = new List<int>();
        
		if (sortedCollection == null || sortedCollection.Length == 0)
        {
            return occurrenceIndices;
        }
		
		 if (sortedCollection.Length == 1)
        {
            if (sortedCollection[0].CompareTo(targetElement) == 0)
            {
                occurrenceIndices.Add(0);
            }
            return occurrenceIndices;
        }
		
		
        int firstIndex = FindFirstOccurrence(sortedCollection, targetElement);
        
        if (firstIndex == -1)
        {
            return occurrenceIndices;
        }
        
        int lastIndex = FindLastOccurrence(sortedCollection, targetElement);
        
        for (int i = firstIndex; i <= lastIndex; i++)
        {
            occurrenceIndices.Add(i);
        }
        
        return occurrenceIndices;
    }
    
    private static int FindFirstOccurrence(T[] sortedCollection, T targetElement)
    {
        int left = 0;
        int right = sortedCollection.Length - 1;
        int result = -1;
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            
		 	int comparison = sortedCollection[mid].CompareTo(targetElement);

            if (comparison == 0)
            {
                result = mid;
                right = mid - 1;
            }
            else if (comparison < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        
        return result;
    }
    
    private static int FindLastOccurrence(T[] sortedCollection, T targetElement)
    {
        int left = 0;
        int right = sortedCollection.Length - 1;
        int result = -1;
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            
            int comparison = sortedCollection[mid].CompareTo(targetElement);
            
            if (comparison == 0)
            {
                result = mid;
                left = mid + 1;
            }
            else if (comparison < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        
        return result;
    }
}

#endregion

public class Program
{
	public static void Main()
	{
		int[] arr = {20, 67, 34, 12, 55, 67, 89, 67, 21, 33, 67};
		int k = 67;
		
		Console.WriteLine($"Array :[{string.Join(", ",arr)}]");
		
		//Linear Search
		Console.WriteLine($"Count of {k} = {FindKOccurrences.CountOccurrencesLinear(arr, k)}");
		
		string[] stringArray = {"apple", "banana", "apple", "orange", "apple", "grape"};
        string targetString = "apple";
        Console.WriteLine($"Count of \"{targetString}\" in string array = {FindKOccurrences.CountOccurrencesLinear(stringArray, targetString)}");
		
		double[] numberList = new double[] {1.1, 2.2, 3.3, 2.2, 4.4, 2.2};
        double targetDouble = 2.2;
        Console.WriteLine($"Count of {targetDouble} in number list = {FindKOccurrences.CountOccurrencesLinear(numberList, targetDouble)}");
		
		//Binary Search
		arr = new int[]{ 12, 20, 21, 33, 34, 55, 67, 67, 67, 67, 89 };
        k = 67;
        
        List<int> occurrences = FindKOccurrencesBS<int>.FindAllOccurrences(arr, k);
        
        Console.WriteLine($"All occurrences of {k} found at indices:");
		Console.WriteLine($"[{string.Join(", ", occurrences)}]");
	
		string[] sortedStrings = { "apple", "banana", "banana", "banana", "cherry", "date", "elderberry" };
        targetString = "banana";
        
        Console.WriteLine($"Sorted Array: [{string.Join(", ", sortedStrings)}]");
		List<int> stringOccurrences = FindKOccurrencesBS<string>.FindAllOccurrences(sortedStrings, targetString);
		Console.WriteLine($"[{string.Join(", ", stringOccurrences)}]");
	}
}