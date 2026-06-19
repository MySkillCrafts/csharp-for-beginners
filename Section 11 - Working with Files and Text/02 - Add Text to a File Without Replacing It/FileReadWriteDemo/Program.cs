File.WriteAllText("log.txt", "Line 1\n");
File.AppendAllText("log.txt", "Line 2\n");
File.AppendAllText("log.txt", "Line 3\n");

string all = File.ReadAllText("log.txt");
Console.WriteLine(all);

string[] lines = File.ReadAllLines("log.txt");
Console.WriteLine($"Total lines: {lines.Length}");

foreach (string line in lines)
{
    Console.WriteLine($"  > {line}");
}
