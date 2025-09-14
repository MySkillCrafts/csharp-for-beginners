using System;
using System.Collections.Generic;
using System.Linq;

// ═══════════════════════════════════════════════════════════════════════════════════
// STUDENT GRADE TRACKER - PROGRESSIVE LESSONS
// ═══════════════════════════════════════════════════════════════════════════════════
// This application is built progressively through 9 lessons:
// 
// LESSON 1: Arrays vs Lists: When Each Makes Sense
// LESSON 2: Generics Made Simple: What `<T>` Means
// LESSON 3: `List<string>` Basics: Add, Remove, Show
// LESSON 4: Clean Up Lists: Sort and Find (No LINQ)
// LESSON 5: Dictionaries for Grades: Keys and Values
// LESSON 6: Gentle Validation: Names and Score Rules
// LESSON 7: Simple LINQ I: Filter and Sort
// LESSON 8: Simple LINQ II: Averages and Reports
// LESSON 9: **Mini-project:** Student Grade Tracker
// ═══════════════════════════════════════════════════════════════════════════════════

class Program
{
    // =============================================================================
    // LESSON 1: Arrays vs Lists: When Each Makes Sense
    // =============================================================================
    // We explore both arrays and lists, showing when each is appropriate
    // Arrays: fixed size, fast access, simple scenarios
    // Lists: dynamic size, flexible, growing collections
    
    // LESSON 1: Fixed array example - subjects don't change often
    static string[] fixedSubjects = { "Math", "Science", "English", "History" };
    
    // LESSON 1: Dynamic collections for growing data
    static List<string> studentNames = new List<string>();
    static List<string> teacherNames = new List<string>(); // Additional example for lesson 1
    
    // =============================================================================
    // LESSON 5: Dictionaries for Grades: Keys and Values
    // =============================================================================
    // Dictionary introduction - "by student name, find their grades"
    
    // LESSON 5: Dictionary for storing grades by student name
    static Dictionary<string, List<int>> studentGrades = new Dictionary<string, List<int>>();
    
    // LESSON 5: Additional dictionary example for lesson depth
    static Dictionary<string, string> studentEmails = new Dictionary<string, string>();
    
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
                    AddStudent();           // LESSON 2-3
                    break;
                case "2":
                    RemoveStudent();        // LESSON 3  
                    break;
                case "3":
                    ShowAllStudents();      // LESSON 2-3
                    break;
                case "4":
                    SortStudentsAZ();       // LESSON 4
                    break;
                case "5":
                    SortStudentsZA();       // LESSON 4
                    break;
                case "6":
                    FindStudentByName();    // LESSON 4 & 6
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
                case "13":
                    ManageTeachers();       // LESSON 1 (additional example)
                    break;
                case "14":
                    ManageEmails();         // LESSON 4 (additional example)
                    break;
                case "15":
                    ShowArrayExamples();    // LESSON 1
                    break;
                case "16":
                    DemonstrateListCapabilities(); // LESSON 2-3
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
    // LESSON 1: Arrays vs Lists: When Each Makes Sense
    // =============================================================================
    
    static void ShowArrayExamples()
    {
        ConsoleHelper.ShowInfo("=== ARRAYS: Fixed Size Collections ===");
        
        // Array basics - fixed size
        ConsoleHelper.ShowInfo("Our subjects (using array):");
        for (int i = 0; i < fixedSubjects.Length; i++)
        {
            Console.WriteLine($"  {i + 1}. {fixedSubjects[i]}");
        }
        Console.WriteLine($"Array size is FIXED at: {fixedSubjects.Length}");
        Console.WriteLine();
        
        // Array access by index
        ConsoleHelper.ShowInfo("Direct access by index:");
        Console.WriteLine($"First subject: {fixedSubjects[0]}");
        Console.WriteLine($"Last subject: {fixedSubjects[fixedSubjects.Length - 1]}");
        Console.WriteLine();
        
        // Array limitations
        ConsoleHelper.ShowWarning("Array limitations:");
        Console.WriteLine("- Cannot add new subjects easily");
        Console.WriteLine("- Size must be known at creation");
        Console.WriteLine("- Good for: fixed data, performance-critical code");
        Console.WriteLine();
        
        ConsoleHelper.ShowInfo("=== LISTS: Dynamic Collections ===");
        Console.WriteLine("- Can grow and shrink");
        Console.WriteLine("- Perfect for student management");
        Console.WriteLine("- More methods available (Add, Remove, etc.)");
    }
    
