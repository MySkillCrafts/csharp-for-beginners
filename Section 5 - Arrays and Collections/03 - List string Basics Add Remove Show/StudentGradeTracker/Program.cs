using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Arrays - Fixed size collections (from lesson 1)
        string[] subjects = { "Math", "Science", "English", "History" };
        
        // Lists - Dynamic collections (from lesson 1)
        List<string> studentNames = new List<string>();
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith");
        studentNames.Add("Carol Davis");
        studentNames.Add("David Wilson");
        studentNames.Add("Eva Brown");
        
        // Different types of Lists (from lesson 2)
        List<int> studentAges = new List<int>();
        studentAges.Add(18);
        studentAges.Add(19);
        studentAges.Add(17);
        studentAges.Add(20);
        studentAges.Add(18);
        
        List<double> studentGrades = new List<double>();
        studentGrades.Add(85.5);
        studentGrades.Add(92.0);
        studentGrades.Add(78.5);
        studentGrades.Add(88.0);
        studentGrades.Add(95.5);
        
        // Now let's explore List operations
        // Initial state
        int initialCount = studentNames.Count;
        
        // Adding more students
        studentNames.Add("Frank Miller");
        studentNames.Add("Grace Lee");
        
        int afterAdding = studentNames.Count;
        
        // Removing students
        bool removed = studentNames.Remove("Bob Smith");
        int afterRemoval = studentNames.Count;
        
        bool notFound = studentNames.Remove("John Doe");
        
        // Remove by index
        if (studentNames.Count > 0)
        {
            string removedStudent = studentNames[0];
            studentNames.RemoveAt(0);
            int afterIndexRemoval = studentNames.Count;
        }
        
        // Checking students
        bool containsAlice = studentNames.Contains("Alice Johnson");
        bool containsBob = studentNames.Contains("Bob Smith");
        
        // Clearing all students
        int countBeforeClear = studentNames.Count;
        studentNames.Clear();
        int countAfterClear = studentNames.Count;
    }
}