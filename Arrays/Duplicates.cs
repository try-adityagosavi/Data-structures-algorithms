using System;
using System.Collections.Generic;
using System.Diagnostics;
//using System.Linq;

public class DuplicateFinder
{
    public static void DisplayArray<T>(T[] array)
    {
        Console.WriteLine($"Array: [{string.Join(", ", array)}]");
    }
    
    // Brute Force Approach
    // Time Complexity: O(n²)
    // Space Complexity: O(k) - k is the number of duplicates found
    public static List<T> FindDuplicatesBruteForce<T>(T[] nums)
    {
        List<T> duplicates = new List<T>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (EqualityComparer<T>.Default.Equals(nums[i], nums[j]) && !duplicates.Contains(nums[i]))
                {
                    duplicates.Add(nums[i]);
                    break;
                }
            }
        }
        
        return duplicates;
    }
    
    // Better Approach - Using Dictionary
    // Time Complexity: O(n)
    // Space Complexity: O(n) - Dictionary can store at most n elements
    public static List<T> FindDuplicatesBetter<T>(T[] nums)
    {
        List<T> duplicates = new List<T>();
        Dictionary<T, int> frequency = new Dictionary<T, int>();
        
        foreach (var num in nums)
        {
            if (frequency.ContainsKey(num))
            {
                frequency[num]++;
                if (frequency[num] == 2)
                {
                    duplicates.Add(num);
                }
            }
            else
            {
                frequency[num] = 1;
            }
        }
        
        return duplicates;
    }
    
    // Optimal Approach - Using HashSet
    // Time Complexity: O(n)
    // Space Complexity: O(n)
    public static List<T> FindDuplicatesOptimal<T>(T[] nums)
    {
        List<T> duplicates = new List<T>();
        HashSet<T> seen = new HashSet<T>();
        
        foreach (var num in nums)
        {
            if (!seen.Add(num))
            {
                if (!duplicates.Contains(num))
                {
                    duplicates.Add(num);
                }
            }
        }
        
        return duplicates;
    }
    
    public static void Main()
    {
        int[] numbers = { 1, 2, 3, 1, 2, 5, 6, 7, 8, 9, 9 };
        TestWithArray(numbers, "Integers");
        
        string[] words = { "apple", "banana", "orange", "apple", "grape", "banana", "kiwi" };
        TestWithArray(words, "Strings");
        
        double[] doubles = { 1.1, 2.2, 3.3, 1.1, 4.4, 2.2, 5.5 };
        TestWithArray(doubles, "Doubles");
        
        Person[] people = {
            new Person("Alice", 25),
            new Person("Bob", 30),
            new Person("Charlie", 35),
            new Person("Alice", 25),
            new Person("David", 40),
            new Person("Bob", 30)
        };
        TestWithArray(people, "Person objects");
    }
    
    private static void TestWithArray<T>(T[] array, string arrayType)
    {
        Console.WriteLine($"\nTesting with {arrayType}:");
        DisplayArray(array);
        
        Stopwatch stopwatch = new Stopwatch();
        
        stopwatch.Start();
        var duplicatesBruteForce = FindDuplicatesBruteForce(array);
        stopwatch.Stop();
        Console.WriteLine($"Brute Force Approach: [{string.Join(", ", duplicatesBruteForce)}]");
        Console.WriteLine($"Time taken: {stopwatch.ElapsedTicks} ticks");
        
        stopwatch.Restart();
        var duplicatesBetter = FindDuplicatesBetter(array);
        stopwatch.Stop();
        Console.WriteLine($"\nBetter Approach: [{string.Join(", ", duplicatesBetter)}]");
        Console.WriteLine($"Time taken: {stopwatch.ElapsedTicks} ticks");
        
        stopwatch.Restart();
        var duplicatesOptimal = FindDuplicatesOptimal(array);
        stopwatch.Stop();
        Console.WriteLine($"\nOptimal Approach: [{string.Join(", ", duplicatesOptimal)}]");
        Console.WriteLine($"Time taken: {stopwatch.ElapsedTicks} ticks");
        
        Console.WriteLine(new string('-', 50));
		
		//Console.WriteLine(string.Concat(Enumerable.Repeat(Environment.NewLine, 1)));
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
    
    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
            return Name == other.Name && Age == other.Age;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }
}