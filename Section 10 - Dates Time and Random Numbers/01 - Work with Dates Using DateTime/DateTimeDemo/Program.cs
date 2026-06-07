DateTime now = DateTime.Now;
//Console.WriteLine(now);
Console.WriteLine($"Year: {now.Year}");
Console.WriteLine($"Month: {now.Month}");
Console.WriteLine($"Day: {now.Day}");

Console.WriteLine($"Day of week: {now.DayOfWeek}");


DateTime birthday = new DateTime(1990, 6, 15);
Console.WriteLine($"Birthday: {birthday}");
Console.WriteLine($"Born on: {birthday.DayOfWeek}");
