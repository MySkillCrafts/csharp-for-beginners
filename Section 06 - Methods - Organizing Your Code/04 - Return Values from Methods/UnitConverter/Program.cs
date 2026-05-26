
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
    else if (choice == "1")
    {
        Console.Write("Enter temperature in Fahrenheit: ");
        double fahrenheit = double.Parse(Console.ReadLine());
        double celsius = CalculateCelsius(fahrenheit);
        Console.WriteLine($"{fahrenheit} °F = {celsius} °C");

    }
    else if (choice == "2")
    {
        Console.Write("Enter distance in miles: ");
        double miles = double.Parse(Console.ReadLine());

        double km = ConvertMilesToKm(miles);
        Console.WriteLine($"{miles} miles = {km} km");
    }
    else if (choice == "3")
    {
        Console.Write("Enter weight in pounds: ");
        double pounds = double.Parse(Console.ReadLine());
        double kilograms = CalculateKilograms(pounds);
        Console.WriteLine($"{pounds} lbs = {kilograms} kg");
    }

    Console.WriteLine();
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

void ShowWelcome()
{
    Console.WriteLine("==============================");
    Console.WriteLine("      UNIT CONVERTER");
    Console.WriteLine("==============================");
    Console.WriteLine();
}

void ShowMenu()
{
    Console.WriteLine("What would you like to convert?");
    Console.WriteLine();
    Console.WriteLine("1. Fahrenheit >> Celsius");
    Console.WriteLine("2. Miles >> Kilometers");
    Console.WriteLine("3. Pounds >> Kilograms");
    Console.WriteLine("0. Exit");
    Console.WriteLine();
}

static void ShowDivider()
{
    Console.WriteLine("------------------------------");
}

double ConvertMilesToKm(double miles)
{
    double result = miles * 1.60934;
    return result;
}

double CalculateCelsius(double fahrenheit)
{
    double result = (fahrenheit - 32) * 5.0 / 9.0;  
    return result;
}

double CalculateKilograms(double pounds) {
    double result = pounds * 0.453592;
    return result;
}