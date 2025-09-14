using System;
using System.Collections.Generic;

// ═══════════════════════════════════════════════════════════════════════════════════
// LESSON 1: Arrays vs Lists - When Each Makes Sense
// ═══════════════════════════════════════════════════════════════════════════════════
// This is what students will have at the END of Lesson 1
// They will understand:
// 1. What arrays are and when to use them
// 2. What lists are and when to use them
// 3. Basic operations with both
// 4. The difference between fixed and dynamic collections
// ═══════════════════════════════════════════════════════════════════════════════════

class Program
{
    static void Main()
    {
        Console.WriteLine("=== LESSON 1: Arrays vs Lists ===");
        Console.WriteLine();
        
        // PART 1: ARRAYS - Fixed Size Collections
        Console.WriteLine("📚 PART 1: ARRAYS - Fixed Size Collections");
        Console.WriteLine("═══════════════════════════════════════════");
        
        // Array example - subjects that don't change often
        string[] subjects = { "Math", "Science", "English", "History" };
        
        Console.WriteLine("Our school subjects (using array):");
        for (int i = 0; i < subjects.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {subjects[i]}");
        }
        Console.WriteLine($"Array size is FIXED at: {subjects.Length}");
        Console.WriteLine();
        
        // Array access by index
        Console.WriteLine("Direct access by index:");
        Console.WriteLine($"First subject: {subjects[0]}");
        Console.WriteLine($"Last subject: {subjects[subjects.Length - 1]}");
        Console.WriteLine();
        
        // Array limitations
        Console.WriteLine("Array limitations:");
        Console.WriteLine("- Cannot add new subjects easily");
        Console.WriteLine("- Size must be known at creation");
        Console.WriteLine("- Good for: fixed data, performance-critical code");
        Console.WriteLine();
        
        // PART 2: LISTS - Dynamic Collections
        Console.WriteLine("📝 PART 2: LISTS - Dynamic Collections");
        Console.WriteLine("═══════════════════════════════════════════");
        
        // List example - students that can grow
        List<string> studentNames = new List<string>();
        
        Console.WriteLine("Student list starts empty:");
        Console.WriteLine($"Number of students: {studentNames.Count}");
        Console.WriteLine();
        
        // Adding students to the list
        Console.WriteLine("Adding students to the list:");
        studentNames.Add("Alice Johnson");
        Console.WriteLine($"Added Alice. Total students: {studentNames.Count}");
        
        studentNames.Add("Bob Smith");
        Console.WriteLine($"Added Bob. Total students: {studentNames.Count}");
        
        studentNames.Add("Carol Davis");
        Console.WriteLine($"Added Carol. Total students: {studentNames.Count}");
        Console.WriteLine();
        
        // Displaying the list
        Console.WriteLine("Current students in the list:");
        for (int i = 0; i < studentNames.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {studentNames[i]}");
        }
        Console.WriteLine();
        
        // List advantages
        Console.WriteLine("List advantages:");
        Console.WriteLine("- Can grow and shrink");
        Console.WriteLine("- Perfect for student management");
        Console.WriteLine("- More methods available (Add, Remove, etc.)");
        Console.WriteLine();
        
        // PART 3: COMPARISON
        Console.WriteLine("🔄 PART 3: COMPARISON");
        Console.WriteLine("═══════════════════════════════════════════");
        
        Console.WriteLine("ARRAYS vs LISTS:");
        Console.WriteLine("┌─────────────────┬─────────────────┐");
        Console.WriteLine("│ Arrays          │ Lists           │");
        Console.WriteLine("├─────────────────┼─────────────────┤");
        Console.WriteLine("│ Fixed size      │ Dynamic size    │");
        Console.WriteLine("│ Fast access     │ Flexible        │");
        Console.WriteLine("│ Simple          │ More methods    │");
        Console.WriteLine("│ Good for:       │ Good for:       │");
        Console.WriteLine("│ - Fixed data    │ - Growing data  │");
        Console.WriteLine("│ - Performance   │ - Student lists │");
        Console.WriteLine("└─────────────────┴─────────────────┘");
        Console.WriteLine();
        
        // PART 4: PRACTICAL EXAMPLE
        Console.WriteLine("🎯 PART 4: PRACTICAL EXAMPLE");
        Console.WriteLine("═══════════════════════════════════════════");
        
        Console.WriteLine("In our Student Grade Tracker:");
        Console.WriteLine("- We use ARRAYS for subjects (they don't change)");
        Console.WriteLine("- We use LISTS for students (they can be added/removed)");
        Console.WriteLine();
        
        Console.WriteLine("Subjects array: " + string.Join(", ", subjects));
        Console.WriteLine("Students list: " + string.Join(", ", studentNames));
        Console.WriteLine();
        
        Console.WriteLine("✅ LESSON 1 COMPLETE!");
        Console.WriteLine("Students now understand:");
        Console.WriteLine("1. Arrays are fixed-size collections");
        Console.WriteLine("2. Lists are dynamic collections");
        Console.WriteLine("3. When to use each type");
        Console.WriteLine("4. Basic operations with both");
    }
}
