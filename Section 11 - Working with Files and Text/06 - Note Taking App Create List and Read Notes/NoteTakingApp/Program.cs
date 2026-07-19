using NoteTakingApp;

string folder = "Notes";

if (!Directory.Exists(folder))
{
    Directory.CreateDirectory(folder);
}


bool running = true;
while (running)
{
    Console.WriteLine();
    Console.WriteLine("--- Note-Taking App ---");
    Console.WriteLine("1. Create Note");
    Console.WriteLine("2. List Notes");
    Console.WriteLine("3. Read Note");
    Console.WriteLine("0. Exit");
    Console.Write("Your choice: ");

    if (!int.TryParse(Console.ReadLine(), out int input))
    {
        Console.WriteLine("Please enter a number.");
        continue;
    }

    MenuChoice choice = (MenuChoice)input;

    switch (choice)
    {
        case MenuChoice.Exit:
            break;
        case MenuChoice.CreateNote:
            Console.Write("Note title: ");
            string title = Console.ReadLine();

            Console.Write("Note content: ");
            string content = Console.ReadLine();

            string path = Path.Combine(folder, title + ".txt");
            File.WriteAllText(path, content);
            Console.WriteLine("Note saved!");         

            break;
        case MenuChoice.ListNotes:
            string[] files = Directory.GetFiles(folder);

            Console.WriteLine("Your notes:");
            foreach (var file in files)
            {
                Console.WriteLine($"  - {Path.GetFileNameWithoutExtension(file)}");
            }
                       
            break;
        case MenuChoice.ReadNote:
            Console.Write("Note title: ");
            string readTitle = Console.ReadLine();
            string readPath = Path.Combine(folder, readTitle + ".txt");
            Console.WriteLine(File.ReadAllText(readPath));

            break;
        case MenuChoice.SearchNotes:
            break;
        case MenuChoice.DeleteNote:
            break;
    }
}