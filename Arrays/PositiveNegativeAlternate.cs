using System;
using System.Collections.Generic;


public static class AlternatingArrangement
{
    // Time Complexity: O(n)
    // Space Complexity: O(n)
    public static int[] RearrangeAlternating(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
        {
            return arr;
        }
        
        List<int> _negativeElements = new List<int>();
        List<int> _positiveElements = new List<int>();
        
        foreach (int element in arr)
        {
			((element < 0) ? _negativeElements : _positiveElements).Add(element);
        }
        
        return MergeAlternating(_negativeElements, _positiveElements, arr.Length);
    }
    
    // Rearranges array with alternating negative and positive elements,
    // starting with negative if available. Handles cases where counts are unequal
    public static int[] RearrangeAlternatingWithOption(int[] arr, bool startWithNegative = true)
    {
        if (arr == null || arr.Length <= 1)
        {
            return arr; 
        }
        
        List<int> _negativeElements = new List<int>();
        List<int> _positiveElements = new List<int>();
        
        foreach (int element in arr)
        {
            if (element < 0)
            {
                _negativeElements.Add(element);
            }
            else
            {
                _positiveElements.Add(element);
            }
        }
        
        var firstList = startWithNegative ? _negativeElements : _positiveElements;
        var secondList = startWithNegative ? _positiveElements : _negativeElements;
        
        return MergeAlternating(firstList, secondList, arr.Length);
    }
    
    // Merges two lists in alternating fashion into a new array
    private static int[] MergeAlternating(List<int> firstList, List<int> secondList, int resultLength)
    {
        int[] result = new int[resultLength];
        int index = 0;
        
        int firstCount = firstList.Count;
        int secondCount = secondList.Count;
        int minCount = Math.Min(firstCount, secondCount);
        
        for (int i = 0; i < minCount; i++)
        {
            result[index++] = firstList[i];
            result[index++] = secondList[i];
        }
        
        for (int i = minCount; i < firstCount; i++)
        {
            result[index++] = firstList[i];
        }
        
        for (int i = minCount; i < secondCount; i++)
        {
            result[index++] = secondList[i];
        }
        
        return result;
    }
    
    //Alternate approach - right rotate elements in place
    // Time Complexity: O(n^2) in worst case (due to rotations)
    // Space Complexity: O(1)
    public static int[] RearrangeAlternatingInPlace(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
        {
            return arr;
        }
        
        int outOfPlace = -1;
        
        for (int i = 0; i < arr.Length; i++)
        {
            if (outOfPlace == -1)
            {
                // At even positions (0, 2, 4...), we expect negative numbers
                // At odd positions (1, 3, 5...), we expect positive numbers
                bool shouldBeNegative = (i % 2 == 0);
                
                if ((shouldBeNegative && arr[i] >= 0) || 
                    (!shouldBeNegative && arr[i] < 0))
                {
                    outOfPlace = i;
                }
            }
            else
            {
                bool currentShouldBeNegative = (i % 2 == 0);
                bool outOfPlaceIsNegative = (arr[outOfPlace] < 0);
                
                if ((currentShouldBeNegative != outOfPlaceIsNegative) && 
                    ((arr[i] < 0) == currentShouldBeNegative))
                {
                    RightRotate(arr, outOfPlace, i);
                    
                    if (i - outOfPlace > 2)
                    {
                        i = outOfPlace + 2;
                    }
                    else
                    {
                        i++;
                    }
                    
                    outOfPlace = -1;
                }
            }
        }
        
        return arr;
    }
    
    private static void RightRotate(int[] arr, int startIndex, int endIndex)
    {
        int temp = arr[endIndex];
        
        for (int i = endIndex; i > startIndex; i--)
        {
            arr[i] = arr[i - 1];
        }
        
        arr[startIndex] = temp;
    }
}

public class Program
{
    public static void Main()
    {
     	int[] arr = { 1, 2, 3, -1, -2, -3 };
		Console.WriteLine($"[{string.Join(", ", arr)}]");
		
		int[] negativeFirstResult = AlternatingArrangement.RearrangeAlternating(arr.Clone() as int[]);
        Console.WriteLine($"Negative First: [{string.Join(", ", negativeFirstResult)}]");
     
		int[] positiveFirstResult = AlternatingArrangement.RearrangeAlternatingWithOption(arr.Clone() as int[], false);
        Console.WriteLine($"Positive First: [{string.Join(", ", positiveFirstResult)}]");

		int[] inPlaceResult = AlternatingArrangement.RearrangeAlternatingInPlace(arr.Clone() as int[]);
        Console.WriteLine($"In-Place Method: [{string.Join(", ", inPlaceResult)}]");
    }
}