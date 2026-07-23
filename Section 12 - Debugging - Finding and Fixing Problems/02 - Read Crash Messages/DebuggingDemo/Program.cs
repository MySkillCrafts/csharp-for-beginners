string[] names = { "Alice", "Bob", "Carol" };

for (int i = 0; i <= names.Length; i++) {
    Console.WriteLine(names[i]);
}

Console.Write("Enter a number: ");
int number = int.Parse(Console.ReadLine());
Console.WriteLine(number * 2);
