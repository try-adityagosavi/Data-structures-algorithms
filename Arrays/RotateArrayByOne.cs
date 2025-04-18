using System;
					
public class ArrayRotation
{
    public static void DisplayArray<T>(T[] array)
    {
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
    }
    
    private static void Swap<T>(T[] array, int firstIndex, int secondIndex)
    {
        T temp = array[firstIndex];
        array[firstIndex] = array[secondIndex];
        array[secondIndex] = temp;
    }
    
    // Time Complexity: O(n)
    // Space Complexity: O(1)
    public static void RotateByOneUsingSwap<T>(T[] array)
    {
        if (array == null || array.Length <= 1)
            return;
            
        int length = array.Length;
        int currentIndex = 0;
        int lastIndex = length - 1;
        
        while (currentIndex != lastIndex)
        {
            Swap(array, currentIndex, lastIndex);
            currentIndex++;
        }
    }
    
    // Time Complexity: O(n)
    ///Space Complexity: O(1)
    public static void RotateByOneUsingShift<T>(T[] array)
    {
        if (array == null || array.Length <= 1)
            return;
            
        int length = array.Length;
        T lastElement = array[length - 1];
        
        for (int i = length - 2; i >= 0; i--)
        {
            array[i + 1] = array[i];
        }
        
        array[0] = lastElement;
    }
    
    public static void Main()
    {
        Console.WriteLine("=== integers ===");
        int[] intArray = new int[] {10, 20, 30, 40, 50, 60};
        DisplayArray(intArray);
        RotateByOneUsingSwap(intArray);
        DisplayArray(intArray);
        
        // strings
        Console.WriteLine("\n=== strings ===");
        string[] stringArray = new string[] {"apple", "banana", "cherry", "date", "elderberry"};
        DisplayArray(stringArray);
        RotateByOneUsingShift(stringArray);
        DisplayArray(stringArray);
        
        // doubles
        Console.WriteLine("\n=== doubles ===");
        double[] doubleArray = new double[] {1.1, 2.2, 3.3, 4.4, 5.5};
        DisplayArray(doubleArray);
        RotateByOneUsingSwap(doubleArray);
        DisplayArray(doubleArray);
        
        // custom objects
        Console.WriteLine("\n=== custom objects ===");
        Person[] personArray = new Person[] {
            new Person("Aditya", 25),
            new Person("Chinmay", 30),
            new Person("Yogesh", 35),
        };
        DisplayArray(personArray);
        RotateByOneUsingShift(personArray);
        DisplayArray(personArray);
    }
}

public class Person
{
    public string Name { get; }
    public int Age { get; }
    
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
    public override string ToString()
    {
        return $"{Name} ({Age})";
    }
}