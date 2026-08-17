using CSharpForBeginners.LessonsHelper;

List<string> studentNames = new List<string>
{
    "Bob Smith",
    "Alice Johnson",
    "Eva Brown",
    "Carol Davis",
    "David Wilson"
};

List<int> studentGrades = new List<int>
{
  92,88,89,85,91,88
};

List<string> sortedByNameList = new List<string>(studentNames);
sortedByNameList.Sort();

List<string> reversedList = new List<string>(sortedByNameList);
reversedList.Reverse();

string foundStudent = studentNames.Find(name => name.StartsWith("A"));

List<string> longNames = studentNames.FindAll(name => name.Length > 10);

bool hasAlice = studentNames.Contains("Alice Johnson");

Section5.Lesson4.DisplayInWebBrowser(
    studentNames,
    studentGrades,
    sortedByNameList,
    reversedList,
    foundStudent,
    longNames);
