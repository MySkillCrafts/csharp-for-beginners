string folder = "MyNotes";

if (!Directory.Exists(folder))
{
    Directory.CreateDirectory(folder);
    Console.WriteLine("Folder created!");
}
else {
    Console.WriteLine("Folder already exists.");
}

string path = Path.Combine(folder, "first-note.txt");
File.WriteAllText(path, "My first note!");
Console.WriteLine("Note saved.");

string[] files = Directory.GetFiles(folder);
foreach (string file in files)
{
    Console.WriteLine($"  {Path.GetFileName(file)}");
}











