DateTime start = new DateTime(DateTime.Now.Year, 1, 1);
DateTime end = DateTime.Now;

TimeSpan difference = end - start;
Console.WriteLine($"Days since New Year: {difference.Days}");

Console.WriteLine($"Total days: {difference.TotalDays}");

TimeSpan twoWeeks = TimeSpan.FromDays(14);
DateTime future = DateTime.Now + twoWeeks;
Console.WriteLine($"Two weeks from now: {future.ToString("d")}");
