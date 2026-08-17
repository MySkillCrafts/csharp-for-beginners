int numberOfCoffees = 0;
double pricePerCoffee = 2.5;
const double salesTax = 0.07;

Console.WriteLine("Welcome to our Coffee Calculator!");
Console.WriteLine("We serve coffee.");

Console.Write("How many cups of coffee would you like? ");
string userInput = Console.ReadLine();
numberOfCoffees = int.Parse(userInput);

double totalPrice = numberOfCoffees * pricePerCoffee;
double totalWithTax = totalPrice + (totalPrice * salesTax);

Console.WriteLine("Your total for " + numberOfCoffees + " cups of coffee is $" + totalPrice + ".");
Console.WriteLine("Including tax, your total comes to $" + totalWithTax + ".");