Console.Write("Enter your birth year: ");
int year = int.Parse(Console.ReadLine());

Console.Write("Enter your birth month: ");
int month = int.Parse(Console.ReadLine());

Console.Write("Enter your birth day: ");
int day = int.Parse(Console.ReadLine());

DateTime birthday = new DateTime(year, month, day);
DateTime today = DateTime.Today;

Console.WriteLine($"You were born on {birthday.ToString("MMMM dd, yyyy")}.");
Console.WriteLine($"That was a {birthday.DayOfWeek}.");

int age = today.Year - birthday.Year;

if (today.Month < birthday.Month || (today.Month == birthday.Month && today.Day < birthday.Day)) {
    age--;
}

Console.WriteLine($"You are {age} years old.");

DateTime nextBirthday = new DateTime(today.Year, birthday.Month, birthday.Day);

if (nextBirthday < today) {
    nextBirthday = nextBirthday.AddYears(1);
}

TimeSpan countdown = nextBirthday - today;
Console.WriteLine($"Days until your next birthday: {countdown.Days}");

string[] messages = {
    "Have an amazing birthday!",
    "Make this year a great one!",
    "Hope your birthday is full of good things!",
    "Time to celebrate!"
};

int index = Random.Shared.Next(0, messages.Length);
Console.WriteLine(messages[index]);
