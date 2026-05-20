
using System.Diagnostics;

List<string> activities = new List<string> {
    "Morning jog",
    "Healthy breakfast",
    "Plan the week",
    "Study new topic",
    "Practice coding",
    "Watch tutorial",
    "Call a friend",
    "Work on hobby",
    "Cook dinner",
    "Yoga session",
    "Drink water",
    "Read a book"
};

bool running = true;
while (running)
{
    Console.WriteLine("==== DAILY PLANNER ====");
    Console.WriteLine("1. View activities");
    Console.WriteLine("2. Add new activities");
    Console.WriteLine("0. Exit");
    Console.WriteLine("-----------------------");
    Console.WriteLine();
    Console.Write("Your choice: ");
    string choice = Console.ReadLine();

    if (choice == "1")
    {
        Console.WriteLine("==== Your activities ====");

        foreach (string activity in activities)
        {
            Console.WriteLine($"- {activity}");
        }

        Console.WriteLine();
        Console.WriteLine($"Total activities: {activities.Count}");
        Console.WriteLine();


    }
    else if (choice == "2")
    {
        Console.WriteLine();
        Console.WriteLine("==== Add activity ====");
        Console.Write("Activity name: ");

        string newActivity = Console.ReadLine();
        activities.Add(newActivity);

        Console.WriteLine();
        Console.WriteLine($"Added: {newActivity}");

    }
}