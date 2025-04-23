using System;
using System.Collections.Generic;
using System.Linq;

class ThreeSum
{
    static void DisplayTriplets(List<(int, int, int)> triplets)
    {
        if (triplets.Count == 0)
        {
            Console.WriteLine("No triplets found with the given sum.");
            return;
        }

        foreach (var (a, b, c) in triplets)
        {
            Console.WriteLine($"Triplet found: ({a}, {b}, {c})");
        }
        Console.WriteLine($"Total triplets found: {triplets.Count}");
    }

    // Brute force approach
    // Time Complexity: O(n³)
    // Space Complexity: O(k) -  k -> number of triplets found
    static List<(int, int, int)> FindTripletsBruteForce(int[] nums, int targetSum)
    {
        var triplets = new List<(int, int, int)>();
        var seen = new HashSet<string>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                for (int k = j + 1; k < nums.Length; k++)
                {
                    if (nums[i] + nums[j] + nums[k] == targetSum)
                    {
                        int[] triplet = { nums[i], nums[j], nums[k] };
                        Array.Sort(triplet);
                        
                        string tripletKey = $"{triplet[0]},{triplet[1]},{triplet[2]}";
                        
                        if (!seen.Contains(tripletKey))
                        {
                            triplets.Add((triplet[0], triplet[1], triplet[2]));
                            seen.Add(tripletKey);
                        }
                    }
                }
            }
        }
        
        return triplets;
    }

    // Time Complexity: O(n²)
    // Space Complexity: O(n + k) - n -> size of the dictionary and k -> number of triplets
    public static List<(int, int, int)> FindTripletsUsingDictionary(int[] nums, int targetSum)
    {
        var triplets = new List<(int, int, int)>();
        var seen = new HashSet<string>();
        
        Dictionary<int, List<int>> valToIndices = new Dictionary<int, List<int>>();
        for (int i = 0; i < nums.Length; i++)
        {
            if (!valToIndices.ContainsKey(nums[i]))
            {
                valToIndices[nums[i]] = new List<int>();
            }
            valToIndices[nums[i]].Add(i);
        }
        
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                int complement = targetSum - nums[i] - nums[j];
                
                if (valToIndices.ContainsKey(complement))
                {
                    foreach (int k in valToIndices[complement])
                    {
                        if (k != i && k != j)
                        {
                            int[] triplet = { nums[i], nums[j], complement };
                            Array.Sort(triplet);
                            
                            string tripletKey = $"{triplet[0]},{triplet[1]},{triplet[2]}";
                            
                            if (!seen.Contains(tripletKey))
                            {
                                triplets.Add((triplet[0], triplet[1], triplet[2]));
                                seen.Add(tripletKey);
                            }

                            break;
                        }
                    }
                }
            }
        }
        
        return triplets;
    }

    // Sorting and Two-Pointer approach 
    // Time Complexity: O(n²) - Sorting O(n log n) + O(n²)
    // Space Complexity: O(k)
    static List<(int, int, int)> FindTripletsSortAndTwoPointers(int[] nums, int targetSum)
    {
        var triplets = new List<(int, int, int)>();
        
        int[] sortedNums = new int[nums.Length];
        Array.Copy(nums, sortedNums, nums.Length);
        Array.Sort(sortedNums);
        
        for (int i = 0; i < sortedNums.Length - 2; i++)
        {
            if (i > 0 && sortedNums[i] == sortedNums[i - 1])
                continue;
                
            int left = i + 1;
            int right = sortedNums.Length - 1;
            int currentTarget = targetSum - sortedNums[i];
            
            while (left < right)
            {
                int currentSum = sortedNums[left] + sortedNums[right];
                
                if (currentSum == currentTarget)
                {
                    triplets.Add((sortedNums[i], sortedNums[left], sortedNums[right]));
                    
                    while (left < right && sortedNums[left] == sortedNums[left + 1])
                        left++;
                        
                    while (left < right && sortedNums[right] == sortedNums[right - 1])
                        right--;
                        
                    left++;
                    right--;
                }
                else if (currentSum < currentTarget)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }
        
        return triplets;
    }

    public static void DisplayArray<T>(T[] array)
    {
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
    }

    static void Main(string[] args)
    {
        int[] numbers = { -1, 0, 1, 2, -1, -4, 2, 3, -2, 0 };
        int targetSum = 0;

        DisplayArray(numbers);
        Console.WriteLine($"Target sum: {targetSum}\n");

        Console.WriteLine("1.Brute Force Approach:");
        var result1 = FindTripletsBruteForce(numbers, targetSum);
        DisplayTriplets(result1);

        Console.WriteLine("\n2.Dictionary Approach:");
        var result2 = FindTripletsUsingDictionary(numbers, targetSum);
        DisplayTriplets(result2);

        Console.WriteLine("\n3.Sorting and Two-Pointer Approach:");
        var result3 = FindTripletsSortAndTwoPointers(numbers, targetSum);
        DisplayTriplets(result3);
    }
}