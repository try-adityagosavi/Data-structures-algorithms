using System;
using System.Collections.Generic;
using System.Linq;

class FourSum
{
    static void DisplayQuadruplets(IList<IList<int>> quadruplets)
    {
        if (quadruplets.Count == 0)
        {
            Console.WriteLine("No quadruplets found with the given sum.");
            return;
        }

        foreach (var quadruplet in quadruplets)
        {
            Console.WriteLine($"Quadruplet found: ({quadruplet[0]}, {quadruplet[1]}, {quadruplet[2]}, {quadruplet[3]})");
        }
        Console.WriteLine($"Total quadruplets found: {quadruplets.Count}");
    }

    // Brute force approach
    // Time Complexity: O(n⁴)
    // Space Complexity: O(k) - k -> number of quadruplets found
    static IList<IList<int>> FourSumBruteForce(int[] nums, int target)
    {
        var result = new List<IList<int>>();
        var seen = new HashSet<string>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                for (int k = j + 1; k < nums.Length; k++)
                {
                    for (int l = k + 1; l < nums.Length; l++)
                    {
                        if ((long)nums[i] + nums[j] + nums[k] + nums[l] == target)
                        {
                            int[] quadruplet = { nums[i], nums[j], nums[k], nums[l] };
                            Array.Sort(quadruplet);
                            
                            string quadrupletKey = $"{quadruplet[0]},{quadruplet[1]},{quadruplet[2]},{quadruplet[3]}";
                            
                            if (!seen.Contains(quadrupletKey))
                            {
                                result.Add(new List<int> { quadruplet[0], quadruplet[1], quadruplet[2], quadruplet[3] });
                                seen.Add(quadrupletKey);
                            }
                        }
                    }
                }
            }
        }
        
        return result;
    }

    // Dictionary approach
    // Time Complexity: O(n³)
    // Space Complexity: O(n + k) - n -> size of the dictionary and k -> number of quadruplets
    public static IList<IList<int>> FourSumUsingDictionary(int[] nums, int target)
    {
        var result = new List<IList<int>>();
        var seen = new HashSet<string>();
        
        if (nums == null || nums.Length < 4)
            return result;
            
        Array.Sort(nums);
        
        for (int i = 0; i < nums.Length - 3; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;
                
            for (int j = i + 1; j < nums.Length - 2; j++)
            {
                if (j > i + 1 && nums[j] == nums[j - 1])
                    continue;
                    
                int left = j + 1;
                int right = nums.Length - 1;
                
                while (left < right)
                {
                    long sum = (long)nums[i] + nums[j] + nums[left] + nums[right];
                    
                    if (sum == target)
                    {
                        result.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });
                        
                        while (left < right && nums[left] == nums[left + 1])
                            left++;
                            
                        while (left < right && nums[right] == nums[right - 1])
                            right--;
                            
                        left++;
                        right--;
                    }
                    else if (sum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }
        }
        
        return result;
    }

    // Hash Table approach with pairs
    // Time Complexity: O(n²) for building pairs + O(n²) for searching = O(n²)
    // In worst case, O(n²) × O(n²) = O(n⁴)
    // Space Complexity: O(n²) for storing pairs
    static IList<IList<int>> FourSumWithPairs(int[] nums, int target)
    {
        var result = new List<IList<int>>();
        
        if (nums == null || nums.Length < 4)
            return result;
            
        Array.Sort(nums);
        int n = nums.Length;
        
        Dictionary<long, List<(int, int)>> pairSums = new Dictionary<long, List<(int, int)>>();

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                long sum = (long)nums[i] + nums[j];
                
                if (!pairSums.ContainsKey(sum))
                {
                    pairSums[sum] = new List<(int, int)>();
                }
                
                pairSums[sum].Add((i, j));
            }
        }
        
        HashSet<string> used = new HashSet<string>();
        
        for (int i = 0; i < n - 1; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;
                
            for (int j = i + 1; j < n; j++)
            {
                if (j > i + 1 && nums[j] == nums[j - 1])
                    continue;
                    
                long complement = (long)target - nums[i] - nums[j];
                
                if (pairSums.ContainsKey(complement))
                {
                    foreach (var pair in pairSums[complement])
                    {
                        int k = pair.Item1;
                        int l = pair.Item2;
                        
                        if (k > j && k != i && k != j && l != i && l != j)
                        {
                            int[] quad = { nums[i], nums[j], nums[k], nums[l] };
                            Array.Sort(quad);
                            
                            string quadKey = $"{quad[0]},{quad[1]},{quad[2]},{quad[3]}";
                            
                            if (!used.Contains(quadKey))
                            {
                                result.Add(new List<int> { quad[0], quad[1], quad[2], quad[3] });
                                used.Add(quadKey);
                            }
                        }
                    }
                }
            }
        }
        
        return result;
    }

    public static void DisplayArray<T>(T[] array)
    {
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
    }

    static void Main(string[] args)
    {
        int[] numbers = { 1, 0, -1, 0, -2, 2 };
        int target = 0;

        DisplayArray(numbers);
        Console.WriteLine($"Target sum: {target}\n");

        Console.WriteLine("1. Brute Force Approach:");
        var result1 = FourSumBruteForce(numbers, target);
        DisplayQuadruplets(result1);

        Console.WriteLine("\n2. Using Dictionary/Two-Pointer Approach:");
        var result2 = FourSumUsingDictionary(numbers, target);
        DisplayQuadruplets(result2);

        Console.WriteLine("\n3. Using Pairs Approach:");
        var result3 = FourSumWithPairs(numbers, target);
        DisplayQuadruplets(result3);
    }
}