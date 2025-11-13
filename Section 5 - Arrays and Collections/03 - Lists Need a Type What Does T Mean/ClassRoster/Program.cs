using CSharpForBeginners.LessonsHelper;

List<string> studentNames = new List<string>();
studentNames.Add("Alice Johnson");
studentNames.Add("Bob Smith");
studentNames.Add("Carol Davis");
studentNames.Add("David Wilson");
studentNames.Add("Eva Brown");

List<int> studentAges = new List<int>();
studentAges.Add(16);
studentAges.Add(17);
studentAges.Add(16);
studentAges.Add(18);
studentAges.Add(17);

List<double> testGrades = new List<double>();
testGrades.Add(88.5);
testGrades.Add(92.0);
testGrades.Add(85.7);
testGrades.Add(91.3);
testGrades.Add(89.8);

Section5.Lesson2.DisplayInWebBrowser(studentNames, studentAges, testGrades);