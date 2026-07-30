int score = 5;
double exactScore = score;

Console.WriteLine($"score = {score}");
Console.WriteLine($"exactScore = {exactScore}");

double precise = 3.7;
int truncated = (int)precise;

Console.WriteLine($"precise = {precise}");
Console.WriteLine($"truncated = {truncated}");

int rounded = Convert.ToInt32(precise);
Console.WriteLine($"rounded = {rounded}");
