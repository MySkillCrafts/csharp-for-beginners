File.WriteAllText("hello.txt", "Hello from C#!");
Console.WriteLine("File written!");

string content = File.ReadAllText("hello.txt");
Console.WriteLine(content);

File.WriteAllText("hello.txt", "This is new content.");
string updated = File.ReadAllText("hello.txt");
Console.WriteLine(updated);
