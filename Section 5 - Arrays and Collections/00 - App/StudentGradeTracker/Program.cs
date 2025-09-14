// Program.cs

public static class Program
{
    public static void Main()
    {
        ConsoleUI.ShowHeader("Welcome to C# Collections Demo");
        ConsoleUI.ShowInfo("Starting Student Grades Tracker...");
        Console.WriteLine();

        StudentGradesTracker.Run();
    }

}

public readonly record struct Student(string Name, int[] Grades);

internal static class DemoData
{
    // Меняйте имена/оценки здесь. Hot Reload подхватит изменение тела метода.
    public static Student[] GetStudents() => new[]
    {
        new Student("Alice Johnson V1", new[] { 92, 85, 88, 95 }),
        new Student("Bob d Lee",       new[] { 78, 88, 81, 90 }),
        new Student("Carla Gomez",   new[] { 95, 100, 97, 98 }),
        new Student("David Smith",   new[] { 85, 92, 88, 91 })
    };
}

internal static class CollectionsDemo
{
    // 1. Arrays & Lists: Two Tools for Different Jobs
    public static List<string> GetStudentNames()
    {
        var names = new List<string>();
        names.Add("Alice V3 Johnson");
        names.Add("Bob Lee");
        names.Add("Carla Gomez");
        names.Add("David Smith");
        return names;
    }

    // 2. Generics Made Simple: What <T> Means
    public static List<int> GetGrades()
    {
        var grades = new List<int> { 85, 92, 78, 95, 88, 91, 87, 93 };
        return grades;
    }

    // 3. List<T> Essentials: Add, Remove, Show
    public static List<string> GetSubjects()
    {
        var subjects = new List<string>();
        subjects.Add("Mathematics");
        subjects.Add("Physics");
        subjects.Add("Chemistry");
        subjects.Add("Biology");
        subjects.Remove("Biology"); // Удаляем предмет
        subjects.Add("Computer Science"); // Добавляем новый
        return subjects;
    }

    // 4. Tidy Lists: Sort and Basic Find (No LINQ Yet)
    public static List<int> GetSortedGrades()
    {
        var grades = new List<int> { 85, 92, 78, 95, 88, 91, 87, 93 };
        grades.Sort(); // Сортируем по возрастанию
        return grades;
    }

    // 5. Dictionaries for Grades: Keys and Values
    public static Dictionary<string, int> GetStudentAverages()
    {
        var averages = new Dictionary<string, int>();
        averages["Alice"] = 90;
        averages["Bob"] = 85;
        averages["Carla"] = 95;
        averages["David"] = 88;
        return averages;
    }

    // 6. Gentle Validation That Prevents Mistakes
    public static List<int> GetValidatedGrades()
    {
        var grades = new List<int> { 85, 92, 78, 95, 88, 91, 87, 93, 105, -5 }; // Есть невалидные оценки
        var validGrades = new List<int>();

        foreach (var grade in grades)
        {
            if (grade >= 0 && grade <= 100) // Валидация: оценки от 0 до 100
            {
                validGrades.Add(grade);
            }
        }

        return validGrades;
    }

    // 7. LINQ Lite I: Filter and Sort with One-Liners
    public static List<int> GetHighGrades()
    {
        var grades = new List<int> { 85, 92, 78, 95, 88, 91, 87, 93 };
        // Фильтруем только высокие оценки (>= 90)
        return grades.Where(g => g >= 90).ToList();
    }

    // 8. LINQ Lite II: Tiny Reports with Averages
    public static double GetAverageGrade()
    {
        var grades = new List<int> { 85, 92, 78, 95, 88, 91, 87, 93 };
        return grades.Average();
    }
}

internal static class ConsoleUI
{
    // Цвета для консоли
    private static readonly ConsoleColor HeaderColor = ConsoleColor.Cyan;
    private static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
    private static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
    private static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
    private static readonly ConsoleColor InfoColor = ConsoleColor.Blue;
    private static readonly ConsoleColor DataColor = ConsoleColor.White;
    private static readonly ConsoleColor AccentColor = ConsoleColor.Magenta;

    public static void ShowHeader(string title)
    {
        Console.Clear();
        Console.ForegroundColor = HeaderColor;
        Console.WriteLine("╔" + new string('═', title.Length + 2) + "╗");
        Console.WriteLine("║ " + title + " ║");
        Console.WriteLine("╚" + new string('═', title.Length + 2) + "╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = SuccessColor;
        Console.WriteLine($"✅ {message}");
        Console.ResetColor();
    }

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ErrorColor;
        Console.WriteLine($"❌ {message}");
        Console.ResetColor();
    }

