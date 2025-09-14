using System;
using System.Collections.Generic;
using System.Linq;

// ═══════════════════════════════════════════════════════════════════════════════════
// STUDENT GRADE TRACKER - PROGRESSIVE BUILD FOR SECTION 5
// ═══════════════════════════════════════════════════════════════════════════════════
// This application is built progressively through 8 lessons:
// 
// LESSON 1: Arrays vs Lists - Basic concepts and simple array example
// LESSON 2: Generics Made Simple - Understanding List<T> and Dictionary<K,V>  
// LESSON 3: List<T> Essentials - Add, Remove, Show operations
// LESSON 4: Tidy Lists - Sort and basic find operations (no LINQ)
// LESSON 5: Dictionaries for Grades - Key-value pairs for student grades
// LESSON 6: Gentle Validation - Input validation and error prevention
// LESSON 7: LINQ Lite I - Simple filtering and sorting with Where/OrderBy
// LESSON 8: LINQ Lite II - Basic reports with Average and simple aggregations
// ═══════════════════════════════════════════════════════════════════════════════════

class Program
{
    // =============================================================================
    // LESSON 1: Arrays vs Lists - When Each Makes Sense
    // =============================================================================
    // We start by showing a simple array example, then explain why List<T> is better
    // for our growing collection of students
    
    // Simple fixed array example (LESSON 1)
    static string[] fixedSubjects = { "Math", "Science", "English", "History" };
    
    // =============================================================================
    // LESSON 2: Generics Made Simple - What <T> Means  
    // =============================================================================
    // Here we introduce List<string> for student names and Dictionary<string, List<int>>
    // for storing grades. We explain the <T> syntax in simple terms.
    
    // List<string> - "a list that holds names" (LESSON 2)
    static List<string> studentNames = new List<string>();
    
    // Dictionary<string, List<int>> - "by student name, store their grades" (LESSON 2)  
    static Dictionary<string, List<int>> studentGrades = new Dictionary<string, List<int>>();
    
    static void Main()
    {
        ConsoleHelper.ShowAppTitle("Student Grade Tracker - Section 5");
        
        // Add some demo data to make the app interesting from the start
        AddInitialData();
        
        // Main menu loop
        bool running = true;
        while (running)
        {
            ConsoleHelper.ShowMainMenu();
            string choice = Console.ReadLine() ?? "";
            Console.WriteLine();
            
            switch (choice)
            {
                case "1":
                    AddStudent();           // LESSON 3
                    break;
                case "2":
                    RemoveStudent();        // LESSON 3  
                    break;
                case "3":
                    ShowAllStudents();      // LESSON 3
                    break;
                case "4":
                    SortStudentsAZ();       // LESSON 4
                    break;
                case "5":
                    SortStudentsZA();       // LESSON 4
                    break;
                case "6":
                    FindStudentByName();    // LESSON 4
                    break;
                case "7":
                    AddGrade();             // LESSON 5
                    break;
                case "8":
                    ShowStudentGrades();    // LESSON 5
                    break;
                case "9":
                    ShowStudentAverage();   // LESSON 8
                    break;
                case "10":
                    ShowClassReport();      // LESSON 8
                    break;
                case "11":
                    FindTopStudents();      // LESSON 7
                    break;
                case "12":
                    FindStudentsAboveAverage(); // LESSON 7
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    ConsoleHelper.ShowError("Invalid choice. Please try again.");
                    break;
            }
            
            if (running)
            {
                ConsoleHelper.WaitForKeyPress();
            }
        }
        
        ConsoleHelper.ShowSuccess("Thanks for using Student Grade Tracker!");
    }
    
    // =============================================================================
    // LESSON 1: Arrays vs Lists demonstration
    // =============================================================================
    static void ShowArrayExample()
    {
        // This shows the limitation of arrays - fixed size (LESSON 1)
        ConsoleHelper.ShowInfo("Our subjects (using array):");
        for (int i = 0; i < fixedSubjects.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {fixedSubjects[i]}");
        }
        Console.WriteLine($"Array size is fixed at: {fixedSubjects.Length}");
    }
    
    // =============================================================================
    // LESSON 3: List<T> Essentials - Add, Remove, Show
    // =============================================================================
    
