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
		
		var (maxElement, minElement) = FindMaxLinearSearch(arr);
		
		Console.WriteLine($"Maximum element : {maxElement}");
		Console.WriteLine($"Minimum element : {minElement}");
	}
}