    static void ManageTeachers()
    {
        ConsoleHelper.ShowInfo("=== Teacher Management (List Example) ===");
        
        // Show current teachers
        if (teacherNames.Count == 0)
        {
            ConsoleHelper.ShowWarning("No teachers added yet.");
        }
        else
        {
            ConsoleHelper.ShowInfo($"Current teachers ({teacherNames.Count}):");
            for (int i = 0; i < teacherNames.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {teacherNames[i]}");
            }
        }
        
        string action = ConsoleHelper.GetInput("Add teacher (a) or Remove teacher (r)? ");
        
        if (action.ToLower() == "a")
        {
            string name = ConsoleHelper.GetInput("Enter teacher name: ");
            if (!string.IsNullOrWhiteSpace(name))
            {
                teacherNames.Add(name);
                ConsoleHelper.ShowSuccess($"Added teacher: {name}");
            }
        }
        else if (action.ToLower() == "r" && teacherNames.Count > 0)
        {
            string name = ConsoleHelper.GetInput("Enter teacher name to remove: ");
            if (teacherNames.Remove(name))
            {
                ConsoleHelper.ShowSuccess($"Removed teacher: {name}");
            }
            else
            {
                ConsoleHelper.ShowError("Teacher not found!");
            }
        }
    }
    
    // =============================================================================
    // LESSON 2: Generics Made Simple: What `<T>` Means + LESSON 3: List<string> Basics
    // =============================================================================
    
    static void AddStudent()
    {
        string name = ConsoleHelper.GetInput("Enter student name: ");
        
        // Basic validation (will be expanded in LESSON 6)
        if (string.IsNullOrWhiteSpace(name))
        {
            ConsoleHelper.ShowError("Name cannot be empty!");
            return;
        }
        
        // LESSON 3: Add to list - core operation
        studentNames.Add(name);
        
        // LESSON 3: Show list growth
        ConsoleHelper.ShowSuccess($"Added student: {name}");
        ConsoleHelper.ShowInfo($"Total students now: {studentNames.Count}");
        
        // LESSON 5: Initialize empty grade list (dictionary concept)
        studentGrades[name] = new List<int>();
        
        // LESSON 3: Demonstrate index access
        ConsoleHelper.ShowInfo($"This student is at position: {studentNames.Count - 1}");
        ConsoleHelper.ShowInfo($"We can access them by index: {studentNames[studentNames.Count - 1]}");
    }
    
    static void ShowAllStudents()
    {
        // LESSON 3: Count property and display list contents using beautiful tables
        ConsoleHelper.ShowStudentList(studentNames, studentGrades);
        
    }
    
    static void DemonstrateListCapabilities()
    {
        ConsoleHelper.ShowInfo("=== List<T> Capabilities Demo ===");
        
        // Create a demo list with school subjects
        List<string> demoSubjects = new List<string> { "Mathematics", "Science", "English", "History" };
        
        ConsoleHelper.ShowInfo("Original subject list:");
        for (int i = 0; i < demoSubjects.Count; i++)
        {
            Console.WriteLine($"  [{i}] {demoSubjects[i]} (Length: {demoSubjects[i].Length} chars)");
        }
        Console.WriteLine();
        
        // Show operations results
        ConsoleHelper.ShowInfo("List operations:");
        Console.WriteLine($"  First subject: {demoSubjects[0]}");
        Console.WriteLine($"  Last subject: {demoSubjects[demoSubjects.Count - 1]}");
        Console.WriteLine($"  Contains 'Mathematics': {demoSubjects.Contains("Mathematics")}");
        Console.WriteLine($"  'Science' index: {demoSubjects.IndexOf("Science")}");
        Console.WriteLine($"  Total count: {demoSubjects.Count}");
    }
    
    // =============================================================================
    // LESSON 3: List<string> Basics: Add, Remove, Show
    // =============================================================================
    
