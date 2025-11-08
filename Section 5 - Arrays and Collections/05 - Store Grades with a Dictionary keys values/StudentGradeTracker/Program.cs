using CSharpForBeginners.LessonsHelper;

Dictionary<string, int> studentGrades = new Dictionary<string, int>();

studentGrades.Add("Alice Johnson", 95);
studentGrades.Add("Bob Smith", 88);
studentGrades.Add("Carol Davis", 92);

int aliceGrade = studentGrades["Alice Johnson"];

bool hasDavid = studentGrades.ContainsKey("David Wilson");
bool hasAlice = studentGrades.ContainsKey("Alice Johnson");

studentGrades["Bob Smith"] = 91;

studentGrades.Add("David Wilson", 87);
studentGrades.Add("Eva Brown", 94);

Section5.Lesson5.DisplayInWebBrowser(
    studentGrades,
    aliceGrade,
    hasDavid,
    hasAlice);
