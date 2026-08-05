string[] names = { "Alice", "Bob", "Carol" };

for (int i = 0; i < names.Length; i++) {
    Console.WriteLine(names[i]);
}

Console.Write("Enter a number: ");
string? input = Console.ReadLine();

if (int.TryParse(input, out int number)) {
    Console.WriteLine(number * 2);
} else {
    Console.WriteLine("That is not a valid number.");
}
