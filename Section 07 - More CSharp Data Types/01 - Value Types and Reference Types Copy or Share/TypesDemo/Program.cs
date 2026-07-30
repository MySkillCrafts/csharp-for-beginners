int firstScore = 100;
int secondScore = firstScore;

secondScore = 50;

Console.WriteLine($"First score: {firstScore}");
Console.WriteLine($"Second score: {secondScore}");

List<string> shoppingList = new List<string>();
shoppingList.Add("Milk");
shoppingList.Add("Eggs");

List<string> sharedList = shoppingList;
sharedList.Add("Cake");

Console.WriteLine("Shopping list:");
foreach (string item in shoppingList) {
    Console.WriteLine($"  {item}");
}

Console.WriteLine("Shared list:");
foreach (string item in sharedList)
{
    Console.WriteLine($"  {item}");
}
