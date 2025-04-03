using System;
using System.Collections.Generic;
using System.Linq;					

public class Program
{
	public static void RestartProgram()
    {
        Main();
    }
	
	public static bool IsValidInteger(string input)
    {
        return int.TryParse(input, out _);
    }
	
	//Brute force - sort the array, take minimum and maximum - sorting algo - O(n log n)
	
	//using Max and Min function - O(n)
	public static (int,int) FindMaxLinearSearch(int[] arr)
	{
		int n = arr.Length;
		
		int max = Int32.MinValue;
		int min = Int32.MaxValue;
		for(int i = 0; i < n ; i++)
		{
			max = Math.Max(max, arr[i]);
			min = Math.Min(min, arr[i]);
		}
		
		return (max, min);
	}
	
	// Efficient approach - Process elements in pairs - O(n) with fewer comparisons
	public static (int, int) FindMinMaxPairwise(int[] arr)
	{
		int n = arr.Length;

		if (n == 0)
			return (Int32.MinValue, Int32.MaxValue);

		int min, max;
		int i = 0;

		if (n % 2 == 0) // Even
		{
			if (arr[0] < arr[1])
			{
				min = arr[0];
				max = arr[1];
			}
			else
			{
				min = arr[1];
				max = arr[0];
			}
			i = 2;
		}
		else // Odd
		{
			min = max = arr[0];
			i = 1;
		}

		while (i < n - 1)
		{
			if (arr[i] < arr[i + 1])
			{
				min = Math.Min(min, arr[i]);
				max = Math.Max(max, arr[i + 1]);
			}
			else
			{
				min = Math.Min(min, arr[i + 1]);
				max = Math.Max(max, arr[i]);
			}

			i += 2;
		}

		return (max, min);
	}
	
	
	public static void Main()
	{
		Console.WriteLine("Enter size of array : ");
		string sizeInput = Console.ReadLine();
		
		if (!IsValidInteger(sizeInput))
        {
            Console.WriteLine("Invalid input! Size should be an integer.");
            RestartProgram();
        }
		
		int size = int.Parse(sizeInput);
        int[] arr = new int[size];
		
		for(int i = 0 ; i < size ; i++)
		{
			string elementInput = Console.ReadLine();
            if (!IsValidInteger(elementInput))
            {
                Console.WriteLine("Invalid input! Please enter an integer.");
                i--;
                continue;
            }

            arr[i] = int.Parse(elementInput);
		}
		
		Console.WriteLine("The entered array is:");
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
		
		Console.WriteLine();
		
		//var (maxElement, minElement) = FindMaxLinearSearch(arr);
		
		var (maxElement, minElement) = FindMinMaxPairwise(arr);
		
		Console.WriteLine($"Maximum element : {maxElement}");
		Console.WriteLine($"Minimum element : {minElement}");
	}
}