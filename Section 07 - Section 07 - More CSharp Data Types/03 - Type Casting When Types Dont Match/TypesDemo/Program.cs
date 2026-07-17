using TypesDemo;

Season currentSeason = Season.Spring;

Console.WriteLine($"Current season: {currentSeason}");

if (currentSeason == Season.Spring)
{
    Console.WriteLine("Flowers are blooming!");
}
else if (currentSeason == Season.Summer)
{
    Console.WriteLine("Time for the beach!");
}