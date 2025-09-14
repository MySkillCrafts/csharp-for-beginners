using System;
using System.Collections.Generic;
using System.Linq;

// ═══════════════════════════════════════════════════════════════════════════════════
// STUDENT GRADE TRACKER - REBALANCED FOR 9 LESSONS (5-7 MIN EACH)
// ═══════════════════════════════════════════════════════════════════════════════════
// This application is built progressively through 9 lessons:
// 
// LESSON 1: Arrays vs Lists - Fixed vs Dynamic Collections + Basic Array Operations
// LESSON 2: List<T> Fundamentals - Add, Count, Index Access + Simple Display
// LESSON 3: List<T> Management - Remove, Contains, Clear + Data Integrity  
// LESSON 4: Dictionary Basics - Key-Value Concept + Adding/Accessing Grades
// LESSON 5: Dictionary Advanced - TryGetValue, ContainsKey + Safe Operations
// LESSON 6: Sorting & Organizing - Sort, Reverse + StringComparer Concepts
// LESSON 7: Search & Validation - Find by Name + Input Validation Patterns
// LESSON 8: LINQ Introduction - Where, OrderBy + Filtering Concepts
// LESSON 9: LINQ Reports - Average, Min, Max + Data Analysis
// ═══════════════════════════════════════════════════════════════════════════════════

class Program
{
    // =============================================================================
    // LESSON 1: Arrays vs Lists - Fixed vs Dynamic Collections
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
    // LESSON 4: Dictionary Basics - Key-Value Relationships
    // =============================================================================
    // Dictionary introduction - "by student name, find their grades"
    
    // LESSON 4: Dictionary for storing grades by student name
    static Dictionary<string, List<int>> studentGrades = new Dictionary<string, List<int>>();
    
    // LESSON 4: Additional dictionary example for lesson depth
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
                    AddStudent();           // LESSON 2
                    break;
                case "2":
                    RemoveStudent();        // LESSON 3  
                    break;
                case "3":
                    ShowAllStudents();      // LESSON 2
                    break;
                case "4":
                    SortStudentsAZ();       // LESSON 6
                    break;
                case "5":
                    SortStudentsZA();       // LESSON 6
                    break;
                case "6":
                    FindStudentByName();    // LESSON 7
                    break;
                case "7":
                    AddGrade();             // LESSON 4
                    break;
                case "8":
                    ShowStudentGrades();    // LESSON 5
                    break;
                case "9":
                    ShowStudentAverage();   // LESSON 9
                    break;
                case "10":
                    ShowClassReport();      // LESSON 9
                    break;
                case "11":
                    FindTopStudents();      // LESSON 8
                    break;
                case "12":
                    FindStudentsAboveAverage(); // LESSON 8
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
    // LESSON 1: Arrays vs Lists - When Each Makes Sense
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
    // LESSON 2: List<T> Fundamentals - Add, Count, Index Access
    // =============================================================================
    
    static void AddStudent()
    {
        string name = ConsoleHelper.GetInput("Enter student name: ");
        
        // Basic validation (will be expanded in LESSON 7)
        if (string.IsNullOrWhiteSpace(name))
        {
            ConsoleHelper.ShowError("Name cannot be empty!");
            return;
        }
        
        // LESSON 2: Add to list - core operation
        studentNames.Add(name);
        
        // LESSON 2: Show list growth
        ConsoleHelper.ShowSuccess($"Added student: {name}");
        ConsoleHelper.ShowInfo($"Total students now: {studentNames.Count}");
        
        // LESSON 4: Initialize empty grade list (dictionary concept)
        studentGrades[name] = new List<int>();
        
        // LESSON 2: Demonstrate index access
        ConsoleHelper.ShowInfo($"This student is at position: {studentNames.Count - 1}");
        ConsoleHelper.ShowInfo($"We can access them by index: {studentNames[studentNames.Count - 1]}");
    }
    
