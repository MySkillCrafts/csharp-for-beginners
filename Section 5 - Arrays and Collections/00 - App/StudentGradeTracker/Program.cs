// Program.cs
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

public static class Program
{
    public static void Main()
    {
        ConsoleUI.ShowHeader("Welcome to C# Collections Demo");
        ConsoleUI.ShowInfo("Choose your interface:");
        Console.WriteLine();

        var options = new[]
        {
            "Console Interface (Student Grades Tracker)",
            "Web Interface (React App)",
            "Exit"
        };

        ConsoleUI.ShowMenu(options);

        var choice = ConsoleUI.ReadInput("Choose option: ", ConsoleColor.Yellow);
        Console.WriteLine();

        switch (choice)
        {
            case "1":
                StudentGradesTracker.Run();
                break;
            case "2":
                const int DevPort = 5055;
                Show(DevPort);
                ConsoleUI.ShowSuccess($"Web interface started at http://127.0.0.1:{DevPort}/");
                ConsoleUI.WaitForKey("Press any key to exit...");
                break;
            case "3":
                ConsoleUI.ShowInfo("Goodbye!");
                return;
            default:
                ConsoleUI.ShowError("Invalid choice. Exiting...");
                break;
        }
    }

    /// <summary>
    /// Показывает React приложение с данными студентов
    /// </summary>
    /// <param name="port">Порт для веб-сервера</param>
    public static void Show(int port = 5055)
    {
        WebPageHelper.ShowReactApp(port);
    }

    // теперь НЕ принимаем students параметром — берём свежие данные внутри
    private static string BuildStudentsTableHtml()
    {
        var students = DemoData.GetStudents(); // создаётся при каждом запросе

        var sb = new StringBuilder();
        sb.Append("""
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width,initial-scale=1">
<title>Students Grades (Sketch)</title>
<style>
  :root { font-family: ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Arial; }
  body { background:#0b1220; color:#e8eefc; margin:0; padding:32px; }
  .card { max-width: 820px; margin: 0 auto; background: #111a2e; border:1px solid #223354; border-radius:16px; box-shadow:0 10px 30px rgba(0,0,0,.35); }
  .card h1 { margin:0; padding:20px 24px; font-size:22px; border-bottom:1px solid #223354; }
  .content { padding:16px 24px 24px; }
  table { width:100%; border-collapse:collapse; overflow:hidden; border-radius:12px; }
  thead th { text-align:left; font-weight:600; font-size:14px; color:#a9c1ff; padding:12px 14px; background:#13213d; position:sticky; top:0; }
  tbody td { padding:12px 14px; border-top:1px solid #223354; font-size:14px; }
  tbody tr:nth-child(odd) { background:#0f1a33; }
  .pill { display:inline-block; padding:4px 10px; border-radius:999px; background:#1b2a4d; border:1px solid #29457d; font-size:12px; color:#cfe0ff; }
  .muted { color:#a9c1ff; opacity:.8; font-size:12px; }
  .hint { margin-top:12px; }
</style>
</head>
<body>
  <div class="card">
    <h1>Students Grades (Sketch)</h1>
    <div class="content">
      <table role="grid" aria-label="Students grades table">
        <thead>
          <tr><th>Name</th><th>Grades</th><th>Average</th></tr>
        </thead>
        <tbody>
""");

        foreach (var s in students)
        {
            var gradesText = string.Join(" ", s.Grades.Select(g => $"<span class=\"pill\">{g}</span>"));
            var avg = s.Grades.Length == 0 ? 0 : (int)Math.Round(s.Grades.Average());
            sb.Append($$"""
          <tr>
            <td>{{WebPageHelper.E(s.Name)}}</td>
            <td>{{gradesText}}</td>
            <td><strong>{{avg}}</strong> <span class="muted">/ 100</span></td>
          </tr>
""");
        }

        sb.Append("""
        </tbody>
      </table>
      <p class="hint muted">Tip: edit DemoData.GetStudents() → Hot Reload → refresh.</p>
    </div>
  </div>
</body>
</html>
""");
        return sb.ToString();
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
                "Show web interface",
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
                case "13": ShowWebInterface(); break;
                case "14": case "0": return;
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

    private static void ShowWebInterface()
    {
        ConsoleUI.ShowInfo("Starting web interface...");
        WebPageHelper.ShowReactApp(5055);
        ConsoleUI.ShowSuccess("Web interface started at http://127.0.0.1:5055/");
    }

    private static bool ContainsStudent(string name)
        => students.Any(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));

    private static string? FindStudentCanonical(string name)
        => students.FirstOrDefault(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));
}

internal static class WebPageHelper
{
    private static Func<string>? _render;
    private static CancellationTokenSource? _cts;
    private static bool _browserOpened;

