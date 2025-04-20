using System;
using System.Collections.Generic;

class PairSum
{
    // Brute force approach
    // Time Complexity: O(n²)
    // Space Complexity: O(1)
    static void FindPairsBruteForce(int[] nums, int targetSum)
    {
        bool pairFound = false;
        
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == targetSum)
                {
                    Console.WriteLine($"Pair found: ({nums[i]}, {nums[j]})");
                    pairFound = true;
                }
            }
        }
        
        if (!pairFound)
        {
            Console.WriteLine("No pairs found with the given sum.");
        }
    }

    // Sorting and Two-Pointer approach
    // Time Complexity: O(n log n) - Dominated by the sorting operation
    // Space Complexity: O(1)
    static void FindPairsSortAndTwoPointers(int[] nums, int targetSum)
    {
        int[] sortedNums = new int[nums.Length];
        Array.Copy(nums, sortedNums, nums.Length);
        
        Array.Sort(sortedNums);
        
        int left = 0;
        int right = sortedNums.Length - 1;
        bool pairFound = false;
        
        while (left < right)
        {
            int currentSum = sortedNums[left] + sortedNums[right];
            
            if (currentSum == targetSum)
            {
                Console.WriteLine($"Pair found: ({sortedNums[left]}, {sortedNums[right]})");
                pairFound = true;
                left++;
                right--;
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
        
        if (!pairFound)
        {
            Console.WriteLine("No pairs found with the given sum.");
        }
    }

    // HashSet approach
    // Time Complexity: O(n)
    // Space Complexity: O(n)
    static void FindPairsUsingHashSet(int[] nums, int targetSum)
    {
        HashSet<int> seenNumbers = new HashSet<int>();
        bool pairFound = false;
        
        foreach (int num in nums)
        {
            int complement = targetSum - num;
            
            if (seenNumbers.Contains(complement))
            {
                Console.WriteLine($"Pair found: ({complement}, {num})");
                pairFound = true;
            }
            
            seenNumbers.Add(num);
        }
        
        if (!pairFound)
        {
            Console.WriteLine("No pairs found with the given sum.");
        }
    }

    // Dictionary - handle duplicates
    // Time Complexity: O(n)
    // Space Complexity: O(n)
    static void FindPairsUsingDictionary(int[] nums, int targetSum)
    {
        Dictionary<int, List<int>> numIndices = new Dictionary<int, List<int>>();
        bool pairFound = false;
        
        for (int i = 0; i < nums.Length; i++)
        {
            if (!numIndices.ContainsKey(nums[i]))
            {
                numIndices[nums[i]] = new List<int>();
            }
            numIndices[nums[i]].Add(i);
        }
        
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = targetSum - nums[i];
            
            if (numIndices.ContainsKey(complement))
            {
                foreach (int j in numIndices[complement])
                {
                    if (j > i)
                    {
                        Console.WriteLine($"Pair found: ({nums[i]}, {nums[j]}) at indices ({i}, {j})");
                        pairFound = true;
                    }
                }
            }
        }
        
        if (!pairFound)
        {
            Console.WriteLine("No pairs found with the given sum.");
        }
    }
	
	public static void DisplayArray<T>(T[] array)
    {
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
    }

    static void Main(string[] args)
    {
        int[] numbers = { 8, 7, 2, 5, 3, 1, 9, 6, 4 };
        int targetSum = 10;

        DisplayArray(numbers);
        Console.WriteLine($"Target sum: {targetSum}\n");

        Console.WriteLine("Brute Force Approach:");
        FindPairsBruteForce(numbers, targetSum);

        Console.WriteLine("\nSorting and Two-Pointer Approach:");
        FindPairsSortAndTwoPointers(numbers, targetSum);

        Console.WriteLine("\nHashSet Approach:");
        FindPairsUsingHashSet(numbers, targetSum);

        Console.WriteLine("\nDictionary Approach:");
        FindPairsUsingDictionary(numbers, targetSum);
    }
}