    static void RemoveStudent()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to remove!");
            return;
        }

        // Show current students for reference
        ConsoleHelper.ShowInfo("Current students:");
        for (int i = 0; i < studentNames.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {studentNames[i]}");
        }
        
        string name = ConsoleHelper.GetInput("Enter student name to remove: ");
        
        // LESSON 3: Contains check before removal
        if (!studentNames.Contains(name))
        {
            ConsoleHelper.ShowError("Student not found!");
            return;
        }
        
        // LESSON 3: Remove operation
        bool removed = studentNames.Remove(name);
        
        if (removed)
        {
            // LESSON 3: Data integrity - remove related data
            studentGrades.Remove(name);
            studentEmails.Remove(name);
            
            ConsoleHelper.ShowSuccess($"Removed student: {name}");
            ConsoleHelper.ShowInfo($"Remaining students: {studentNames.Count}");
        }
        
        // LESSON 3: Demonstrate other removal methods
        if (studentNames.Count > 0)
        {
            string clearAll = ConsoleHelper.GetInput("Clear all students? (y/n): ");
            if (clearAll.ToLower() == "y")
            {
                int count = studentNames.Count;
                studentNames.Clear();
                studentGrades.Clear();
                studentEmails.Clear();
                ConsoleHelper.ShowWarning($"Cleared all {count} students!");
            }
        }
    }
    
    // =============================================================================
    // LESSON 5: Dictionaries for Grades: Keys and Values
    // =============================================================================
    
    static void AddGrade()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet. Add students first!");
            return;
        }

        // Show available students
        ConsoleHelper.ShowInfo("Students available for grading:");
        for (int i = 0; i < studentNames.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {studentNames[i]}");
        }
        
        string name = ConsoleHelper.GetInput("Enter student name: ");
        
        // LESSON 5: Dictionary key existence check
        if (!studentGrades.ContainsKey(name))
        {
            ConsoleHelper.ShowError("Student not found in gradebook!");
            return;
        }
        
        string gradeInput = ConsoleHelper.GetInput("Enter grade (0-100): ");
        
        if (!int.TryParse(gradeInput, out int grade) || grade < 0 || grade > 100)
        {
            ConsoleHelper.ShowError("Please enter a valid grade (0-100)!");
            return;
        }
        
        // LESSON 5: Dictionary value access and modification
        studentGrades[name].Add(grade);
        
        ConsoleHelper.ShowSuccess($"Added grade {grade} for {name}");
        
        // LESSON 5: Show current grades for this student
        List<int> currentGrades = studentGrades[name];
        ConsoleHelper.ShowInfo($"Current grades for {name}: {string.Join(", ", currentGrades)}");
        ConsoleHelper.ShowInfo($"Total grades: {currentGrades.Count}");
    }
    
    static void ManageEmails()
    {
        ConsoleHelper.ShowInfo("=== Student Email Management (Dictionary Example) ===");
        
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students available!");
            return;
        }
        
        // Show students and their emails
        ConsoleHelper.ShowInfo("Student emails:");
        foreach (string student in studentNames)
        {
            if (studentEmails.ContainsKey(student))
            {
                Console.WriteLine($"  {student}: {studentEmails[student]}");
            }
            else
            {
                Console.WriteLine($"  {student}: (no email)");
            }
        }
        
        string student_name = ConsoleHelper.GetInput("Enter student name to add/update email: ");
        
        if (!studentNames.Contains(student_name))
        {
            ConsoleHelper.ShowError("Student not found!");
            return;
        }
        
        string email = ConsoleHelper.GetInput("Enter email address: ");
        
        // Dictionary assignment
        studentEmails[student_name] = email;
        ConsoleHelper.ShowSuccess($"Email updated for {student_name}");
    }
    
    // =============================================================================
    // LESSON 5: Dictionaries for Grades: Keys and Values (Advanced)
    // =============================================================================
    
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
    // LESSON 4: Clean Up Lists: Sort and Find (No LINQ)
    // =============================================================================
    
    static void SortStudentsAZ()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to sort!");
            return;
        }

        ConsoleHelper.ShowInfo("Before sorting:");
        ShowAllStudents();
        
        // LESSON 4: Basic sort
        studentNames.Sort();
        ConsoleHelper.ShowSuccess("Sorted A→Z (basic sort)");
        
        Console.WriteLine();
        ConsoleHelper.ShowInfo("After basic sort:");
        ShowAllStudents();
        
        // LESSON 4: Case-insensitive sort
        studentNames.Sort(StringComparer.OrdinalIgnoreCase);
        ConsoleHelper.ShowInfo("Applied case-insensitive sort (StringComparer.OrdinalIgnoreCase)");
        
    }
    
    static void SortStudentsZA()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to sort!");
            return;
        }

        // LESSON 4: Sort then reverse
        studentNames.Sort(StringComparer.OrdinalIgnoreCase);
        studentNames.Reverse();
        
        ConsoleHelper.ShowSuccess("Sorted Z→A (Sort + Reverse)");
        ShowAllStudents();
        
    }
    
    // =============================================================================
    // LESSON 6: Gentle Validation: Names and Score Rules + LESSON 4: Find
    // =============================================================================
    
    static void FindStudentByName()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to search!");
            return;
        }

        string searchTerm = ConsoleHelper.GetInput("Enter part of student name: ");

        // LESSON 6: Input validation
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            ConsoleHelper.ShowError("Search term cannot be empty!");
            return;
        }

        if (searchTerm.Length < 2)
        {
            ConsoleHelper.ShowWarning("Search term should be at least 2 characters for better results.");
        }
        
        // LESSON 4: Manual search using Contains
        List<string> exactMatches = new List<string>();
        List<string> partialMatches = new List<string>();
        
        for (int i = 0; i < studentNames.Count; i++)
        {
            string student = studentNames[i];
            
            // Exact match (case-insensitive)
            if (student.Equals(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                exactMatches.Add(student);
            }
            // Partial match
            else if (student.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                partialMatches.Add(student);
            }
        }
        
        // LESSON 4: Display results with different categories
        if (exactMatches.Count > 0)
        {
            ConsoleHelper.ShowSuccess("Exact matches:");
            foreach (string match in exactMatches)
            {
                Console.WriteLine($"  ✓ {match}");
            }
        }
        
        if (partialMatches.Count > 0)
        {
            ConsoleHelper.ShowInfo("Partial matches:");
            foreach (string match in partialMatches)
            {
                Console.WriteLine($"  • {match}");
            }
        }
        
        if (exactMatches.Count == 0 && partialMatches.Count == 0)
        {
            ConsoleHelper.ShowWarning($"No students found containing '{searchTerm}'");
        }
        
    }
    
    // =============================================================================
    // LESSON 7: Simple LINQ I: Filter and Sort
    // =============================================================================
    
    static void FindTopStudents()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }

        ConsoleHelper.ShowInfo("=== LINQ Introduction: Filtering Data ===");
        
        // LESSON 7: Manual approach using loops and conditions
        List<string> studentsWithGrades = new List<string>();
        for (int i = 0; i < studentNames.Count; i++)
        {
            string name = studentNames[i];
            if (studentGrades[name].Count > 0)
            {
                studentsWithGrades.Add(name);
            }
        }
        
        ConsoleHelper.ShowInfo($"Students with grades: {studentsWithGrades.Count} out of {studentNames.Count}");
        
        if (studentsWithGrades.Count == 0)
        {
            ConsoleHelper.ShowWarning("No students have grades yet!");
            return;
        }

        // LESSON 7: Find top students using loops and conditions
        List<string> topStudentNames = new List<string>();
        List<double> topStudentAverages = new List<double>();
        
        for (int i = 0; i < studentsWithGrades.Count; i++)
        {
            string name = studentsWithGrades[i];
            if (studentGrades[name].Count > 0)
            {
                double average = CalculateAverage(studentGrades[name]);
                if (average >= 90)
                {
                    topStudentNames.Add(name);
                    topStudentAverages.Add(average);
                }
            }
        }
        
        // Simple bubble sort for demonstration
        for (int i = 0; i < topStudentNames.Count - 1; i++)
        {
            for (int j = 0; j < topStudentNames.Count - 1 - i; j++)
            {
                if (topStudentAverages[j] < topStudentAverages[j + 1])
                {
                    // Swap names
                    string tempName = topStudentNames[j];
                    topStudentNames[j] = topStudentNames[j + 1];
                    topStudentNames[j + 1] = tempName;
                    
                    // Swap averages
                    double tempAverage = topStudentAverages[j];
                    topStudentAverages[j] = topStudentAverages[j + 1];
                    topStudentAverages[j + 1] = tempAverage;
                }
            }
        }
        
        ShowTopStudentsSimple(topStudentNames, topStudentAverages);
        
    }
    
    static void FindStudentsAboveAverage()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }

        // LESSON 7: Manual approach - collect all grades
        List<int> allGrades = new List<int>();
        for (int i = 0; i < studentNames.Count; i++)
        {
            string name = studentNames[i];
            List<int> studentGradeList = studentGrades[name];
            for (int j = 0; j < studentGradeList.Count; j++)
            {
                allGrades.Add(studentGradeList[j]);
            }
        }
        
        if (allGrades.Count == 0)
        {
            ConsoleHelper.ShowError("No grades entered yet!");
            return;
        }

        double classAverage = CalculateAverage(allGrades);
        
        // LESSON 7: Find students above average manually
        List<string> aboveAverageNames = new List<string>();
        List<double> aboveAverageValues = new List<double>();
        
        for (int i = 0; i < studentNames.Count; i++)
        {
            string name = studentNames[i];
            if (studentGrades[name].Count > 0)
            {
                double studentAverage = CalculateAverage(studentGrades[name]);
                if (studentAverage > classAverage)
                {
                    aboveAverageNames.Add(name);
                    aboveAverageValues.Add(studentAverage);
                }
            }
        }
        
        // Simple sort by average (descending)
        for (int i = 0; i < aboveAverageNames.Count - 1; i++)
        {
            for (int j = 0; j < aboveAverageNames.Count - 1 - i; j++)
            {
                if (aboveAverageValues[j] < aboveAverageValues[j + 1])
                {
                    // Swap names
                    string tempName = aboveAverageNames[j];
                    aboveAverageNames[j] = aboveAverageNames[j + 1];
                    aboveAverageNames[j + 1] = tempName;
                    
                    // Swap averages
                    double tempAverage = aboveAverageValues[j];
                    aboveAverageValues[j] = aboveAverageValues[j + 1];
                    aboveAverageValues[j + 1] = tempAverage;
                }
            }
        }
        
        ShowAboveAverageStudentsSimple(classAverage, aboveAverageNames, aboveAverageValues);
        
    }
    
    // =============================================================================
    // LESSON 8: Simple LINQ II: Averages and Reports
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
                // LESSON 8: Basic statistics calculation
                double average = CalculateAverage(grades);
                int minGrade = FindMinGrade(grades);
                int maxGrade = FindMaxGrade(grades);
                int totalGrades = grades.Count;
                
                ConsoleHelper.ShowStudentAverage(name, average, grades);
                
                // LESSON 8: Additional statistics
                Console.WriteLine();
                ConsoleHelper.ShowInfo("Detailed statistics:");
                Console.WriteLine($"- Average: {average:F2}%");
                Console.WriteLine($"- Highest grade: {maxGrade}%");
                Console.WriteLine($"- Lowest grade: {minGrade}%");
                Console.WriteLine($"- Grade range: {maxGrade - minGrade} points");
                Console.WriteLine($"- Total assessments: {totalGrades}");
                
                // LESSON 8: Manual counting with conditions
                int highGradeCount = 0;
                int lowGradeCount = 0;
                for (int i = 0; i < grades.Count; i++)
                {
                    if (grades[i] >= 90)
                        highGradeCount++;
                    if (grades[i] < 70)
                        lowGradeCount++;
                }
                
                Console.WriteLine($"- Grades 90+: {highGradeCount} ({(double)highGradeCount / totalGrades * 100:F1}%)");
                Console.WriteLine($"- Grades below 70: {lowGradeCount} ({(double)lowGradeCount / totalGrades * 100:F1}%)");
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

        // LESSON 8: Manual data collection
        List<int> allGrades = new List<int>();
        for (int i = 0; i < studentNames.Count; i++)
        {
            string name = studentNames[i];
            List<int> studentGradeList = studentGrades[name];
            for (int j = 0; j < studentGradeList.Count; j++)
            {
                allGrades.Add(studentGradeList[j]);
            }
        }
        
        if (allGrades.Count == 0)
        {
            ConsoleHelper.ShowError("No grades entered yet!");
            return;
        }

        // LESSON 8: Basic statistics
        double classAverage = CalculateAverage(allGrades);
        int totalGrades = allGrades.Count;
        int highestGrade = FindMaxGrade(allGrades);
        int lowestGrade = FindMinGrade(allGrades);
        
        // LESSON 8: Find top performer manually
        string topPerformer = "";
        double topAverage = 0;
        for (int i = 0; i < studentNames.Count; i++)
        {
            string name = studentNames[i];
            if (studentGrades[name].Count > 0)
            {
                double average = CalculateAverage(studentGrades[name]);
                if (average > topAverage)
                {
                    topAverage = average;
                    topPerformer = name;
                }
            }
        }
        
        // LESSON 8: Create students with grades lists manually (using separate lists)
        List<string> studentNamesWithGrades = new List<string>();
        List<double> studentAverages = new List<double>();
        List<int> studentGradeCounts = new List<int>();
        
        for (int i = 0; i < studentNames.Count; i++)
        {
            string name = studentNames[i];
            if (studentGrades[name].Count > 0)
            {
                double average = CalculateAverage(studentGrades[name]);
                int gradeCount = studentGrades[name].Count;
                
                studentNamesWithGrades.Add(name);
                studentAverages.Add(average);
                studentGradeCounts.Add(gradeCount);
            }
        }
        
        // Simple sort by average (descending) - sort all three lists together
        for (int i = 0; i < studentNamesWithGrades.Count - 1; i++)
        {
            for (int j = 0; j < studentNamesWithGrades.Count - 1 - i; j++)
            {
                if (studentAverages[j] < studentAverages[j + 1])
                {
                    // Swap names
                    string tempName = studentNamesWithGrades[j];
                    studentNamesWithGrades[j] = studentNamesWithGrades[j + 1];
                    studentNamesWithGrades[j + 1] = tempName;
                    
                    // Swap averages
                    double tempAverage = studentAverages[j];
                    studentAverages[j] = studentAverages[j + 1];
                    studentAverages[j + 1] = tempAverage;
                    
                    // Swap grade counts
                    int tempCount = studentGradeCounts[j];
                    studentGradeCounts[j] = studentGradeCounts[j + 1];
                    studentGradeCounts[j + 1] = tempCount;
                }
            }
        }
        
        // LESSON 8: Grade distribution analysis manually
        List<string> gradeRanges = new List<string>();
        List<int> gradeCounts = new List<int>();
        int[] rangeCounts = new int[11]; // 0-9, 10-19, ..., 90-99, 100
        
        for (int i = 0; i < allGrades.Count; i++)
        {
            int grade = allGrades[i];
            int rangeIndex = grade / 10;
            if (rangeIndex > 10) rangeIndex = 10; // Handle grade 100
            rangeCounts[rangeIndex]++;
        }
        
        for (int i = 10; i >= 0; i--) // Start from highest range
        {
            if (rangeCounts[i] > 0)
            {
                string range = i == 10 ? "100" : $"{i * 10}-{i * 10 + 9}";
                gradeRanges.Add(range);
                gradeCounts.Add(rangeCounts[i]);
            }
        }
        
        ShowClassReportSimple(studentNames.Count, totalGrades, classAverage, 
            highestGrade, lowestGrade, topPerformer, topAverage, 
            studentNamesWithGrades, studentAverages, studentGradeCounts);
        
        // LESSON 8: Show grade distribution
        Console.WriteLine();
        ConsoleHelper.ShowInfo("Grade Distribution:");
        for (int i = 0; i < gradeRanges.Count; i++)
        {
            string range = gradeRanges[i];
            int count = gradeCounts[i];
            double percentage = (double)count / totalGrades * 100;
            Console.WriteLine($"  {range}%: {count} grades ({percentage:F1}%)");
        }
        
    }
    
    // =============================================================================
    // Helper Methods
    // =============================================================================
    
    static bool StudentExists(string name)
    {
        // Simple loop-based search
        for (int i = 0; i < studentNames.Count; i++)
        {
            if (studentNames[i].Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
    
    // =============================================================================
    // Helper Methods (Simple implementations without LINQ)
    // =============================================================================
    
    // Calculate average of a list of integers
    static double CalculateAverage(List<int> numbers)
    {
        if (numbers.Count == 0) return 0;
        
        int sum = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            sum += numbers[i];
        }
        return (double)sum / numbers.Count;
    }
    
    // Find minimum grade in a list
    static int FindMinGrade(List<int> grades)
    {
        if (grades.Count == 0) return 0;
        
        int min = grades[0];
        for (int i = 1; i < grades.Count; i++)
        {
            if (grades[i] < min)
                min = grades[i];
        }
        return min;
    }
    
    // Find maximum grade in a list
    static int FindMaxGrade(List<int> grades)
    {
        if (grades.Count == 0) return 0;
        
        int max = grades[0];
        for (int i = 1; i < grades.Count; i++)
        {
            if (grades[i] > max)
                max = grades[i];
        }
        return max;
    }
    
    // Simple method to show top students without tuples
    static void ShowTopStudentsSimple(List<string> names, List<double> averages)
    {
        if (names.Count == 0)
        {
            ConsoleHelper.ShowWarning("No students with 90+ average yet.");
            return;
        }

        ConsoleHelper.ShowInfo("🏆 Top Students (average 90+):");
        for (int i = 0; i < names.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {names[i]} - {averages[i]:F1}%");
        }
    }
    
    // Simple method to show above average students without tuples
    static void ShowAboveAverageStudentsSimple(double classAverage, List<string> names, List<double> averages)
    {
        if (names.Count == 0)
        {
            ConsoleHelper.ShowWarning("No students above class average.");
            return;
        }

        ConsoleHelper.ShowInfo($"📈 Students Above Class Average ({classAverage:F1}%):");
        for (int i = 0; i < names.Count; i++)
        {
            double difference = averages[i] - classAverage;
            Console.WriteLine($"  • {names[i]} - {averages[i]:F1}% (+{difference:F1}%)");
        }
    }
    
    // Simple method to show class report without tuples
    static void ShowClassReportSimple(int totalStudents, int totalGrades, double classAverage,
        int highestGrade, int lowestGrade, string topPerformer, double topAverage,
        List<string> studentNames, List<double> averages, List<int> gradeCounts)
    {
        ConsoleHelper.ShowInfo("📈 Class Report");
        Console.WriteLine("".PadLeft(50, '═'));

        // Basic statistics
        Console.WriteLine($"📊 Class Statistics:");
        Console.WriteLine($"   Total students: {totalStudents}");
        Console.WriteLine($"   Total grades: {totalGrades}");
        Console.WriteLine($"   Class average: {classAverage:F2}%");
        Console.WriteLine($"   Highest grade: {highestGrade}%");
        Console.WriteLine($"   Lowest grade: {lowestGrade}%");
        Console.WriteLine();

        // Top performer
        if (!string.IsNullOrEmpty(topPerformer))
        {
            ConsoleHelper.ShowInfo($"🏆 Top Performer: {topPerformer} ({topAverage:F1}%)");
        }

        // Individual averages
        Console.WriteLine();
        ConsoleHelper.ShowInfo("📋 Individual Performance:");
        for (int i = 0; i < studentNames.Count; i++)
        {
            string performance = averages[i] >= 90 ? "🏆 Excellent" :
                                averages[i] >= 70 ? "👍 Good" :
                                "📈 Improving";
            Console.WriteLine($"   {i + 1}. {studentNames[i]}: {averages[i]:F1}% ({gradeCounts[i]} grades) {performance}");
        }
    }

    // Add some initial data to make the app interesting
    static void AddInitialData()
    {
        // LESSON 3: Add initial students
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith"); 
        studentNames.Add("Carol Davis");
        studentNames.Add("David Wilson");
        
        // LESSON 1: Add initial teachers for demonstration
        teacherNames.Add("Dr. Smith");
        teacherNames.Add("Prof. Johnson");
        
        // LESSON 5: Add initial grades
        studentGrades["Alice Johnson"] = new List<int> { 92, 88, 95, 90 };
        studentGrades["Bob Smith"] = new List<int> { 78, 85, 82 };
        studentGrades["Carol Davis"] = new List<int> { 95, 98, 97, 100, 94 };
        studentGrades["David Wilson"] = new List<int> { 88, 91, 87, 93 };
        
        // LESSON 5: Add initial emails
        studentEmails["Alice Johnson"] = "alice.j@school.edu";
        studentEmails["Bob Smith"] = "bob.s@school.edu";
    }
}

// =============================================================================
// LESSON PROGRESSION SUMMARY:
// =============================================================================
// LESSON 1: Arrays vs Lists: When Each Makes Sense
// LESSON 2: Generics Made Simple: What `<T>` Means
// LESSON 3: `List<string>` Basics: Add, Remove, Show
// LESSON 4: Clean Up Lists: Sort and Find (No LINQ)
// LESSON 5: Dictionaries for Grades: Keys and Values
// LESSON 6: Gentle Validation: Names and Score Rules
// LESSON 7: Simple LINQ I: Filter and Sort
// LESSON 8: Simple LINQ II: Averages and Reports
// LESSON 9: **Mini-project:** Student Grade Tracker (Complete Application)
// =============================================================================