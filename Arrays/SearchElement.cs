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
	
	//brute-force
	//linear search
	public static bool DoesElementExists(int[] arr, int size, int k)
	{
		for(int i = 0 ; i < size ; i++)
		{
			if(arr[i] == k)
				return true;
		}
		
		return false;
	}
	
	//better approach - using binary search(only if array is sorted)
	public static bool BinarySearch(int[] arr, int n, int k)
	{
		int left = 0, right = n-1;
		
		while(left <= right)
		{
			int mid = left + (right - left) / 2;
			if(arr[mid] == k)
				return true;
			
			if(arr[mid] < k)
				left = mid + 1;
			else
				right = mid - 1;
		}
		
		return false;
	}
	
	//using HashSet
	public static bool HashSetSearch(int[] arr, int n)
	{
		 HashSet<int> set = new HashSet<int>(arr);
    	 return set.Contains(n);
	}
	
	// using parallel linq
	public static bool ParallelSearch(int[] arr, int n)
	{
		return arr.AsParallel().Any(x => x == n);
	}
	
	// Jump search
	public static bool JumpSearch(int[] arr, int n)
	{
		int length = arr.Length;
		int stepSize = (int)Math.Floor(Math.Sqrt(length));
		int step = stepSize;
		int prev = 0;

		if (n < arr[0] || n > arr[length - 1])
			return false;

		while (step < length && arr[step - 1] < n)
		{
			prev = step;
			step += stepSize;
			if (prev >= length)
				return false;
		}

		if (step < length && arr[step - 1] == n)
			return true;

		while (prev < Math.Min(step, length) && arr[prev] < n)
		{
			prev++;
		}

		return (prev < length && arr[prev] == n);
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
		
		Console.WriteLine("Enter element to search : ");
		string elementToSearch = Console.ReadLine();
		
		if (!IsValidInteger(elementToSearch))
        {
            Console.WriteLine("Invalid input! Size should be an integer.");
            RestartProgram();
        }
		
		int k = int.Parse(elementToSearch);
		
		// Linear Search
		//bool linearResult  = DoesElementExists(arr, size, k);
		//Console.WriteLine($"Result: {(linearResult ? "Element exists" : "Element does not exist")}");
		
		//Binary Search
		//bool binaryResult = BinarySearch(arr, size, k);
		//Console.WriteLine($"Result: {(binaryResult ? "Element exists" : "Element does not exist")}");

		// HashSet Search		
		//bool hashSetResult = HashSetSearch(arr, k);
		//Console.WriteLine($"Result: {(hashSetResult ? "Element exists" : "Element does not exist")}");

		//Parallel Search
		//bool parallelResult = ParallelSearch(arr, k);
		//Console.WriteLine($"Result: {(parallelResult ? "Element exists" : "Element does not exist")}");
		
		//Jump Search
		Array.Sort(arr);
		bool jumpResult = JumpSearch(arr, k);
		Console.WriteLine($"Result: {(jumpResult ? "Element exists" : "Element does not exist")}");
	}
}