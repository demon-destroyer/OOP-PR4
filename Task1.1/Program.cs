using System;
using System.Collections.Generic;

public static class GenericUtils
{
    
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    
    public static T FindMin<T>(T[] array) where T : IComparable<T>
    {
        if (array == null || array.Length == 0)
            throw new ArgumentException("Масив не може бути порожнім або null.");

        T min = array[0];
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i].CompareTo(min) < 0)
                min = array[i];
        }
        return min;
    }

    
    public static T[] Filter<T>(T[] array, Func<T, bool> predicate)
    {
        if (array == null || predicate == null)
            return Array.Empty<T>();

        var result = new List<T>();
        foreach (T item in array)
        {
            if (predicate(item))
                result.Add(item);
        }
        return result.ToArray(); 
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        
        int x = 10, y = 20;
        GenericUtils.Swap(ref x, ref y);
        Console.WriteLine($"Swap: x={x}, y={y}"); 

        string s1 = "Привіт", s2 = "Світ";
        GenericUtils.Swap(ref s1, ref s2);
        Console.WriteLine($"Swap: s1={s1}, s2={s2}"); 

        
        int[] numbers = { 5, 3, 8, 1, 9, 2 };
        Console.WriteLine($"Min: {GenericUtils.FindMin(numbers)}"); 

        string[] words = { "банан", "апельсин", "яблуко" };
        Console.WriteLine($"Min: {GenericUtils.FindMin(words)}");   

        
        try
        {
            GenericUtils.FindMin(Array.Empty<int>());
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Перехоплено очікуваний виняток: {ex.Message}");
        }

        
        int[] evens = GenericUtils.Filter(numbers, n => n % 2 == 0);
        Console.WriteLine($"Парні: {string.Join(", ", evens)}"); 

        
        int[] negatives = GenericUtils.Filter(numbers, n => n < 0);
        Console.WriteLine($"Від'ємні (довжина масиву): {negatives.Length}"); 
    }
}