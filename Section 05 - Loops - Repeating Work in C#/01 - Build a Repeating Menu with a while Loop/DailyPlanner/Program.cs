
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
        Console.WriteLine("Actually, we have no activities yet. We'll fix that soon!");  
    }

}