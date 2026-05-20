
using System.Diagnostics;

List<string> activityDays = new List<string>
{
    "Monday", "Monday", "Monday",
    "Tuesday", "Tuesday", "Tuesday",
    "Wednesday", "Wednesday", "Wednesday",
    "Thursday", "Thursday", "Thursday"
};

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

        for (int i = 0; i < activities.Count; i++)
        {
            string activity = activities[i];
            string day = activityDays[i];

            Console.WriteLine($"{i + 1}. {activity} ({day})");
        }

        Console.WriteLine();
        Console.WriteLine($"Total activities: {activities.Count}");
        Console.WriteLine();


    }
    else if (choice == "2")
    {
        Console.WriteLine();
        Console.WriteLine("==== Add activity ====");
        Console.WriteLine("(Type 'done' when finished)");          

        while (true)
        {
            Console.Write("Activity name: ");
            string newActivity = Console.ReadLine();
            if (newActivity.ToLower() == "done")
            {
                Console.WriteLine("Finished adding activities!");
                break;
            }

            Console.Write("Day (Monday–Friday): ");
            string day = Console.ReadLine();

            activities.Add(newActivity);
            activityDays.Add(day);

            Console.WriteLine();
            Console.WriteLine($"Added: {newActivity} on {day}");
        }
    }
}