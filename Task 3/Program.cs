using System;
using System.Collections.Generic;

public class StudentRecord
{
    public string Name { get; set; }
    public int? MathGrade { get; set; }
    public int? PhysicsGrade { get; set; }
    public int? ProgrammingGrade { get; set; }
}

public class GradeAnalyzer
{
    private static Dictionary<string, double> _cachedSubjectAverages;
    
    public static double? CalculateAverage(StudentRecord student)
    {
        if (student == null) return null;

        int sum = 0;
        int count = 0;

        if (student.MathGrade.HasValue)
        {
            sum += student.MathGrade.Value;
            count++;
        }
        if (student.PhysicsGrade.HasValue)
        {
            sum += student.PhysicsGrade.Value;
            count++;
        }
        if (student.ProgrammingGrade.HasValue)
        {
            sum += student.ProgrammingGrade.Value;
            count++;
        }

        return count > 0 ? (double)sum / count : null;
    }
    
    public static void PrintReport(List<StudentRecord> students)
    {
        Console.WriteLine($"{"Ім'я",-15} {"Математика",12} {"Фізика",10} {"Програм.",10} {"Середній",10}");
        Console.WriteLine(new string('-', 60));

        if (students == null) return;

        foreach (var student in students)
        {
            string name = student?.Name ?? "Невідомий";
            string math = student?.MathGrade?.ToString() ?? "н/а";
            string phys = student?.PhysicsGrade?.ToString() ?? "н/а";
            string prog = student?.ProgrammingGrade?.ToString() ?? "н/а";

            double? avg = CalculateAverage(student);
            string avgStr = avg?.ToString("F1") ?? "н/а";

            Console.WriteLine($"{name,-15} {math,12} {phys,10} {prog,10} {avgStr,10}");
        }
    }

    
    public static Dictionary<string, double> GetSubjectAverages(List<StudentRecord> students)
    {
        
        _cachedSubjectAverages ??= new Dictionary<string, double>();
        _cachedSubjectAverages.Clear();

        if (students == null || students.Count == 0)
            return _cachedSubjectAverages;

        int mathSum = 0, mathCount = 0;
        int physSum = 0, physCount = 0;
        int progSum = 0, progCount = 0;

        foreach (var s in students)
        {
            if (s?.MathGrade.HasValue == true)
            {
                mathSum += s.MathGrade.GetValueOrDefault();
                mathCount++;
            }
            if (s?.PhysicsGrade.HasValue == true)
            {
                physSum += s.PhysicsGrade.GetValueOrDefault();
                physCount++;
            }
            if (s?.ProgrammingGrade.HasValue == true)
            {
                progSum += s.ProgrammingGrade.GetValueOrDefault();
                progCount++;
            }
        }

        _cachedSubjectAverages["Математика"] = mathCount > 0 ? (double)mathSum / mathCount : 0.0;
        _cachedSubjectAverages["Фізика"] = physCount > 0 ? (double)physSum / physCount : 0.0;
        _cachedSubjectAverages["Програмування"] = progCount > 0 ? (double)progSum / progCount : 0.0;

        return _cachedSubjectAverages;
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var students = new List<StudentRecord>
        {
            new StudentRecord { Name = "Олена",    MathGrade = 90,   PhysicsGrade = 85,   ProgrammingGrade = 95 },
            new StudentRecord { Name = "Андрій",   MathGrade = 75,   PhysicsGrade = null, ProgrammingGrade = 80 },
            new StudentRecord { Name = "Марія",    MathGrade = null, PhysicsGrade = 92,   ProgrammingGrade = 88 },
            new StudentRecord { Name = "Петро",    MathGrade = 60,   PhysicsGrade = 70,   ProgrammingGrade = null },
            new StudentRecord { Name = "Катерина", MathGrade = null, PhysicsGrade = null, ProgrammingGrade = 95 }
        };
        
        int? bonusCheck = null;
        bonusCheck ??= 60;

        GradeAnalyzer.PrintReport(students);

        Console.WriteLine("\nСередні бали по предметах:");
        var averages = GradeAnalyzer.GetSubjectAverages(students);
        foreach (var kvp in averages)
            Console.WriteLine($"  {kvp.Key}: {kvp.Value:F1}");
    }
}