    public static void ShowReactApp(int port)
    {
        if (_cts is null)
        {
            _cts = new CancellationTokenSource();
            StartReactServer(port, _cts.Token);
            OpenBrowser($"http://127.0.0.1:{port}/");
        }
    }

    public static void ShowOnWebPage(Func<string> renderHtml, int port)
    {
        _render = renderHtml;
        if (_cts is null)
        {
            _cts = new CancellationTokenSource();
            StartServer(port, _cts.Token);
            OpenBrowser($"http://127.0.0.1:{port}/");
        }
    }

    private static void StartReactServer(int port, CancellationToken token)
    {
        var listener = new TcpListener(IPAddress.Loopback, port);
        try { listener.Start(); }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Port {port} is in use. Stop the previous run or change the port in Program.cs.");
            Console.ResetColor();
            throw;
        }

        _ = Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var client = await listener.AcceptTcpClientAsync(token);
                    _ = Task.Run(() => HandleReactClientAsync(client, token));
                }
                catch { /* ignore on shutdown */ }
            }
        }, token);
    }

    private static void StartServer(int port, CancellationToken token)
    {
        var listener = new TcpListener(IPAddress.Loopback, port);
        try { listener.Start(); }
        catch (SocketException ex) when (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Port {port} is in use. Stop the previous run or change the port in Program.cs.");
            Console.ResetColor();
            throw;
        }

        _ = Task.Run(async () =>
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var client = await listener.AcceptTcpClientAsync(token);
                    _ = Task.Run(() => HandleClientAsync(client, token));
                }
                catch { /* ignore on shutdown */ }
            }
        }, token);
    }

    private static async Task HandleReactClientAsync(TcpClient client, CancellationToken token)
    {
        using var _ = client;
        using var stream = client.GetStream();

        // читаем заголовки до пустой строки
        var buf = new byte[4096];
        var req = new StringBuilder();
        while (stream.DataAvailable || req.Length == 0)
        {
            var read = await stream.ReadAsync(buf.AsMemory(0, buf.Length), token);
            if (read <= 0) break;
            req.Append(Encoding.ASCII.GetString(buf, 0, read));
            if (req.ToString().Contains("\r\n\r\n")) break;
        }

        var request = req.ToString();
        var lines = request.Split('\n');
        var requestLine = lines[0];
        var path = requestLine.Split(' ')[1];

        byte[] body;
        string contentType;
        string status = "200 OK";

        if (path.StartsWith("/api/"))
        {
            // API endpoints для получения разных типов данных
            object data = path switch
            {
                "/api/students" => DemoData.GetStudents(),
                "/api/names" => CollectionsDemo.GetStudentNames(),
                "/api/grades" => CollectionsDemo.GetGrades(),
                "/api/subjects" => CollectionsDemo.GetSubjects(),
                "/api/sorted-grades" => CollectionsDemo.GetSortedGrades(),
                "/api/averages" => CollectionsDemo.GetStudentAverages(),
                "/api/validated-grades" => CollectionsDemo.GetValidatedGrades(),
                "/api/high-grades" => CollectionsDemo.GetHighGrades(),
                "/api/average-grade" => new { average = CollectionsDemo.GetAverageGrade() },
                _ => new { error = "Unknown endpoint" }
            };

            var json = System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions
            {
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            });
            body = Encoding.UTF8.GetBytes(json);
            contentType = "application/json; charset=utf-8";
        }
        else
        {
            // Главная страница с React приложением
            var html = BuildReactAppHtml();
            body = Encoding.UTF8.GetBytes(html);
            contentType = "text/html; charset=utf-8";
        }

        var header =
            $"HTTP/1.1 {status}\r\n" +
            $"Content-Type: {contentType}\r\n" +
            "Cache-Control: no-store\r\n" +
            "Access-Control-Allow-Origin: *\r\n" +
            "Connection: close\r\n" +
            $"Content-Length: {body.Length}\r\n\r\n";

        await stream.WriteAsync(Encoding.ASCII.GetBytes(header), token);
        await stream.WriteAsync(body, token);
        await stream.FlushAsync(token);
    }

    private static async Task HandleClientAsync(TcpClient client, CancellationToken token)
    {
        using var _ = client;
        using var stream = client.GetStream();

        // читаем заголовки до пустой строки
        var buf = new byte[4096];
        var req = new StringBuilder();
        while (stream.DataAvailable || req.Length == 0)
        {
            var read = await stream.ReadAsync(buf.AsMemory(0, buf.Length), token);
            if (read <= 0) break;
            req.Append(Encoding.ASCII.GetString(buf, 0, read));
            if (req.ToString().Contains("\r\n\r\n")) break;
        }

        string html;
        try { html = _render is null ? "<h1>No content</h1>" : _render(); }
        catch (Exception ex) { html = $"<h1>Render error</h1><pre style='color:#c00'>{E(ex.Message)}</pre>"; }

        var body = Encoding.UTF8.GetBytes(html);
        var header =
            "HTTP/1.1 200 OK\r\n" +
            "Content-Type: text/html; charset=utf-8\r\n" +
            "Cache-Control: no-store\r\n" +
            "Connection: close\r\n" +
            $"Content-Length: {body.Length}\r\n\r\n";

        await stream.WriteAsync(Encoding.ASCII.GetBytes(header), token);
        await stream.WriteAsync(body, token);
        await stream.FlushAsync(token);
    }

    private static string BuildReactAppHtml()
    {
        return """
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1">
    <title>C# Collections Demo - React App</title>
    <style>
        :root { 
            font-family: ui-sans-serif, system-ui, -apple-system, Segoe UI, Roboto, Arial; 
        }
        body { 
            background: #0b1220; 
            color: #e8eefc; 
            margin: 0; 
            padding: 32px; 
        }
        .card { 
            max-width: 900px; 
            margin: 0 auto; 
            background: #111a2e; 
            border: 1px solid #223354; 
            border-radius: 16px; 
            box-shadow: 0 10px 30px rgba(0,0,0,.35); 
        }
        .card h1 { 
            margin: 0; 
            padding: 20px 24px; 
            font-size: 22px; 
            border-bottom: 1px solid #223354; 
        }
        .content { 
            padding: 16px 24px 24px; 
        }
        .tabs {
            display: flex;
            gap: 8px;
            margin-bottom: 20px;
            flex-wrap: wrap;
        }
        .tab {
            background: #1b2a4d;
            border: 1px solid #29457d;
            color: #cfe0ff;
            padding: 8px 16px;
            border-radius: 8px;
            cursor: pointer;
            font-size: 14px;
            transition: all 0.2s;
        }
        .tab:hover {
            background: #29457d;
        }
        .tab.active {
            background: #3b5b8c;
            border-color: #4a6ba0;
        }
        table { 
            width: 100%; 
            border-collapse: collapse; 
            overflow: hidden; 
            border-radius: 12px; 
        }
        thead th { 
            text-align: left; 
            font-weight: 600; 
            font-size: 14px; 
            color: #a9c1ff; 
            padding: 12px 14px; 
            background: #13213d; 
            position: sticky; 
            top: 0; 
        }
        tbody td { 
            padding: 12px 14px; 
            border-top: 1px solid #223354; 
            font-size: 14px; 
        }
        tbody tr:nth-child(odd) { 
            background: #0f1a33; 
        }
        .pill { 
            display: inline-block; 
            padding: 4px 10px; 
            border-radius: 999px; 
            background: #1b2a4d; 
            border: 1px solid #29457d; 
            font-size: 12px; 
            color: #cfe0ff; 
            margin-right: 4px;
        }
        .muted { 
            color: #a9c1ff; 
            opacity: .8; 
            font-size: 12px; 
        }
        .hint { 
            margin-top: 12px; 
        }
        .loading {
            text-align: center;
            padding: 40px;
            color: #a9c1ff;
        }
        .error {
            color: #ff6b6b;
            background: #2d1b1b;
            padding: 12px;
            border-radius: 8px;
            margin: 12px 0;
        }
        .refresh-btn {
            background: #1b2a4d;
            border: 1px solid #29457d;
            color: #cfe0ff;
            padding: 8px 16px;
            border-radius: 8px;
            cursor: pointer;
            font-size: 14px;
            margin-bottom: 16px;
        }
        .refresh-btn:hover {
            background: #29457d;
        }
        .description {
            background: #1a2332;
            padding: 12px;
            border-radius: 8px;
            margin-bottom: 16px;
            font-size: 14px;
            color: #b8c5d6;
        }
    </style>
</head>
<body>
    <div id="root"></div>

    <!-- React и ReactDOM из CDN -->
    <script crossorigin src="https://unpkg.com/react@18/umd/react.development.js"></script>
    <script crossorigin src="https://unpkg.com/react-dom@18/umd/react-dom.development.js"></script>
    
    <!-- Babel для JSX трансформации -->
    <script src="https://unpkg.com/@babel/standalone/babel.min.js"></script>

    <script type="text/babel">
        const { useState, useEffect } = React;

        const endpoints = [
            { key: 'students', label: 'Students', description: 'Array of students with grades' },
            { key: 'names', label: 'Names List', description: 'List<string> - Student names' },
            { key: 'grades', label: 'Grades List', description: 'List<int> - All grades' },
            { key: 'subjects', label: 'Subjects', description: 'List<string> - Add/Remove operations' },
            { key: 'sorted-grades', label: 'Sorted Grades', description: 'List<int> - Sorted grades' },
            { key: 'averages', label: 'Averages Dict', description: 'Dictionary<string, int> - Student averages' },
            { key: 'validated-grades', label: 'Validated Grades', description: 'List<int> - Only valid grades (0-100)' },
            { key: 'high-grades', label: 'High Grades', description: 'List<int> - LINQ filter (>=90)' },
            { key: 'average-grade', label: 'Average Grade', description: 'double - LINQ Average()' }
        ];

        function CollectionsDemo() {
            const [activeTab, setActiveTab] = useState('students');
            const [data, setData] = useState(null);
            const [loading, setLoading] = useState(false);
            const [error, setError] = useState(null);

            const fetchData = async (endpoint) => {
                try {
                    setLoading(true);
                    setError(null);
                    const response = await fetch(`/api/${endpoint}`);
                    if (!response.ok) {
                        throw new Error('Failed to fetch data');
                    }
                    const result = await response.json();
                    setData(result);
                } catch (err) {
                    setError(err.message);
                } finally {
                    setLoading(false);
                }
            };

            useEffect(() => {
                fetchData(activeTab);
            }, [activeTab]);

            const renderData = () => {
                if (loading) return <div className="loading">Loading...</div>;
                if (error) return <div className="error">Error: {error}</div>;
                if (!data) return null;

                const currentEndpoint = endpoints.find(e => e.key === activeTab);
                
                if (activeTab === 'students') {
                    return (
                        <table>
                            <thead>
                                <tr><th>Name</th><th>Grades</th><th>Average</th></tr>
                            </thead>
                            <tbody>
                                {data.map((student, index) => {
                                    const average = student.grades.length === 0 
                                        ? 0 
                                        : Math.round(student.grades.reduce((a, b) => a + b, 0) / student.grades.length);
                                    return (
                                        <tr key={index}>
                                            <td>{student.name}</td>
                                            <td>
                                                {student.grades.map((grade, gradeIndex) => (
                                                    <span key={gradeIndex} className="pill">{grade}</span>
                                                ))}
                                            </td>
                                            <td><strong>{average}</strong> <span className="muted">/ 100</span></td>
                                        </tr>
                                    );
                                })}
                            </tbody>
                        </table>
                    );
                }
                
                if (activeTab === 'averages') {
                    return (
                        <table>
                            <thead>
                                <tr><th>Student</th><th>Average</th></tr>
                            </thead>
                            <tbody>
                                {Object.entries(data).map(([name, average], index) => (
                                    <tr key={index}>
                                        <td>{name}</td>
                                        <td><strong>{average}</strong> <span className="muted">/ 100</span></td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    );
                }
                
                if (activeTab === 'average-grade') {
                    return (
                        <div style={{ textAlign: 'center', padding: '40px' }}>
                            <div style={{ fontSize: '48px', fontWeight: 'bold', color: '#4a9eff' }}>
                                {data.average.toFixed(1)}
                            </div>
                            <div className="muted">Average Grade</div>
                        </div>
                    );
                }
                
                // For arrays and lists
                if (Array.isArray(data)) {
                    return (
                        <div>
                            {data.map((item, index) => (
                                <span key={index} className="pill" style={{ marginBottom: '8px', display: 'inline-block' }}>
                                    {item}
                                </span>
                            ))}
                        </div>
                    );
                }
                
                return <pre>{JSON.stringify(data, null, 2)}</pre>;
            };

            return (
                <div className="card">
                    <h1>C# Collections Demo - React App</h1>
                    <div className="content">
                        <div className="tabs">
                            {endpoints.map(endpoint => (
                                <button
                                    key={endpoint.key}
                                    className={`tab ${activeTab === endpoint.key ? 'active' : ''}`}
                                    onClick={() => setActiveTab(endpoint.key)}
                                >
                                    {endpoint.label}
                                </button>
                            ))}
                        </div>
                        
                        <div className="description">
                            <strong>{endpoints.find(e => e.key === activeTab)?.label}:</strong> {endpoints.find(e => e.key === activeTab)?.description}
                        </div>
                        
                        <button className="refresh-btn" onClick={() => fetchData(activeTab)}>
                            🔄 Refresh Data
                        </button>
                        
                        {renderData()}
                        
                        <p className="hint muted">
                            💡 Tip: Edit CollectionsDemo methods in Program.cs → Hot Reload → refresh to see changes
                        </p>
                    </div>
                </div>
            );
        }

        // Рендерим React приложение
        const root = ReactDOM.createRoot(document.getElementById('root'));
        root.render(<CollectionsDemo />);
    </script>
</body>
</html>
""";
    }

    public static string E(string s) =>
        s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

    private static void OpenBrowser(string url)
    {
        if (_browserOpened) return;
        _browserOpened = true;
        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch { Console.WriteLine($"Open your browser: {url}"); }
    }
}
