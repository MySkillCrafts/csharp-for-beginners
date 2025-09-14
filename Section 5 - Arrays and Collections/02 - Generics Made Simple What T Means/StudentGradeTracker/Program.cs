using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // =============================================================================
        // LESSON 1: Arrays vs Lists - When Each Makes Sense (from previous lesson)
        // =============================================================================
        
        // Arrays - Fixed size collections
        string[] subjects = { "Math", "Science", "English", "History" };
        
        // Lists - Dynamic collections
        List<string> studentNames = new List<string>();
        
        // Array example - fixed size
        int arraySize = subjects.Length;
        
        // List example - dynamic size
        int initialCount = studentNames.Count;
        
        // Add students to list
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith");
        studentNames.Add("Carol Davis");
        
        int afterAdding = studentNames.Count;
        
        // Add more students
        studentNames.Add("David Wilson");
        studentNames.Add("Eva Brown");
        
        int finalCount = studentNames.Count;
        
        // Comparison
        int arrayFinalSize = subjects.Length;
        int listFinalSize = studentNames.Count;
        
        // =============================================================================
        // LESSON 2: Generics Made Simple - What <T> Means
        // =============================================================================
        
        // Now let's explore different types of Lists
        // List<int> - holds only integers
        List<int> studentAges = new List<int>();
        studentAges.Add(18);
        studentAges.Add(19);
        studentAges.Add(17);
        studentAges.Add(20);
        studentAges.Add(18);
        
        // List<double> - holds only decimals
        List<double> studentGrades = new List<double>();
        studentGrades.Add(85.5);
        studentGrades.Add(92.0);
        studentGrades.Add(78.5);
        studentGrades.Add(88.0);
        studentGrades.Add(95.5);
        
        // All use the same methods
        int stringCount = studentNames.Count;
        int intCount = studentAges.Count;
        int doubleCount = studentGrades.Count;
        
        // Type safety - these would cause errors:
        // studentNames.Add(123); // ERROR! Can't add int to List<string>
        // studentAges.Add("Alice"); // ERROR! Can't add string to List<int>
    }
}