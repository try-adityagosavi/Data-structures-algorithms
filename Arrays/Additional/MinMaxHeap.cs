using System;
using System.Collections.Generic;

public enum HeapType
{
    MaxHeap,
    MinHeap
}

public class Heap<T> where T : IComparable<T>
{
    private List<T> _elements;
    private HeapType _heapType;
    
    public Heap(HeapType heapType = HeapType.MaxHeap)
    {
        _elements = new List<T>();
        _heapType = heapType;
    }
    
    public Heap(IEnumerable<T> collection, HeapType heapType = HeapType.MaxHeap)
    {
        _elements = new List<T>(collection);
        _heapType = heapType;
        BuildHeap();
    }
    
    public int Count => _elements.Count;
    
    public bool IsEmpty => Count == 0;
    
    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty");
        
        return _elements[0];
    }
    
    public void Add(T item)
    {
        _elements.Add(item);
        HeapifyUp(Count - 1);
    }
    
    public T Extract()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty");
        
        T result = _elements[0];
        
        _elements[0] = _elements[Count - 1];
        _elements.RemoveAt(Count - 1);
        
        if (!IsEmpty)
            HeapifyDown(0);
        
        return result;
    }
    
    private void BuildHeap()
    {
        for (int i = (Count / 2) - 1; i >= 0; i--)
        {
            HeapifyDown(i);
        }
    }
    
    private bool HasHigherPriority(T a, T b)
    {
        int compareResult = a.CompareTo(b);
        
        // For MaxHeap, a has higher priority if a > b (compareResult > 0)
        // For MinHeap, a has higher priority if a < b (compareResult < 0)
        return _heapType == HeapType.MaxHeap ? compareResult > 0 : compareResult < 0;
    }
    
    private void HeapifyUp(int index)
    {
        int parent = (index - 1) / 2;
        
        if (index > 0 && HasHigherPriority(_elements[index], _elements[parent]))
        {
            T temp = _elements[index];
            _elements[index] = _elements[parent];
            _elements[parent] = temp;
            
            HeapifyUp(parent);
        }
    }
    
    private void HeapifyDown(int index)
    {
        int highest = index;
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        
        if (left < Count && HasHigherPriority(_elements[left], _elements[highest]))
            highest = left;
        
        if (right < Count && HasHigherPriority(_elements[right], _elements[highest]))
            highest = right;
        
        if (highest != index)
        {
            T temp = _elements[index];
            _elements[index] = _elements[highest];
            _elements[highest] = temp;
            
            HeapifyDown(highest);
        }
    }
    
    public IEnumerable<T> GetElements()
    {
        return _elements;
    }
}

class Program
{
    static void Main()
    {
        //Creating max heap
        Console.WriteLine("Max Heap Example:");
        Heap<int> maxHeap = new Heap<int>(HeapType.MaxHeap);
        maxHeap.Add(4);
        maxHeap.Add(10);
        maxHeap.Add(8);
        maxHeap.Add(5);
        maxHeap.Add(1);
        
        Console.WriteLine("Max Heap elements:");
        foreach (var item in maxHeap.GetElements())
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
        
        Console.WriteLine("Extracting elements from Max Heap:");
        while (!maxHeap.IsEmpty)
        {
            Console.Write($"{maxHeap.Extract()} ");
        }
        Console.WriteLine("\n");
        
        //Creating min heap
        Console.WriteLine("Min Heap Example:");
        Heap<int> minHeap = new Heap<int>(HeapType.MinHeap);
        minHeap.Add(4);
        minHeap.Add(10);
        minHeap.Add(8);
        minHeap.Add(5);
        minHeap.Add(1);
        
        Console.WriteLine("Min Heap elements:");
        foreach (var item in minHeap.GetElements())
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
        
        Console.WriteLine("Extracting elements from Min Heap:");
        while (!minHeap.IsEmpty)
        {
            Console.Write($"{minHeap.Extract()} ");
        }
        Console.WriteLine();
        
        Console.WriteLine("\nCreating heaps from an existing collection:");
        int[] numbers = { 3, 1, 6, 5, 2, 4 };
        
        Heap<int> maxHeapFromCollection = new Heap<int>(numbers, HeapType.MaxHeap);
        Console.WriteLine("Max Heap from collection:");
        foreach (var item in maxHeapFromCollection.GetElements())
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
        
        Heap<int> minHeapFromCollection = new Heap<int>(numbers, HeapType.MinHeap);
        Console.WriteLine("Min Heap from collection:");
        foreach (var item in minHeapFromCollection.GetElements())
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    }
}