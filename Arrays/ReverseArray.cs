using System;

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
	
	//Approach 1: using additional space - O(n) and TC - O(n)
	public static int[] ReverseArray(int[] arr)
	{
		int n = arr.Length;
		int[] auxiliary = new int[n];
		int j = 0;
		for(int i = n-1; i >= 0 ; i--)
		{
			auxiliary[j++] = arr[i];
		}
		return auxiliary;
	}
	
	//Approach 2 : using 2 pointer approach, TC - O(n)
	public static void Reverse(int[] arr)
	{
		int n = arr.Length;
		
		int i = 0, j = n-1;
		
		while(i<=j)
		{
			(arr[i], arr[j]) = (arr[j], arr[i]);
			i++; j--;
		}
	}
	
	//Approach 3 : using recursive method
	public static void RecursiveReverse(int[] arr, int start, int end)
	{
		if(start < end)
		{
			(arr[start], arr[end]) = (arr[end], arr[start]);
			RecursiveReverse(arr, start + 1, end - 1);
		}
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
		
		//Approach 1
		/*Reverse(arr);
		Console.WriteLine("After reversal :");
        foreach (var item in arr)
        {
            Console.Write(item + " ");
        }*/
		
		//Approach 2:
		/*int[] reversedArray = ReverseArray(arr);
		
		foreach (var item in reversedArray)
        {
            Console.Write(item + " ");
        }*/
		
		//Approach 3
		RecursiveReverse(arr, 0, size - 1);
		Console.WriteLine("After reversal :");
		foreach (var item in arr)
        {
            Console.Write(item + " ");
        }
	}
}