    public static void ShowWarning(string message)
    {
        Console.ForegroundColor = WarningColor;
        Console.WriteLine($"⚠️  {message}");
        Console.ResetColor();
    }

    public static void ShowInfo(string message)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"ℹ️  {message}");
        Console.ResetColor();
    }

    public static void ShowTable<T>(IEnumerable<T> data, string[] headers, Func<T, string[]> rowSelector)
    {
        if (!data.Any())
        {
            ShowWarning("No data to display");
            return;
        }

        var rows = data.Select(rowSelector).ToList();
        var columnWidths = new int[headers.Length];

        // Вычисляем ширину колонок
        for (int i = 0; i < headers.Length; i++)
        {
            columnWidths[i] = Math.Max(headers[i].Length, rows.Max(row => row[i].Length));
        }

        // Верхняя граница
        Console.ForegroundColor = AccentColor;
        Console.Write("┌");
        for (int i = 0; i < columnWidths.Length; i++)
        {
            Console.Write(new string('─', columnWidths[i] + 2));
            if (i < columnWidths.Length - 1) Console.Write("┬");
        }
        Console.WriteLine("┐");
        Console.ResetColor();

        // Заголовки
        Console.ForegroundColor = HeaderColor;
        Console.Write("│");
        for (int i = 0; i < headers.Length; i++)
        {
            Console.Write($" {headers[i].PadRight(columnWidths[i])} │");
        }
        Console.WriteLine();
        Console.ResetColor();

        // Разделитель
        Console.ForegroundColor = AccentColor;
        Console.Write("├");
        for (int i = 0; i < columnWidths.Length; i++)
        {
            Console.Write(new string('─', columnWidths[i] + 2));
            if (i < columnWidths.Length - 1) Console.Write("┼");
        }
        Console.WriteLine("┤");
        Console.ResetColor();

        // Данные
        Console.ForegroundColor = DataColor;
        foreach (var row in rows)
        {
            Console.Write("│");
            for (int i = 0; i < row.Length; i++)
            {
                Console.Write($" {row[i].PadRight(columnWidths[i])} │");
            }
            Console.WriteLine();
        }
        Console.ResetColor();

        // Нижняя граница
        Console.ForegroundColor = AccentColor;
        Console.Write("└");
        for (int i = 0; i < columnWidths.Length; i++)
        {
            Console.Write(new string('─', columnWidths[i] + 2));
            if (i < columnWidths.Length - 1) Console.Write("┴");
        }
        Console.WriteLine("┘");
        Console.ResetColor();
    }

    public static void ShowProgressBar(int current, int total, string label = "")
    {
        if (total == 0) return;

        var percentage = (double)current / total;
        var barLength = 30;
        var filledLength = (int)(barLength * percentage);

        Console.ForegroundColor = InfoColor;
        Console.Write($"{label} [");
        Console.ForegroundColor = SuccessColor;
        Console.Write(new string('█', filledLength));
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(new string('░', barLength - filledLength));
        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"] {percentage:P1} ({current}/{total})");
        Console.ResetColor();
    }

    public static void ShowMenu(string[] options)
    {
        Console.ForegroundColor = AccentColor;
        Console.WriteLine("┌─ Menu ─────────────────────────────────────────┐");
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"│ {i + 1,2}. {options[i].PadRight(40)} │");
        }
        Console.WriteLine("└───────────────────────────────────────────────┘");
        Console.ResetColor();
    }

    public static void ShowStats(Dictionary<string, object> stats)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine("📊 Statistics:");
        Console.ResetColor();

        foreach (var stat in stats)
        {
            Console.ForegroundColor = DataColor;
            Console.Write($"  {stat.Key}: ");
            Console.ForegroundColor = AccentColor;
            Console.WriteLine(stat.Value);
            Console.ResetColor();
        }
    }

    public static string ReadInput(string prompt, ConsoleColor promptColor = ConsoleColor.White)
    {
        Console.ForegroundColor = promptColor;
        Console.Write(prompt);
        Console.ResetColor();
        return Console.ReadLine() ?? "";
    }

    public static void WaitForKey(string message = "Press any key to continue...")
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.ReadKey();
    }
}

internal static class StudentGradesTracker
{
    private static readonly List<string> students = new();
    private static readonly Dictionary<string, List<int>> grades = new(StringComparer.OrdinalIgnoreCase);

