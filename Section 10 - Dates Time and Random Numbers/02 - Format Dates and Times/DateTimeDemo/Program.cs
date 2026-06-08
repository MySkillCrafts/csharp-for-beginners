DateTime now = DateTime.Now;
//Console.WriteLine(now);
Console.WriteLine($"Year: {now.Year}");
Console.WriteLine($"Month: {now.Month}");
Console.WriteLine($"Day: {now.Day}");

Console.WriteLine($"Day of week: {now.DayOfWeek}");


DateTime birthday = new DateTime(1990, 6, 15);
Console.WriteLine(birthday.ToString("dd/MM/yyyy"));
Console.WriteLine(birthday.ToString("dddd, MMMM dd"));
Console.WriteLine("-----------------------------------");

Console.WriteLine(birthday.ToString("d"));
Console.WriteLine(birthday.ToString("D"));
Console.WriteLine(birthday.ToString("f"));