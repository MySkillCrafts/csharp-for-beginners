
List<string> activities = new List<string> {
     "Morning jog",
     "Healthy breakfast",
     "Plan the week",
     "Study new topic",
     "Practice coding",
     "Watch tutorial"
};

bool running = true;
while (running)
{
    Console.WriteLine("==== DAILY PLANNER ====");
    Console.WriteLine("1. View activities");
    Console.WriteLine("0. Exit");
    Console.WriteLine("-----------------------");
    Console.WriteLine();
    Console.Write("Your choice: ");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.WriteLine("==== Your activities ====");
        Console.WriteLine($"- {activities[0]}");
        Console.WriteLine($"- {activities[1]}");
        Console.WriteLine($"- {activities[2]}");
        Console.WriteLine($"- {activities[3]}");
        Console.WriteLine($"- {activities[4]}");
        Console.WriteLine($"- {activities[5]}");

        Console.WriteLine();
        Console.WriteLine($"Total activities: {activities.Count}");
        Console.WriteLine();


    }

}