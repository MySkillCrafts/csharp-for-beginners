DateTime today = DateTime.Now;

DateTime nextWeek = today.AddDays(7);

Console.WriteLine($"Today: {today.ToString("d")}");
Console.WriteLine($"Next week: {nextWeek.ToString("d")}");

DateTime nextMonth = today.AddMonths(1);
Console.WriteLine($"Next month: {nextMonth.ToString("d")}");

DateTime lastYear = today.AddYears(-1);
Console.WriteLine($"Last year: {lastYear.ToString("d")}");

if (nextWeek > today)
{
    Console.WriteLine("Next week is in the future.");
}
