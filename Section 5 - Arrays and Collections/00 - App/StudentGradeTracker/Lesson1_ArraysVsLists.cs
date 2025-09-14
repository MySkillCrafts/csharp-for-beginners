using System;
using System.Collections.Generic;

// ═══════════════════════════════════════════════════════════════════════════════════
// LESSON 1: Arrays vs Lists - When Each Makes Sense
// ═══════════════════════════════════════════════════════════════════════════════════

class Program
{
    static void Main()
    {
        // ARRAYS - Fixed Size Collections
        string[] subjects = { "Math", "Science", "English", "History" };
        
        // Array access by index
        string firstSubject = subjects[0];
        string lastSubject = subjects[subjects.Length - 1];
        
        // LISTS - Dynamic Collections  
        List<string> studentNames = new List<string>();
        
        // Adding students to the list
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith");
        studentNames.Add("Carol Davis");
        
        // Displaying the list
        for (int i = 0; i < studentNames.Count; i++)
        {
            string student = studentNames[i];
        }
        
        // PRACTICAL EXAMPLE
        // In our Student Grade Tracker:
        // - We use ARRAYS for subjects (they don't change)
        // - We use LISTS for students (they can be added/removed)
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
