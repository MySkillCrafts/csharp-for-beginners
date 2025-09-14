using System;
using System.Collections.Generic;

class Program
{
    static List<string> students = new List<string>();

    static void Main()
    {
        // Initial state
        int initialCount = students.Count;
        
        // Adding students
        students.Add("Alice Johnson");
        students.Add("Bob Smith");
        students.Add("Carol Davis");
        
        int afterAdding3 = students.Count;
        
        students.Add("David Wilson");
        students.Add("Eva Brown");
        
        int afterAdding5 = students.Count;
        
        // Removing students
        bool removed = students.Remove("Bob Smith");
        int afterRemoval = students.Count;
        
        bool notFound = students.Remove("John Doe");
        
        // Remove by index
        if (students.Count > 0)
        {
            string removedStudent = students[0];
            students.RemoveAt(0);
            int afterIndexRemoval = students.Count;
        }
        
        // Checking students
        bool containsAlice = students.Contains("Alice Johnson");
        bool containsBob = students.Contains("Bob Smith");
        
        // Clearing all students
        int countBeforeClear = students.Count;
        students.Clear();
        int countAfterClear = students.Count;
    }
}