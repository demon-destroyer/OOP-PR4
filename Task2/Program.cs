using System;

public class SimpleQueue<T>
{
    private T[] _items;
    private int _head;
    private int _tail;
    private int _count;

    public SimpleQueue(int capacity = 8)
    {
        if (capacity <= 0)
            throw new ArgumentException("Ємність черги має бути більшою за 0.");

        _items = new T[capacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }

    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public void Enqueue(T item)
    {
        if (_count == _items.Length)
            Resize();

        _items[_tail] = item;
        
        _tail = (_tail + 1) % _items.Length;
        _count++;
    }

    public T Dequeue()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Неможливо виконати Dequeue: черга порожня.");

        T item = _items[_head];
        
        _items[_head] = default(T);
        
        _head = (_head + 1) % _items.Length;
        _count--;
        return item;
    }

    public T Peek()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Неможливо виконати Peek: черга порожня.");

        return _items[_head];
    }

    private void Resize()
    {
        T[] newArray = new T[_items.Length * 2];
        for (int i = 0; i < _count; i++)
        {
            newArray[i] = _items[(_head + i) % _items.Length];
        }
        _items = newArray;
        _head = 0;
        _tail = _count;
    }

    public void PrintAll()
    {
        if (IsEmpty)
        {
            Console.WriteLine("Черга порожня");
            return;
        }

        var result = new T[_count];
        for (int i = 0; i < _count; i++)
        {
            result[i] = _items[(_head + i) % _items.Length];
        }
        Console.WriteLine(string.Join(", ", result));
    }
}


public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }

    public TaskItem(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public override string ToString() => $"[#{Id}: {Title}]";
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        
        Console.WriteLine("1. Черга int ");
        var intQueue = new SimpleQueue<int>(capacity: 2);
        intQueue.Enqueue(10);
        intQueue.Enqueue(20);
        intQueue.Enqueue(30); 

        Console.WriteLine($"Count: {intQueue.Count}");         
        Console.WriteLine($"Peek: {intQueue.Peek()}");         
        Console.WriteLine($"Dequeue: {intQueue.Dequeue()}");   
        Console.WriteLine($"Dequeue: {intQueue.Dequeue()}");   
        Console.WriteLine($"Count: {intQueue.Count}");         

        
        Console.WriteLine("\n 2. Черга string ");
        var strQueue = new SimpleQueue<string>();
        strQueue.Enqueue("Перший");
        strQueue.Enqueue("Другий");
        strQueue.Enqueue("Третій");
        strQueue.PrintAll(); 

        
        Console.WriteLine("\n 3. Черга TaskItem (власний клас) ");
        var taskQueue = new SimpleQueue<TaskItem>();
        taskQueue.Enqueue(new TaskItem(1, "Написати код"));
        taskQueue.Enqueue(new TaskItem(2, "Протестувати чергу"));
        taskQueue.Enqueue(new TaskItem(3, "Зробити git push"));
        taskQueue.PrintAll();
        Console.WriteLine($"Виконується завдання: {taskQueue.Dequeue()}");
        taskQueue.PrintAll();

        
        Console.WriteLine("\n 4. Перевірка винятку на порожній черзі ");
        var emptyQueue = new SimpleQueue<double>();
        try
        {
            emptyQueue.Dequeue();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Перехоплено виняток: {ex.Message}");
        }
    }
}