    static void ShowAllStudents()
    {
        // LESSON 2: Count property and display list contents using beautiful tables
        ConsoleHelper.ShowStudentList(studentNames, studentGrades);
        
        // LESSON 2: Show different access methods
        Console.WriteLine();
        ConsoleHelper.ShowInfo("List access methods demonstrated:");
        Console.WriteLine("- Index access: studentNames[0]");
        Console.WriteLine("- Count property: studentNames.Count");
        Console.WriteLine("- foreach loop: simple iteration");
        Console.WriteLine("- for loop: with index control");
    }
    
    static void DemonstrateListCapabilities()
    {
        ConsoleHelper.ShowInfo("=== List<T> Capabilities Demo ===");
        
        // Create a demo list
        List<string> demoList = new List<string> { "Apple", "Banana", "Cherry", "Date" };
        
        // Show list in table format
        var listData = demoList.Select((item, index) => new
        {
            Index = index,
            Item = item,
            Length = item.Length,
            FirstChar = item[0]
        });
        
        ConsoleHelper.ShowTable(listData, "Original List Contents");
        
        // Show operations results
        var operations = new[]
        {
            new { Operation = "First item", Result = demoList[0] },
            new { Operation = "Last item", Result = demoList[demoList.Count - 1] },
            new { Operation = "Contains 'Apple'", Result = demoList.Contains("Apple").ToString() },
            new { Operation = "'Banana' index", Result = demoList.IndexOf("Banana").ToString() },
            new { Operation = "Total count", Result = demoList.Count.ToString() }
        };
        
        ConsoleHelper.ShowTable(operations, "List Operations Results");
    }
    
    // =============================================================================
    // LESSON 3: List<T> Management - Remove, Contains, Clear + Data Integrity
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
            ConsoleHelper.ShowInfo("Other removal options:");
            Console.WriteLine("- RemoveAt(index) - remove by position");
            Console.WriteLine("- RemoveAll(condition) - remove multiple items");
            Console.WriteLine("- Clear() - remove everything");
            
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
    // LESSON 4: Dictionary Basics - Key-Value Concept + Adding/Accessing Grades
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
        
        // LESSON 4: Dictionary key existence check
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
        
        // LESSON 4: Dictionary value access and modification
        studentGrades[name].Add(grade);
        
        ConsoleHelper.ShowSuccess($"Added grade {grade} for {name}");
        
        // LESSON 4: Show current grades for this student
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
    // LESSON 5: Dictionary Advanced - TryGetValue, ContainsKey + Safe Operations
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
            
