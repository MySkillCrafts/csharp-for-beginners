using System;
using System.Collections.Generic;
using System.Linq;

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
    /// Показывает список студентов в красивой таблице
    /// </summary>
    public static void ShowStudentList(List<string> students, Dictionary<string, List<int>> grades)
    {
        if (students.Count == 0)
        {
            ShowWarning("No students yet. Add some students first!");
            return;
        }

        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"👥 All Students ({students.Count} total):");
        Console.WriteLine("".PadLeft(50, '─'));
        Console.ResetColor();

        for (int i = 0; i < students.Count; i++)
        {
            string name = students[i];
            int gradeCount = grades[name].Count;
            
            // Цвет зависит от количества оценок
            if (gradeCount == 0)
                Console.ForegroundColor = ConsoleColor.DarkGray;
            else if (gradeCount >= 5)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.White;
                
            Console.WriteLine($"  {i + 1,2}. {name.PadRight(20)} ({gradeCount} grades)");
        }
        Console.ResetColor();
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

        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"📊 Grades for {studentName}:");
        Console.WriteLine("".PadLeft(30, '─'));
        Console.ResetColor();

        for (int i = 0; i < grades.Count; i++)
        {
            int grade = grades[i];
            
            // Цвет оценки зависит от значения
            if (grade >= 90)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (grade >= 70)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
                
            Console.WriteLine($"  Grade {i + 1}: {grade}%");
        }
        
        Console.ResetColor();
        Console.WriteLine($"  Total grades: {grades.Count}");
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

        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"🔍 Found {matches.Count} student(s) containing '{searchTerm}':");
        Console.ResetColor();
        
        for (int i = 0; i < matches.Count; i++)
        {
            Console.ForegroundColor = SuccessColor;
            Console.WriteLine($"  • {matches[i]}");
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Показывает топ студентов с их средними баллами
    /// </summary>
    public static void ShowTopStudents(List<(string name, double average)> topStudents)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine("🏆 Top Students (average 90+):");
        Console.WriteLine("".PadLeft(35, '─'));
        Console.ResetColor();

        if (topStudents.Count == 0)
        {
            ShowWarning("No students with 90+ average yet.");
            return;
        }

        for (int i = 0; i < topStudents.Count; i++)
        {
            var student = topStudents[i];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  {i + 1}. {student.name} - {student.average:F1}%");
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Показывает студентов выше среднего
    /// </summary>
    public static void ShowAboveAverageStudents(double classAverage, List<(string name, double average)> students)
    {
        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"📈 Students Above Class Average ({classAverage:F1}%):");
        Console.WriteLine("".PadLeft(45, '─'));
        Console.ResetColor();

        if (students.Count == 0)
        {
            ShowWarning("No students above class average.");
            return;
        }

        for (int i = 0; i < students.Count; i++)
        {
            var student = students[i];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  • {student.name} - {student.average:F1}%");
        }
        Console.ResetColor();
    }

    /// <summary>
    /// Показывает подробный отчет по классу
    /// </summary>
    public static void ShowClassReport(int totalStudents, int totalGrades, double classAverage, 
        int highestGrade, int lowestGrade, string topPerformer, double topAverage,
        List<(string name, double average, int gradeCount)> allStudents)
    {
        Console.ForegroundColor = HeaderColor;
        Console.WriteLine("📈 Class Report");
        Console.WriteLine("".PadLeft(50, '═'));
        Console.ResetColor();

        // Основная статистика
        Console.ForegroundColor = InfoColor;
        Console.WriteLine($"📊 Class Statistics:");
        Console.ResetColor();
        Console.WriteLine($"   Total students: {totalStudents}");
        Console.WriteLine($"   Total grades: {totalGrades}");
        Console.WriteLine($"   Class average: {classAverage:F2}%");
        Console.WriteLine($"   Highest grade: {highestGrade}%");
        Console.WriteLine($"   Lowest grade: {lowestGrade}%");
        Console.WriteLine();

        // Лучший студент
        if (!string.IsNullOrEmpty(topPerformer))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"🏆 Top Performer: {topPerformer} ({topAverage:F1}%)");
            Console.ResetColor();
        }

        // Индивидуальные средние
        Console.WriteLine();
        Console.ForegroundColor = InfoColor;
        Console.WriteLine("📋 Individual Averages:");
        Console.ResetColor();
        
        for (int i = 0; i < allStudents.Count; i++)
        {
            var student = allStudents[i];
            
            // Цвет зависит от среднего балла
            if (student.average >= 90)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (student.average >= 70)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
                
            Console.WriteLine($"   {student.name}: {student.average:F1}% ({student.gradeCount} grades)");
        }
        Console.ResetColor();
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
        Console.ReadKey();
        Console.Clear();
    }

    /// <summary>
    /// Очищает экран
    /// </summary>
    public static void ClearScreen()
    {
        Console.Clear();
    }
}
