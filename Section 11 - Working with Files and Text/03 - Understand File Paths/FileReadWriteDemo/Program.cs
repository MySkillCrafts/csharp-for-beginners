string folder = "MyNotes";
string fileName = "note.txt";

string fullPath = Path.Combine(folder, fileName);
Console.WriteLine(fullPath);

string path = @"C:\Users\Demo\Documents\hello.txt";

Console.WriteLine(Path.GetFileName(path));
Console.WriteLine(Path.GetExtension(path));
Console.WriteLine(Path.GetFileNameWithoutExtension(path));