            // LESSON 5: Additional dictionary operations
            ConsoleHelper.ShowInfo("Dictionary operations demonstrated:");
            Console.WriteLine($"- TryGetValue: Safe access without exceptions");
            Console.WriteLine($"- Key exists: {studentGrades.ContainsKey(name)}");
            Console.WriteLine($"- Total students in gradebook: {studentGrades.Count}");
            Console.WriteLine($"- All keys: {string.Join(", ", studentGrades.Keys)}");
        }
        else
        {
            ConsoleHelper.ShowError("Student not found!");
            
            // LESSON 5: Show the difference between ContainsKey and direct access
            ConsoleHelper.ShowInfo("Dictionary safety:");
            Console.WriteLine("- TryGetValue returns false if key not found");
            Console.WriteLine("- ContainsKey checks existence first");
            Console.WriteLine("- Direct access [key] would throw exception if key missing");
        }
    }
    
    // =============================================================================
    // LESSON 6: Sorting & Organizing - Sort, Reverse + StringComparer Concepts
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
        
        // LESSON 6: Basic sort
        studentNames.Sort();
        ConsoleHelper.ShowSuccess("Sorted A→Z (basic sort)");
        
        Console.WriteLine();
        ConsoleHelper.ShowInfo("After basic sort:");
        ShowAllStudents();
        
        // LESSON 6: Case-insensitive sort
        studentNames.Sort(StringComparer.OrdinalIgnoreCase);
        ConsoleHelper.ShowInfo("Applied case-insensitive sort (StringComparer.OrdinalIgnoreCase)");
        
        // LESSON 6: Explain sorting concepts
        Console.WriteLine();
        ConsoleHelper.ShowInfo("Sorting concepts:");
        Console.WriteLine("- Sort() modifies the original list");
        Console.WriteLine("- Default sort is case-sensitive (A-Z, then a-z)");
        Console.WriteLine("- StringComparer.OrdinalIgnoreCase treats A=a");
        Console.WriteLine("- Other options: CurrentCulture, InvariantCulture");
    }
    
    static void SortStudentsZA()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to sort!");
            return;
        }
        
        // LESSON 6: Sort then reverse
        studentNames.Sort(StringComparer.OrdinalIgnoreCase);
        studentNames.Reverse();
        
        ConsoleHelper.ShowSuccess("Sorted Z→A (Sort + Reverse)");
        ShowAllStudents();
        
        // LESSON 6: Alternative approaches
        Console.WriteLine();
        ConsoleHelper.ShowInfo("Alternative approaches:");
        Console.WriteLine("1. Sort() then Reverse() - what we just did");
        Console.WriteLine("2. Custom comparer with reversed logic");
        Console.WriteLine("3. LINQ OrderByDescending() - coming in lesson 8!");
        
        // LESSON 6: Demonstrate other list organization methods
        ConsoleHelper.ShowInfo("Other list organization methods:");
        Console.WriteLine("- Reverse() - flip the order");
        Console.WriteLine("- Shuffle would need custom implementation");
        Console.WriteLine("- Group by first letter - possible with LINQ later");
    }
    
    // =============================================================================
    // LESSON 7: Search & Validation - Find by Name + Input Validation Patterns
    // =============================================================================
    
    static void FindStudentByName()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students to search!");
            return;
        }
        
        string searchTerm = ConsoleHelper.GetInput("Enter part of student name: ");
        
        // LESSON 7: Input validation
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            ConsoleHelper.ShowError("Search term cannot be empty!");
            return;
        }
        
        if (searchTerm.Length < 2)
        {
            ConsoleHelper.ShowWarning("Search term should be at least 2 characters for better results.");
        }
        
        // LESSON 7: Manual search using Contains (no LINQ yet)
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
        
        // LESSON 7: Display results with different categories
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
        
        // LESSON 7: Validation concepts summary
        Console.WriteLine();
        ConsoleHelper.ShowInfo("Validation patterns demonstrated:");
        Console.WriteLine("- Null/empty checks with string.IsNullOrWhiteSpace()");
        Console.WriteLine("- Length validation for meaningful input");
        Console.WriteLine("- Case-insensitive comparisons");
        Console.WriteLine("- Exact vs partial matching strategies");
    }
    
    // =============================================================================
    // LESSON 8: LINQ Introduction - Where, OrderBy + Filtering Concepts
    // =============================================================================
    
    static void FindTopStudents()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }
        
        ConsoleHelper.ShowInfo("=== LINQ Introduction: Filtering Data ===");
        
        // LESSON 8: First LINQ query - Where clause
        var studentsWithGrades = studentNames
            .Where(name => studentGrades[name].Count > 0)
            .ToList();
        
        ConsoleHelper.ShowInfo($"Students with grades: {studentsWithGrades.Count} out of {studentNames.Count}");
        
        if (studentsWithGrades.Count == 0)
        {
            ConsoleHelper.ShowWarning("No students have grades yet!");
            return;
        }
        
        // LESSON 8: LINQ with multiple conditions
        var topStudents = studentNames
            .Where(name => studentGrades[name].Count > 0)           // Has grades
            .Where(name => studentGrades[name].Average() >= 90)     // High average
            .OrderByDescending(name => studentGrades[name].Average()) // Sort by average
            .Select(name => (name, studentGrades[name].Average()))   // Transform to tuple
            .ToList();
        
        ConsoleHelper.ShowTopStudents(topStudents);
        
        // LESSON 8: Explain LINQ concepts
        Console.WriteLine();
        ConsoleHelper.ShowInfo("LINQ concepts introduced:");
        Console.WriteLine("- Where() - filters data based on conditions");
        Console.WriteLine("- OrderByDescending() - sorts in descending order");
        Console.WriteLine("- Select() - transforms data to new format");
        Console.WriteLine("- ToList() - converts result to List<T>");
        Console.WriteLine("- Method chaining - operations flow left to right");
        
        // LESSON 8: Show the equivalent without LINQ for comparison
        ConsoleHelper.ShowInfo("Without LINQ, this would require:");
        Console.WriteLine("- Multiple loops");
        Console.WriteLine("- Temporary variables");
        Console.WriteLine("- Manual sorting logic");
        Console.WriteLine("- Much more code!");
    }
    
    static void FindStudentsAboveAverage()
    {
        if (studentNames.Count == 0)
        {
            ConsoleHelper.ShowError("No students yet!");
            return;
        }
        
        // LESSON 8: Complex LINQ - SelectMany to flatten collections
        var allGrades = studentGrades.Values
            .SelectMany(grades => grades)
            .ToList();
        
        if (allGrades.Count == 0)
        {
            ConsoleHelper.ShowError("No grades entered yet!");
            return;
        }
        
        double classAverage = allGrades.Average();
        
        // LESSON 8: Combining multiple LINQ operations
        var aboveAverageStudents = studentNames
            .Where(name => studentGrades[name].Count > 0)
            .Where(name => studentGrades[name].Average() > classAverage)
            .OrderByDescending(name => studentGrades[name].Average())
            .Select(name => (name, studentGrades[name].Average()))
            .ToList();
        
        ConsoleHelper.ShowAboveAverageStudents(classAverage, aboveAverageStudents);
        
        // LESSON 8: Advanced LINQ concepts
        Console.WriteLine();
        ConsoleHelper.ShowInfo("Advanced LINQ concepts:");
        Console.WriteLine("- SelectMany() - flattens nested collections");
        Console.WriteLine("- Multiple Where() clauses - each filters further");
        Console.WriteLine("- Combining aggregations (Average) with filtering");
        Console.WriteLine("- Anonymous types and tuples for data shaping");
    }
    
    // =============================================================================
    // LESSON 9: LINQ Reports - Average, Min, Max + Data Analysis
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
                // LESSON 9: Basic aggregation methods
                double average = grades.Average();
                int minGrade = grades.Min();
                int maxGrade = grades.Max();
                int totalGrades = grades.Count;
                
                ConsoleHelper.ShowStudentAverage(name, average, grades);
                
                // LESSON 9: Additional statistics
                Console.WriteLine();
                ConsoleHelper.ShowInfo("Detailed statistics:");
                Console.WriteLine($"- Average: {average:F2}%");
                Console.WriteLine($"- Highest grade: {maxGrade}%");
                Console.WriteLine($"- Lowest grade: {minGrade}%");
                Console.WriteLine($"- Grade range: {maxGrade - minGrade} points");
                Console.WriteLine($"- Total assessments: {totalGrades}");
                
                // LESSON 9: LINQ aggregation with conditions
                var highGrades = grades.Where(g => g >= 90).ToList();
                var lowGrades = grades.Where(g => g < 70).ToList();
                
                Console.WriteLine($"- Grades 90+: {highGrades.Count} ({(double)highGrades.Count / totalGrades * 100:F1}%)");
                Console.WriteLine($"- Grades below 70: {lowGrades.Count} ({(double)lowGrades.Count / totalGrades * 100:F1}%)");
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
        
        // LESSON 9: Complex data analysis with LINQ
        var allGrades = studentGrades.Values.SelectMany(grades => grades).ToList();
        
        if (allGrades.Count == 0)
        {
            ConsoleHelper.ShowError("No grades entered yet!");
            return;
        }
        
        // LESSON 9: Comprehensive statistics
        double classAverage = allGrades.Average();
        int totalGrades = allGrades.Count;
        int highestGrade = allGrades.Max();
        int lowestGrade = allGrades.Min();
        
        // LESSON 9: Advanced LINQ queries
        var topPerformer = studentNames
            .Where(name => studentGrades[name].Count > 0)
            .OrderByDescending(name => studentGrades[name].Average())
            .FirstOrDefault();
        
        double topAverage = 0;
        if (topPerformer != null)
        {
            topAverage = studentGrades[topPerformer].Average();
        }
        
        // LESSON 9: Grouping and analysis
        var studentsWithGrades = studentNames
            .Where(name => studentGrades[name].Count > 0)
            .OrderByDescending(name => studentGrades[name].Average())
            .Select(name => (name, studentGrades[name].Average(), studentGrades[name].Count))
            .ToList();
        
        // LESSON 9: Grade distribution analysis
        var gradeDistribution = allGrades
            .GroupBy(grade => grade / 10 * 10) // Group by decade (90-99, 80-89, etc.)
            .OrderByDescending(group => group.Key)
            .Select(group => (Range: $"{group.Key}-{group.Key + 9}", Count: group.Count()))
            .ToList();
        
        ConsoleHelper.ShowClassReport(studentNames.Count, totalGrades, classAverage, 
            highestGrade, lowestGrade, topPerformer ?? "", topAverage, studentsWithGrades);
        
        // LESSON 9: Show grade distribution
        Console.WriteLine();
        ConsoleHelper.ShowInfo("Grade Distribution:");
        foreach (var range in gradeDistribution)
        {
            double percentage = (double)range.Count / totalGrades * 100;
            Console.WriteLine($"  {range.Range}%: {range.Count} grades ({percentage:F1}%)");
        }
        
        // LESSON 9: LINQ concepts summary
        Console.WriteLine();
        ConsoleHelper.ShowInfo("LINQ aggregation methods used:");
        Console.WriteLine("- Average() - calculates mean value");
        Console.WriteLine("- Min() / Max() - finds extremes");
        Console.WriteLine("- Count() - counts items");
        Console.WriteLine("- GroupBy() - groups data by criteria");
        Console.WriteLine("- FirstOrDefault() - gets first item or default");
        Console.WriteLine("- SelectMany() - flattens nested collections");
    }
    
    // =============================================================================
    // Helper Methods
    // =============================================================================
    
    static bool StudentExists(string name)
    {
        // Simple loop-based search (used in early lessons before LINQ)
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
        // LESSON 2: Add initial students
        studentNames.Add("Alice Johnson");
        studentNames.Add("Bob Smith"); 
        studentNames.Add("Carol Davis");
        studentNames.Add("David Wilson");
        
        // LESSON 1: Add initial teachers for demonstration
        teacherNames.Add("Dr. Smith");
        teacherNames.Add("Prof. Johnson");
        
        // LESSON 4: Add initial grades
        studentGrades["Alice Johnson"] = new List<int> { 92, 88, 95, 90 };
        studentGrades["Bob Smith"] = new List<int> { 78, 85, 82 };
        studentGrades["Carol Davis"] = new List<int> { 95, 98, 97, 100, 94 };
        studentGrades["David Wilson"] = new List<int> { 88, 91, 87, 93 };
        
        // LESSON 4: Add initial emails
        studentEmails["Alice Johnson"] = "alice.j@school.edu";
        studentEmails["Bob Smith"] = "bob.s@school.edu";
    }
}

// =============================================================================
// REBALANCED LESSON PROGRESSION SUMMARY:
// =============================================================================
// LESSON 1: Arrays vs Lists + Basic Operations (40+ lines of teaching content)
// LESSON 2: List<T> Fundamentals + Add/Count/Index (50+ lines of teaching content)
// LESSON 3: List<T> Management + Remove/Contains/Clear (60+ lines of teaching content)
// LESSON 4: Dictionary Basics + Key-Value Operations (55+ lines of teaching content)
// LESSON 5: Dictionary Advanced + Safe Access Patterns (45+ lines of teaching content)
// LESSON 6: Sorting & Organizing + StringComparer (50+ lines of teaching content)
// LESSON 7: Search & Validation + Input Patterns (65+ lines of teaching content)
// LESSON 8: LINQ Introduction + Where/OrderBy (60+ lines of teaching content)
// LESSON 9: LINQ Reports + Aggregation Methods (70+ lines of teaching content)
// =============================================================================