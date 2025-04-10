using System;	 
using System.Collections.Generic;
using System.Linq;

//Two List approach
/// Time Complexity: O(n)
/// Space Complexity: O(n) - additional space needed for the lists
public class TwoListSeparator
{
	private static List<int> _negativeNumbers;
    private static List<int> _positiveNumbers;
	
	public static int[] SeparateNumbers(int[] arr)
	{
		int length = arr.Length;
		Initialize();
		
		for(int i = 0 ; i < length ; i++)
    		(arr[i] < 0 ? _negativeNumbers : _positiveNumbers).Add(arr[i]);
		
		int[] result = _negativeNumbers.Concat(_positiveNumbers).ToArray();
		
		return result;
	}
	
	public static void Initialize()
	{
		_negativeNumbers = new List<int>();
		_positiveNumbers = new List<int>();
	}
}

//Two pointer approach
//Time Complexity - O(n)
//Space Complexity - O(1)
public static class InPlaceSeparator
{
	public static int[] SeparateNumbers(int[] arr)
	{
		int length = arr.Length;
		
		int negativeIndex = 0, currentIndex = 0; 
		
		while (currentIndex < length)
        {
            if (arr[currentIndex] < 0)
            {
                Swap(arr, currentIndex, negativeIndex);
                negativeIndex++;
            }
            
            currentIndex++;
        }
		
		return arr;
	}
	
	private static void Swap(int[] array, int firstIndex, int secondIndex)
    {
        if (firstIndex != secondIndex)
        {
            (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);
        }
    }
}

public class Program
{
	public static void Main()
	{
		int[] arr = { 1, -1, 3, 2, -7, -5, 11, 6 };
		
		Console.WriteLine($"[{string.Join(", ", arr)}]");
		
		Console.WriteLine("\nCalling Shift()");
		
		int[] dummy = (int[])arr.Clone();
		int[] result = InPlaceSeparator.SeparateNumbers(dummy);
	
		Console.WriteLine($"[{string.Join(", ", result)}]");

		//using Two list approach
		Console.WriteLine("\nTwo list approach");

		int[] dummy2 = (int[])arr.Clone();
		int[] result2 = TwoListSeparator.SeparateNumbers(dummy2);
	
		Console.WriteLine($"[{string.Join(", ", result2)}]");
		
	}
}