using System;
using System.Collections.Generic;

class TwoSum
{
    // Brute force approach
    // Time Complexity: O(n²)
    // Space Complexity: O(1)
    static int[] FindPairsBruteForce(int[] nums, int targetSum)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == targetSum)
                {
                    Console.WriteLine($"Pair found: ({nums[i]}, {nums[j]}) at indices ({i}, {j})");
                    return new int[] { i, j };
                }
            }
        }
        
        Console.WriteLine("No pairs found with the given sum.");
        return new int[0];
    }

    // Sorting and Two-Pointer approach
    // Time Complexity: O(n log n)
    // Space Complexity: O(n)
    static int[] FindPairsSortAndTwoPointers(int[] nums, int targetSum)
    {
        List<(int value, int index)> numsWithIndices = new List<(int, int)>();
        for (int i = 0; i < nums.Length; i++)
        {
            numsWithIndices.Add((nums[i], i));
        }
        
        numsWithIndices.Sort((a, b) => a.value.CompareTo(b.value));
        
        int left = 0;
        int right = numsWithIndices.Count - 1;
        
        while (left < right)
        {
            int currentSum = numsWithIndices[left].value + numsWithIndices[right].value;
            
            if (currentSum == targetSum)
            {
                int index1 = numsWithIndices[left].index;
                int index2 = numsWithIndices[right].index;
                Console.WriteLine($"Pair found: ({nums[index1]}, {nums[index2]}) at indices ({index1}, {index2})");
                return new int[] { index1, index2 };
            }
            else if (currentSum < targetSum)
            {
                left++;
            }
            else
            {
                right--;
            }
        }
        
        Console.WriteLine("No pairs found with the given sum.");
        return new int[0];
    }

    // Dictionary approach
    // Time Complexity: O(n)
    // Space Complexity: O(n)
    static int[] FindPairsUsingDictionary(int[] nums, int targetSum)
    {
        Dictionary<int, int> seenNumbers = new Dictionary<int, int>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = targetSum - nums[i];
            
            if (seenNumbers.ContainsKey(complement))
            {
                int complementIndex = seenNumbers[complement];
                Console.WriteLine($"Pair found: ({nums[complementIndex]}, {nums[i]}) at indices ({complementIndex}, {i})");
                return new int[] { complementIndex, i };
            }
            
            if (!seenNumbers.ContainsKey(nums[i]))
            {
                seenNumbers.Add(nums[i], i);
            }
        }
        
        Console.WriteLine("No pairs found with the given sum.");
        return new int[0];
    }
    
    public static void DisplayArray<T>(T[] array)
    {
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
    }

    static void Main(string[] args)
    {
        int[] numbers1 = { 2, 7, 11, 15 };
        int targetSum1 = 9;
        
        DisplayArray(numbers1);
        Console.WriteLine($"Target sum: {targetSum1}");
        
        Console.WriteLine("\nBrute Force Approach:");
        int[] result1BF = FindPairsBruteForce(numbers1, targetSum1);
        DisplayArray(result1BF);
        
        Console.WriteLine("\nSorting and Two-Pointer Approach:");
        int[] result1TP = FindPairsSortAndTwoPointers(numbers1, targetSum1);
        DisplayArray(result1TP);
        
        Console.WriteLine("\nDictionary Approach:");
        int[] result1D =  FindPairsUsingDictionary(numbers1, targetSum1);
        DisplayArray(result1D);
        
		Console.WriteLine("\n\nSecond Example");
        int[] numbers2 = { 3, 2, 4 };
        int targetSum2 = 6;
        
        DisplayArray(numbers2);
        Console.WriteLine($"\nTarget sum: {targetSum2}");
        
        Console.WriteLine("\nBrute Force Approach:");
        int[] result2BF = FindPairsBruteForce(numbers2, targetSum2);
        DisplayArray(result2BF);
        
        Console.WriteLine("\nSorting and Two-Pointer Approach:");
        int[] result2TP = FindPairsSortAndTwoPointers(numbers2, targetSum2);
        DisplayArray(result2TP);
        
        Console.WriteLine("\nDictionary Approach:");
        int[] result2D =  FindPairsUsingDictionary(numbers2, targetSum2);
        DisplayArray(result2D);
        
		Console.WriteLine("\n\nThird Example");
        int[] numbers3 = { 3, 3 };
        int targetSum3 = 6;
        
        DisplayArray(numbers3);
        Console.WriteLine($"\nTarget sum: {targetSum3}");
        
        Console.WriteLine("\nBrute Force Approach:");
        int[] result3BF = FindPairsBruteForce(numbers3, targetSum3);
        DisplayArray(result3BF);
        
        Console.WriteLine("\nSorting and Two-Pointer Approach:");
        int[] result3TP = FindPairsSortAndTwoPointers(numbers3, targetSum3);
        DisplayArray(result3TP);
        
        Console.WriteLine("\nDictionary Approach:");
        int[] result3D =  FindPairsUsingDictionary(numbers3, targetSum3);
        DisplayArray(result3D);
    }
}