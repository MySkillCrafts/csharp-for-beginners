int numberOfCoffees = 0;
double pricePerCoffee = 2.5;
const double salesTax = 0.07;

Console.WriteLine("Welcome to our Coffee Calculator!");
Console.WriteLine("We serve coffee.");

Console.Write("How many cups of coffee would you like? ");
string userInput = Console.ReadLine();

bool isValidNumber = int.TryParse(userInput, out numberOfCoffees);

if (!isValidNumber)
{
    Console.WriteLine("Oops! That doesn't look like a valid number. We'll set your order to 1 coffee by default.");
    numberOfCoffees = 1;
}

// Ensure minimum order of 1 cup using Math.Max()
numberOfCoffees = Math.Max(numberOfCoffees, 1);

Console.Write("Would you like sugar in your coffee? (y/n): ");
char sugarResponse = char.Parse(Console.ReadLine());

bool hasSugar = sugarResponse == 'y';

Console.Write("Would you like cream in your coffee? (y/n): ");
char creamResponse = char.Parse(Console.ReadLine());

bool hasCream = creamResponse == 'y';

if (hasSugar && hasCream)
{
    Console.WriteLine("Coffee with sugar and cream.");
}
else if (hasSugar || hasCream)
{
    Console.WriteLine("You chose either sugar or cream. Enjoy your coffee!");
}
else
{
    Console.WriteLine("Plain coffee coming right up!");
}

double totalPrice = numberOfCoffees * pricePerCoffee;

double totalWithTax = totalPrice + (totalPrice * salesTax);
double roundedTotalWithTax = Math.Round(totalWithTax, 2);

double totalPriceRounded = Math.Round(totalPrice, 2);

Console.WriteLine($"Your total for {numberOfCoffees} cups of coffee is ${totalPriceRounded}.");
Console.WriteLine($"Including tax, your total comes to ${roundedTotalWithTax}.");