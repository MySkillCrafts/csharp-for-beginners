using CSharpForBeginners.LessonsHelper;

Dictionary<string, int> studentGrades = new Dictionary<string, int>();

studentGrades.Add("Alice Johnson", 95);
studentGrades.Add("Bob Smith", 88);
studentGrades.Add("Carol Davis", 92);

int aliceGrade = studentGrades["Alice Johnson"];

Section5.Lesson5.DisplayInWebBrowser(
    studentGrades,
    aliceGrade,
    false,
    false);
