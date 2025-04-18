//Missing Number in an array of size n, where numbers are from 1 to n+1

using System;
using System.Collections.Generic;
					
public class MissingNumberFinder
{
    public static void DisplayArray<T>(T[] array)
    {
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
    }
    
    // Time Complexity: O(n)
    // Space Complexity: O(1)
    public static int FindMissingNumberUsingSum(int[] array)
    {
        if (array == null || array.Length == 0)
            Console.WriteLine("Array cannot be null or empty");
            
        int length = array.Length + 1;
        int expectedSum = length * (length + 1) / 2;
        
        int actualSum = 0;
        foreach (int num in array)
        {
            actualSum += num;
        }
        
        return expectedSum - actualSum;
    }
    
    // Time Complexity: O(n)
    // Space Complexity: O(1)
    public static int FindMissingNumberUsingXOR(int[] array)
    {
        if (array == null || array.Length == 0)
            Console.WriteLine("Array cannot be null or empty");
            
        int length = array.Length + 1;
        
        int xor1 = 0;
        for (int i = 1; i <= length; i++)
        {
            xor1 ^= i;
        }
        
        int xor2 = 0;
        foreach (int num in array)
        {
            xor2 ^= num;
        }

        return xor1 ^ xor2;
    }
     
    // Time Complexity: O(n)
    // Space Complexity: O(n)
    public static int FindMissingNumberUsingHashSet(int[] array, int minValue, int maxValue)
    {
        if (array == null || array.Length == 0)
            Console.WriteLine("Array cannot be null or empty");
            
        HashSet<int> numberSet = new HashSet<int>(array);
        
        for (int i = minValue; i <= maxValue; i++)
        {
            if (!numberSet.Contains(i))
            {
                return i;
            }
        }
        
        throw new ArgumentException("No missing number found in the specified range");
    }
    
    // Time Complexity: O(n log n) for sorting + O(n) for finding = O(n log n)
    // Space Complexity: O(1)
    public static int FindMissingNumberUsingSorting(int[] array)
    {
        if (array == null || array.Length == 0)
            Console.WriteLine("Array cannot be null or empty");
            
        int[] sortedArray = (int[])array.Clone();
        Array.Sort(sortedArray);
        
        if (sortedArray[0] != 1)
            return 1;
            
        for (int i = 0; i < sortedArray.Length - 1; i++)
        {
            if (sortedArray[i + 1] - sortedArray[i] > 1)
            {
                return sortedArray[i] + 1;
            }
        }
        
        return sortedArray[sortedArray.Length - 1] + 1;
    }
    
    // Time Complexity: O(n)
    // Space Complexity: O(1)
    public static int FindMissingNumberUsingIndexMarking(int[] array)
    {
        if (array == null || array.Length == 0)
            Console.WriteLine("Array cannot be null or empty");
            
        int[] tempArray = (int[])array.Clone();
        int length = tempArray.Length;
        
        for (int i = 0; i < length; i++)
        {
            int absValue = Math.Abs(tempArray[i]);
            if (absValue <= length && absValue > 0)
            {
                int index = absValue - 1;
                if (index < length && tempArray[index] > 0)
                {
                    tempArray[index] = -tempArray[index];
                }
            }
        }
        
        for (int i = 0; i < length; i++)
        {
            if (tempArray[i] > 0)
            {
                return i + 1; 
            }
        }
        
        return length + 1;
    }
    
    // Time Complexity: O(n)
    // Space Complexity: O(1)
    public static int FindMissingNumberUsingBinarySearch(int[] sortedArray)
    {
        if (sortedArray == null || sortedArray.Length == 0)
            throw new ArgumentException("Array cannot be null or empty");
            
        int left = 0;
        int right = sortedArray.Length - 1;
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            
            if (sortedArray[mid] == mid + 1)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return left + 1;
    }
    
    public static void Main()
    {
        int[] array = new int[] {1, 2, 3, 4, 5, 6, 8, 9, 10};
        int size = array.Length;
        DisplayArray(array);
        
        Console.WriteLine("=== Different methods to find the missing number ===");
        
        // Sum Formula
        int missingSumMethod = FindMissingNumberUsingSum(array);
        Console.WriteLine($"Sum Method: {missingSumMethod}");
        
        // XOR
        int missingXorMethod = FindMissingNumberUsingXOR(array);
        Console.WriteLine($"XOR Method: {missingXorMethod}");
        
        // HashSet
        int missingHashSetMethod = FindMissingNumberUsingHashSet(array, array[0], array[size - 1]);
        Console.WriteLine($"HashSet Method: {missingHashSetMethod}");
        
        // Sorting
        int missingSortingMethod = FindMissingNumberUsingSorting(array);
        Console.WriteLine($"Sorting Method: {missingSortingMethod}");
        
        // Index Marking
        int missingIndexMarkingMethod = FindMissingNumberUsingIndexMarking(array);
        Console.WriteLine($"Index Marking Method: {missingIndexMarkingMethod}");
        
        // Binary Search
        int[] sortedArray = (int[])array.Clone();
        Array.Sort(sortedArray);
        int missingBinarySearchMethod = FindMissingNumberUsingBinarySearch(sortedArray);
        Console.WriteLine($"Binary Search Method: {missingBinarySearchMethod}");
        
        Console.WriteLine("\n=== Another array ===");
        int[] array2 = new int[] {1, 2, 4, 5, 6, 7, 8, 9, 10};
        DisplayArray(array2);
        Console.WriteLine($"Sum Method: {FindMissingNumberUsingSum(array2)}");
    }
}