    public static void Run()
    {
        // Добавляем демо-данные
        AddDemoData();

        while (true)
        {
            ConsoleUI.ShowHeader("Student Grades Tracker");

            var menuOptions = new[]
            {
                "Add student",
                "Remove student",
                "Show all students",
                "Sort by name (A→Z)",
                "Sort by name (Z→A)",
                "Find by name part",
                "Add grade to student",
                "Show grades for student",
                "Show student average",
                "Show class average",
                "Show top performer",
                "Show detailed report",
                "Exit"
            };

            ConsoleUI.ShowMenu(menuOptions);

            var choice = ConsoleUI.ReadInput("Choose option: ", ConsoleColor.Yellow);
            Console.WriteLine();

            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": ShowAllStudents(); break;
                case "4": SortStudentsAZ(); break;
                case "5": SortStudentsZA(); break;
                case "6": FindByNamePart(); break;
                case "7": AddGradeToStudent(); break;
                case "8": ShowGradesForStudent(); break;
                case "9": ShowStudentAverage(); break;
                case "10": ShowClassAverage(); break;
                case "11": ShowTopPerformer(); break;
                case "12": ShowDetailedReport(); break;
                case "13": case "0": return;
                default: ConsoleUI.ShowError("Unknown option. Try again."); break;
            }

            ConsoleUI.WaitForKey();
        }
    }

    private static void AddDemoData()
    {
        students.AddRange(new[] { "Alice Johnson", "Bob Lee", "Carla Gomez", "David Smith" });
        grades["Alice Johnson"] = new List<int> { 92, 85, 88, 95 };
        grades["Bob Lee"] = new List<int> { 78, 88, 81, 90 };
        grades["Carla Gomez"] = new List<int> { 95, 100, 97, 98 };
        grades["David Smith"] = new List<int> { 85, 92, 88, 91 };
    }

    private static void AddStudent()
    {
        var name = ConsoleUI.ReadInput("Enter student name: ");
        if (string.IsNullOrWhiteSpace(name))
        {
            ConsoleUI.ShowError("Name cannot be empty.");
            return;
        }

        if (ContainsStudent(name))
        {
            ConsoleUI.ShowWarning("This student already exists.");
            return;
        }

        students.Add(name);
        grades[name] = new List<int>();
        ConsoleUI.ShowSuccess($"Added: {name}");
    }

    private static void RemoveStudent()
    {
        var name = ConsoleUI.ReadInput("Enter student name to remove: ");
        var canonical = FindStudentCanonical(name);
        if (canonical is null)
        {
            ConsoleUI.ShowError("Student not found.");
            return;
        }

        students.RemoveAll(s => s.Equals(canonical, StringComparison.OrdinalIgnoreCase));
        grades.Remove(canonical);
        ConsoleUI.ShowSuccess($"Removed: {canonical}");
    }

    private static void ShowAllStudents()
    {
        if (students.Count == 0)
        {
            ConsoleUI.ShowWarning("No students yet.");
            return;
        }

        var studentData = students.Select((name, index) => new
        {
            Index = index + 1,
            Name = name,
            GradeCount = grades[name].Count,
            Average = grades[name].Count > 0 ? grades[name].Average().ToString("F1") : "N/A"
        });

        ConsoleUI.ShowTable(studentData,
            new[] { "#", "Name", "Grades", "Average" },
            s => new[] { s.Index.ToString(), s.Name, s.GradeCount.ToString(), s.Average });
    }

    private static void SortStudentsAZ()
    {
        if (students.Count == 0)
        {
            ConsoleUI.ShowWarning("No students to sort.");
            return;
        }

        students.Sort(StringComparer.OrdinalIgnoreCase);
        ConsoleUI.ShowSuccess("Sorted A→Z.");
    }

    private static void SortStudentsZA()
    {
        if (students.Count == 0)
        {
            ConsoleUI.ShowWarning("No students to sort.");
            return;
        }

        var sorted = students.OrderByDescending(s => s, StringComparer.OrdinalIgnoreCase).ToList();
        students.Clear();
        students.AddRange(sorted);
        ConsoleUI.ShowSuccess("Sorted Z→A.");
    }

    private static void FindByNamePart()
    {
        if (students.Count == 0)
        {
            ConsoleUI.ShowWarning("No students yet.");
            return;
        }

        var part = ConsoleUI.ReadInput("Enter part of the name: ");
        var matches = students.Where(s => s.Contains(part, StringComparison.OrdinalIgnoreCase)).ToList();

        if (matches.Count == 0)
        {
            ConsoleUI.ShowWarning("No matches found.");
            return;
        }

        ConsoleUI.ShowInfo($"Found {matches.Count} match(es):");
        foreach (var match in matches)
        {
            ConsoleUI.ShowSuccess($"- {match}");
        }
    }

    private static void AddGradeToStudent()
    {
        var name = ConsoleUI.ReadInput("Student name: ");
        var canonical = FindStudentCanonical(name);
        if (canonical is null)
        {
            ConsoleUI.ShowError("Student not found.");
            return;
        }

        var gradeInput = ConsoleUI.ReadInput("Enter grade (0-100): ");
        if (!int.TryParse(gradeInput, out int grade))
        {
            ConsoleUI.ShowError("Please enter a valid number.");
            return;
        }

        if (grade < 0 || grade > 100)
        {
            ConsoleUI.ShowError("Grade must be between 0 and 100.");
            return;
        }

        grades[canonical].Add(grade);
        ConsoleUI.ShowSuccess($"Added grade {grade} to {canonical}.");
    }

    private static void ShowGradesForStudent()
    {
        var name = ConsoleUI.ReadInput("Student name: ");
        var canonical = FindStudentCanonical(name);
        if (canonical is null)
        {
            ConsoleUI.ShowError("Student not found.");
            return;
        }

        var studentGrades = grades[canonical];
        if (studentGrades.Count == 0)
        {
            ConsoleUI.ShowWarning($"{canonical} has no grades yet.");
            return;
        }

        ConsoleUI.ShowInfo($"{canonical}'s grades:");
        ConsoleUI.ShowTable(studentGrades.Select((g, i) => new { Index = i + 1, Grade = g }),
            new[] { "#", "Grade" },
            g => new[] { g.Index.ToString(), g.Grade.ToString() });
    }

    private static void ShowStudentAverage()
    {
        var name = ConsoleUI.ReadInput("Student name: ");
        var canonical = FindStudentCanonical(name);
        if (canonical is null)
        {
            ConsoleUI.ShowError("Student not found.");
            return;
        }

        var studentGrades = grades[canonical];
        if (studentGrades.Count == 0)
        {
            ConsoleUI.ShowWarning($"{canonical} has no grades to average.");
            return;
        }

        var average = studentGrades.Average();
        ConsoleUI.ShowInfo($"{canonical}'s average: {average:F2}");

        // Показываем прогресс-бар
        ConsoleUI.ShowProgressBar((int)average, 100, "Average");
    }

    private static void ShowClassAverage()
    {
        var allGrades = grades.Values.SelectMany(g => g).ToList();
        if (allGrades.Count == 0)
        {
            ConsoleUI.ShowWarning("No grades in class yet.");
            return;
        }

        var average = allGrades.Average();
        ConsoleUI.ShowInfo($"Class average: {average:F2}");
        ConsoleUI.ShowProgressBar((int)average, 100, "Class Average");
    }

    private static void ShowTopPerformer()
    {
        var performers = grades
            .Where(kvp => kvp.Value.Count > 0)
            .Select(kvp => new { Name = kvp.Key, Avg = kvp.Value.Average() })
            .OrderByDescending(x => x.Avg)
            .Take(3)
            .Select((p, index) => new { Rank = index + 1, Name = p.Name, Average = p.Avg })
            .ToList();

        if (performers.Count == 0)
        {
            ConsoleUI.ShowWarning("No grades to determine top performers.");
            return;
        }

        ConsoleUI.ShowInfo("Top Performers:");
        ConsoleUI.ShowTable(performers,
            new[] { "Rank", "Name", "Average" },
            p => new[] { p.Rank.ToString(), p.Name, p.Average.ToString("F2") });
    }

    private static void ShowDetailedReport()
    {
        if (students.Count == 0)
        {
            ConsoleUI.ShowWarning("No students to report on.");
            return;
        }

        var stats = new Dictionary<string, object>
        {
            ["Total Students"] = students.Count,
            ["Total Grades"] = grades.Values.Sum(g => g.Count),
            ["Class Average"] = grades.Values.SelectMany(g => g).DefaultIfEmpty(0).Average().ToString("F2"),
            ["Highest Grade"] = grades.Values.SelectMany(g => g).DefaultIfEmpty(0).Max(),
            ["Lowest Grade"] = grades.Values.SelectMany(g => g).DefaultIfEmpty(0).Min()
        };

        ConsoleUI.ShowStats(stats);
    }


    private static bool ContainsStudent(string name)
        => students.Any(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));

    private static string? FindStudentCanonical(string name)
        => students.FirstOrDefault(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));
}

