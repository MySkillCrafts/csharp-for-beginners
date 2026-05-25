
ShowWelcome();

bool running = true;

while (running)
{
    ShowMenu();
    Console.Write("Your choice: ");
    string choice = Console.ReadLine();
    ShowDivider();

    if (choice == "0")
    {
        running = false;
    }

    Console.WriteLine();
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

void ShowWelcome() {
    Console.WriteLine("==============================");
    Console.WriteLine("      UNIT CONVERTER");
    Console.WriteLine("==============================");
    Console.WriteLine();
}

void ShowMenu() {
    Console.WriteLine("What would you like to convert?");
    Console.WriteLine();
    Console.WriteLine("1. Fahrenheit >> Celsius");
    Console.WriteLine("2. Miles >> Kilometers");
    Console.WriteLine("3. Pounds >> Kilograms");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
}

static void ShowDivider() {
    Console.WriteLine("------------------------------");
}