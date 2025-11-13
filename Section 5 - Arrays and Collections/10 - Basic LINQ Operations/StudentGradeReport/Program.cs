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

var topStudents = studentGrades.Where(s => s.Value >= 90);

var sortedByGrade = studentGrades.OrderByDescending(s => s.Value);

var studentNames = studentGrades.Select(s => s.Key);

double averageGrade = studentGrades.Average(s => s.Value);

int maxGrade = studentGrades.Max(s => s.Value);

int minGrade = studentGrades.Min(s => s.Value);

Section5.Lesson6.DisplayInWebBrowser(
    studentGrades,
    topStudents,
    sortedByGrade,
    studentNames,
    averageGrade,
    maxGrade,
    minGrade,
    new List<string>(),
    new List<string>());
