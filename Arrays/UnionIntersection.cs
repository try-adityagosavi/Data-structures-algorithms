using System;
using System.Collections.Generic;

namespace ArrayOperations
{
    public static class ArrayExtensions
    {
        private enum SetOperation { Union, Intersection }

        public static T[] Union<T>(this T[] first, T[] second, bool areSorted = false) 
            where T : IComparable<T>, IEquatable<T>
        {
            return PerformSetOperation(first, second, SetOperation.Union, areSorted);
        }

        public static T[] Intersection<T>(this T[] first, T[] second, bool areSorted = false) 
            where T : IComparable<T>, IEquatable<T>
        {
            return PerformSetOperation(first, second, SetOperation.Intersection, areSorted);
        }

        private static T[] PerformSetOperation<T>(T[] first, T[] second, SetOperation operation, bool areSorted) 
            where T : IComparable<T>, IEquatable<T>
        {
            if (first == null || second == null)
                throw new ArgumentNullException(first == null ? nameof(first) : nameof(second));

            if (first.Length == 0 || second.Length == 0)
            {
                if (operation == SetOperation.Intersection || first.Length == 0 && second.Length == 0)
                    return Array.Empty<T>();
                return first.Length == 0 ? 
                    (areSorted ? RemoveDuplicatesSorted(second) : RemoveDuplicatesUnsorted(second)) : 
                    (areSorted ? RemoveDuplicatesSorted(first) : RemoveDuplicatesUnsorted(first));
            }

            return areSorted
                ? ProcessSortedArrays(first, second, operation)
                : ProcessUnsortedArrays(first, second, operation);
        }

		// Time: O(n + m) for the sorted array approach
        // Space: O(n + m) for union, O(min(n, m)) for intersection
        private static T[] ProcessSortedArrays<T>(T[] first, T[] second, SetOperation operation) 
            where T : IComparable<T>, IEquatable<T>
        {
            int capacity = operation == SetOperation.Union ? 
                first.Length + second.Length : Math.Min(first.Length, second.Length);
            List<T> result = new List<T>(capacity);
            
            int i = 0, j = 0;

            while (i < first.Length && j < second.Length)
            {
                while (i > 0 && i < first.Length && first[i].Equals(first[i - 1])) i++;
                while (j > 0 && j < second.Length && second[j].Equals(second[j - 1])) j++;
                
                if (i >= first.Length || j >= second.Length) break;

                int comparison = first[i].CompareTo(second[j]);
                
                if (comparison < 0)
                {
                    if (operation == SetOperation.Union)
                        result.Add(first[i]);
                    i++;
                }
                else if (comparison > 0)
                {
                    if (operation == SetOperation.Union)
                        result.Add(second[j]);
                    j++;
                }
                else
                {
                    result.Add(first[i]);
                    i++;
                    j++;
                }
            }

            if (operation == SetOperation.Union)
            {
                while (i < first.Length)
                {
                    if (i == 0 || !first[i].Equals(first[i - 1]))
                        result.Add(first[i]);
                    i++;
                }
                
                while (j < second.Length)
                {
                    if (j == 0 || !second[j].Equals(second[j - 1]))
                        result.Add(second[j]);
                    j++;
                }
            }

            return result.ToArray();
        }

		// Time: O(n + m) for the hash-based approach
        // Space: O(n + m) for union, O(min(n, m)) for intersection 
        private static T[] ProcessUnsortedArrays<T>(T[] first, T[] second, SetOperation operation) 
            where T : IComparable<T>, IEquatable<T>
        {
            if (operation == SetOperation.Intersection && first.Length > second.Length)
            {
                T[] temp = first;
                first = second;
                second = temp;
            }
            
            HashSet<T> set = new HashSet<T>(operation == SetOperation.Intersection ? first.Length : first.Length + second.Length);
            List<T> result = new List<T>(operation == SetOperation.Union ? first.Length + second.Length : Math.Min(first.Length, second.Length));
            
            if (operation == SetOperation.Union)
            {
                foreach (T item in first)
                {
                    if (set.Add(item))
                        result.Add(item);
                }
                
                foreach (T item in second)
                {
                    if (set.Add(item))
                        result.Add(item);
                }
            }
            else // Intersection
            {
                foreach (T item in first)
                    set.Add(item);
                
                HashSet<T> seen = new HashSet<T>(Math.Min(first.Length, second.Length));
                foreach (T item in second)
                    if (set.Contains(item) && seen.Add(item))
                        result.Add(item);
            }
            
            return result.ToArray();
        }

