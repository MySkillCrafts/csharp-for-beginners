using CSharpForBeginners.LessonsHelper;

string[] subjects = { "Math", "Science", "English", "History" };

List<string> studentsNames = new List<string>();

int subjectsArraySize = subjects.Length;
int studentNamesCountEmpty = studentsNames.Count;

studentsNames.Add("Alice Johnson");
studentsNames.Add("Bob Smith");
studentsNames.Add("Carol Davis");

int studentNamesCountAfterThree = studentsNames.Count;

studentsNames.Add("David Wilson");
studentsNames.Add("Eva Brown");

int strudentNamesCountFinal = studentsNames.Count;

subjects[0] = "Mathematics";

Section5.Lesson1.DisplayInWebBrowser(
    subjects,
    studentsNames,
    subjectsArraySize,
    studentNamesCountEmpty,
    studentNamesCountAfterThree,
    strudentNamesCountFinal);