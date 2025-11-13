using CSharpForBeginners.LessonsHelper;

Dictionary<string, int> studentGrades = new Dictionary<string, int>
{
    {"Alice Johnson", 95},
    {"Bob Smith", 88},
    {"Carol Davis", 92},
    {"David Wilson", 87},
    {"Eva Brown", 94},
    {"Frank Miller", 78 },
    {"Grace Lee", 91}
};

// Manual approach without LINQ
Dictionary<string, int> topStudentsManual = new Dictionary<string, int>();
foreach (var student in studentGrades)
{
    if (student.Value >= 90)
    {
        topStudentsManual.Add(student.Key, student.Value);
    }
}

Section5.Lesson6.DisplayInWebBrowser(
    studentGrades,
    topStudentsManual,
    studentGrades,
    new List<string>(),
    0,
    0,
    0,
    new List<string>(),
    new List<string>());
