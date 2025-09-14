using System;
using System.Collections.Generic;
using System.Linq;

// ═══════════════════════════════════════════════════════════════════════════════════
// LESSON 1: Arrays vs Lists - When Each Makes Sense
// ═══════════════════════════════════════════════════════════════════════════════════

class Program
{
    static void Main()
    {
        ConsoleHelper.ShowAppTitle("LESSON 1: Arrays vs Lists");
        
        // ARRAYS - Fixed Size Collections
        string[] subjects = { "Math", "Science", "English", "History" };
        
        ConsoleHelper.ShowInfo("📚 ARRAYS - Fixed Size Collections");
        ConsoleHelper.ShowArrayDemo(subjects, "School Subjects (Array)");
        Console.WriteLine();
        
        // LISTS - Dynamic Collections  
        List<string> studentNames = new List<string>();
        
        ConsoleHelper.ShowInfo("📝 LISTS - Dynamic Collections");
        ConsoleHelper.ShowInfo($"Initial count: {studentNames.Count}");
        
        // Adding students to the list
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith");
        studentNames.Add("Carol Davis");
        
        ConsoleHelper.ShowListDemo(studentNames, "Students List (After adding students)");
        Console.WriteLine();
        
        // COMPARISON
        ConsoleHelper.ShowInfo("🔄 COMPARISON");
        var comparisonData = new[]
        {
            new { Collection = "Subjects (Array)", Count = subjects.Length, Type = "FIXED", UseCase = "Don't change" },
            new { Collection = "Students (List)", Count = studentNames.Count, Type = "DYNAMIC", UseCase = "Can grow/shrink" }
        };
        
        ConsoleHelper.ShowTable(comparisonData, "Arrays vs Lists Comparison");
        
        ConsoleHelper.ShowSuccess("✅ In our Student Grade Tracker:");
        Console.WriteLine("- ARRAYS for subjects (they don't change)");
        Console.WriteLine("- LISTS for students (they can be added/removed)");
    }
}

/*
═══════════════════════════════════════════════════════════════════════════════════
LESSON 1 SUMMARY - What Students Learned:
═══════════════════════════════════════════════════════════════════════════════════

🎯 KEY CONCEPTS:
1. ARRAYS - Fixed size collections
   - Size determined at creation
   - Good for: subjects, fixed data, performance-critical code
   - Access by index: subjects[0], subjects[1]

2. LISTS - Dynamic collections  
   - Can grow and shrink
   - Good for: students, growing data, flexible collections
   - Methods: Add(), Remove(), Count

🔄 WHEN TO USE EACH:
- ARRAYS: When you know the size won't change (school subjects)
- LISTS: When data can grow/shrink (student names)

📚 PRACTICAL APPLICATION:
In Student Grade Tracker:
- subjects[] array - fixed list of school subjects
- studentNames List<string> - dynamic list of students

✅ STUDENTS NOW UNDERSTAND:
- Difference between fixed and dynamic collections
- When to use arrays vs lists
- Basic operations with both types
- How this applies to our grade tracking app
═══════════════════════════════════════════════════════════════════════════════════
*/
