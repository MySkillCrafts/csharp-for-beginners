using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // List<string> - holds only strings
        List<string> studentNames = new List<string>();
        studentNames.Add("Alice");
        studentNames.Add("Bob");
        studentNames.Add("Carol");
        
        // List<int> - holds only integers
        List<int> studentAges = new List<int>();
        studentAges.Add(18);
        studentAges.Add(19);
        studentAges.Add(17);
        
        // List<double> - holds only decimals
        List<double> studentGrades = new List<double>();
        studentGrades.Add(85.5);
        studentGrades.Add(92.0);
        studentGrades.Add(78.5);
        
        // All use the same methods
        int stringCount = studentNames.Count;
        int intCount = studentAges.Count;
        int doubleCount = studentGrades.Count;
        
        // Type safety - these would cause errors:
        // studentNames.Add(123); // ERROR! Can't add int to List<string>
        // studentAges.Add("Alice"); // ERROR! Can't add string to List<int>
    }
}