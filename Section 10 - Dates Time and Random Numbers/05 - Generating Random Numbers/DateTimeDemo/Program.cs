int number = Random.Shared.Next(1, 101);
Console.WriteLine($"Random number (1-100): {number}");


int dice = Random.Shared.Next(1, 7);
Console.WriteLine($"Dice roll: {dice}");

string[] greetings = { "Hello!", "Hi there!", "Hey!", "Welcome!" };

int index = Random.Shared.Next(0, greetings.Length);
Console.WriteLine(greetings[index]);