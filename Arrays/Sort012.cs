using System;

// This problem is also known as the Dutch National Flag problem
// We can sort array which separates 0, 1, 2 into their respective groups		
// For quick sort : TC - O(nlogn), SC - O(log n)

//Sort based on count
// TC - O(n), SC - O(1)

public static class SortBasedOnCount
{
	static int countZeros = 0, countOnes = 0, countTwos = 0;
	
	public static int[] SortArray(int[] arr)
	{
		int length = arr.Length;
		 
		for(int i = 0; i < length ; i++)
		{
			if(arr[i] == 0)
				countZeros++;
			
			if(arr[i] == 1)
				countOnes++;
			
			if(arr[i] == 2)
				countTwos++;
		}
		
		int currentIndex = 0;
		
		for(int k = 0; k < countZeros; k++)
			arr[currentIndex++] = 0;
		
		for(int k = 0; k < countOnes; k++)
			arr[currentIndex++] = 1;
		
		for(int k = 0; k < countTwos; k++)
			arr[currentIndex++] = 2;
		
		return arr;
	}
}

//using three pointer
//TC - O(n), SC - O(1)
public static class ThreePointers
{
	public static int[] SortArray(int[] arr)
	{
		int length  = arr.Length;
        int lowIndex = 0;       // Points to the end of 0s section
        int midIndex = 0;       // Current element being processed
        int highIndex = length - 1; // Points to the start of 2s section
		
		while(midIndex <= highIndex)
		{
			switch(arr[midIndex])
            {
                case 0:
                    Swap(arr, lowIndex, midIndex);
                    lowIndex++;
                    midIndex++;
                    break;
                    
                case 1:
                    midIndex++;
                    break;
                    
                case 2:
                    Swap(arr, midIndex, highIndex);
                    highIndex--;
                    break;
            }	
		}
		
		return arr;
	}

    private static void Swap(int[] array, int firstIndex, int secondIndex)
    {
        (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);
    }
}

public class Program
{
	public static void Main()
	{
		int[] arr = {0, 1, 2, 0, 1, 2};
		
		Console.WriteLine($"[{string.Join(", ", arr)}]");
		
        int[] dummy = (int[])arr.Clone();
		int[] result = SortBasedOnCount.SortArray(dummy);
		
		Console.WriteLine("After separation : ");
		
		Console.WriteLine($"[{string.Join(", ", arr)}]");

        //Three pointer approach
        dummy = (int[])arr.Clone();
		int[] result2 = ThreePointers.SortArray(arr);
		
		Console.WriteLine($"Three pointers => [{string.Join(", ", result2)}]");
	}
}