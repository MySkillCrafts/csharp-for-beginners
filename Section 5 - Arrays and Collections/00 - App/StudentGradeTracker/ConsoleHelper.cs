using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ═══════════════════════════════════════════════════════════════════════════════════
// CONSOLE HELPER - КРАСИВЫЙ ВЫВОД ДЛЯ СТУДЕНТОВ
// ═══════════════════════════════════════════════════════════════════════════════════
// Этот класс скрывает всю сложность красивого вывода в консоль.
// Студенты просто вызывают методы и передают данные - не нужно понимать, как это работает!
// Фокус остается на изучении коллекций, LINQ и основ C#.
// ═══════════════════════════════════════════════════════════════════════════════════

public static class ConsoleHelper
{
    // Цвета для разных типов сообщений
    private static readonly ConsoleColor HeaderColor = ConsoleColor.Cyan;
    private static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
    private static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
    private static readonly ConsoleColor WarningColor = ConsoleColor.Yellow;
    private static readonly ConsoleColor InfoColor = ConsoleColor.Blue;
    private static readonly ConsoleColor MenuColor = ConsoleColor.Magenta;

    /// <summary>
    /// Показывает заголовок приложения
    /// </summary>
    public static void ShowAppTitle(string title)
    {
        Console.Clear();
        Console.ForegroundColor = HeaderColor;
        Console.WriteLine("╔" + new string('═', title.Length + 2) + "╗");
        Console.WriteLine("║ " + title + " ║");
        Console.WriteLine("╚" + new string('═', title.Length + 2) + "╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// Показывает успешное сообщение (зеленое с галочкой)
    /// </summary>
    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = SuccessColor;
        Console.WriteLine($"✅ {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// Показывает сообщение об ошибке (красное с крестиком)
    /// </summary>
    public static void ShowError(string message)
    {
        Console.ForegroundColor = ErrorColor;
        Console.WriteLine($"❌ {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// Показывает предупреждение (желтое с восклицательным знаком)
    /// </summary>
    public static void ShowWarning(string message)
    {
        Console.ForegroundColor = WarningColor;
        Console.WriteLine($"⚠️  {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// Показывает информационное сообщение (синее с иконкой)
    /// </summary>
    public static void ShowInfo(string message)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"ℹ️  {message}");
        Console.ResetColor();
    }

    /// <summary>
    /// Универсальный метод для отображения данных в красивой таблице
    /// </summary>
    public static void ShowTable<T>(IEnumerable<T> data, string title = "")
    {
        var dataList = data.ToList();
        if (dataList.Count == 0)
        {
            ShowWarning("No data to display.");
            return;
        }

        if (!string.IsNullOrEmpty(title))
        {
            ShowInfo(title);
        }

        var firstItem = dataList.First();
        var properties = firstItem?.GetType().GetProperties()
            .Where(p => p.CanRead)
            .ToArray() ?? new System.Reflection.PropertyInfo[0];

        if (properties.Length == 0)
        {
            // Простые типы (string, int, etc.)
            ShowSimpleList(dataList, title);
            return;
        }

        // Получаем заголовки и данные
        var headers = properties.Select(p => p.Name).ToArray();
        var rows = dataList.Select(item => 
            properties.Select(p => p.GetValue(item)?.ToString() ?? "").ToArray()
        ).ToArray();

        ShowFormattedTable(headers, rows);
    }

    /// <summary>
    /// Показывает список студентов в красивой таблице
    /// </summary>
    public static void ShowStudentList(List<string> students, Dictionary<string, List<int>> grades)
    {
        if (students.Count == 0)
        {
            ShowWarning("No students yet. Add some students first!");
            return;
        }

        var studentData = students.Select((name, index) => new
        {
            No = index + 1,
            Name = name,
            Grades = grades[name].Count,
            Average = grades[name].Count > 0 ? grades[name].Average().ToString("F1") + "%" : "N/A"
        });

        ShowTable(studentData, $"👥 All Students ({students.Count} total)");
    }

    /// <summary>
    /// Показывает простой список элементов
    /// </summary>
    private static void ShowSimpleList<T>(List<T> items, string title)
    {
        var headers = new[] { "#", "Value" };
        var rows = items.Select((item, index) => new[] 
        { 
            (index + 1).ToString(), 
            item?.ToString() ?? "" 
        }).ToArray();

        ShowFormattedTable(headers, rows);
    }

    /// <summary>
    /// Отображает отформатированную таблицу с рамками
    /// </summary>
    private static void ShowFormattedTable(string[] headers, string[][] rows)
    {
        if (rows.Length == 0) return;

        // Вычисляем ширину колонок
        var columnWidths = new int[headers.Length];
        for (int i = 0; i < headers.Length; i++)
        {
            columnWidths[i] = Math.Max(headers[i].Length, 
                rows.Max(row => i < row.Length ? row[i].Length : 0));
            columnWidths[i] = Math.Max(columnWidths[i], 3); // Минимальная ширина
        }

        // Верхняя граница
        Console.ForegroundColor = MenuColor;
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

        // Разделитель заголовков
        Console.ForegroundColor = MenuColor;
        Console.Write("├");
        for (int i = 0; i < columnWidths.Length; i++)
        {
            Console.Write(new string('─', columnWidths[i] + 2));
            if (i < columnWidths.Length - 1) Console.Write("┼");
        }
        Console.WriteLine("┤");
        Console.ResetColor();

        // Данные
        for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
        {
            var row = rows[rowIndex];
            
            // Чередующиеся цвета строк для лучшей читаемости
            Console.ForegroundColor = rowIndex % 2 == 0 ? ConsoleColor.White : ConsoleColor.Gray;
            
            Console.Write("│");
            for (int i = 0; i < headers.Length; i++)
            {
                string cellValue = i < row.Length ? row[i] : "";
                
                // Специальная раскраска для некоторых значений
                if (IsNumericGrade(cellValue))
                {
                    Console.ForegroundColor = GetGradeColor(cellValue);
                }
                
                Console.Write($" {cellValue.PadRight(columnWidths[i])} │");
                
                // Возвращаем цвет строки
                Console.ForegroundColor = rowIndex % 2 == 0 ? ConsoleColor.White : ConsoleColor.Gray;
            }
            Console.WriteLine();
        }
        Console.ResetColor();

        // Нижняя граница
        Console.ForegroundColor = MenuColor;
        Console.Write("└");
        for (int i = 0; i < columnWidths.Length; i++)
        {
            Console.Write(new string('─', columnWidths[i] + 2));
            if (i < columnWidths.Length - 1) Console.Write("┴");
        }
        Console.WriteLine("┘");
        Console.ResetColor();
        Console.WriteLine();
    }

    /// <summary>
    /// Проверяет, является ли строка числовой оценкой
    /// </summary>
    private static bool IsNumericGrade(string value)
    {
        return value.EndsWith("%") && double.TryParse(value.TrimEnd('%'), out _);
    }

    /// <summary>
    /// Возвращает цвет для оценки
    /// </summary>
    private static ConsoleColor GetGradeColor(string gradeText)
    {
        if (double.TryParse(gradeText.TrimEnd('%'), out double grade))
        {
            return grade >= 90 ? ConsoleColor.Green :
                   grade >= 70 ? ConsoleColor.Yellow :
                   ConsoleColor.Red;
        }
        return ConsoleColor.White;
    }

    /// <summary>
    /// Показывает оценки конкретного студента
    /// </summary>
    public static void ShowStudentGrades(string studentName, List<int> grades)
    {
        if (grades.Count == 0)
        {
            ShowWarning($"{studentName} has no grades yet.");
            return;
        }

        var gradeData = grades.Select((grade, index) => new
        {
            No = index + 1,
            Grade = grade + "%",
            Status = grade >= 90 ? "Excellent" : 
                    grade >= 70 ? "Good" : 
                    "Needs Improvement"
        });

        ShowTable(gradeData, $"📊 Grades for {studentName}");
        
        ShowInfo($"Total grades: {grades.Count} | Average: {grades.Average():F1}%");
    }

    /// <summary>
    /// Показывает результаты поиска студентов
    /// </summary>
    public static void ShowSearchResults(string searchTerm, List<string> matches)
    {
        if (matches.Count == 0)
        {
            ShowError($"No students found containing '{searchTerm}'");
            return;
        }

        var searchResults = matches.Select((name, index) => new
        {
            No = index + 1,
            StudentName = name,
            MatchType = name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase) ? "Exact" : "Partial"
        });

        ShowTable(searchResults, $"🔍 Found {matches.Count} student(s) containing '{searchTerm}'");
    }

    /// <summary>
    /// Показывает топ студентов с их средними баллами
    /// </summary>
    public static void ShowTopStudents(List<(string name, double average)> topStudents)
    {
        if (topStudents.Count == 0)
        {
            ShowWarning("No students with 90+ average yet.");
            return;
        }

        var topData = topStudents.Select((student, index) => new
        {
            Rank = index + 1,
            StudentName = student.name,
            Average = student.average.ToString("F1") + "%",
            Achievement = "🏆 Excellent"
        });

        ShowTable(topData, "🏆 Top Students (average 90+)");
    }

    /// <summary>
    /// Показывает студентов выше среднего
    /// </summary>
    public static void ShowAboveAverageStudents(double classAverage, List<(string name, double average)> students)
    {
        if (students.Count == 0)
        {
            ShowWarning("No students above class average.");
            return;
        }

        var aboveAverageData = students.Select((student, index) => new
        {
            No = index + 1,
            StudentName = student.name,
            Average = student.average.ToString("F1") + "%",
            Difference = "+" + (student.average - classAverage).ToString("F1") + "%"
        });

        ShowTable(aboveAverageData, $"📈 Students Above Class Average ({classAverage:F1}%)");
    }

    /// <summary>
    /// Показывает подробный отчет по классу
    /// </summary>
    public static void ShowClassReport(int totalStudents, int totalGrades, double classAverage, 
        int highestGrade, int lowestGrade, string topPerformer, double topAverage,
        List<(string name, double average, int gradeCount)> allStudents)
    {
        ShowInfo("📈 Class Report");
        Console.WriteLine("".PadLeft(50, '═'));

        // Основная статистика в виде таблицы
        var statistics = new[]
        {
            new { Metric = "Total Students", Value = totalStudents.ToString() },
            new { Metric = "Total Grades", Value = totalGrades.ToString() },
            new { Metric = "Class Average", Value = classAverage.ToString("F2") + "%" },
            new { Metric = "Highest Grade", Value = highestGrade.ToString() + "%" },
            new { Metric = "Lowest Grade", Value = lowestGrade.ToString() + "%" },
            new { Metric = "Top Performer", Value = !string.IsNullOrEmpty(topPerformer) ? $"{topPerformer} ({topAverage:F1}%)" : "N/A" }
        };

        ShowTable(statistics, "📊 Class Statistics");

        // Индивидуальные средние в таблице
        if (allStudents.Count > 0)
        {
            var studentData = allStudents.Select((student, index) => new
            {
                Rank = index + 1,
                StudentName = student.name,
                Average = student.average.ToString("F1") + "%",
                GradeCount = student.gradeCount,
                Performance = student.average >= 90 ? "🏆 Excellent" :
                             student.average >= 70 ? "👍 Good" :
                             "📈 Improving"
            });

            ShowTable(studentData, "📋 Individual Performance");
        }
    }

    /// <summary>
    /// Показывает главное меню приложения
    /// </summary>
    public static void ShowMainMenu()
    {
        Console.ForegroundColor = HeaderColor;
        Console.WriteLine("=== Student Grade Tracker ===");
        Console.ResetColor();
        Console.WriteLine();
        
        Console.ForegroundColor = MenuColor;
        Console.WriteLine("📚 Student Management:");
        Console.ResetColor();
        Console.WriteLine("  1. Add student");
        Console.WriteLine("  2. Remove student"); 
        Console.WriteLine("  3. Show all students");
        Console.WriteLine();
        
        Console.ForegroundColor = MenuColor;
        Console.WriteLine("🔤 Organize Students:");
        Console.ResetColor();
        Console.WriteLine("  4. Sort students A→Z");
        Console.WriteLine("  5. Sort students Z→A");
        Console.WriteLine("  6. Find student by name");
        Console.WriteLine();
        
        Console.ForegroundColor = MenuColor;
        Console.WriteLine("📊 Grade Management:");
        Console.ResetColor();
        Console.WriteLine("  7. Add grade to student");
        Console.WriteLine("  8. Show student's grades");
        Console.WriteLine("  9. Show student average");
        Console.WriteLine();
        
        Console.ForegroundColor = MenuColor;
        Console.WriteLine("📈 Reports:");
        Console.ResetColor();
        Console.WriteLine(" 10. Show class report");
        Console.WriteLine(" 11. Find top students (90+)");
        Console.WriteLine(" 12. Find students above average");
        Console.WriteLine();
        
        Console.ForegroundColor = MenuColor;
        Console.WriteLine("🎓 Learning Examples:");
        Console.ResetColor();
        Console.WriteLine(" 13. Manage teachers (List demo)");
        Console.WriteLine(" 14. Manage emails (Dictionary demo)");
        Console.WriteLine(" 15. Array vs List examples");
        Console.WriteLine(" 16. List capabilities demo");
        Console.WriteLine();
        Console.WriteLine("  0. Exit");
        Console.WriteLine();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Choose an option: ");
        Console.ResetColor();
    }

    /// <summary>
    /// Запрашивает ввод от пользователя с подсказкой
    /// </summary>
    public static string GetInput(string prompt)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(prompt);
        Console.ResetColor();
        return Console.ReadLine() ?? "";
    }

    /// <summary>
    /// Показывает среднюю оценку студента
    /// </summary>
    public static void ShowStudentAverage(string studentName, double average, List<int> grades)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"📊 {studentName}'s average: {average:F2}%");
        Console.ResetColor();
        
        Console.WriteLine($"   Grades: {string.Join(", ", grades)}");
        Console.WriteLine($"   Total grades: {grades.Count}");
    }

    /// <summary>
    /// Ждет нажатия любой клавиши для продолжения
    /// </summary>
    public static void WaitForKeyPress(string message = "Press any key to continue...")
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"\n{message}");
        Console.ResetColor();
        
        try
        {
            Console.ReadKey();
            Console.Clear();
        }
        catch (InvalidOperationException)
        {
            // Console input is redirected, just wait a moment and continue
            Console.WriteLine("(Input redirected - continuing automatically)");
            System.Threading.Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Очищает экран
    /// </summary>
    public static void ClearScreen()
    {
        Console.Clear();
    }
}
