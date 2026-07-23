// Bug 1: Off-by-one
int sum = 0;
for (int i = 0; i < 5; i++) {
    sum = sum + i;
}

Console.WriteLine($"Sum of 1 to 5: {sum}");

// Bug 2: Wrong comparison
int temperature = 35;
if (temperature < 30) {
    Console.WriteLine("It's hot outside!");
} else {
    Console.WriteLine("Nice weather.");
}

// Bug 3: Unreachable grade branch
Console.WriteLine(GetGrade(75));

// Bug 4: Wrong value in the expression
int width = 5;
int height = 10;
int area = width * width;
Console.WriteLine($"Area: {area}");

static string GetGrade(int score)
{
    if (score >= 90)
    {
        return "A";
    }

    if (score >= 80)
    {
        return "B";
    }

    return "F";

    if (score >= 70)
    {
        return "C";
    }
}
