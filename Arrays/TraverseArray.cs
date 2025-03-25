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
	}
}