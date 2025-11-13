using CSharpForBeginners.LessonsHelper;

string[] subjects = { "Math", "Science", "English", "History" };

int subjectsArraySize = subjects.Length;

subjects[0] = "Mathematics";

Section5.Lesson1.DisplayInWebBrowser(
    subjects,
    new List<string>(),
    subjectsArraySize,
    0,
    0,
    0);