        // Time: O(n) where n is the length of the sorted array
        // Space: O(n) in worst case for the result array
        private static T[] RemoveDuplicatesSorted<T>(T[] array) 
            where T : IComparable<T>, IEquatable<T>
        {
            if (array.Length <= 1)
                return array;
                
            List<T> result = new List<T>(array.Length);
            result.Add(array[0]);
            
            for (int i = 1; i < array.Length; i++)
                if (!array[i].Equals(array[i - 1]))
                    result.Add(array[i]);
            
            return result.ToArray();
        }
		
		// Time: O(n) where n is the length of the unsorted array
        // Space: O(n) for the HashSet and result array
        private static T[] RemoveDuplicatesUnsorted<T>(T[] array) 
            where T : IComparable<T>, IEquatable<T>
        {
            if (array.Length <= 1)
                return array;
                
            HashSet<T> seen = new HashSet<T>(array.Length);
            List<T> result = new List<T>(array.Length);
            
            foreach (T item in array)
                if (seen.Add(item))
                    result.Add(item);
            
            return result.ToArray();
        }
    }

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("===== UNION OPERATIONS =====");
            TestUnion();
            
            Console.WriteLine("\n===== INTERSECTION OPERATIONS =====");
            TestIntersection();
        }
        
        private static void TestUnion()
        {
            // sorted arrays
            int[] sortedA = { 1, 3, 5, 7 };
            int[] sortedB = { 2, 3, 6, 7, 9 };
            DisplayArray("Sorted Union Result", sortedA.Union(sortedB, true));
            DisplayArray("Original arrays:", sortedA, sortedB);
            
            // sorted arrays with duplicates
            int[] sortedC = { 1, 3, 3, 5, 7 };
            int[] sortedD = { 2, 3, 5, 7, 9 };
            DisplayArray("Sorted Arrays with Duplicates - Union Result", sortedC.Union(sortedD, true));
            DisplayArray("Original arrays with duplicates:", sortedC, sortedD);
            
            // unsorted arrays with duplicates
            int[] unsortedA = { 7, 3, 1, 3, 5 };
            int[] unsortedB = { 9, 3, 2, 7, 5 };
            DisplayArray("Unsorted Arrays with Duplicates - Union Result", unsortedA.Union(unsortedB, false));
            DisplayArray("Original unsorted arrays with duplicates:", unsortedA, unsortedB);
            
            // Empty and non-Empty
            int[] empty = { };
            int[] nonEmpty = { 1, 2, 3 };
            DisplayArray("Empty + Non-Empty Union", empty.Union(nonEmpty, false));
            DisplayArray("Empty + Empty Union", empty.Union(empty, false));
        }
        
        private static void TestIntersection()
        {
            // sorted arrays with common elements
            int[] sortedA = { 1, 3, 5, 7, 9 };
            int[] sortedB = { 2, 3, 5, 7, 11 };
            DisplayArray("Sorted Intersection Result", sortedA.Intersection(sortedB, true));
            DisplayArray("Original arrays:", sortedA, sortedB);
            
            // sorted arrays with duplicates
            int[] sortedC = { 1, 3, 3, 5, 7, 9 };
            int[] sortedD = { 2, 3, 3, 5, 7, 11 };
            DisplayArray("Sorted Arrays with Duplicates - Intersection Result", sortedC.Intersection(sortedD, true));
            DisplayArray("Original arrays with duplicates:", sortedC, sortedD);
            
            // unsorted arrays with duplicates
            int[] unsortedA = { 7, 3, 1, 3, 5, 9 };
            int[] unsortedB = { 11, 3, 2, 7, 5 };
            DisplayArray("Unsorted Arrays with Duplicates - Intersection Result", unsortedA.Intersection(unsortedB, false));
            DisplayArray("Original unsorted arrays with duplicates:", unsortedA, unsortedB);
            
            // Arrays with no common elements
            int[] noCommonA = { 1, 3, 5 };
            int[] noCommonB = { 2, 4, 6 };
            DisplayArray("Arrays with No Common Elements - Intersection Result", noCommonA.Intersection(noCommonB, false));
            
            // Empty and non-Empty
            int[] empty = { };
            int[] nonEmpty = { 1, 2, 3 };
            DisplayArray("Empty + Non-Empty Intersection", empty.Intersection(nonEmpty, false));
        }
        
        private static void DisplayArray<T>(string label, T[] first, T[] second = null)
        {
            if (second == null)
            {
                Console.WriteLine($"{label}: [{string.Join(", ", first)}]");
            }
            else
            {
                Console.WriteLine($"{label}");
                Console.WriteLine($"  First: [{string.Join(", ", first)}]");
                Console.WriteLine($"  Second: [{string.Join(", ", second)}]");
            }
        }
    }
    
}