    static void AddStudent()
    {
        string name = ConsoleHelper.GetInput("Enter student name: ");
        
        // LESSON 6: Gentle validation - prevent empty names
        if (string.IsNullOrWhiteSpace(name))
        {
            ConsoleHelper.ShowError("Name cannot be empty!");
            return;
        }
        
        // LESSON 6: Gentle validation - prevent duplicate names  
        if (StudentExists(name))
        {
            ConsoleHelper.ShowError("Student already exists!");
            return;
        }
        
        // LESSON 3: Add to list
        studentNames.Add(name);
        // LESSON 5: Initialize empty grade list for this student
        studentGrades[name] = new List<int>();
        
        ConsoleHelper.ShowSuccess($"Added student: {name}");
    }
    
    static void RemoveStudent()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to remove!");
            return;
        }
        
        string name = ConsoleHelper.GetInput("Enter student name to remove: ");
        
        // LESSON 3: Remove from list (using Remove method)
        bool removed = studentNames.Remove(name);
        
        if (removed)
        {
            // LESSON 5: Also remove their grades
            studentGrades.Remove(name);
            ConsoleHelper.ShowSuccess($"Removed student: {name}");
        }
        else
        {
            ConsoleHelper.ShowError("Student not found!");
        }
    }
    
    static void ShowAllStudents()
    {
        // LESSON 3: Check Count property and display list contents
        ConsoleHelper.ShowStudentList(studentNames, studentGrades);
    }
    
    // =============================================================================
    // LESSON 4: Tidy Lists - Sort and Basic Find (No LINQ Yet)
    // =============================================================================
    
    static void SortStudentsAZ()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to sort!");
            return;
        }
        
        // LESSON 4: Simple Sort() method - case-insensitive
        studentNames.Sort(StringComparer.OrdinalIgnoreCase);
        ConsoleHelper.ShowSuccess("Students sorted A→Z");
        ShowAllStudents();
    }
    
    static void SortStudentsZA()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to sort!");
            return;
        }
        
        // LESSON 4: Sort then reverse for Z→A
        studentNames.Sort(StringComparer.OrdinalIgnoreCase);
        studentNames.Reverse();
        ConsoleHelper.ShowSuccess("Students sorted Z→A");
        ShowAllStudents();
    }
    
    static void FindStudentByName()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to search!");
            return;
        }
        
        string searchTerm = ConsoleHelper.GetInput("Enter part of student name: ");
        
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            ConsoleHelper.ShowError("Search term cannot be empty!");
            return;
        }
        
        // LESSON 4: Simple search using Contains (no LINQ yet)
        List<string> matches = new List<string>();
        
        for (int i = 0; i < studentNames.Count; i++)
        {
            if (studentNames[i].Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                matches.Add(studentNames[i]);
            }
        }
        
        ConsoleHelper.ShowSearchResults(searchTerm, matches);
    }
    
    // =============================================================================
    // LESSON 5: Dictionaries for Grades - Keys and Values
    // =============================================================================
    
    static void AddGrade()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet. Add students first!");
            return;
        }
        
        string name = ConsoleHelper.GetInput("Enter student name: ");
        
        // LESSON 5: Check if key exists in dictionary using ContainsKey
        if (!studentGrades.ContainsKey(name))
        {
            ConsoleHelper.ShowError("Student not found!");
            return;
        }
        
        string gradeInput = ConsoleHelper.GetInput("Enter grade (0-100): ");
        
        // LESSON 6: Gentle validation using TryParse (from Section 4)
        if (!int.TryParse(gradeInput, out int grade))
        {
            ConsoleHelper.ShowError("Please enter a valid number!");
            return;
        }
        
        // LESSON 6: Gentle validation - grade range
        if (grade < 0 || grade > 100)
        {
            ConsoleHelper.ShowError("Grade must be between 0 and 100!");
            return;
        }
        
        // LESSON 5: Add to the list of grades for this student
        studentGrades[name].Add(grade);
        ConsoleHelper.ShowSuccess($"Added grade {grade} for {name}");
    }
    
    static void ShowStudentGrades()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }
        
        string name = ConsoleHelper.GetInput("Enter student name: ");
        
        // LESSON 5: Safe dictionary access using TryGetValue
        if (studentGrades.TryGetValue(name, out List<int>? grades))
        {
            ConsoleHelper.ShowStudentGrades(name, grades);
        }
        else
        {
            ConsoleHelper.ShowError("Student not found!");
        }
    }
    
    // =============================================================================
    // LESSON 7: LINQ Lite I - Filter and Sort with One-Liners
    // =============================================================================
    
    static void FindTopStudents()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }
        
        // LESSON 7: Simple LINQ - Where to filter, OrderByDescending to sort
        var topStudents = studentNames
            .Where(name => studentGrades[name].Count > 0 && studentGrades[name].Average() >= 90)
            .OrderByDescending(name => studentGrades[name].Average())
            .Select(name => (name, studentGrades[name].Average()))
            .ToList();
        
        ConsoleHelper.ShowTopStudents(topStudents);
    }
    
    static void FindStudentsAboveAverage()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }
        
        // LESSON 8: Calculate class average first
        var allGrades = studentGrades.Values.SelectMany(grades => grades).ToList();
        
        if (allGrades.Count == 0)
        {
            ConsoleHelper.ShowError("No grades entered yet!");
            return;
        }
        
        double classAverage = allGrades.Average();
        
        // LESSON 7: LINQ Where to filter students above average
        var aboveAverageStudents = studentNames
            .Where(name => studentGrades[name].Count > 0 && studentGrades[name].Average() > classAverage)
            .OrderByDescending(name => studentGrades[name].Average())
            .Select(name => (name, studentGrades[name].Average()))
            .ToList();
        
        ConsoleHelper.ShowAboveAverageStudents(classAverage, aboveAverageStudents);
    }
    
    // =============================================================================
    // LESSON 8: LINQ Lite II - Tiny Reports with Averages
    // =============================================================================
    
    static void ShowStudentAverage()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }
        
        string name = ConsoleHelper.GetInput("Enter student name: ");
        
        if (studentGrades.TryGetValue(name, out List<int>? grades))
        {
            if (grades.Count == 0)
            {
                ConsoleHelper.ShowError($"{name} has no grades to average.");
            }
            else
            {
                // LESSON 8: Simple Average() method
                double average = grades.Average();
                ConsoleHelper.ShowStudentAverage(name, average, grades);
            }
        }
        else
        {
            ConsoleHelper.ShowError("Student not found!");
        }
    }
    
    static void ShowClassReport()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }
        
        // LESSON 8: Calculate various statistics using LINQ
        var allGrades = studentGrades.Values.SelectMany(grades => grades).ToList();
        
        if (allGrades.Count == 0)
        {
            ConsoleHelper.ShowError("No grades entered yet!");
            return;
        }
        
        // Basic statistics
        double classAverage = allGrades.Average();
        int totalGrades = allGrades.Count;
        int highestGrade = allGrades.Max();
        int lowestGrade = allGrades.Min();
        
        // LESSON 8: Top performer using OrderByDescending and FirstOrDefault
        var topPerformer = studentNames
            .Where(name => studentGrades[name].Count > 0)
            .OrderByDescending(name => studentGrades[name].Average())
            .FirstOrDefault();
        
        double topAverage = 0;
        if (topPerformer != null)
        {
            topAverage = studentGrades[topPerformer].Average();
        }
        
        // All students with averages
        var studentsWithGrades = studentNames
            .Where(name => studentGrades[name].Count > 0)
            .OrderByDescending(name => studentGrades[name].Average())
            .Select(name => (name, studentGrades[name].Average(), studentGrades[name].Count))
            .ToList();
        
        ConsoleHelper.ShowClassReport(studentNames.Count, totalGrades, classAverage, 
            highestGrade, lowestGrade, topPerformer ?? "", topAverage, studentsWithGrades);
    }
    
    // =============================================================================
    // Helper Methods
    // =============================================================================
    
    // LESSON 6: Helper method for validation
    static bool StudentExists(string name)
    {
        // Simple loop-based search (before LINQ)
        for (int i = 0; i < studentNames.Count; i++)
        {
            if (studentNames[i].Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
    
    // Add some initial data to make the app interesting
    static void AddInitialData()
    {
        // LESSON 3: Add initial students
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith"); 
        studentNames.Add("Carol Davis");
        
        // LESSON 5: Add initial grades
        studentGrades["Alice Johnson"] = new List<int> { 92, 88, 95, 90 };
        studentGrades["Bob Smith"] = new List<int> { 78, 85, 82 };
        studentGrades["Carol Davis"] = new List<int> { 95, 98, 97, 100, 94 };
    }
}

// =============================================================================
// LESSON PROGRESSION SUMMARY:
// =============================================================================
// LESSON 1: Introduced arrays vs lists concept with fixedSubjects array
// LESSON 2: Introduced List<string> and Dictionary<string, List<int>> generics
// LESSON 3: Implemented Add, Remove, Show operations for students
// LESSON 4: Added Sort and basic Find functionality (no LINQ)
// LESSON 5: Implemented Dictionary operations for storing/retrieving grades
// LESSON 6: Added gentle validation for names, grades, and input safety
// LESSON 7: Introduced simple LINQ (Where, OrderBy) for filtering and sorting
// LESSON 8: Added reporting with Average, Min, Max and basic aggregations
// =============================================================================