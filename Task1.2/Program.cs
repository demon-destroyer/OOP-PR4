using System;
using System.Collections.Generic;

public static class ConstrainedUtils
{
    
    public static T CreateAndDisplay<T>() where T : new()
    {
        T instance = new T();
        Console.WriteLine(instance.ToString());
        return instance;
    }

    
    public static void CompareAndSort<T>(List<T> items) where T : IComparable<T>
    {
        if (items == null) return;

        
        items.Sort((a, b) => a.CompareTo(b));
        Console.WriteLine(string.Join(", ", items));
    }

    
    public static T CreateAndGetMax<T>(T other) where T : class, IComparable<T>, new()
    {
        T created = new T();
        return created.CompareTo(other) >= 0 ? created : other;
    }
}


public class Student : IComparable<Student>
{
    public string Name { get; set; } = "Невідомий";
    public int Grade { get; set; } = 60;

    public int CompareTo(Student other) =>
        other == null ? 1 : Grade.CompareTo(other.Grade);

    public override string ToString() => $"Student: {Name}, Grade: {Grade}";
}


public class InvalidModel
{
    public InvalidModel(int id) { }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        
        var student = ConstrainedUtils.CreateAndDisplay<Student>();
        Console.WriteLine(student.GetType().Name); 

        
        var numbers = new List<int> { 5, 2, 8, 1, 9 };
        ConstrainedUtils.CompareAndSort(numbers); 

        var names = new List<string> { "Марія", "Андрій", "Олена" };
        ConstrainedUtils.CompareAndSort(names); 

        
        var topStudent = new Student { Name = "Богдан", Grade = 95 };
        var best = ConstrainedUtils.CreateAndGetMax(topStudent);
        Console.WriteLine($"Кращий студент: {best}");

        // Демонстрація перевірки обмежень компілятором
        // ConstrainedUtils.CreateAndDisplay<InvalidModel>();
        //ConstrainedUtils.CompareAndSort(new List<InvalidModel>());
        //ConstrainedUtils.CreateAndGetMax(10);
    }
}