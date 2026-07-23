int total = 0;
int count = 5;

for (int i = 1; i <= count; i++) {
    total = total + i;
    if (total > 10) {
        Console.WriteLine($"Crossed 10 at i = {i}");
    }
}

Console.WriteLine($"Total: {total}");
