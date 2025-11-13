using CSharpForBeginners.LessonsHelper;


List<string> classRoster = new List<string>();
classRoster.Add("Alice Johnson");
classRoster.Add("Bob Smith");
classRoster.Add("Carol Davis");
classRoster.Add("David Wilson");
classRoster.Add("Eva Brown");

classRoster.Add("Frank Miller");

classRoster.Remove("Frank Miller");

classRoster.Add("Grace Taylor");

classRoster.RemoveAt(1);

classRoster.Insert(1, "Henry Clark");

Section5.Lesson2.DisplayInWebBrowser(classRoster);