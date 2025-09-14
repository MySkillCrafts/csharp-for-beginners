using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // =============================================================================
        // LESSON 1: Arrays vs Lists - When Each Makes Sense
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
    }
}